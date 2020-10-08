using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ShapesClassLib
{
    public class ShapeControl
    {
        public ShapeControl()
        {
        }

        public ShapeControl(ShapeBase sb)
        {
            currentShape = sb;
        }

        public const int controlSize = 8;

        private ShapeBase currentShape = null;
        public ShapeBase CurrentShape
        {
            get { return currentShape; }
            set { currentShape = value; }
        }

        private RectangleF centerPointControl = new RectangleF(0, 0, controlSize, controlSize);
        public RectangleF CenterPointControl
        {
            get { return centerPointControl; }
            set { centerPointControl = value; }
        }

        private Rectangle NSizeControl = new Rectangle(0, 0, controlSize, controlSize);
        private Rectangle ESizeControl = new Rectangle(0, 0, controlSize, controlSize);
        private Rectangle SSizeControl = new Rectangle(0, 0, controlSize, controlSize);
        private Rectangle WSizeControl = new Rectangle(0, 0, controlSize, controlSize);
        private Rectangle NWSizeControl = new Rectangle(0, 0, controlSize, controlSize);
        private Rectangle NESizeControl = new Rectangle(0, 0, controlSize, controlSize);
        private Rectangle SWSizeControl = new Rectangle(0, 0, controlSize, controlSize);
        private Rectangle SESizeControl = new Rectangle(0, 0, controlSize, controlSize);

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

        public void DrawControl(PaintEventArgs e, GraphicsPath gp)
        {
            DefineControlRegion(gp);

            SolidBrush brCorner = new SolidBrush(Color.FromArgb(80, Color.PowderBlue));
            SolidBrush brSides = new SolidBrush(Color.FromArgb(80, Color.LightCoral));
            Pen p = new Pen(Color.DimGray);

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
            if (currentShape.CurrentBrush.ShapeFillType == FillType.pathGradient)
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

        protected void DefineControlRegion(GraphicsPath gp)
        {
            Rectangle rct = Rectangle.Ceiling(gp.GetBounds());

            NWSizeControl.Location = new Point(rct.Location.X - controlSize, rct.Location.Y - controlSize);
            NSizeControl = new Rectangle(rct.Location.X,
                rct.Y - controlSize,
                rct.Width,
                controlSize);
            NESizeControl.Location = new Point(rct.Location.X + rct.Width, rct.Location.Y - controlSize);
            ESizeControl = new Rectangle(rct.X + rct.Width,
                rct.Y,
                controlSize,
                rct.Height);
            SESizeControl.Location = new Point(rct.X + rct.Width, rct.Y + rct.Height);
            SSizeControl = new Rectangle(rct.X,
                rct.Y + rct.Height,
                rct.Width,
                controlSize);
            SWSizeControl.Location = new Point(rct.X - controlSize, rct.Y + rct.Height);
            WSizeControl = new Rectangle(rct.X - controlSize,
                rct.Y,
                controlSize,
                rct.Height);

            //Convert center point to actual location
            PointF ptf = currentShape.CurrentBrush.CenterPoint;
            CenterPointControl = new RectangleF(new PointF(
                ptf.X - (controlSize / 2),
                ptf.Y - (controlSize / 2)),
                new SizeF(controlSize, controlSize));
        }
    }
}
