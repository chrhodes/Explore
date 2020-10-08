using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using ShapesClassLib;

namespace SketcherControlLib
{
    public partial class AboutGradiator : Form
    {
        public AboutGradiator()
        {
            InitializeComponent();
            this.Height = 296;
        }

        private void AboutGradiator_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.CompositingQuality = CompositingQuality.GammaCorrected;

            DrawBackground(e.Graphics);
            //DrawBackgroundOverlay(e.Graphics);
            DrawButton(e.Graphics);
            DrawText(e.Graphics);
        }

        private void DrawBackground(Graphics g)
        {
            Rectangle rct = this.ClientRectangle;
            GraphicsPath gp = CreateRoundedRectangle(rct, 15);
            this.Region = new Region(gp);

            PathGradientBrush pgb = new PathGradientBrush(gp);

            Color[] clr = new Color[] {
                Color.FromArgb(255, 153, 204, 204),
                Color.FromArgb(255, 112, 149, 149),
                Color.FromArgb(255, 79, 79, 79),
                Color.FromArgb(255, 139, 163, 178),
                Color.FromArgb(255, 95, 113, 123),
                Color.FromArgb(255, 60, 60, 60) };
            float[] pos = new float[] { 0f, .82f, .85f, .86f, .92f, 1f };

            ColorBlend cb = new ColorBlend();
            cb.Positions = pos;
            cb.Colors = clr;

            pgb.InterpolationColors = cb;
            pgb.SurroundColors = clr;
            pgb.CenterPoint = new PointF(rct.Width / 2, rct.Height / 2);

            g.FillPath(pgb, gp); 

            gp.Dispose();
            pgb.Dispose();
        }

        private const int GAP = 24;
        private const int ARC_WIDTH = 100;
        public static GraphicsPath CreateRoundedRectangle(Rectangle rct, int radius)
        {
            GraphicsPath gp = new GraphicsPath();
            Point pt1 = new Point(rct.X + radius, rct.Y + GAP);
            Point pt2 = new Point(rct.X + (rct.Width / 2) - (ARC_WIDTH / 2), rct.Y + GAP);
            Rectangle rct1 = new Rectangle(rct.X, rct.Y + GAP, radius, radius);

            gp.AddArc(rct1, 180f, 90f);
            gp.AddLine(pt1, pt2);

            rct1 = new Rectangle(pt2.X, rct.Y, ARC_WIDTH, ARC_WIDTH / 2);
            gp.AddArc(rct1, 182f, 176f);

            pt1 = new Point(rct.X + (rct.Width / 2) + (ARC_WIDTH / 2), pt2.Y);
            pt2 = new Point(rct.X + rct.Width - radius, pt2.Y);
            gp.AddLine(pt1, pt2);

            rct1.Location = pt2;
            rct1.Size = new Size(radius, radius);
            gp.AddArc(rct1, 270f, 90f);

            pt1 = new Point(rct.X + rct.Width, rct.Y + radius + GAP);
            pt2 = new Point(rct.X + rct.Width, rct.Y + rct.Height - radius);
            gp.AddLine(pt1, pt2);

            rct1.Location = new Point(rct.X + rct.Width - radius, rct.Y + rct.Height - radius);
            gp.AddArc(rct1, 0f, 90f);

            pt1 = new Point(rct.X + rct.Width - radius, rct.Y + rct.Height);
            pt2 = new Point(rct.X + radius, rct.Y + rct.Height);
            gp.AddLine(pt1, pt2);

            rct1.Location = new Point(rct.X, rct.Y + rct.Height - radius);
            gp.AddArc(rct1, 90f, 90f);

            gp.CloseFigure();

            return gp;
        }
        
        private void DrawBackgroundOverlay(Graphics g)
        {
            Rectangle rct = new Rectangle(10, 10, 231, 276);
            GraphicsPath gp = CreateRoundedRectangle(rct, 15);
            PathGradientBrush pgb = new PathGradientBrush(gp);
            ColorBlend cb = new ColorBlend();
            Pen p = new Pen(Color.Black);

            float[] pos = new float[] { 0f, .65f, 1f };
            Color[] clr = new Color[] { Color.FromArgb(100, 221, 236, 255),
                                        Color.FromArgb(100, 50, 92, 158),
                                        Color.FromArgb(100, 17, 32, 101) };

            cb.Positions = pos;
            cb.Colors = clr;
            pgb.InterpolationColors = cb;
            pgb.SurroundColors = clr;
            pgb.CenterPoint = new PointF(rct.Width * .8f, rct.Height * .8f);

            g.FillPath(pgb, gp);
            //g.DrawPath(p, gp);

            p.Dispose();
            gp.Dispose();
            pgb.Dispose();
        }

        private Rectangle btnRct = new Rectangle(75, 1, 102, 54);
        private void DrawButton(Graphics g)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(btnRct);
            PathGradientBrush pgb = new PathGradientBrush(gp);
            ColorBlend cb = new ColorBlend();
            Pen p = new Pen(Color.Black);
            Region rgn = new Region(gp);

            float[] pos = new float[] { 0f, .87f, .7f, .88f, 1f };
            Color[] clr = new Color[] { Color.FromArgb(255, 131, 149, 184),
                                        Color.FromArgb(255, 20, 20, 20),
                                        Color.FromArgb(255, 20, 20, 20),
                                        Color.FromArgb(255, 139, 163, 178),
                                        Color.FromArgb(255, 44, 65, 82) };

            if (hit)
                clr[0] = Color.FromArgb(210, Color.LightGreen);

            cb.Positions = pos;
            cb.Colors = clr;
            pgb.InterpolationColors = cb;
            pgb.SurroundColors = clr;

            g.FillPath(pgb, gp);
            g.DrawPath(p, gp);

            rgn.Dispose();
            p.Dispose();
            gp.Dispose();
            pgb.Dispose();
        }

        private void DrawText(Graphics g)
        {
            Rectangle rct = new Rectangle(30, 65, 200, 60);
            Rectangle trct = new Rectangle(110, 16, 40, 22);
            Rectangle crct = new Rectangle(30, 126, 200, 173);

            Font fnt = new Font("Palatino Linotype", 14);
            Font f1 = new Font("Palatino Linotype", 10);

            LinearGradientBrush br = new LinearGradientBrush(rct, Color.FromArgb(255, 20, 20, 20), Color.FromArgb(255, 100, 100), 45f, true);
            SolidBrush b = new SolidBrush(Color.White);
            SolidBrush b1 = new SolidBrush(Color.Black);

            g.DrawString("Gradiator Version 2.0\n   by Mike Hankey", fnt, br, rct);
            g.DrawString("OK", fnt, b, trct);
            g.DrawString("This software is provided on an as-is basis, the author takes no responsibility for the usability or upkeep of this application.\n\nYa'll enjoy\nMike ",
                f1, b1, crct);

            f1.Dispose();
            b1.Dispose();
            b.Dispose();
            fnt.Dispose();
            br.Dispose();
        }

        private void AboutGradiator_MouseDown(object sender, MouseEventArgs e)
        {
            if (btnRct.Contains(e.Location))
                Close();
        }

        private bool hit = false;
        private bool phit = false;
        private void AboutGradiator_MouseMove(object sender, MouseEventArgs e)
        {
            Rectangle offRct = btnRct;
            offRct.Inflate(-3, -3);
            if (offRct.Contains(e.Location))
                hit = true;
            else
                hit = false;
            if (phit != hit)
            {
                phit = hit;
                Invalidate(btnRct);
            }
        }
    }
}