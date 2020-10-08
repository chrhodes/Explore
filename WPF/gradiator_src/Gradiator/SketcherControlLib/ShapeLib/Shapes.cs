using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Data;
using System;
using System.Drawing.Text;
using System.Xml;
using System.Text.RegularExpressions;

//===================================================================================================
//
//	Module Name: Shapes
//
//	Author: Mike Hankey
//
//	Create Date: 10/26/07
//
//	Copyright: WoodWare 2002-2007
//
//	Version History: 1.0
//
//	Notes: Been working on this for quite a while but this is the first attempt to actually make it
//          a true project.
//
//====================================================================================================

namespace SketcherControlLib.ShapeLib
{
	/// <summary>
	/// Summary description for Rectangular Shape.
	/// </summary>
    public class Rectangular : ShapeBase
    {
        public Rectangular(Rectangle r)
        {
            shape._id = rnd.Next();
            shape._rectangle = r;
            shape._type = Shapes.Rectangular;

            CalculateArea();
        }

        public Rectangular(XmlNode drv)
        {
            shape._id = int.Parse(drv["Id"].InnerText);
            shape._type = Shapes.Rectangular;

            //Convert from x,y,w,h Xml format to Rectangle
            string str = drv["Rectangle"].InnerText;
            shape._rectangle = ConvertStringToRectangle(str);

            //Convert from Color [A=nnn,R=nnn,G=nnn,B=nnn] to Color
            str = drv["Color"].InnerText;
            BorderColor = ShapeBase.ConvertStringToColor(str);

            CalculateArea();
        }

        public override void Draw(PaintEventArgs pe)
        {
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            gp.Reset();

            gp.AddRectangle(shape._rectangle);

            Brush br = SetBrush(shape._rectangle);

            if (border)
                pe.Graphics.DrawPath(penBorder, gp);

            if (br != null)
            {
                pe.Graphics.FillPath(br, gp);
                br.Dispose();
            }

            if (this.ShapeSelected)
                DrawControl(pe, true);
        }
    }

    /// <summary>
    /// Summary description for RoundedRectangle Shape.
    /// </summary>
    public class RoundedRectangle : ShapeBase
    {
        public RoundedRectangle(Rectangle r)
        {
            shape._id = rnd.Next();
            shape._rectangle = r;
            shape._type = Shapes.RoundedRectangle;

            CalculateArea();
        }

        public RoundedRectangle(XmlNode drv)
        {
            shape._id = int.Parse(drv["Id"].InnerText);
            shape._type = Shapes.RoundedRectangle;

            //Convert from x,y,w,h Xml format to Rectangle
            string str = drv["Rectangle"].InnerText;
            shape._rectangle = ConvertStringToRectangle(str);

            //Convert from Color [A=nnn,R=nnn,G=nnn,B=nnn] to Color
            str = drv["Color"].InnerText;
            BorderColor = ShapeBase.ConvertStringToColor(str);

            CalculateArea();
        }

        public override void Draw(PaintEventArgs pe)
        {
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            gp.Reset();

            gp = CreateRoundedRectangle(shape._rectangle, 16);

            Brush br = SetBrush(shape._rectangle);
            
            if (border)
                pe.Graphics.DrawPath(penBorder, gp);

            if (br != null)
            {
                pe.Graphics.FillPath(br, gp);
                br.Dispose();
            }

            if (this.ShapeSelected)
                this.DrawControl(pe, true);
        }
    }

	/// <summary>
	/// Elliptical Shape class
	/// </summary>
	public class Elliptical : ShapeBase
	{
		public Elliptical(Rectangle r)
		{
            shape._id = rnd.Next();
            shape._rectangle = r;
            shape._type = Shapes.Elliptical;

            CalculateArea();
		}

		public Elliptical(XmlNode drv)
		{
            shape._id = int.Parse(drv["Id"].InnerText);
            shape._type = Shapes.Elliptical;

            //Convert from x,y,w,h Xml format to Rectangle
            string str = drv["Rectangle"].InnerText;
            shape._rectangle = ConvertStringToRectangle(str);

            //Convert from Color [A=nnn,R=nnn,G=nnn,B=nnn] to Color
            str = drv["Color"].InnerText;
            BorderColor = ShapeBase.ConvertStringToColor(str);

            CalculateArea();
		}

		public override void Draw(PaintEventArgs pe)
		{
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pe.Graphics.PixelOffsetMode = PixelOffsetMode.Half;

            gp.Reset();

            gp.AddEllipse(shape._rectangle);

            Brush br = SetBrush(shape._rectangle);

            if (border)
                pe.Graphics.DrawPath(penBorder, gp);

            if (br != null)
            {
                pe.Graphics.FillPath(br, gp);
                br.Dispose();
            }
         
            if (ShapeSelected)
                DrawControl(pe, true);
        }
	}

	/// <summary>
	/// Summary description for Rectangular Shape.
	/// </summary>
    public class Text : ShapeBase
    {
        public Text(Rectangle r)
        {
            shape._id = rnd.Next();
            shape._rectangle = r;
            shape._type = Shapes.Text;

            CalculateArea();
        }

        public Text(XmlNode drv)
        {
            shape._id = int.Parse(drv["Id"].InnerText);
            shape._type = Shapes.Text;

            //Convert from x,y,w,h Xml format to Rectangle
            string str = drv["Rectangle"].InnerText;
            shape._rectangle = ConvertStringToRectangle(str);

            //Convert from Color [A=nnn,R=nnn,G=nnn,B=nnn] to Color
            str = drv["Color"].InnerText;
            BorderColor = ShapeBase.ConvertStringToColor(str);

            //Convert from Color [A=nnn,R=nnn,G=nnn,B=nnn] to Color
            str = drv["Color2"].InnerText;
            TextColor = ShapeBase.ConvertStringToColor(str);

            FontFamily = drv["FontFamily"].InnerText;
            Text = drv["Text"].InnerText;
            FontSize = float.Parse(drv["FontSize"].InnerText);

            CalculateArea();
        }

        public override void Draw(PaintEventArgs pe)
        {
            Brush br = new SolidBrush(TextColor);
            Font fnt = new Font(FontFamily, FontSize);

            gp.Reset();

            gp.AddRectangle(shape._rectangle);

            if (border)
                pe.Graphics.DrawPath(penBorder, gp);

            pe.Graphics.DrawString(Text, fnt, br, Rectangle.Ceiling(gp.GetBounds()));

            if (ShapeSelected)
                DrawControl(pe, true);

            br.Dispose();
            fnt.Dispose();
        }
    }

    /// <summary>
    /// Summary description for Image Shape.
    /// </summary>
    public class ShapeImage : ShapeBase
    {
        public ShapeImage(Rectangle r)
        {
            shape._id = rnd.Next();
            shape._rectangle = r;
            shape._type = Shapes.Image;

            CalculateArea();
        }

        public ShapeImage(XmlNode drv)
        {
            shape._id = int.Parse(drv["Id"].InnerText);
            shape._type = Shapes.Image;

            //Convert from x,y,w,h Xml format to Rectangle
            string str = drv["Rectangle"].InnerText;
            shape._rectangle = ConvertStringToRectangle(str);

            //Convert from Color [A=nnn,R=nnn,G=nnn,B=nnn] to Color
            str = drv["Color"].InnerText;
            BorderColor = ShapeBase.ConvertStringToColor(str);

            Text = drv["Text"].InnerText;

            CalculateArea();
        }

        /// <summary>
        /// When drawing apply a ColorMatrix to the Image.  The Color White
        ///     indicates no transformation is applied.
        /// </summary>
        /// <param name="pe"></param>
        public override void Draw(PaintEventArgs pe)
        {
            ColorMatrix cm = new ColorMatrix();

            //Get the ARGB values
            float f = float.Parse(Color1.A.ToString());
            cm.Matrix33 = f / 255.0f;

            f = float.Parse(Color1.R.ToString());
            cm.Matrix00 = f / 255.0f;

            f = float.Parse(Color1.G.ToString());
            cm.Matrix11 = f / 255.0f;

            f = float.Parse(Color1.B.ToString());
            cm.Matrix22 = f / 255.0f;

            ImageAttributes imgAttributes = new ImageAttributes();
            imgAttributes.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            gp.Reset();

            gp.AddRectangle(shape._rectangle);

            if (ShapeImage != null)
                pe.Graphics.DrawImage(ShapeImage, Rectangle.Ceiling(gp.GetBounds()), 0, 0, ShapeImage.Width, ShapeImage.Height, GraphicsUnit.Pixel, imgAttributes);

            if (border)
                pe.Graphics.DrawPath(penBorder, gp);

            if (ShapeSelected)
                DrawControl(pe, true);

            pe.Graphics.ResetTransform();
        }
    }
}
