using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text.RegularExpressions;

//===================================================================================================
//
//	Module Name: ShapeBase
//
//	Author: Mike Hankey
//
//	Create Date: 10/26/07
//
//	Copyright: WoodWare 2002-2007
//
//	Version History: 1.0
//
//	Notes: I have been working on this for some time but am just now acknowledging as true project
//          and am cleaning it up for release as part of the Gradiator::Phase II release.
//       11/1/07 Tried using the IDisposable Interface here but since I remove items from the list
//          during z-order manip. that it would dispose of it during remove and when I re-inserted
//          it on the list it would be invalid.
//
//====================================================================================================

namespace ShapesClassLib
{
	public enum Shapes
	{
		pointer = 1,
		select,     //TODO Add func.
        Line,       //TODO Add func.
        arrow,      //TODO Add func. ??
		Rectangular,
        RoundedRectangle,
		Elliptical,
		Text,
        Image
	};

    public enum FillType
    {
        none,
        solid, 
        linearGradient,
        pathGradient
    };

	/// <summary>
	/// Summary description for ShapeBase.
	/// </summary>
    public class ShapeBase : object, ICloneable
    {
        #region Shape Properties

        protected ShapeBrush currentBrush = null;
        public ShapeBrush CurrentBrush
        {
            get { return currentBrush; }
            set { currentBrush = value; }
        }

        protected int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        protected Rectangle _rectangle = new Rectangle();
        public Rectangle ShapeRectangle
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }

        protected string _fontFamily = "Palatino Linotype";
        public string FontFamily
        {
            get { return _fontFamily; }
            set { _fontFamily = value; }
        }

        protected float _fontSize = 10;
        public float FontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; }
        }

        protected string _text = string.Empty;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        protected Shapes _type = Shapes.Rectangular;
        public Shapes ShapeType
        {
            get { return _type; }
            set { _type = value; }
        }

        protected bool border = true;
        public bool HasBorder
        {
            get { return border; }
            set { border = value; }
        }

        #endregion

        #region Local Properties

        protected Pen penBorder = new Pen(Color.Black);
        protected Brush brDim = new SolidBrush(Color.LightGray);
        protected static Random rnd = new Random(-1);

        private Image _image = null;
        public Image ShapeImage
        {
            get { return _image; }
            set { _image = value; }
        }

        private bool _selected;
        public bool ShapeSelected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        private float _area;
        public float ShapeArea
        {
            get { return _area; }
            set { _area = value; }
        }

        protected ShapeControl shapeControlObject = new ShapeControl();
        public ShapeControl ShapeControlObject
        {
            get { return shapeControlObject; }
            set { shapeControlObject = value; }
        }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public ShapeBase()
        {
            _id = rnd.Next();
            _rectangle = new Rectangle(0, 0, 10, 10);
            _type = Shapes.Rectangular;

            currentBrush = new ShapeBrush();   //TEST Default brush
            shapeControlObject.CurrentShape = this;
        }

        #region IClonable interface member definitions
        /// <summary>
        /// Clone the object currently selected, from IClonable
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            ShapeBase obj = new ShapeBase();

            switch (_type)
            {
                case Shapes.Rectangular:
                    obj = new Rectangular(_rectangle);
                    break;
                case Shapes.Elliptical:
                    obj = new Elliptical(_rectangle);
                    break;
                case Shapes.RoundedRectangle:
                    obj = new RoundedRectangle(_rectangle);
                    break;
                case Shapes.Text:
                    obj = new Text(_rectangle);
                    break;
                case Shapes.Image:
                    obj = new ShapeImage(_rectangle);
                    break;
            }

            obj._id = rnd.Next();
            obj._type = _type; 
          
            obj.FontFamily = FontFamily;
            obj.FontSize = FontSize;
            obj.Text = Text;

            obj.HasBorder = HasBorder;
            
            //Clone the properties at this level
            obj.currentBrush = (ShapeBrush) currentBrush.Clone();

            return (object)obj;
        }

          /// <summary>
        /// Creates a ShapeObject XmlNode and returns it.
        /// </summary>
        /// <param name="dom"></param>
        /// <returns></returns>
        public XmlNode ObjectToXML(ref XmlDataDocument dom)
        {
            XmlElement root = dom.CreateElement("ShapeObject");

            XmlElement elem = dom.CreateElement("Id");
            elem.InnerText = _id.ToString();
            root.AppendChild(elem);
            
            elem = dom.CreateElement("Rectangle");
            string str = _rectangle.X.ToString() + "," +
                _rectangle.Y.ToString() + "," +
                _rectangle.Width.ToString() + "," +
                _rectangle.Height.ToString();
            elem.InnerText = str;
            root.AppendChild(elem);

            elem = dom.CreateElement("FontFamily");
            elem.InnerText = FontFamily;
            root.AppendChild(elem);

            elem = dom.CreateElement("FontSize");
            elem.InnerText = FontSize.ToString();
            root.AppendChild(elem);

            elem = dom.CreateElement("Text");
            elem.InnerText = Text;
            root.AppendChild(elem);

            elem = dom.CreateElement("Type");
            elem.InnerText = _type.ToString();
            root.AppendChild(elem);

            elem = dom.CreateElement("HasBorder");
            elem.InnerText = HasBorder.ToString();
            root.AppendChild(elem);

            currentBrush.ObjectToXml(ref dom, root);
            
            return root;
        }

        #endregion

        #region Virtual Methods

        public virtual void Draw(PaintEventArgs pe, float scale)
        {
        }

        public virtual double CalculateArea()
        {
            _area = _rectangle.Width * _rectangle.Height;
            return _area;
        }

        #endregion

        #region Move/Resize methods

        /// <summary>
        /// Moves the object to the new position after checking boundries
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="bnds"></param>
        public virtual void Move(int x, int y, Rectangle bnds)
        {
            Point pt = CheckBoundries(new Point(x, y), bnds);

            _rectangle.Location = pt;

            //Need to update the brushes reference rectangle
            currentBrush.ReferenceRectangle = _rectangle;
        }

        /// <summary>
        /// Checks and Adjust as necessary tp keep object inbounds
        /// </summary>
        /// <param name="p"></param>
        /// <param name="bnds"></param>
        /// <returns></returns>
        private Point CheckBoundries(Point p, Rectangle bnds)
        {
            Point pt = p;

            //Keeps the item inside boundry but allows movement
            //	on adjacent side.
            if (pt.X < bnds.X)
                pt.X = bnds.X;

            if  (pt.Y < bnds.Y)
                pt.Y = bnds.Y;

            if ((pt.X + _rectangle.Width) > bnds.Width)
                pt.X = bnds.Width - _rectangle.Width;

            if ((pt.Y + _rectangle.Height) > bnds.Height)
                pt.Y = bnds.Height - _rectangle.Height;

            return pt;
        }

        /// <summary>
        /// This code was obtained in part from the DrawTools article on CodeProject by Alex Fr.
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="ctrlNew"></param>
        public void NormalizeRectangle(Point pt, Rectangle bnds, int ctrlNew)
        {
            int left = this._rectangle.Left;
            int top = this._rectangle.Top;
            int right = this._rectangle.Right;
            int bottom = this._rectangle.Bottom;

            switch ( ctrlNew )
            {
                case 1:
                    left = pt.X;
                    top = pt.Y;
                    break;
                case 2:
                    top = pt.Y;
                    break;
                case 3:
                    right = pt.X;
                    top = pt.Y;
                    break;
                case 4:
                    right = pt.X;
                    break;
                case 5:
                    right = pt.X;
                    bottom = pt.Y;
                    break;
                case 6:
                    bottom = pt.Y;
                    break;
                case 7:
                    left = pt.X;
                    bottom = pt.Y;
                    break;
                case 8:
                    left = pt.X;
                    break;
            }
            
            this._rectangle.X = left;
            this._rectangle.Y = top;
            this._rectangle.Width = right - left;
            this._rectangle.Height = bottom - top;

            //TODO Better but still not quite right - CheckBoundries
            _rectangle.Location = CheckBoundries(_rectangle.Location, bnds);
        }

        /// <summary>
        /// Resizes the object also moves control in proper direction
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="bnds"></param>
        public virtual void Resize(int ctrl, int x, int y, Rectangle bnds)
        {
            //Resize according to direction
            NormalizeRectangle(new Point(x, y), bnds, ctrl);

            //Need to update the brushes reference rectangle
            currentBrush.ReferenceRectangle = _rectangle;
            
            //Calculate area of new rectangle
            CalculateArea();
        }

        /// <summary>
        /// An absolute Resize call, but doesn't check the bounderies
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="ht"></param>
        public void Resize(int wid, int ht)
        {
            _rectangle.Size = new Size(wid, ht);

            //Need to update the brushes reference rectangle
            currentBrush.ReferenceRectangle = _rectangle;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Converts a string "Color [A=n,R=n,G=n,B=n]" to Color
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Color ConvertStringToColor(string s)
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
        public static string ConvertColorToString(Color clr)
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

        /// <summary>
        /// Converts a string "n,n,n,n" to a Rectangle
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Rectangle ConvertStringToRectangle(string str)
        {
            Rectangle rct = new Rectangle();
            Regex r = new Regex(@"\d+");

            MatchCollection mc = r.Matches(str);
            
            rct.X = int.Parse(mc[0].ToString());
            rct.Y = int.Parse(mc[1].ToString());
            rct.Width = int.Parse(mc[2].ToString());
            rct.Height = int.Parse(mc[3].ToString());

            return rct;
        }

        /// <summary>
        /// Creates and returns a GraphicsPath containing a Rounded Rectangle (duh?)
        /// </summary>
        /// <param name="rct"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static GraphicsPath CreateRoundedRectangle(Rectangle rct, int radius)
        {
            GraphicsPath gp = new GraphicsPath();
            Point pt1 = new Point(rct.X + radius, rct.Y);
            Point pt2 = new Point(rct.X + rct.Width - radius, rct.Y);
            Rectangle rct1 = new Rectangle(rct.X, rct.Y, radius, radius);

            gp.AddArc(rct1, 180f, 90f);
            gp.AddLine(pt1, pt2);

            rct1.Location = pt2;
            gp.AddArc(rct1, 270f, 90f);

            pt1 = new Point(rct.X + rct.Width, rct.Y + radius);
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

        #endregion

    }
}
