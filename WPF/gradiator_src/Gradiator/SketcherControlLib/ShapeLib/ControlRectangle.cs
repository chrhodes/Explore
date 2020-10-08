using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SketcherControl.ShapeLib
{
	/// <summary>
	/// ControlRetangle Shape class
	/// </summary>
	public class ControlRectangle : Object
	{
		public Rectangle itemRct = new Rectangle(0, 0, 0, 0);

		private const int controlSize = 5;
		private Pen ctrlPen = new Pen(Color.Blue, 1);
		private Brush resizeBrush = new SolidBrush(Color.Red);
		private Brush floatBrush = new SolidBrush(Color.Red);

		private Rectangle NWSizeControl = new Rectangle(0, 0, controlSize, controlSize);
		private Rectangle NESizeControl = new Rectangle(0, 0, controlSize, controlSize);
		private Rectangle SWSizeControl = new Rectangle(0, 0, controlSize, controlSize);
		private Rectangle SESizeControl = new Rectangle(0, 0, controlSize * 2, controlSize * 2);
		private Rectangle FloatControl = new Rectangle(0, 0, controlSize, controlSize);
		private Rectangle Bounds = new Rectangle(0, 0, 0, 0);

		public int ControlSize
		{
			get { return controlSize; }
		}

		private bool _isControlVisible = true;
		public bool IsControlVisible 
		{
			get { return _isControlVisible; }
			set { _isControlVisible = value; }
		}

		public ControlRectangle()
		{
			itemRct = new Rectangle(0, 0, 0, 0);
			FloatControl.Location = new Point(0, 0);
		}

		public ControlRectangle(Rectangle r)
		{
			itemRct = new Rectangle(r.Location, r.Size);

			Initialize(itemRct);
			FloatControl.Location = r.Location;
		}
	
		public ControlRectangle(Rectangle r, Point mp)
		{
			itemRct = new Rectangle(r.Location, r.Size);
			Initialize(itemRct);
			FloatControl.Location = mp;
		}

		public bool HitCheckSizeControl(int x, int y)
		{
			return SESizeControl.Contains(x, y);
		}

		public void Initialize(Rectangle itemRct)
		{
			Bounds = itemRct;

			NWSizeControl.Location = new Point(itemRct.Location.X, itemRct.Location.Y);
			NESizeControl.Location = new Point(itemRct.Location.X + itemRct.Width - controlSize, itemRct.Location.Y);
			SWSizeControl.Location = new Point(itemRct.Location.X, itemRct.Location.Y + itemRct.Height - controlSize);
			SESizeControl.Location = new Point(itemRct.Location.X + itemRct.Width - controlSize, itemRct.Location.Y + itemRct.Height - controlSize);
		}

		public void Move(int x, int y)
		{
			itemRct.X = x;
			itemRct.Y = y;

			NWSizeControl.Location = new Point(x, y);
			SWSizeControl.Location = new Point(x, y + Bounds.Height - controlSize);
			NESizeControl.Location = new Point(x + Bounds.Width - controlSize, y);
			SESizeControl.Location = new Point(x + Bounds.Width - (controlSize * 2), y + Bounds.Height - (controlSize * 2));
			FloatControl.Location = new Point(x, y);
		}

		public void Resize(int x, int y, int w, int h)
		{
			//Do not let user go neg. with width or height
			if (w < this.ControlSize)
				w = this.ControlSize;
			if (h < this.ControlSize)
				h = this.ControlSize;

			itemRct.Width = w;
			itemRct.Height = h;

			Bounds = new Rectangle(x, y, w, h);

			NWSizeControl.Location = new Point(x, y);
			NESizeControl.Location = new Point(x + Bounds.Width - controlSize, y);
			SWSizeControl.Location = new Point(x, y + Bounds.Height - controlSize);
			SESizeControl.Location = new Point(x + Bounds.Width - (controlSize * 2), y + Bounds.Height - (controlSize * 2));
			FloatControl.Location = new Point(x, y);
		}

		public void Draw(PaintEventArgs pe)
		{
			GraphicsPath gp = new GraphicsPath();

			if (!_isControlVisible)
				return;

			gp.AddRectangle(new Rectangle(NWSizeControl.Location, NWSizeControl.Size));
			gp.AddRectangle(new Rectangle(SWSizeControl.Location, SWSizeControl.Size));
			gp.AddRectangle(new Rectangle(NESizeControl.Location, NESizeControl.Size));
            gp.AddEllipse(new Rectangle(SESizeControl.Location, SESizeControl.Size));
            pe.Graphics.DrawPath(ctrlPen, gp);

			gp.Dispose();
		}
	}
}
