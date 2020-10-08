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
//
//====================================================================================================

namespace SketcherControlLib.ShapeLib
{
	public enum Shapes
	{
		pointer = 1,
		select,     //TODO Add func.
        Line,       //TODO Add func.
		Rectangular,
        RoundedRectangle,
		Elliptical,
		Text,
        Image
	};

    public enum FillType
    {
        solid, 
        linearGradient,
        pathGradient
    };

	/// <summary>
	/// Summary description for ShapeBase.
	/// </summary>
    public class ShapeBase : object, ICloneable
    {
        #region Properties and such

        protected Pen penBorder = new Pen(Color.Black);
        protected Brush brDim = new SolidBrush(Color.LightGray);
        protected GraphicsPath gp = new GraphicsPath();
        protected static Random rnd = new Random(-1);

        public List<Color> surroundingColors = new List<Color>();
        public List<float> positions = new List<float>();
        public List<float> factors = new List<float>();

        //These are the core properties/fields for the Shape class!
        public struct ShapeObject
        {
            public int _id;
            public Shapes _type;
            public Rectangle _rectangle;
        }
        public ShapeObject shape = new ShapeObject();

        private Image _image = null;
        public Image ShapeImage
        {
            get { return _image; }
            set { _image = value; }
        }

        private string _fontFamily = "Palatino Linotype";
        public string FontFamily
        {
            get { return _fontFamily; }
            set { _fontFamily = value; }
        }

        private float _fontSize = 8;
        public float FontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; }
        }

        private string _text = string.Empty;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        //protected Brush fillBrush = null;
        //public Brush FillBrush
        //{
        //    get { return fillBrush; }
        //    set { fillBrush = value; }
        //}

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

        #endregion

        #region Gradiator Properties and such
       
        private Blend blnd = new Blend();
        private ColorBlend cb = new ColorBlend(4);

        private FillType fillType = FillType.solid;
        public FillType ShapeFillType
        {
            get { return fillType; }
            set { fillType = value; }
        }

        private float _angle;
        public float Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }

        private Color _textColor = Color.Black;
        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; }
        }
        protected bool border = false;
        public bool HasBorder
        {
            get { return border; }
            set { border = value; }
        }

        private Color _borderColor = Color.Black;
        public Color BorderColor
        {
            get { return _borderColor; }
            set 
            { 
                _borderColor = value;
                penBorder.Color = value;
            }
        }

        private Color color1 = Color.White;
        public Color Color1
        {
            get { return color1; }
            set { color1 = value; }
        }

        private Color color2 = Color.Black;
        public Color Color2
        {
            get { return color2; }
            set { color2 = value; }
        }

        private bool useSigmaBell = false;
        public bool UseSigmaBell
        {
            get { return useSigmaBell; }
            set { useSigmaBell = value; }
        }

        private float focus = .50f;
        public float LinearFocus
        {
            get { return focus; }
            set
            {
                if (value < 0 || value > 1)
                    throw new InvalidExpressionException("Value must be greater than zero and less than or equal to 1");
                focus = value;
            }
        }

        private float scale = .50f;
        public float LinearScale
        {
            get { return scale; }
            set
            {
                if (value < 0 || value > 1)
                    throw new InvalidExpressionException("Value must be greater than zero and less than or equal to 1");
                scale = value;
            }
        }

        private bool useBlendTriangle = false;
        public bool UseBlendTriangle
        {
            get { return useBlendTriangle; }
            set { useBlendTriangle = value; }
        }

        private bool useBlend = false;
        public bool UseBlend
        {
            get { return useBlend; }
            set { useBlend = value; }
        }

        private bool useColorBlend = false;
        public bool UseColorBlend
        {
            get { return useColorBlend; }
            set { useColorBlend = value; }
        }

        private float angle = 90f;
        public float ViewAngle
        {
            get { return angle; }
            set
            {
                if (value < 0 || value > 360)
                    throw new InvalidExpressionException("Value must be between 0 and 360");
                else
                    angle = value; 
            }
        }

        private bool useGammaCorrection;
        public bool UseGammaCorrection
        {
            get { return useGammaCorrection; }
            set { useGammaCorrection = value; }
        }

        private PointF centerPoint = new Point(0, 0);
        public PointF CenterPoint
        {
            get { return centerPoint; }
            set 
            {
                PointF pt = new PointF(
                    (float) (value.X - shape._rectangle.X) / shape._rectangle.Width,
                    (float) (value.Y - shape._rectangle.Y) / shape._rectangle.Height);
                centerPoint = pt;
            }
        }

        private Point focusScale = new Point(0, 0);
        public Point FocusScale
        {
            get { return focusScale; }
            set { focusScale = value; }
        }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public ShapeBase()
        {
            shape._id = rnd.Next();
            shape._rectangle = new Rectangle(0, 0, 10, 10);
            shape._type = Shapes.Rectangular;

            //Convert center point to actual location at center of shape
            CenterPoint = new PointF(shape._rectangle.X + (shape._rectangle.Width / 2),
                shape._rectangle.Y + (shape._rectangle.Height / 2));

            surroundingColors.Add(Color.White);
            surroundingColors.Add(Color.Black);
            positions.Add(0.0f);
            positions.Add(1.0f);
            factors.Add(.2f);
            factors.Add(.8f);
        }

        /// <summary>
        /// Clone the object currently selected, from IClonable
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            ShapeBase obj = new ShapeBase();

            switch (shape._type)
            {
                case Shapes.Rectangular:
                    obj = new Rectangular(shape._rectangle);
                    break;
                case Shapes.Elliptical:
                    obj = new Elliptical(shape._rectangle);
                    break;
                case Shapes.RoundedRectangle:
                    obj = new RoundedRectangle(shape._rectangle);
                    break;
                case Shapes.Text:
                    obj = new Text(shape._rectangle);
                    break;
                case Shapes.Image:
                    obj = new ShapeImage(shape._rectangle);
                    break;
            }

            obj.shape._id = rnd.Next();
            obj.shape._type = shape._type; 
          
            obj.shape = shape;
            obj.FontFamily = FontFamily;
            obj.FontSize = FontSize;
            obj.Text = Text;

            //Clone the properties at this level
            obj.CenterPoint = new PointF(shape._rectangle.X + (CenterPoint.X * shape._rectangle.Width),
                shape._rectangle.Y + (CenterPoint.Y * shape._rectangle.Height));
            obj.Color1 = Color1;
            obj.Color2 = Color2;
            obj.TextColor = TextColor;
            obj.BorderColor = BorderColor;

            foreach (float f in factors)
                obj.factors.Add(f);

            obj.HasBorder = HasBorder;
            obj.LinearFocus = LinearFocus;
            obj.LinearScale = LinearScale;
            
            foreach (float f in positions)
                obj.positions.Add(f);

            obj.ShapeFillType = ShapeFillType;
            
            foreach (Color c in surroundingColors)
                obj.surroundingColors.Add(c);

            obj.UseBlendTriangle = UseBlendTriangle;
            obj.UseGammaCorrection = UseGammaCorrection;
            obj.UseSigmaBell = UseSigmaBell;
            obj.ViewAngle = ViewAngle;

            obj.UseBlend = UseBlend;
            obj.FocusScale = FocusScale;
            obj.UseColorBlend = UseColorBlend;

            return (object)obj;
        }

        #region Virtual Methods

        public virtual void Draw(PaintEventArgs pe)
        {
        }

        public virtual double CalculateArea()
        {
            _area = shape._rectangle.Width * shape._rectangle.Height;
            return _area;
        }

        #endregion

        #region Methods to Create proper brush

        /// <summary>
        /// Sets the fillBrush global
        /// </summary>
        /// <param name="rct"></param>
        protected Brush SetBrush(Rectangle rct)
        {
            Brush fillBrush = null;
            
            switch (fillType)
            {
                case FillType.solid:
                    Color clr = color1;
                    fillBrush = new SolidBrush(clr);
                    break;
                case FillType.linearGradient:
                    fillBrush = SetLinearBrushProperties(rct);
                    break;
                case FillType.pathGradient:
                    fillBrush = SetPathBrushProperties(rct);
                    break;
                default:
                    fillBrush = new SolidBrush(Color.White);
                    Debug.WriteLine("Using Default FillBrush");
                    break;
                    
            }
            return fillBrush;
        }

        /// <summary>
        /// This method is used to Set the PathGradientBrush properties prior to drawing
        /// </summary>
        /// <param name="rct">The client area</param>
        private Brush SetLinearBrushProperties(Rectangle rct)
        {
            //Create Brush and ?declare? that I'm using it as a LinearG..Brush
            LinearGradientBrush lgb = new LinearGradientBrush(rct, color1, color2, angle, false);
            
            if (useSigmaBell)
                lgb.SetSigmaBellShape(focus, scale);

            if (useBlendTriangle)
                lgb.SetBlendTriangularShape(focus, scale);

            if (useGammaCorrection)
                lgb.GammaCorrection = true;

            if (UseBlend)
            {
                blnd.Positions = positions.ToArray();
                blnd.Factors = factors.ToArray();
                lgb.Blend = blnd;
            }

            if (UseColorBlend)
            {
                cb.Colors = surroundingColors.ToArray();
                cb.Positions = positions.ToArray();
                lgb.InterpolationColors = cb;
            }
            return lgb;
        }

        /// <summary>
        /// This method is used to Set the PathGradientBrush properties prior to drawing
        /// </summary>
        /// <param name="rct">The client area</param>
        private Brush SetPathBrushProperties(Rectangle rct)
        {
            PathGradientBrush pgb = new PathGradientBrush(gp);

            CenterPointControl.Location = new PointF(shape._rectangle.X + (centerPoint.X * shape._rectangle.Width),
                shape._rectangle.Y + (centerPoint.Y * shape._rectangle.Height));
            pgb.CenterPoint = CenterPointControl.Location;
            pgb.CenterColor = color1;
            pgb.FocusScales = new PointF(
                ((float)focusScale.X / 100.0f),
                ((float)focusScale.Y / 100.0f));

            if (UseSigmaBell)
                pgb.SetSigmaBellShape(focus, scale);

            if (useBlendTriangle)
                pgb.SetBlendTriangularShape(focus, scale);

            if (UseBlend)
            {
                blnd.Positions = positions.ToArray();
                blnd.Factors = factors.ToArray();
                pgb.Blend = blnd;
            }

            if (UseColorBlend)
            {
                //When shown together as below it creates much nicer/cleaner images!
                //NOTE SurroundingColors can not exceed number of points in the Path
                if (surroundingColors.Count <= gp.PathData.Points.Length)
                    pgb.SurroundColors = surroundingColors.ToArray();

                cb.Colors = surroundingColors.ToArray();
                cb.Positions = positions.ToArray();
                pgb.InterpolationColors = cb;
            }
            return pgb;
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
            
            shape._rectangle.Location = pt;
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

            if ((pt.X + shape._rectangle.Width) > bnds.Width)
                pt.X = bnds.Width - shape._rectangle.Width;

            if ((pt.Y + shape._rectangle.Height) > bnds.Height)
                pt.Y = bnds.Height - shape._rectangle.Height;

            return pt;
        }

        /// <summary>
        /// This code was obtained in part from the DrawTools article on CodeProject by Alex Fr.
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="ctrlNew"></param>
        public void NormalizeRectangle(Point pt, Rectangle bnds, int ctrlNew)
        {
            int left = this.shape._rectangle.Left;
            int top = this.shape._rectangle.Top;
            int right = this.shape._rectangle.Right;
            int bottom = this.shape._rectangle.Bottom;

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
            
            this.shape._rectangle.X = left;
            this.shape._rectangle.Y = top;
            this.shape._rectangle.Width = right - left;
            this.shape._rectangle.Height = bottom - top;

            //TODO Better but still not quite right - CheckBoundries
            shape._rectangle.Location = CheckBoundries(shape._rectangle.Location, bnds);
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

            //Calculate area of new rectangle
            CalculateArea();
        }

        #endregion

        /// <summary>
        /// Creates a ShapeObject XmlNode and returns it.
        /// </summary>
        /// <param name="dom"></param>
        /// <returns></returns>
        public XmlNode ObjectToXML(ref XmlDataDocument dom)
        {
            XmlElement root = dom.CreateElement("ShapeObject");

            XmlElement elem = dom.CreateElement("Id");
            elem.InnerText = shape._id.ToString();
            root.AppendChild(elem);
            
            elem = dom.CreateElement("Rectangle");
            string str = shape._rectangle.X.ToString() + "," +
                shape._rectangle.Y.ToString() + "," +
                shape._rectangle.Width.ToString() + "," +
                shape._rectangle.Height.ToString();
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
            elem.InnerText = shape._type.ToString();
            root.AppendChild(elem);

            return root;
        }

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

        #region Control stuff - I'm going to hold off on cleaning up til I move it!

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

            //TODO Add Horiz and Vert tick marks???

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
            if (fillType == FillType.pathGradient)
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

        #endregion
    }
}
