using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SketcherControlLib.ShapeLib;

namespace SketcherControlLib
{
    public partial class CenterPointControl : UserControl
    {
        private Point centerPoint;
        private Rectangle controlRectangle = new Rectangle(0, 0, CONTROL_SIZE, CONTROL_SIZE);

        public delegate void OnCenterPointChangingEventHandler(object sender, CenterPointChangingEventArgs e);
        public event OnCenterPointChangingEventHandler OnCenterPointChanging;

        public Point CenterPoint
        {
            get { return centerPoint; }
            set 
            { 
                centerPoint = value;
                Invalidate();
            }
        }

        public CenterPointControl()
        {
            InitializeComponent();
        }

        private const int CONTROL_SIZE = 10;
        private void CenterPointControl_Paint(object sender, PaintEventArgs e)
        {
            //int x = (this.Width * centerPoint.X / 100) - (CONTROL_SIZE / 2) + offset.X;
            //int y = (this.Height * centerPoint.Y / 100) - (CONTROL_SIZE / 2) + offset.Y;
            controlRectangle.Location = centerPoint;

            e.Graphics.DrawEllipse(new Pen(Color.Black), controlRectangle);
        }

        private bool mouseDown = false;
        private bool isMoving = false;
        private Point offset = new Point(0);
        private void CenterPointControl_MouseDown(object sender, MouseEventArgs e)
        {            
            if (e.Button == MouseButtons.Left)
                mouseDown = true;

            if (controlRectangle.Contains(e.Location))
            {
                offset.X = e.X - controlRectangle.Location.X;
                offset.Y = e.Y - controlRectangle.Location.Y;

                isMoving = true;
            }
        }

        private void CenterPointControl_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            isMoving = false;
        }

        private void CenterPointControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown && isMoving)
            {
                controlRectangle.X = e.X - offset.X;
                controlRectangle.Y = e.Y - offset.Y;

                CenterPoint = controlRectangle.Location;

                if (OnCenterPointChanging != null)
                    OnCenterPointChanging(this, new CenterPointChangingEventArgs(CenterPoint));
            }
        }
    }
        public class CenterPointChangingEventArgs : EventArgs
        {
            public Point point;

            public CenterPointChangingEventArgs(Point pt)
            {
                point = pt;
            }
        }
}

