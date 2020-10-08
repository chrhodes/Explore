using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Windows.Forms;
using System.Diagnostics;

//===================================================================================================
//
//	Module Name: ShapeBrush
//
//	Author: Mike Hankey
//
//	Create Date: 11/09/07
//
//	Copyright: WoodWare 2002-2007
//
//	Version History: 1.0
//
//	Notes: This is the base class for all the brushes Solid, Linear and Path
//
//====================================================================================================

namespace ShapesClassLib
{
    public class ShapeBrush : Object, ICloneable
    {
        public ShapeBrush()
        {
            //Blending and Color Blending require these.
            surroundingColors.Add(Color.White);
            surroundingColors.Add(Color.Black);
            positions.Add(0.0f);
            positions.Add(1.0f);
            factors.Add(.2f);
            factors.Add(.8f);
        }

        #region properties describing the brush for this shape

        private string name;
        public string BrushName
        {
            get { return name; }
            set { name = value; }
        }

        private Rectangle referenceRct;
        public Rectangle ReferenceRectangle
        {
            get { return referenceRct; }
            set { referenceRct = value; }
        }

        protected FillType fillType = FillType.solid;
        public FillType ShapeFillType
        {
            get { return fillType; }
            set { fillType = value; }
        }

        protected Color color = Color.White;
        public Color ShapeColor
        {
            get { return color; }
            set { color = value; }
        }

        private Color color1 = Color.Black;
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

        private float angle = 90f;
        public float Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        private bool useSigmaBell = false;
        public bool UseSigmaBell
        {
            get { return useSigmaBell; }
            set { useSigmaBell = value; }
        }

        private bool useBlendTriangle = false;
        public bool UseBlendTriangle
        {
            get { return useBlendTriangle; }
            set { useBlendTriangle = value; }
        }

        protected List<Color> surroundingColors = new List<Color>();
        public List<Color> SurroundingColors
        {
            get { return surroundingColors; }
            set { surroundingColors = value; }
        }

        private float focus = .5f;
        /// <summary>
        /// Value must be between 0.0 - 1.0
        /// </summary>
        public float LinearFocus
        {
            get { return focus; }
            set { focus = value; }
        }

        private float scale = .5f;
        public float LinearScale
        {
            get { return scale; }
            set { scale = value; }
        }

        protected List<float> positions = new List<float>();
        public List<float> Positions
        {
            get { return positions; }
            set { positions = value; }
        }

        protected List<float> factors = new List<float>();
        public List<float> Factors
        {
            get { return factors; }
            set { factors = value; }
        }

        private PointF centerPoint = new PointF(.5f, .5f);
        public PointF CenterPoint
        {
            get
            {
                PointF pt = new PointF(
                   (float)(referenceRct.Width * centerPoint.X) + referenceRct.X,
                   (float)(referenceRct.Height * centerPoint.Y) + referenceRct.Y);
                return pt;
            }
            set { centerPoint = value; }
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

        private bool useGammaCorrection = false;
        public bool UseGammaCorrection
        {
            get { return useGammaCorrection; }
            set { useGammaCorrection = value; }
        }

        private PointF focusScale = new PointF(.5f, .5f);
        public PointF FocusScale
        {
            get { return focusScale; }
            set { focusScale = value; }
        }

        #endregion

        #region IClonable interface method

        public object Clone()
        {
            ShapeBrush sb = new ShapeBrush();

            sb.name = this.name;
            sb.angle = this.angle;
            sb.color = this.color;
            sb.color1 = this.color1;
            sb.color2 = this.color2;
            sb.centerPoint = this.centerPoint;
            sb.referenceRct = this.referenceRct;

            sb.fillType = this.fillType;
            sb.focus = this.focus;
            sb.scale = this.scale;
            sb.focusScale = this.focusScale;

            sb.factors.Clear();
            foreach (float f in factors)
                sb.factors.Add(f);

            sb.positions.Clear();
            foreach (float f in positions)
                sb.positions.Add(f);

            sb.surroundingColors.Clear();
            foreach (Color c in this.surroundingColors)
                sb.surroundingColors.Add(c);

            sb.useBlend = this.useBlend;
            sb.useBlendTriangle = this.useBlendTriangle;
            sb.useColorBlend = this.useColorBlend;
            sb.useGammaCorrection = this.useGammaCorrection;
            sb.useSigmaBell = this.useSigmaBell;

            return sb;
        }

        #endregion

        #region SetBrush and associated methods

        private Blend blnd = new Blend();
        private ColorBlend cb = new ColorBlend();

        /// <summary>
        /// Sets the fillBrush global
        /// </summary>
        /// <param name="rct"></param>
        public Brush SetBrush(Rectangle rct, GraphicsPath gp)
        {
            Brush fillBrush = null;

            switch (ShapeFillType)
            {
                case FillType.solid:
                    Color clr = ShapeColor;
                    fillBrush = new SolidBrush(clr);
                    break;
                case FillType.linearGradient:
                    fillBrush = SetLinearBrushProperties(rct, gp);
                    break;
                case FillType.pathGradient:
                    fillBrush = SetPathBrushProperties(rct, gp);
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
        private Brush SetLinearBrushProperties(Rectangle rct, GraphicsPath gp)
        {
            //Create Brush and ?declare? that I'm using it as a LinearG..Brush
            LinearGradientBrush lgb = new LinearGradientBrush(
                rct,
                ShapeColor,
                Color2,
                Angle,
                false);

            if (UseSigmaBell)
                lgb.SetSigmaBellShape(LinearFocus, LinearScale);

            if (UseBlendTriangle)
                lgb.SetBlendTriangularShape(LinearFocus, LinearScale);

            if (UseGammaCorrection)
                lgb.GammaCorrection = true;

            if (UseBlend)
            {
                blnd.Positions = Positions.ToArray();
                blnd.Factors = Factors.ToArray();
                lgb.Blend = blnd;
            }

            if (UseColorBlend)
            {
                cb.Colors = SurroundingColors.ToArray();
                cb.Positions = Positions.ToArray();
                lgb.InterpolationColors = cb;
            }
            return lgb;
        }

        /// <summary>
        /// This method is used to Set the PathGradientBrush properties prior to drawing
        /// </summary>
        /// <param name="rct">The client area</param>
        private Brush SetPathBrushProperties(Rectangle rct, GraphicsPath gp)
        {
            PathGradientBrush pgb = new PathGradientBrush(gp);

            pgb.CenterPoint = CenterPoint;
            pgb.CenterColor = ShapeColor;
            pgb.FocusScales = FocusScale; //TODO Does this work woth ColorBlend?? According to lit. YES

            if (UseSigmaBell)
                pgb.SetSigmaBellShape(LinearFocus, LinearScale);

            if (UseBlendTriangle)
                pgb.SetBlendTriangularShape(LinearFocus, LinearScale);

            if (UseBlend)
            {
                blnd.Positions = Positions.ToArray();
                blnd.Factors = Factors.ToArray();
                pgb.Blend = blnd;
            }

            if (UseColorBlend)
            {
                //When shown together as below it creates much nicer/cleaner images!
                //NOTE SurroundingColors can not exceed number of points in the Path
                if (SurroundingColors.Count <= gp.PathData.Points.Length)
                    pgb.SurroundColors = SurroundingColors.ToArray();

                cb.Colors = SurroundingColors.ToArray();
                cb.Positions = Positions.ToArray();
                pgb.InterpolationColors = cb;
            }
            else  
                //TODO Is this a potential problem. 
                //  If not using ColorBlend use Color2 as the outer color
                pgb.SurroundColors = new Color[] { Color2 };
            
            return pgb;
        }

        #endregion

        #region DrawThumbnail method

        /// <summary>
        /// Draw a thumbnail using the current brush
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rct">The location and size of the thumbnail</param>
        public void DrawThumbnail(Graphics g)
        {
            GraphicsPath gp = null;
            ShapeBase sb = new ShapeBase();

            sb.CurrentBrush = this;
            gp = ShapeBase.CreateRoundedRectangle(sb.CurrentBrush.referenceRct, 10);

            Brush br = SetBrush(sb.CurrentBrush.referenceRct, gp);
            g.FillPath(br, gp);
            g.DrawPath(new Pen(Color.Black), gp);

            gp.Dispose();
            br.Dispose();
        }

        #endregion

        #region GDI <-> XML conversion methods

        /// <summary>
        /// GDI -> XML conversion
        /// </summary>
        /// <param name="dom"></param>
        /// <param name="node"></param>
        public void ObjectToXml(ref XmlDataDocument dom, XmlElement node)
        {
            XmlElement elem;

            elem = dom.CreateElement("BrushName");
            elem.InnerText = BrushName;
            node.AppendChild(elem);

            elem = dom.CreateElement("FillType");
            elem.InnerText = ShapeFillType.ToString();
            node.AppendChild(elem);

            elem = dom.CreateElement("Color");
            elem.InnerText = ShapeColor.ToString();
            node.AppendChild(elem);

            elem = dom.CreateElement("Color1");
            elem.InnerText = Color1.ToString();
            node.AppendChild(elem);

            elem = dom.CreateElement("Color2");
            elem.InnerText = Color2.ToString();
            node.AppendChild(elem);

            elem = dom.CreateElement("LinearAngle");
            elem.InnerText = Angle.ToString();
            node.AppendChild(elem);

            elem = dom.CreateElement("CenterPoint");
            elem.InnerText = centerPoint.ToString();
            node.AppendChild(elem);

            elem = dom.CreateElement("UseSigmaShape");
            elem.InnerText = UseSigmaBell.ToString();
            node.AppendChild(elem);

            elem = dom.CreateElement("UseBlendShape");
            elem.InnerText = UseBlendTriangle.ToString();
            node.AppendChild(elem);

            elem = dom.CreateElement("SurroundColors");
            string str = string.Empty;
            foreach (Color c in SurroundingColors)
                str += ShapeBase.ConvertColorToString(c);
            elem.InnerText = str;
            node.AppendChild(elem);

            //TODO Do I need to convert to 0 - 1
            elem = dom.CreateElement("Focus");
            elem.InnerText = LinearFocus.ToString();
            node.AppendChild(elem);

            //TODO Do I need to convert to 0 - 1
            elem = dom.CreateElement("Scale");
            elem.InnerText = LinearScale.ToString();
            node.AppendChild(elem);

            elem = dom.CreateElement("Positions");
            str = string.Empty;
            foreach (float flt in Positions)
                str += flt.ToString() + ",";
            str = str.Substring(0, str.Length - 1);
            elem.InnerText = str;
            node.AppendChild(elem);

            elem = dom.CreateElement("Factors");
            str = string.Empty;
            foreach (float flt in Factors)
                str += flt.ToString() + ",";
            str = str.Substring(0, str.Length - 1);
            elem.InnerText = str;
            node.AppendChild(elem);

            elem = dom.CreateElement("UseBlend");
            elem.InnerText = UseBlend.ToString();
            node.AppendChild(elem);

            elem = dom.CreateElement("UseColorBlend");
            elem.InnerText = UseColorBlend.ToString();
            node.AppendChild(elem);

            elem = dom.CreateElement("UseGammaCorrection");
            elem.InnerText = UseGammaCorrection.ToString();
            node.AppendChild(elem);

            elem = dom.CreateElement("FocusScale");
            elem.InnerText = FocusScale.ToString();
            node.AppendChild(elem);
        }

        /// <summary>
        /// XML -> GDI
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="node"></param>
        public void ConvertXmlToGDI(ref ShapeBase sb, XmlNode node)
        {
            string str = node["FillType"].InnerText;
            FillType ft = (FillType)Enum.Parse(typeof(FillType), str);
            sb.CurrentBrush = new ShapeBrush();
            sb.CurrentBrush.ShapeFillType = ft;

            if (node["BrushName"] != null)
                sb.CurrentBrush.BrushName = node["BrushName"].InnerText;

            sb.CurrentBrush.Angle = float.Parse(node["LinearAngle"].InnerText);

            sb.CurrentBrush.LinearFocus = float.Parse(node["Focus"].InnerText);
            sb.CurrentBrush.LinearScale = float.Parse(node["Scale"].InnerText);

            sb.CurrentBrush.UseBlendTriangle = bool.Parse(node["UseBlendShape"].InnerText);
            sb.CurrentBrush.UseSigmaBell = bool.Parse(node["UseSigmaShape"].InnerText);
            sb.CurrentBrush.UseGammaCorrection = bool.Parse(node["UseGammaCorrection"].InnerText);
            sb.CurrentBrush.UseBlend = bool.Parse(node["UseBlend"].InnerText);
            sb.CurrentBrush.UseColorBlend = bool.Parse(node["UseColorBlend"].InnerText);
            
            str = node["CenterPoint"].InnerText;
            sb.CurrentBrush.CenterPoint = ConvertStringToPointF(str);
            sb.CurrentBrush.ReferenceRectangle = sb.ShapeRectangle;

            str = node["FocusScale"].InnerText;
            sb.CurrentBrush.FocusScale = ConvertStringToPointF(str);
            //Put these checks in here because some of my older stuff has this prop as a Point and
            //  the value is between 0 - 100!
            if (sb.CurrentBrush.focusScale.X > 1)
                sb.CurrentBrush.focusScale.X /= 100f;
            if (sb.CurrentBrush.focusScale.Y > 1)
                sb.CurrentBrush.focusScale.Y /= 100f;

            str = node["Color"].InnerText;
            sb.CurrentBrush.ShapeColor = ShapeBase.ConvertStringToColor(str);

            str = node["Color1"].InnerText;
            sb.CurrentBrush.Color1 = ShapeBase.ConvertStringToColor(str);

            str = node["Color2"].InnerText;
            sb.CurrentBrush.Color2 = ShapeBase.ConvertStringToColor(str);

            str = node["SurroundColors"].InnerText;
            sb.CurrentBrush.SurroundingColors = ConvertStringToColorCollection(str);

            str = node["Factors"].InnerText;
            sb.CurrentBrush.Factors = ConvertStringToFloatArray(str);

            str = node["Positions"].InnerText;
            sb.CurrentBrush.Positions = ConvertStringToFloatArray(str);
        }

        private Point ConvertStringToPoint(string s)
        {
            Regex r = new Regex(@"\d+");
            MatchCollection mc = r.Matches(s);

            return new Point(int.Parse(mc[0].ToString()), int.Parse(mc[1].ToString())); ;
        }

        private PointF ConvertStringToPointF(string s)
        {
            Regex r = new Regex(@"\d*\.\d*|\d+");
            MatchCollection mc = r.Matches(s);

            return new PointF(float.Parse(mc[0].ToString()), float.Parse(mc[1].ToString())); ;
        }

        private List<Color> ConvertStringToColorCollection(string s)
        {
            List<Color> lc = new List<Color>();

            Regex r = new Regex(@"\[(?>[^\[\]]+|\[(?<number>)|\](?<-number>))*(?(number)(/w+))\]");
            MatchCollection mc = r.Matches(s);

            foreach (Match m in mc)
                lc.Add(ShapeBase.ConvertStringToColor(m.ToString()));

            return lc;
        }

        private List<float> ConvertStringToFloatArray(string s)
        {
            List<float> lf = new List<float>();

            string[] strs = s.Split(',');
            float[] fa = new float[strs.Length];

            foreach (string str in strs)
                lf.Add(float.Parse(str));

            return lf;
        }

        #endregion
    }
}
