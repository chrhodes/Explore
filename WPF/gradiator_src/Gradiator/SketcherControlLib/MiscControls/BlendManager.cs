using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ShapesClassLib;

//===================================================================================================
//
//	Module Name: BlendManager
//
//	Author: Mike Hankey
//
//	Create Date: 10/26/07
//
//	Copyright: WoodWare 2002-2007
//
//	Version History: 1.0
//
//	Notes: I converted the BlendEditor dialog to a userControl so I could put it in a TabControl
//      on the main form.  There where getting to be to many dialogs to keep track of and it was
//      restricting the space that I had to work with.
//
//====================================================================================================

namespace SketcherControlLib.MiscControls
{
    #region public enums and structs

    public enum BlendStyle
    {
        enabled,
        disabled,
        regular,
        color
    }

    public enum BlendAction
    {
        add,
        update,
        remove,
        reset,
        moveUp,
        moveDown
    }

    public struct BlendGroupStruct
    {
        public float factor;
        public float position;
        public Color color;

        public BlendGroupStruct(float f, float p, Color c)
        {
            factor = f;
            position = p;
            color = c;
        }
    }

    #endregion

    public partial class BlendManager : UserControl
    {
        public BlendManager()
        {
            InitializeComponent();
        }

        #region Properties and such

        private List<BlendGroupStruct> blendGroupList = new List<BlendGroupStruct>();

        //Used when setting checkboxes and radio buttons to let the handler know we are manually
        //  setting these values and to ignore event.
        private bool ignore = false;

        private ShapeBase currentShape = null;
        public ShapeBase CurrentShape
        {
            get { return currentShape; }
            set
            {
                currentShape = value;
                SetCurrentShapeProperties(value);
            }
        }

        //A reference to the color picker.  I only use one picker throughout!
        //  Left click to get color, right click to set color.
        private ColorSelectorDialog colorDialog;
        public ColorSelectorDialog ColorDialogReference
        {
            get { return colorDialog; }
            set { colorDialog = value; }
        }

        #endregion

        #region delegates and events

        public delegate void OnBlendActionEventHandler(object sender, BlendActionEventArgs e);
        [Category("WoodWare")]
        [Description("Fired when an items are added, updated, removed or moved")]
        public event OnBlendActionEventHandler OnBlendAction;

        public delegate void OnBlendEditorStyleChangeEventHandler(object sender, BlendEditorStyleChangeEventArgs e);
        [Category("WoodWare")]
        [Description("Fired when the Blend style has changed")]
        public event OnBlendEditorStyleChangeEventHandler OnBlendEditorStyleChange;

        #endregion

        private void BlendManager_Load(object sender, EventArgs e)
        {
            Clear(true);
        }

        #region Methods to set and clear control values

        /// <summary>
        /// Set the properties for the currentShape
        /// </summary>
        /// <param name="cs"></param>
        private void SetCurrentShapeProperties(ShapeBase cs)
        {
            //If theres no item to show just clear
            if (cs == null)
            {
                Clear(true);
                return;
            }

            //Let the handlers know we are manully setting the check status and to ignore the event
            ignore = true;

            //Set the Blend checkbox and style
            if (!cs.CurrentBrush.UseBlend && !cs.CurrentBrush.UseColorBlend)
            {
                checkBox1.Checked = false;
                rbBlend.Checked = false;
                rbColorBlend.Checked = false;
            }
            else
            {
                checkBox1.Checked = true;
                if (cs.CurrentBrush.UseBlend)
                {
                    rbBlend.Checked = true;
                    rbColorBlend.Checked = false;
                }
                else
                {
                    rbBlend.Checked = false;
                    rbColorBlend.Checked = true;
                }
            }

            //Resume normal processing
            ignore = false;

            BlendGroupStruct bgs;
            Clear(false);

            //Because color, factors and positions must all have the same number of elements
            //  I can key on just factors to load all items!
            if (currentShape.CurrentBrush.UseBlend || currentShape.CurrentBrush.UseColorBlend)
            {
                //Get the smallest count from the three so if theres a mis-match we won't crash
                //  but instead use the least count!
                int cnt = cs.CurrentBrush.SurroundingColors.Count;
                if (cs.CurrentBrush.Positions.Count < cnt)
                    cnt = cs.CurrentBrush.Positions.Count;
                else
                    if (cs.CurrentBrush.Factors.Count < cnt)
                        cnt = cs.CurrentBrush.Factors.Count;

                //Load up the listBox and local List<BlendGroupStruct>
                for (int i = 0; i < cnt; i++)
                {
                    bgs = new BlendGroupStruct((float)cs.CurrentBrush.Factors[i],
                        (float)cs.CurrentBrush.Positions[i],
                        (Color)cs.CurrentBrush.SurroundingColors[i]);
                    AddBlendGroup(bgs);
                }
            }
        }

        /// <summary>
        /// Clear controls and lists as needed
        /// </summary>
        /// <param name="clearAll"></param>
        public void Clear(bool clearAll)
        {
            if (currentShape == null || clearAll)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                toolStrip1.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                toolStrip1.Enabled = true;
            }
            blendGroupList.Clear();
            listBox1.Items.Clear();
        }

        #endregion

        #region Blend group manipulation methods

        /// <summary>
        /// Add an item to the BlendGroup list and to the listBox.
        /// </summary>
        /// <param name="bgs">BlendGroupStruct item to add</param>
        /// <param name="insert">Either insert at end - 1 or  at end</param>
        public void AddBlendGroup(BlendGroupStruct bgs)
        {
            blendGroupList.Add(bgs);
            listBox1.Items.Add("");
        }

        /// <summary>
        /// Adds an item after the index passed
        /// <remarks>
        /// Notifies the parent form that item was added
        /// </remarks>
        /// </summary>
        /// <param name="bgs">BlendGroupStruct item to add</param>
        /// <param name="insert">Insert after index passed</param>
        public void InsertBlendGroup(BlendGroupStruct bgs, int index)
        {
            if (OnBlendAction != null)
                OnBlendAction(this, new BlendActionEventArgs(index, BlendAction.add, bgs));
        }

        /// <summary>
        /// Tells the subscriber that the BlendGroup at index n are to be updated with new struct data passed.
        /// </summary>
        /// <param name="bgs"></param>
        private void UpdateBlendGroup(BlendGroupStruct bgs)
        {
            if (OnBlendAction != null)
                OnBlendAction(this, new BlendActionEventArgs(listBox1.SelectedIndex, BlendAction.update, bgs));
        }

        /// <summary>
        /// Reset the object back to its default state
        /// </summary>
        private void Reset()
        {
            BlendGroupStruct bgs = new BlendGroupStruct();
            if (OnBlendAction != null)
                OnBlendAction(this, new BlendActionEventArgs(0, BlendAction.reset, bgs));
        }

        /// <summary>
        /// Remove current item
        /// <remarks>
        /// Notifies the parent form that item was added
        /// </remarks>
        /// </summary>
        private void RemoveBlendGroup()
        {
            BlendGroupStruct bgs = new BlendGroupStruct();
            if (OnBlendAction != null)
                OnBlendAction(this, new BlendActionEventArgs(listBox1.SelectedIndex, BlendAction.remove, bgs));
        }

        #endregion

        #region ListBox stuff

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (currentShape == null || e.Index < 0 || e.Index >= currentShape.CurrentBrush.SurroundingColors.Count)
                return;

            Pen p = new Pen(Color.Black);
            Font fnt = new Font("Verdano", 8f);

            Color c = currentShape.CurrentBrush.SurroundingColors[e.Index];
            string ctext = "(" + c.A.ToString() + "," + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + ")";
            float pos = currentShape.CurrentBrush.Positions[e.Index] * 100f;
            float fact = currentShape.CurrentBrush.Factors[e.Index] * 100f;
            int p1 = (int)pos;
            int f1 = (int)fact;

            SolidBrush br = new SolidBrush(c);
            SolidBrush b = new SolidBrush(Color.Black);

            Rectangle imageRct = new Rectangle(e.Bounds.X + 4, e.Bounds.Y + 4, 12, 12);
            Rectangle imageText = new Rectangle(imageRct.X + imageRct.Width + 4, imageRct.Y, 100, 12);
            Rectangle text1Rct = new Rectangle(imageText.X + imageText.Width + 4, imageRct.Y, 28, 12);
            Rectangle text2Rct = new Rectangle(text1Rct.X + text1Rct.Width + 4, imageRct.Y, 28, 12);

            e.Graphics.FillRectangle(br, imageRct);
            e.Graphics.DrawRectangle(new Pen(Color.Black), imageRct);

            e.Graphics.DrawString(ctext, fnt, b, imageText);
            e.Graphics.DrawString(p1.ToString(), fnt, b, text1Rct);
            e.Graphics.DrawString(f1.ToString(), fnt, b, text2Rct);

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(80, Color.RosyBrown)), e.Bounds);

            fnt.Dispose();
            p.Dispose();
            br.Dispose();
            b.Dispose();
        }

        /// <summary>
        /// Catch when selected index has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            BlendGroupStruct bgs;
            if (index >= 0 && index < blendGroupList.Count)
            {
                bgs = (BlendGroupStruct)blendGroupList[index];
                lblColor.BackColor = bgs.color;
                scPosition.CurrentValue = bgs.position * 100f;
                scFactor.CurrentValue = bgs.factor * 100f;
                listBox1.Invalidate();
            }
        }

        #endregion

        #region misc. handlers

        /// <summary>
        /// Generic handler to handle Add, Remove, Update and Clear of lists
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void genericButton_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index < 0)
                return;

            ToolStripButton btn = (ToolStripButton)sender;
            float posVal = (float)scPosition.CurrentValue / 100.0f;
            float factVal = (float)scFactor.CurrentValue / 100.0f;
            BlendGroupStruct bgs = new BlendGroupStruct();

            switch (btn.Name)
            {
                case "tsbAdd":
                    bgs.color = lblColor.BackColor;
                    bgs.position = posVal;
                    bgs.factor = factVal;
                    //Add only invalid if the last item is selected
                    if (index < (listBox1.Items.Count - 1))
                        InsertBlendGroup(bgs, listBox1.SelectedIndex + 1);
                    else
                        MessageBox.Show("Cannot insert blnd");
                    break;
                case "tsbUpdate":
                    if (index >= 0)
                    {
                        //Get current blend and update it with user input
                        bgs = blendGroupList[index];
                        bgs.color = lblColor.BackColor;
                        bgs.factor = factVal;

                        //Only position is critical here.
                        //NOTE When using blends there are three things that are critical;
                        //  1) factors, positions and surroundingColors have to have the same count.
                        //  2) The number of blend sets cannot be greater than the number of points in the path
                        //  3) There must be two blend sets defined and first pos. = 0 and the last pos = 1.
                        if (IsLegalBlendAction(BlendAction.update, posVal))
                            bgs.position = posVal;

                        UpdateBlendGroup(bgs);
                    }
                    break;
                case "tsbRemove":
                    if (IsLegalBlendAction(BlendAction.remove, 0))
                        RemoveBlendGroup();
                    break;
                case "tsbClear":
                    Reset();
                    break;
                case "tsbUp":
                    if (OnBlendAction != null)
                        OnBlendAction(this, new BlendActionEventArgs(listBox1.SelectedIndex, BlendAction.moveUp, bgs));
                    break;
                case "tsbDown":
                    if (OnBlendAction != null)
                        OnBlendAction(this, new BlendActionEventArgs(listBox1.SelectedIndex, BlendAction.moveDown, bgs));
                    break;
            }
            listBox1.SelectedIndex = index;
        }

        /// <summary>
        /// Determine if the pending blend action is valid
        /// </summary>
        /// <param name="ba"></param>
        /// <param name="pval"></param>
        /// <returns></returns>
        private bool IsLegalBlendAction(BlendAction ba, float pval)
        {
            int index = listBox1.SelectedIndex;
            if (index < 0 || index >= blendGroupList.Count)
                return false;

            BlendGroupStruct bgs = blendGroupList[index];

            if (index == 0 || index == blendGroupList.Count - 1)
            {
                if (ba == BlendAction.remove)
                    MessageBox.Show("There must be at least 2 elements, the first position must be 0 and the last must be 1");
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// MouseDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblColor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                lblColor.BackColor = colorDialog.colorSelector1.PrimaryColor;
            else
                colorDialog.colorSelector1.PrimaryColor = lblColor.BackColor;
        }

        /// <summary>
        /// Label3 is the ListBox column header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label3_Paint(object sender, PaintEventArgs e)
        {
            Color clr1 = Color.White;
            Color clr2 = Color.FromArgb(0, 0, 75);
            LinearGradientBrush lgb = new LinearGradientBrush(label3.ClientRectangle, clr1, clr2, 90f, true);
            Font fnt = new Font("Tahoma", 10);
            SolidBrush br = new SolidBrush(Color.WhiteSmoke);

            e.Graphics.FillRectangle(lgb, label3.ClientRectangle);
            e.Graphics.DrawString("   Color                  Pos    Fac", fnt, br, label3.ClientRectangle);

            br.Dispose();
            fnt.Dispose();
            lgb.Dispose();
        }

        /// <summary>
        /// Paints the ToolStrip backgound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStrip_Paint(object sender, PaintEventArgs e)
        {
            Color clr1 = Color.White;
            Color clr2 = Color.FromArgb(0, 0, 75);
            LinearGradientBrush lgb = new LinearGradientBrush(toolStrip1.ClientRectangle, clr1, clr2, 90f, true);

            e.Graphics.FillRectangle(lgb, toolStrip1.ClientRectangle);

            lgb.Dispose();
        }

        /// <summary>
        /// Handles when checkbox check state changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (currentShape == null || ignore)
                return;

            if (checkBox1.Checked)
            {
                rbBlend.Enabled = true;
                rbColorBlend.Enabled = true;
            }
            else
            {
                rbBlend.Enabled = false;
                rbColorBlend.Enabled = false;
            }

            BlendEditorStyleChangeEventArgs bes = null;

            if (checkBox1.Checked)
                bes = new BlendEditorStyleChangeEventArgs(BlendStyle.enabled);
            else
                bes = new BlendEditorStyleChangeEventArgs(BlendStyle.disabled);

                if (OnBlendEditorStyleChange != null)
                    OnBlendEditorStyleChange(this, bes);
        }
        
        /// <summary>
        /// Handles the radio button check change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (currentShape == null || ignore)
                return;

            BlendEditorStyleChangeEventArgs bes = null;
            if (checkBox1.CheckState == CheckState.Checked)
            {
                if (rbBlend.Checked || rbColorBlend.Checked)
                {
                    if (rbBlend.Checked)
                        bes = new BlendEditorStyleChangeEventArgs(BlendStyle.regular);
                    else
                        bes = new BlendEditorStyleChangeEventArgs(BlendStyle.color);
                }
                else
                    bes = new BlendEditorStyleChangeEventArgs(BlendStyle.enabled);
            }

            if (OnBlendEditorStyleChange != null)
                OnBlendEditorStyleChange(this, bes);

        }

        #endregion
    }

    #region Blend event arg classes

    public class BlendEditorStyleChangeEventArgs : EventArgs
    {
        public BlendStyle style;
        public bool state;

        public BlendEditorStyleChangeEventArgs(BlendStyle bs)
        {
            style = bs;
        }
    }

    public class BlendActionEventArgs : EventArgs
    {
        public BlendAction act;
        public BlendGroupStruct item;
        public int index;

        public BlendActionEventArgs(int i, BlendAction a, BlendGroupStruct itm)
        {
            index = i;
            act = a;
            item = itm;
        }
    }

    #endregion
}
