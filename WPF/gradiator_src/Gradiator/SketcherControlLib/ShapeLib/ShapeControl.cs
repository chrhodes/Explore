using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SketcherControlLib.ShapeLib
{
    class ShapeControl
    {
        private ShapeBase currentShape;
        public ShapeBase CurrentShape
        {
            get { return currentShape; }
            set { currentShape = value; }
        }

        private GraphicsPath gp = new GraphicsPath();
        private const int controlSize = 8;

        private Rectangle NSizeControl = new Rectangle(0, 0, controlSize, controlSize);
        private Rectangle ESizeControl = new Rectangle(0, 0, controlSize, controlSize);
        private Rectangle SSizeControl = new Rectangle(0, 0, controlSize, controlSize);
        private Rectangle WSizeControl = new Rectangle(0, 0, controlSize, controlSize);
        private Rectangle NWSizeControl = new Rectangle(0, 0, controlSize, controlSize);
        private Rectangle NESizeControl = new Rectangle(0, 0, controlSize, controlSize);
        private Rectangle SWSizeControl = new Rectangle(0, 0, controlSize, controlSize);
        private Rectangle SESizeControl = new Rectangle(0, 0, controlSize, controlSize);

        private RectangleF CenterPointControl = new RectangleF(0, 0, controlSize, controlSize);

        public ShapeControl(ShapeBase sb)
        {
            currentShape = sb;
        }

        public int ControlHitCheck(int x, int y)
        {
            int ret = 0;

            if (NWSizeControl.Contains(x, y))
                ret = 1;
            if (NSizeControl.Contains(x, y))
                ret = 2;
            if (NESizeControl.Contains(x, y))
                ret = 3;
            if (ESizeControl.Contains(x, y))
                ret = 4;
            if (SESizeControl.Contains(x, y))
                ret = 5;
            if (SSizeControl.Contains(x, y))
                ret = 6;
            if (SWSizeControl.Contains(x, y))
                ret = 7;
            if (WSizeControl.Contains(x, y))
                ret = 8;
            if (CenterPointControl.Contains(x, y))
                ret = 9;

            return ret;
        }

        public void DrawControl(PaintEventArgs e, bool drawRotateControl)
        {
            DefineControlRegion();

            SolidBrush brCorner = new SolidBrush(Color.FromArgb(80, Color.PowderBlue));
            SolidBrush brSides = new SolidBrush(Color.FromArgb(80, Color.LightCoral));
            Pen p = new Pen(Color.DimGray);

            //TODO Add Horiz and Vert tick marks

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            e.Graphics.FillRectangle(brCorner, NWSizeControl);
            e.Graphics.FillRectangle(brSides, NSizeControl);
            e.Graphics.FillRectangle(brCorner, NESizeControl);
            e.Graphics.FillRectangle(brSides, ESizeControl);
            e.Graphics.FillRectangle(brCorner, SESizeControl);
            e.Graphics.FillRectangle(brSides, SSizeControl);
            e.Graphics.FillRectangle(brCorner, SWSizeControl);
            e.Graphics.FillRectangle(brSides, WSizeControl);

            e.Graphics.DrawRectangle(p, NWSizeControl);
            e.Graphics.DrawRectangle(p, NSizeControl);
            e.Graphics.DrawRectangle(p, NESizeControl);
            e.Graphics.DrawRectangle(p, ESizeControl);
            e.Graphics.DrawRectangle(p, SESizeControl);
            e.Graphics.DrawRectangle(p, SSizeControl);
            e.Graphics.DrawRectangle(p, SWSizeControl);
            e.Graphics.DrawRectangle(p, WSizeControl);

            //Draw Center Point
            if (currentShape.fillType == FillType.pathGradient)
            {
                SolidBrush br = new SolidBrush(Color.Red);

                //Offset the inner filled Elipse and fill it
                RectangleF tmp = CenterPointControl;
                tmp.Inflate(-2, -2);
                e.Graphics.FillEllipse(br, tmp);

                //Draw the outer circle
                e.Graphics.DrawEllipse(new Pen(Color.Black), CenterPointControl);
                br.Dispose();
            }

            p.Dispose();
            brCorner.Dispose();
            brSides.Dispose();
        }

        protected void DefineControlRegion()
        {
            Rectangle me = Rectangle.Ceiling(gp.GetBounds());

            NWSizeControl.Location = me.Location;
            NSizeControl = new Rectangle(me.Location.X + controlSize,
                me.Y,
                me.Width - (controlSize * 2),
                controlSize);
            NESizeControl.Location = new Point(me.Location.X + me.Width - controlSize, me.Location.Y);
            ESizeControl = new Rectangle(me.X + me.Width - controlSize,
                me.Y + controlSize,
                controlSize,
                me.Height - (controlSize * 2));
            SESizeControl.Location = new Point(me.X + me.Width - controlSize, me.Y + me.Height - controlSize);
            SSizeControl = new Rectangle(me.X + controlSize,
                me.Y + me.Height - controlSize,
                me.Width - (controlSize * 2),
                controlSize);
            SWSizeControl.Location = new Point(me.X, me.Y + me.Height - controlSize);
            WSizeControl = new Rectangle(me.X,
                me.Y + controlSize,
                controlSize,
                me.Height - (controlSize * 2));

            //Convert center point to actual location
            CenterPointControl.Location = new PointF(
                shape._rectangle.X + (centerPoint.X * shape._rectangle.Width) - (controlSize / 2),
                shape._rectangle.Y + (centerPoint.Y * shape._rectangle.Height) - (controlSize / 2));
        }
    }
}
