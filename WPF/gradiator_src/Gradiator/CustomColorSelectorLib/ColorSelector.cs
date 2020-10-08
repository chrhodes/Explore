using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Xml;
using ColorPickerControlLib.ColorPicker;

namespace CustomColorSelectorLib
{
    public partial class ColorSelector : UserControl
    {
        private enum ScrollAction
        {
            scrollUp,
            scrollDown
        };

        private enum ActiveColorControl
        {
            RGB,
            HSV
        };

        public ColorSelector()
        {
            InitializeComponent();
        }

        #region Properties and fields

        private XmlDocument doc = new XmlDocument();
        private List<Color> colorList = new List<Color>();

        private bool scrollBarUpActive = false;
        private bool scrollBarDownActive = false;
        private bool isEditActive = false;

        private const int COLOR_SQUARE_SIZE = 8;
        private const int COLUMN_COUNT = 12;
        private const int ROW_CNT = 12;

        private Rectangle pickerRct = new Rectangle(84, 20, 96, ROW_CNT * 10);
        private Rectangle primaryColorRct = new Rectangle(15, 100, 30, 30);
        private Rectangle secondaryColorRct = new Rectangle(35, 120, 30, 30);
        private Rectangle scrollRctUp = Rectangle.Empty;
        private Rectangle scrollRctDown = Rectangle.Empty;
        private Rectangle switchRct = new Rectangle(50, 98, 16, 16);
        private Rectangle addRct = new Rectangle(50, 75, 16, 16);
        private Rectangle editActiveRct = new Rectangle(50, 50, 16, 16);

        private int colorCursor = 0;

        private Image switchImage = null;
        private Image addImage = null;
        private Image editNonActiveImage = null;
        private Image editActiveImage = null;

        private Color primaryColor = Color.White;
        [Category("WoodWare")]
        public Color PrimaryColor
        {
            get { return primaryColor; }
            set 
            { 
                primaryColor = value;
                SetRGBColor(primaryColor);
                SetHSVColor(primaryColor);
                Invalidate();
            }
        }

        private Color secondaryColor = Color.Black;
        [Category("WoodWare")]
        public Color SecondaryColor
        {
            get { return secondaryColor; }
            set 
            { 
                secondaryColor = value;
                Invalidate();
            }
        }

        private Color pickerBackColor = Color.LightSteelBlue;
        [Category("WoodWare")]
        public Color PickerBackColor
        {
            get { return pickerBackColor; }
            set 
            { 
                pickerBackColor = value;
                Invalidate(pickerRct);
            }
        }

        private Color controlBackColor = SystemColors.Control;
        [Category("WoodWare")]
        public Color ControlBackColor
        {
            get { return controlBackColor; }
            set 
            { 
                controlBackColor = value;
                Invalidate();
            }
        }

        private bool controlBorder;
        [Category("WoodWare")]
        public bool ControlBorder
        {
            get { return controlBorder; }
            set 
            { 
                controlBorder = value;
                Invalidate();
            }
        }

	
        #endregion

        #region Delegates and Events

        public delegate void OnColorChangeEventHandler(object sender, ColorSelectorEventArgs e);
        [Category("WoodWare")]
        public event OnColorChangeEventHandler OnColorChange;

        #endregion

        /// <summary>
        /// Do all init. here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorSelector_Load(object sender, EventArgs e)
        {
            //Get the image to display for switching pri./sec. colors
            switchImage = global::CustomColorSelectorLib.Properties.Resources.Switch;
            addImage = global::CustomColorSelectorLib.Properties.Resources.Add;
            editActiveImage = global::CustomColorSelectorLib.Properties.Resources.Edit_Active;
            editNonActiveImage = global::CustomColorSelectorLib.Properties.Resources.Edit_NonActive;

            //Define scroll bars here
            scrollRctUp = new Rectangle(pickerRct.X, pickerRct.Y - 10, pickerRct.Width, 10);
            scrollRctDown = new Rectangle(pickerRct.X, pickerRct.Y + pickerRct.Height, pickerRct.Width, 10);
        }

        #region static methods

        /// <summary>
        /// Converts a string "Color [A=n,R=n,G=n,B=n]" to Color
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static Color ConvertStringToColor(string s)
        {
            Color c = Color.White;

            Regex r = new Regex(@"\[.*\]");
            Match m = r.Match(s);

            //Get string and strip brackets
            string str = m.ToString();
            str = str.Substring(1, str.Length - 2);

            //Values are stored in 2 different formats
            //  1. Color [Black]
            //  2. Color [A=255,R=0,G=0,B=0]
            if (str.StartsWith("A="))
            {
                r = new Regex(@"\d+");
                MatchCollection mc = r.Matches(str);

                c = Color.FromArgb(int.Parse(mc[0].ToString()),
                    int.Parse(mc[1].ToString()),
                    int.Parse(mc[2].ToString()),
                    int.Parse(mc[3].ToString()));
            }
            else
                c = Color.FromName(str);

            return c;
        }

        /// <summary>
        /// Coverts a Color to a string "Color [A=n,R=n,G=n,B=n]"
        /// </summary>
        /// <param name="clr"></param>
        /// <returns></returns>
        private static string ConvertColorToString(Color clr)
        {
            string ret = "Color [";

            if (clr.IsKnownColor)
                ret += clr.ToKnownColor();
            else
                ret += "A=" + clr.A.ToString() + "," +
                      "R=" + clr.R.ToString() + "," +
                      "G=" + clr.G.ToString() + "," +
                      "B=" + clr.B.ToString();

            return ret += "]";
        }

        #endregion

        #region Paint and draw methods

        /// <summary>
        /// Paint event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorSelector_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rct = this.ClientRectangle;
            SolidBrush pbc = new SolidBrush(controlBackColor);
            Pen p = new Pen(Color.Black);

            //Fill in the backgound with color
            e.Graphics.FillRectangle(pbc, rct);
            rct.Offset(-1, -1);
            if (controlBorder)
                e.Graphics.DrawRectangle(p, rct);

            //Fill the picker area and put a border around it 
            rct = pickerRct;
            pbc.Color = pickerBackColor;
            e.Graphics.FillRectangle(pbc, rct);
            e.Graphics.DrawRectangle(p, rct);

            //Populate picker area
            DrawPicker(e.Graphics);

            //Draw the primary and secondary areas
            DrawPrimarySecondaryControls(e.Graphics);

            //Draw the scroll bars
            DrawScrollBars(e.Graphics);

            //Draw the switch color image
            e.Graphics.DrawImage(switchImage, switchRct);
            e.Graphics.DrawImage(addImage, addRct);

            if (isEditActive)
                e.Graphics.DrawImage(editActiveImage, editActiveRct);
            else
                e.Graphics.DrawImage(editNonActiveImage, editActiveRct);

            //Draw a border around the control
            rct = ClientRectangle;
            rct.Inflate(-1, -1);
            e.Graphics.DrawRectangle(p, this.ClientRectangle);

            pbc.Dispose();
            p.Dispose();
        }

        /// <summary>
        /// Draws the Primary and Secondary picker controls
        /// </summary>
        /// <param name="g"></param>
        private void DrawPrimarySecondaryControls(Graphics g)
        {
            SolidBrush br = new SolidBrush(Color.AntiqueWhite);
            Pen p = new Pen(Color.Black);
          
            //Draw a rect within a rect then fill the inner rect with the secondary color
            Rectangle r = secondaryColorRct;
            g.DrawRectangle(p, r);
            r.Inflate(-3, -3);
            br.Color = secondaryColor;
            g.FillRectangle(br, r);
            g.DrawRectangle(p, r);

            //Draw a rect within a rect then fill the inner rect with the primary color
            p.Width = 2f;
            r = primaryColorRct;
            g.DrawRectangle(p, r);
            r.Inflate(-3, -3);
            br.Color = primaryColor;
            g.FillRectangle(br, r);
            g.DrawRectangle(p, r);

            p.Dispose();
            br.Dispose();
        }

        /// <summary>
        /// Draws the picker items
        /// </summary>
        /// <param name="g"></param>
        private void DrawPicker(Graphics g)
        {
            SolidBrush br = new SolidBrush(Color.AntiqueWhite);
            Pen p = new Pen(Color.Black);
            Rectangle rct = pickerRct;

            int index = colorCursor * COLUMN_COUNT;
            int col = 0;

            int cnt = 0;
            int maxIndex = (pickerRct.Width * pickerRct.Height) / (COLOR_SQUARE_SIZE * COLOR_SQUARE_SIZE);
            for (int row = 0; (index < colorList.Count) && (cnt < maxIndex); index++, cnt++)
            {
                rct = new Rectangle(
                    pickerRct.X + col * COLOR_SQUARE_SIZE,
                    pickerRct.Y + row * COLOR_SQUARE_SIZE,
                    COLOR_SQUARE_SIZE,
                    COLOR_SQUARE_SIZE);

                br.Color = colorList[index];
                g.FillRectangle(br, rct);
                g.DrawRectangle(p, rct);
                if (++col >= COLUMN_COUNT)
                {
                    col = 0;
                    ++row;
                }
            }
            p.Dispose();
            br.Dispose();
        }

        /// <summary>
        /// Draws the scroll bars
        /// </summary>
        /// <param name="g"></param>
        private void DrawScrollBars(Graphics g)
        {
            Rectangle dotRctU = new Rectangle(scrollRctUp.X + (scrollRctUp.Width) / 2 - 4, scrollRctUp.Y + 2, 8, 6);
            Rectangle dotRctD = new Rectangle(scrollRctDown.X + (scrollRctDown.Width) / 2 - 4, scrollRctDown.Y + 2, 8, 6);
            LinearGradientBrush lgbU = new LinearGradientBrush(scrollRctUp, Color.White, Color.LightSlateGray, 65f, true);
            LinearGradientBrush lgbD = new LinearGradientBrush(scrollRctDown, Color.White, Color.LightSlateGray, 65f, true);
            SolidBrush br = new SolidBrush(Color.LightGreen);
            
            Pen p = new Pen(Color.Black);

            //Draw the scroll bars
            g.FillRectangle(lgbU, scrollRctUp);
            g.FillRectangle(lgbD, scrollRctDown);
            g.DrawRectangle(p, scrollRctUp);
            g.DrawRectangle(p, scrollRctDown);

            //Draw the scroll dots
            g.DrawEllipse(p, dotRctU);
            g.DrawEllipse(p, dotRctD);

            if (!scrollBarUpActive)
                br.Color = Color.Gray;
            g.FillEllipse(br, dotRctU);

            if (scrollBarDownActive)
                br.Color = Color.LightGreen;
            else
                br.Color = Color.Gray;
            g.FillEllipse(br, dotRctD);

            p.Dispose();
            br.Dispose();
            lgbU.Dispose();
            lgbD.Dispose();
        }

        #endregion

        #region Color Palette file methods

        /// <summary>
        /// Load the color palette with the file name passed
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool LoadColorPalette(string fileName)
        {
            bool ret = false;
            DialogResult dr = DialogResult.OK;

            if (fileName == string.Empty)
            {
                openFileDialog1.DefaultExt = "xml";
                openFileDialog1.Filter = "Color Palette(*.XML)|*.XML";
                dr = openFileDialog1.ShowDialog();
                fileName = openFileDialog1.FileName;
                colorCursor = 0;
            }

            if (dr == DialogResult.OK)
            {
                try
                {
                    doc.Load(fileName);
                    colorList.Clear();
                    PopulateColorList("ColorPalette");
                    SetScrollBarStatus();
                    ret = true;
                }
                catch
                {
                    MessageBox.Show("Problem loading color palette");
                    ret = false;
                }
            }
            return ret;
        }

        /// <summary>
        /// Populates the color list with the elements loaded
        /// </summary>
        /// <param name="palName"></param>
        private void PopulateColorList(string palName)
        {
            XmlNode node = doc.SelectSingleNode(palName);
            if (node == null)
            {
                MessageBox.Show("Format not correct for a color palette");
                return;
            }

            Color clr;
            foreach (XmlNode n in node)
            {
                clr = ConvertStringToColor(n.InnerText);
                colorList.Add(clr);
            }
        }

        /// <summary>
        /// Saves the color palette to the file user selects
        /// </summary>
        private void SavePalette()
        {
            XmlDataDocument dom = new XmlDataDocument();
            XmlElement elem = dom.CreateElement("ColorPalette");
            dom.AppendChild(elem);

            XmlNode root = dom.SelectSingleNode("ColorPalette");

            foreach (Color c in colorList)
            {
                elem = dom.CreateElement("Color");
                elem.InnerText = ConvertColorToString(c);
                root.AppendChild(elem);
            }
            saveFileDialog1.DefaultExt = "xml";
            saveFileDialog1.FileName = "color palette.xml";
            saveFileDialog1.Filter = "Xml files (*.xml)|*.xml";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                dom.Save(saveFileDialog1.FileName);
            else
                MessageBox.Show("Save cancelled");
        }

        #endregion

        #region Mouse handler(s)

        /// <summary>
        /// Mouse down handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorSelector_MouseDown(object sender, MouseEventArgs e)
        {
            int row = 0;
            int col = 0;
            int index = 0;

            //If the user clicked in the pickerRct then we need to determine if
            //  they clicked on a valid color square or in a blank area.  Also need
            //  to base calculation on scrolling!
            if (pickerRct.Contains(e.Location))
            {
                //First find the row/col of the square within the picker
                row = (e.Y - pickerRct.Y) / COLOR_SQUARE_SIZE;
                col = (e.X - pickerRct.X) / COLOR_SQUARE_SIZE;
                
                //Next convert the row/col to an index
                index = (row * COLUMN_COUNT) + col;

                //If the area is scrolled this will be a non-zero value so we need to add
                //  the nonvisible squares to the total.
                index += colorCursor * COLUMN_COUNT;

                //After all that if we are still in range then we have a valid hit
                if (index >= 0 && index < colorList.Count)
                {
                    //If it the left mouse button - primary else secondary
                    if (e.Button == MouseButtons.Left)
                    {
                        //We've found a valid target.  If the editActive flag set then its a remove
                        //  else it an add
                        if (isEditActive)
                            colorList.Remove(colorList[index]);
                        else
                        {
                            primaryColor = colorList[index];
                            SetRGBColor(primaryColor);
                            SetHSVColor(primaryColor);
                        }
                    }
                    else
                        secondaryColor = colorList[index];

                    //Notify the user that a new color has been selected
                    if (OnColorChange != null)
                        OnColorChange(this, new ColorSelectorEventArgs(primaryColor, secondaryColor));
                }
            }
            else
            {
                //Swap Primary/Seconday colors
                Color tc = primaryColor;
                if (switchRct.Contains(e.Location))
                {
                    primaryColor = secondaryColor;
                    secondaryColor = tc;
                    SetRGBColor(primaryColor);
                    SetHSVColor(primaryColor);
                }

                //Add the primary color to the color palette
                if (addRct.Contains(e.Location))
                {
                    colorList.Add(primaryColor);
                    SetScrollBarStatus();
                    Invalidate(pickerRct);
                }

                //Scroll Up
                if (scrollRctUp.Contains(e.Location))
                    ScrollControl(ScrollAction.scrollUp);

                //Scroll Down
                if (scrollRctDown.Contains(e.Location))
                    ScrollControl(ScrollAction.scrollDown);

                //"X" marks the spot
                if (editActiveRct.Contains(e.Location))
                    ToggleEditState();

                //Notify the user that a new color has been selected
                if (primaryColorRct.Contains(e.Location) || secondaryColorRct.Contains(e.Location))
                    if (OnColorChange != null)
                        OnColorChange(this, new ColorSelectorEventArgs(primaryColor, secondaryColor));

            }
            Invalidate();
        }

        #endregion

        /// <summary>
        /// Toggles the Delete edit state.  In MouseDown I look at this value and if true I 
        ///     remove items as opposed to adding them to the list.
        /// </summary>
        private void ToggleEditState()
        {
            if (isEditActive)
                isEditActive = false;
            else
                isEditActive = true;
        }

        /// <summary>
        /// Displays RGB or HSV sliders depending on which is active
        /// </summary>
        /// <param name="acc"></param>
        private void SetControlsVisiblity(ActiveColorControl acc)
        {
            if (acc == ActiveColorControl.RGB)
            {
                scRed.Visible = true;
                scGreen.Visible = true;
                scBlue.Visible = true;

                scHue.Visible = false;
                scSaturation.Visible = false;
                scBrightness.Visible = false;
            }
            else
            {
                scRed.Visible = false;
                scGreen.Visible = false;
                scBlue.Visible = false;

                scHue.Visible = true;
                scSaturation.Visible = true;
                scBrightness.Visible = true;
            }
        }

        /// <summary>
        /// Scrolls the control up or down 
        /// </summary>
        /// <param name="sa"></param>
        private void ScrollControl(ScrollAction sa)
        {
            int maxRowCnt = (colorList.Count + (COLUMN_COUNT - 1)) / COLUMN_COUNT;
            switch (sa)
            {
                case ScrollAction.scrollDown:
                    if (scrollBarDownActive)
                        if (++colorCursor > maxRowCnt)
                        colorCursor = maxRowCnt;
                    break;
                case ScrollAction.scrollUp:
                    if (scrollBarUpActive)
                        if (--colorCursor < 0)
                        colorCursor = 0;
                    break;
            }
            SetScrollBarStatus();
        }

        /// <summary>
        /// Sets the scroll bar status depending on whether it can scroll in that direction.
        ///     When Active the Ellipse in the Scroll bar area is green and DimGray when
        ///     inactive.
        /// </summary>
        private void SetScrollBarStatus()
        {
            int maxRowCnt = (colorList.Count + (COLUMN_COUNT - 1)) / COLUMN_COUNT;
            if (colorCursor > 0)
                scrollBarUpActive = true;
            else
                scrollBarUpActive = false;

            int maxRows = pickerRct.Height / COLOR_SQUARE_SIZE;
            if (colorCursor + maxRows < maxRowCnt)
                scrollBarDownActive = true;
            else
                scrollBarDownActive = false;
        }

        /// <summary>
        /// Sets the RGB slider values
        /// </summary>
        /// <param name="clr"></param>
        private void SetRGBColor(Color clr)
        {
            scAlpha.CurrentValue = clr.A;
            scRed.CurrentValue = clr.R;
            scGreen.CurrentValue = clr.G;
            scBlue.CurrentValue = clr.B;
            
        }

        /// <summary>
        /// Sets the HSV slider values
        /// </summary>
        /// <param name="clr"></param>
        private void SetHSVColor(Color clr)
        {
            ColorHandler.RGB RGB = new ColorHandler.RGB(
             (int)clr.A,
             (int)clr.R,
             (int)clr.G,
             (int)clr.B);

            ColorHandler.HSV HSV = ColorHandler.RGBtoHSV(RGB);
            scAlpha.CurrentValue = HSV.Alpha;
            scHue.CurrentValue = (float)HSV.Hue;
            scSaturation.CurrentValue = (float)HSV.Saturation;
            scBrightness.CurrentValue = (float)HSV.value;
        }

        /// <summary>
        /// ToolStrip Button click handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void genericButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Name)
            {
                case "btnLoadPalette":
                    LoadColorPalette(string.Empty);
                    Invalidate();
                    break;
                case "btnSavePalette":
                    SavePalette();
                    break;
                case "btnNewPalette":
                    colorList.Clear();
                    Invalidate(pickerRct);
                    break;
            }
        }

        /// <summary>
        /// Alpha slider value changed handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AlphaSlider_OnValueChange(object sender, CustomSliderControlLib.SliderEventArgs e)
        {
            Color clr = Color.FromArgb(
                (int)scAlpha.CurrentValue,
                (int)scRed.CurrentValue,
                (int)scGreen.CurrentValue,
                (int)scBlue.CurrentValue);
            SetRGBColor(clr);
            SetHSVColor(clr);
            primaryColor = clr;
            Invalidate();
        }

        /// <summary>
        /// RGB slider value chage handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void genericRGBSlider_OnValueChange(object sender, CustomSliderControlLib.SliderEventArgs e)
        {
            Color clr = Color.FromArgb(
                (int)scAlpha.CurrentValue,
                (int)scRed.CurrentValue,
                (int)scGreen.CurrentValue,
                (int)scBlue.CurrentValue);
            SetHSVColor(clr);
            primaryColor = clr;
            Invalidate();
        }

        /// <summary>
        /// HSV slider value change handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void genericHSVSlider_OnValueChage(object sender, CustomSliderControlLib.SliderEventArgs e)
        {
            ColorHandler.HSV HSV = new ColorHandler.HSV(
                (int)scAlpha.CurrentValue,
                (int)scHue.CurrentValue,
                (int)scSaturation.CurrentValue,
                (int)scBrightness.CurrentValue);
            
            primaryColor = ColorHandler.HSVtoColor(HSV);
            SetRGBColor(primaryColor);
            Invalidate();
        }

        /// <summary>
        /// RGB/HSV radio button click handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_CheckChange(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                SetControlsVisiblity(ActiveColorControl.RGB);
            else
                SetControlsVisiblity(ActiveColorControl.HSV);
        }
    }

    /// <summary>
    /// Event args for the ColorSelectorControl 
    /// </summary>
    public class ColorSelectorEventArgs : EventArgs
    {
        public Color priColor;
        public Color secColor;

        public ColorSelectorEventArgs(Color primary, Color secondary)
        {
            priColor = primary;
            secColor = secondary;
        }
    }
}