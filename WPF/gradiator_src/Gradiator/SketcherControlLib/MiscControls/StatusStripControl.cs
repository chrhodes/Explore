using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ShapesClassLib;

namespace SketcherControlLib.MiscControls
{
    public enum ShapeScrollDirection
    {
        up,
        down
    }

    public enum StatusControlChangeType
    {
        x,
        y,
        width,
        height,
        item
    }

    public partial class StatusStripControl : UserControl
    {
        private ShapeBase currentShape;
        public ShapeBase CurrentShape
        {
            get { return currentShape; }
            set 
            { 
                currentShape = value;
                SetValues();
            }
        }

        public delegate void OnStatusControlValueChangeEventHandler(object sender, StatusStripControlEventArgs e);
        [Category("WoodWare")]
        [Description("Fired when one of the value changes")]
        public event OnStatusControlValueChangeEventHandler OnStatusControlValueChange;

        public delegate void OnShapeScrollEventHandler(object sender, StatusStripControlEventArgs e);
        [Category("WoodWare")]
        [Description("Fired when one of the objec scroll bar is clicked")]
        public event OnShapeScrollEventHandler OnShapeScroll;

        public StatusStripControl()
        {
            InitializeComponent();
        }

        private void SetValues()
        {
            if (currentShape == null)
                return;

            tbXCoord.Text = currentShape.ShapeRectangle.X.ToString();
            tbYCoord.Text = currentShape.ShapeRectangle.Y.ToString();
            tbWidth.Text = currentShape.ShapeRectangle.Width.ToString();
            tbHeight.Text = currentShape.ShapeRectangle.Height.ToString();

            lblItem.Text = currentShape.ShapeType.ToString();
        }

        public void UpdateStatus(ShapeBase sb)
        {
            CurrentShape = sb;
        }

        public void Clear()
        {
            tbXCoord.Text = string.Empty;
            tbYCoord.Text = string.Empty;
            tbWidth.Text = string.Empty;
            tbHeight.Text = string.Empty;

            lblItem.Text = string.Empty;
        }

        private void StatusStripControl_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush lgb = new LinearGradientBrush(this.ClientRectangle, Color.White, Color.LightSteelBlue, 90f);

            e.Graphics.FillRectangle(lgb, this.ClientRectangle);
        }

        private void genericControl_Leave(object sender, EventArgs e)
        {
            StatusStripControlEventArgs args = null;

            TextBox tb = (TextBox)sender;
            
            //If not a valid string return without doing anything
            if (tb.Text == string.Empty)
                return;

            switch (tb.Name)
            {
                case "tbXCoord":
                    args = new StatusStripControlEventArgs(StatusControlChangeType.x, int.Parse(tbXCoord.Text));
                    break;
                case "tbYCoord":
                    args = new StatusStripControlEventArgs(StatusControlChangeType.y, int.Parse(tbYCoord.Text));
                    break;
                case "tbWidth":
                    args = new StatusStripControlEventArgs(StatusControlChangeType.width, int.Parse(tbWidth.Text));
                    break;
                case "tbHeight":
                    args = new StatusStripControlEventArgs(StatusControlChangeType.height, int.Parse(tbHeight.Text));
                    break;
            }

            if (OnStatusControlValueChange != null)
                OnStatusControlValueChange(this, args);
        }

        private Random rnd = new Random();
        private bool inNumericUpDown1 = false;
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (inNumericUpDown1)
                return;
            inNumericUpDown1 = true;

            //I did this because the event is not fired when value equals min or max or soame number
            //  so I chose 100,000 as max and pick a random number with that as max so chances are
            //  value won't repeat. (Rarely)
            numericUpDown1.Value = rnd.Next(100000);

            inNumericUpDown1 = false;
        }

        private void numericUpDown1_MouseDown(object sender, MouseEventArgs e)
        {
            ShapeScrollDirection scd;
            if (e.Y < 10)
                scd = ShapeScrollDirection.up;
            else
                scd = ShapeScrollDirection.down;

            if (OnShapeScroll != null)
                OnShapeScroll(this, new StatusStripControlEventArgs(scd));
        }
    }

    public class StatusStripControlEventArgs : EventArgs
    {
        public ShapeScrollDirection direction;
        public StatusControlChangeType type;
        public int value;

        public StatusStripControlEventArgs(StatusControlChangeType typ, int val)
        {
            type = typ;
            value = val;
        }

        public StatusStripControlEventArgs(ShapeScrollDirection dir)
        {
            dir = direction;
        }
    }
}
