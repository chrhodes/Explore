using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ShapesClassLib;

namespace SketcherControlLib.MiscControls
{
    public partial class BrushManager : UserControl
    {
        public BrushManager()
        {
            InitializeComponent();
        }

        #region Properties and such

        private enum ScrollAction
        {
            scrollUp,
            scrollDown
        };

        private List<ShapeBrush> brushList = new List<ShapeBrush>();

        private const int BRUSH_IMAGE_SIZE = 30;
        private const int COLUMN_COUNT = 5;
        private const int ROW_COUNT = 10;

        private ShapeBrush currentBrush = new ShapeBrush();
        public ShapeBrush CurrentBrush
        {
            get { return currentBrush; }
            set { currentBrush = value; }
        }

        public delegate void OnBrushSelectedEventHandler(object sender, BrushManagerEventArgs e);
        [Category("WoodWare")]
        public event OnBrushSelectedEventHandler OnBrushSelected;

        //Rectangles for the scroll bars
        private Rectangle scrollRctUp = Rectangle.Empty;
        private Rectangle scrollRctDown = Rectangle.Empty;

        //Flag to indicate if the scroll bar is active/scrollable
        private bool scrollBarUpActive = false;
        private bool scrollBarDownActive = false;

        //An offset to determine the starting row
        private int brushCursor = 0;
        
        //ControlRct is the drawable background
        Rectangle controlRct = Rectangle.Empty;
        //InnerRct is the area where brush thumbnails are being viewed
        Rectangle innerRct = Rectangle.Empty;

        #endregion

        private void BrushManager_Load(object sender, EventArgs e)
        {
            //Got this from MSDN - a way to tell if in design mode since DesignMode is flackey!
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
                return;
            
            //The rectangle that is the background area
            controlRct = new Rectangle(1, toolStrip1.Height + 26, this.Width - 2, this.Height - 70);

            //The rectangle that is the area to draw brushes in
            innerRct = controlRct;
            innerRct.Inflate(-6, -6);
            
            //Define scroll bars here
            scrollRctUp = new Rectangle(1, this.Location.Y + 32, this.Width - 2, 10);
            scrollRctDown = new Rectangle(1, this.Location.Y + this.Height - 21, this.Width - 2, 10);

            //Load the default brush palette
            string fn = Application.StartupPath + @"\default brush palette.xml";
            LoadBrushPalette(fn, true);
        }

        #region List manipulation methods

        /// <summary>
        /// Adds a brush to the list, also;
        ///     Set scroll bar status - see if added item needs to be scrolled to?
        ///     Repaint (NOTE Could determine if the brush added is out of visisble area?)
        /// </summary>
        /// <param name="sb"></param>
        public void AddBrush(ShapeBrush sb)
        {
            brushList.Add(sb);
            SetScrollBarStatus();
            Invalidate(controlRct);
        }

        #endregion

        #region Draw methods

        /// <summary>
        /// Paint method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrushManager_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            //Draw the backgound
            DrawControl(e.Graphics);

            //Draw the Scroll bars
            DrawScrollBars(e.Graphics);

            //Assign to a volatile variable
            Rectangle rct = innerRct;
            //Calculate the start index 
            int index = brushCursor * COLUMN_COUNT;
            //Calculate the number of items that can be displayed within inner area
            int maxCount = ROW_COUNT * COLUMN_COUNT;

            ShapeBrush sb = null;
            int col = 0;
            int cnt = 0;
            //Draw the brush items
            for (int row = 0; (index < brushList.Count) && (cnt < maxCount); index++, cnt++)
            {
                rct = new Rectangle(
                    innerRct.X + col * (BRUSH_IMAGE_SIZE + 8),
                    innerRct.Y + row * (BRUSH_IMAGE_SIZE + 8),
                    BRUSH_IMAGE_SIZE,
                    BRUSH_IMAGE_SIZE);
                sb = brushList[index];
                sb.ReferenceRectangle = rct;
                sb.DrawThumbnail(e.Graphics);
                if (++col >= COLUMN_COUNT)
                {
                    col = 0;
                    ++row;
                }
            }
        }

        /// <summary>
        /// Draw the background rounded rectangle
        /// </summary>
        /// <param name="g"></param>
        private void DrawControl(Graphics g)
        {
            GraphicsPath gp = ShapeBase.CreateRoundedRectangle(controlRct, 10);
            SolidBrush sb = new SolidBrush(Color.White);
            Pen p = new Pen(Color.Black);

            g.FillPath(sb, gp);
            g.DrawPath(p, gp);

            gp.Dispose();
            sb.Dispose();
            p.Dispose();
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

        #region Toolstrip methods

        /// <summary>
        /// Method to handle toolstrip button clicks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void genericToolStrip_ButtonClick(object sender, EventArgs e)
        {
            ToolStripButton tsb = (ToolStripButton)sender;

            switch (tsb.Name)
            {
                case "tsbNew":
                    brushList.Clear();
                    break;
                case "tsbLoad":
                    LoadBrushPalette(string.Empty, true);
                    break;
                case "tsbSave":
                    SaveBrushPalette();
                    break;
                case "tsbImport":
                    LoadBrushPalette(string.Empty, false);
                    break;
            }
            Invalidate(controlRct);
        }

        #endregion

        #region File methods

        /// <summary>
        /// Load or Import a Brush palette
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="clear"></param>
        /// <returns></returns>
        public bool LoadBrushPalette(string fileName, bool clear)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            XmlDocument doc = new XmlDocument();
            XmlNode root = null;
            bool ret = false;
            DialogResult dr = DialogResult.OK;
            ShapeBase sb = null;

            if (fileName == string.Empty)
            {
                openFileDialog1.DefaultExt = "xml";
                openFileDialog1.Filter = "Brush Palette(*.XML)|*.XML";
                dr = openFileDialog1.ShowDialog();
                fileName = openFileDialog1.FileName;
            }

            if (dr == DialogResult.OK)
            {
                try
                {
                    if (clear)
                    {
                        brushList.Clear();
                        brushCursor = 0;
                    }

                    doc.Load(fileName);
                    root = doc.SelectSingleNode("BrushObjects");

                    foreach (XmlNode node in root)
                    {
                        sb = new ShapeBase();
                        currentBrush.ConvertXmlToGDI(ref sb, node);
                        brushList.Add(sb.CurrentBrush);
                    }
                    //Update scroll bar status
                    SetScrollBarStatus();
                    ret = true;
                }
                catch
                {
                    MessageBox.Show("Problem loading brush palette");
                    ret = false;
                }
            }
            return ret;
        }

        public void SaveBrushPalette()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            XmlDataDocument dom = new XmlDataDocument();
            XmlElement root = dom.CreateElement("BrushObjects");
            dom.AppendChild(root);

            foreach (ShapeBrush sb in brushList)
            {
                XmlElement elem = dom.CreateElement("Brush");
                root.AppendChild(elem);

                sb.ObjectToXml(ref dom, elem);
            }

            saveFileDialog1.DefaultExt = "xml";
            saveFileDialog1.FileName = "default palette.xml";
            saveFileDialog1.Filter = "Xml files (*.xml)|*.xml";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                dom.Save(saveFileDialog1.FileName);
        }

        #endregion

        #region Mouse methods

        /// <summary>
        /// Mouse down handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrushManager_MouseDown(object sender, MouseEventArgs e)
        {
            ShapeBrush tmpBr = null;
            bool needsRepainted = false;

            //Only accept left mouse button
            if (e.Button == MouseButtons.Left)
            {
                ShapeBrush br = null;

                //Find start row for lookup
                int index = brushCursor * COLUMN_COUNT;
                for (int i = index; i < index + (ROW_COUNT * COLUMN_COUNT) && i < brushList.Count; i++)
                {
                    br = brushList[i];
                    br.ReferenceRectangle.Offset(0, i * BRUSH_IMAGE_SIZE);
                    if (br.ReferenceRectangle.Contains(e.Location))
                    {
                        tmpBr = (ShapeBrush)br.Clone();
                        break;
                    }
                }

                //Scroll Up
                if (tmpBr == null && scrollRctUp.Contains(e.Location))
                    needsRepainted |= ScrollControl(ScrollAction.scrollUp);

                //Scroll Down
                if (tmpBr == null && scrollRctDown.Contains(e.Location))
                    needsRepainted |= ScrollControl(ScrollAction.scrollDown);

                //Notify the subscriber of the change
                if (tmpBr != null && OnBrushSelected != null)
                    OnBrushSelected(this, new BrushManagerEventArgs(tmpBr));

                //Repaint as needed
                if (needsRepainted)
                    Invalidate(controlRct);
            }
        }

        #endregion

        #region Scroll methods

        /// <summary>
        /// Scrolls the control up or down 
        /// </summary>
        /// <param name="sa"></param>
        /// <returns>indicates if a scroll was done</returns>
        private bool ScrollControl(ScrollAction sa)
        {
            bool ret = false;

            int maxRowCnt = ROW_COUNT * COLUMN_COUNT;
            switch (sa)
            {
                case ScrollAction.scrollDown:
                    if (scrollBarDownActive)
                    {
                        if (++brushCursor > maxRowCnt)
                            brushCursor = maxRowCnt;
                        ret = true;
                    }
                    break;
                case ScrollAction.scrollUp:
                    if (scrollBarUpActive)
                    {
                        ret = true;
                        if (--brushCursor < 0)
                            brushCursor = 0;
                    }
                    break;
            }
            if (ret)
                SetScrollBarStatus();

            return ret;
        }

        /// <summary>
        /// Sets the scroll bar status depending on whether it can scroll in that direction.
        ///     When Active the Ellipse in the Scroll bar area is green and DimGray when
        ///     inactive.
        /// </summary>
        private void SetScrollBarStatus()
        {
            int maxRowCnt = (brushList.Count + (COLUMN_COUNT - 1)) / COLUMN_COUNT;
            if (brushCursor > 0)
                scrollBarUpActive = true;
            else
                scrollBarUpActive = false;

            int maxRows = controlRct.Height / BRUSH_IMAGE_SIZE;
            if (brushCursor + maxRows < maxRowCnt)
                scrollBarDownActive = true;
            else
                scrollBarDownActive = false;
        }

        #endregion
    }

    public class BrushManagerEventArgs : EventArgs
    {
        public ShapeBrush brush;

        public BrushManagerEventArgs(ShapeBrush theBrush)
        {
            brush = theBrush;
        }
    }
}
