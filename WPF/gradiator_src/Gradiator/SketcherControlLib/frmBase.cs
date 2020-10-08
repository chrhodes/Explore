using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace SketcherControlLib
{
    public partial class frmBase : Form
    {
        private string caption = string.Empty;
        [Category("WoodWare")]
        [Description("Form Caption")]
        public string Caption
        {
            get { return caption; }
            set 
            { 
                caption = value;
                this.Invalidate();
            }
        }
        public frmBase()
        {
            InitializeComponent();
        }

        private GraphicsPath gp = new GraphicsPath();
        private void frmBase_Paint(object sender, PaintEventArgs e)
        {
            gp.Reset();

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

            DrawFormBackground(e);
            DrawNonClientArea(e);
            DrawClientArea(e);

        }

        private void DrawFormBackground(PaintEventArgs e)
        {
            gp = ShapeLib.ShapeBase.CreateRoundedRectangle(this.DisplayRectangle, 16);

            this.Region = new Region(gp);

            Color[] surroundingColors = new Color[] { 
                Color.FromArgb(180, 255, 255, 255),
                Color.FromArgb(180, 221, 214, 170),
                Color.FromArgb(180, 87, 87, 87),
                Color.FromArgb(180, 20, 32, 87),
                Color.FromArgb(105, 64, 117, 253),
                Color.FromArgb(255, 17, 32, 101) };

            PathGradientBrush pgb = new PathGradientBrush(gp);

            pgb.CenterPoint = new PointF(this.Width / 2,
                            this.Height / 2);
            pgb.CenterColor = Color.White;
            pgb.SurroundColors = surroundingColors;
            pgb.WrapMode = WrapMode.Tile;

            ColorBlend cb = new ColorBlend(6);
            cb.Colors = surroundingColors;
            cb.Positions = new float[] {
                0.0f, 0.92f, 0.94f, 0.95f, 0.97f, 1.0f};
            pgb.InterpolationColors = cb;
            
            e.Graphics.FillPath(pgb, gp);

            pgb.Dispose();
        }

        private Rectangle exitRct = new Rectangle();
        private Image exitImage = null;
        private void DrawNonClientArea(PaintEventArgs e)
        {
            Font fnt = new Font("Palatino Linotype", 14);
            PointF pt = new Point(21, 14);
            SolidBrush br = new SolidBrush(Color.Black);

            e.Graphics.DrawString(caption, fnt, br, pt);

            if (exitImage != null)
                e.Graphics.DrawImage(exitImage, exitRct);

            fnt.Dispose();
            br.Dispose();
        }

        private void DrawClientArea(PaintEventArgs e)
        {
            Rectangle rct = new Rectangle(22, 44, this.Width - 45, this.Height - 70);

            gp.Reset();
            gp = ShapeLib.ShapeBase.CreateRoundedRectangle(rct, 16);

            e.Graphics.DrawPath(new Pen(Color.DimGray, 2), gp);
        }

        private Image prevImg = null;
        private void frmBase_MouseMove(object sender, MouseEventArgs e)
        {
            Rectangle rct = new Rectangle(1, 1, this.Width - 2, 28);

            if (rct.Contains(new Point(e.X, e.Y)))
                Cursor.Current = Cursors.SizeAll;
            else
                Cursor.Current = Cursors.Default;

            if (exitRct.Contains(e.X, e.Y))
            {
                Cursor.Current = Cursors.Arrow;
                exitImage = global::SketcherControlLib.Properties.Resources.Check1;
            }
            else
            {
                Cursor.Current = Cursors.SizeAll;
                exitImage = global::SketcherControlLib.Properties.Resources.Check;
            }

            if (prevImg != exitImage)
            {
                prevImg = exitImage;
                this.Invalidate(exitRct);
            }

            if (mouseDown)
            {
                pt.X += e.X - offset.X;
                pt.Y += e.Y - offset.Y;
                this.Location = pt;
            }

        }

        private Point offset = new Point(0);
        private bool mouseDown = false;
        private Point pt = new Point(0);
        private void frmBase_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;

            offset.X = e.X;
            offset.Y = e.Y;

            if (exitRct.Contains(e.X, e.Y))
                this.Close();
        }

        private void frmBase_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void frmBase_Load(object sender, EventArgs e)
        {
            exitRct = new Rectangle(this.Width - 50, 16, 24, 24);
            exitImage = global::SketcherControlLib.Properties.Resources.Check;
            this.DoubleBuffered = true;
        }
    }
}