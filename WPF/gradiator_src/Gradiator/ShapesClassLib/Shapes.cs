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

namespace ShapesClassLib
{
	/// <summary>
	/// Summary description for Rectangular Shape.
	/// </summary>
    public class Rectangular : ShapeBase
    {
        public Rectangular(Rectangle r)
        {
            _id = rnd.Next();
            _rectangle = r;
            _type = Shapes.Rectangular;

            CalculateArea();
        }

        public Rectangular(XmlNode drv)
        {
            _id = int.Parse(drv["Id"].InnerText);
            _type = Shapes.Rectangular;

            //Convert from x,y,w,h Xml format to Rectangle
            string str = drv["Rectangle"].InnerText;
            _rectangle = ConvertStringToRectangle(str);

            //Convert from Color [A=nnn,R=nnn,G=nnn,B=nnn] to Color
            str = drv["Color"].InnerText;
            currentBrush.ShapeColor = ShapeBase.ConvertStringToColor(str);

            str = drv["Color1"].InnerText;
            currentBrush.Color1 = ShapeBase.ConvertStringToColor(str);

            str = drv["Color2"].InnerText;
            currentBrush.Color2 = ShapeBase.ConvertStringToColor(str);

            CalculateArea();
        }

        public override void Draw(PaintEventArgs pe, float scale)
        {
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            GraphicsPath gp = new GraphicsPath();
            Matrix m = new Matrix();
            m.Scale(scale, scale);
            pe.Graphics.Transform = m;

            gp.AddRectangle(_rectangle);

            if (border)
            {
                penBorder.Color = currentBrush.Color1;
                pe.Graphics.DrawPath(penBorder, gp);
            }

            Brush br = currentBrush.SetBrush(_rectangle, gp);
            if (br != null)
            {
                pe.Graphics.FillPath(br, gp);
                br.Dispose();
            }

            if (this.ShapeSelected && scale == 1f)
                shapeControlObject.DrawControl(pe, gp);

            m.Dispose();
            gp.Dispose();
        }
    }

    /// <summary>
    /// Summary description for RoundedRectangle Shape.
    /// </summary>
    public class RoundedRectangle : ShapeBase
    {
        public RoundedRectangle(Rectangle r)
        {
            _id = rnd.Next();
            _rectangle = r;
            _type = Shapes.RoundedRectangle;

            CalculateArea();
        }

        public RoundedRectangle(XmlNode drv)
        {
            _id = int.Parse(drv["Id"].InnerText);
            _type = Shapes.RoundedRectangle;

            //Convert from x,y,w,h Xml format to Rectangle
            string str = drv["Rectangle"].InnerText;
            _rectangle = ConvertStringToRectangle(str);

            //Convert from Color [A=nnn,R=nnn,G=nnn,B=nnn] to Color
            str = drv["Color"].InnerText;
            currentBrush.ShapeColor = ShapeBase.ConvertStringToColor(str);

            str = drv["Color1"].InnerText;
            currentBrush.Color1 = ShapeBase.ConvertStringToColor(str);

            str = drv["Color2"].InnerText;
            currentBrush.Color2 = ShapeBase.ConvertStringToColor(str);

            CalculateArea();
        }

        public override void Draw(PaintEventArgs pe, float scale)
        {
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            GraphicsPath gp = CreateRoundedRectangle(_rectangle, 16);
            Matrix m = new Matrix();
            m.Scale(scale, scale);
            pe.Graphics.Transform = m;

            if (border)
            {
                penBorder.Color = currentBrush.Color1;
                pe.Graphics.DrawPath(penBorder, gp);
            }

            Brush br = currentBrush.SetBrush(_rectangle, gp);
            if (br != null)
            {
                pe.Graphics.FillPath(br, gp);
                br.Dispose();
            }

            if (this.ShapeSelected && scale == 1f)
                shapeControlObject.DrawControl(pe, gp);

            m.Dispose();
            gp.Dispose();
        }
    }

	/// <summary>
	/// Elliptical Shape class
	/// </summary>
	public class Elliptical : ShapeBase
	{
		public Elliptical(Rectangle r)
		{
            _id = rnd.Next();
            _rectangle = r;
            _type = Shapes.Elliptical;

            CalculateArea();
		}

		public Elliptical(XmlNode drv)
		{
            _id = int.Parse(drv["Id"].InnerText);
            _type = Shapes.Elliptical;

            //Convert from x,y,w,h Xml format to Rectangle
            string str = drv["Rectangle"].InnerText;
            _rectangle = ConvertStringToRectangle(str);

            //Convert from Color [A=nnn,R=nnn,G=nnn,B=nnn] to Color
            str = drv["Color"].InnerText;
            currentBrush.ShapeColor = ShapeBase.ConvertStringToColor(str);

            str = drv["Color1"].InnerText;
            currentBrush.Color1 = ShapeBase.ConvertStringToColor(str);

            str = drv["Color2"].InnerText;
            currentBrush.Color2 = ShapeBase.ConvertStringToColor(str);

            CalculateArea();
		}

		public override void Draw(PaintEventArgs pe, float scale)
		{
            if (this.currentBrush == null)
                return;

            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pe.Graphics.PixelOffsetMode = PixelOffsetMode.Half;

            GraphicsPath gp = new GraphicsPath();
            Matrix m = new Matrix();
            m.Scale(scale, scale);
            pe.Graphics.Transform = m;

            gp.AddEllipse(_rectangle);

            if (border)
            {
                penBorder.Color = currentBrush.Color1;
                pe.Graphics.DrawPath(penBorder, gp);
            }

            Brush br = currentBrush.SetBrush(_rectangle, gp);
            if (br != null)
            {
                pe.Graphics.FillPath(br, gp);
                br.Dispose();
            }
         
            if (ShapeSelected && scale == 1f)
                shapeControlObject.DrawControl(pe, gp);

            m.Dispose();
            gp.Dispose();
        }
	}

	/// <summary>
	/// Summary description for Rectangular Shape.
	/// </summary>
    public class Text : ShapeBase
    {
        public Text(Rectangle r)
        {
            _id = rnd.Next();
            _rectangle = r;
            _type = Shapes.Text;
            currentBrush.ShapeColor = Color.Black;
            currentBrush.Color1 = Color.Black;

            //Have to do this because the default is true
            HasBorder = false;

            CalculateArea();
        }

        public Text(XmlNode drv)
        {
            _id = int.Parse(drv["Id"].InnerText);
            _type = Shapes.Text;

            //Convert from x,y,w,h Xml format to Rectangle
            string str = drv["Rectangle"].InnerText;
            _rectangle = ConvertStringToRectangle(str);

            //Convert from Color [A=nnn,R=nnn,G=nnn,B=nnn] to Color
            str = drv["Color1"].InnerText;
            currentBrush.Color1 = ShapeBase.ConvertStringToColor(str);

            //Convert from Color [A=nnn,R=nnn,G=nnn,B=nnn] to Color
            str = drv["Color"].InnerText;
            currentBrush.ShapeColor = ShapeBase.ConvertStringToColor(str);

            FontFamily = drv["FontFamily"].InnerText;
            Text = drv["Text"].InnerText;
            FontSize = float.Parse(drv["FontSize"].InnerText);

            CalculateArea();
        }

        public override void Draw(PaintEventArgs pe, float scale)
        {
            Font fnt = new Font(FontFamily, FontSize);

            GraphicsPath gp = new GraphicsPath();
            Brush br = new SolidBrush(currentBrush.ShapeColor);

            Matrix m = new Matrix();
            m.Scale(scale, scale);
            pe.Graphics.Transform = m;

            gp.AddRectangle(_rectangle);

            if (border)
            {
                penBorder.Color = currentBrush.Color1;
                pe.Graphics.DrawPath(penBorder, gp);
            }

            pe.Graphics.DrawString(_text, fnt, br, Rectangle.Ceiling(gp.GetBounds()));

            if (ShapeSelected && scale == 1f)
                shapeControlObject.DrawControl(pe, gp);

            m.Dispose();
            gp.Dispose();
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
            _id = rnd.Next();
            _rectangle = r;
            _type = Shapes.Image;

            CalculateArea();
        }

        public ShapeImage(XmlNode drv)
        {
            _id = int.Parse(drv["Id"].InnerText);
            _type = Shapes.Image;

            //Convert from x,y,w,h Xml format to Rectangle
            string str = drv["Rectangle"].InnerText;
            _rectangle = ConvertStringToRectangle(str);

            str = drv["Color"].InnerText;
            currentBrush.ShapeColor = ShapeBase.ConvertStringToColor(str);

            str = drv["Color1"].InnerText;
            currentBrush.Color1 = ShapeBase.ConvertStringToColor(str);

            Text = drv["Text"].InnerText;

            CalculateArea();
        }

        /// <summary>
        /// When drawing apply a ColorMatrix to the Image.  The Color White
        ///     indicates no transformation is applied.
        /// </summary>
        /// <param name="pe"></param>
        public override void Draw(PaintEventArgs pe, float scale)
        {
            ColorMatrix cm = new ColorMatrix();

            //Get the ARGB values
            float f = float.Parse(currentBrush.ShapeColor.A.ToString());
            cm.Matrix33 = f / 255.0f;

            f = float.Parse(currentBrush.ShapeColor.R.ToString());
            cm.Matrix00 = f / 255.0f;

            f = float.Parse(currentBrush.ShapeColor.G.ToString());
            cm.Matrix11 = f / 255.0f;

            f = float.Parse(currentBrush.ShapeColor.B.ToString());
            cm.Matrix22 = f / 255.0f;

            ImageAttributes imgAttributes = new ImageAttributes();
            imgAttributes.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            GraphicsPath gp = new GraphicsPath();

            gp.AddRectangle(_rectangle);
            Matrix m = new Matrix();
            m.Scale(scale, scale);
            pe.Graphics.Transform = m;

            if (ShapeImage != null)
                pe.Graphics.DrawImage(ShapeImage, Rectangle.Ceiling(gp.GetBounds()), 0, 0, ShapeImage.Width, ShapeImage.Height, GraphicsUnit.Pixel, imgAttributes);

            if (border)
            {
                penBorder.Color = currentBrush.Color1;
                pe.Graphics.DrawPath(penBorder, gp);
            }

            if (ShapeSelected && scale == 1f)
                shapeControlObject.DrawControl(pe, gp);

            pe.Graphics.ResetTransform();

            m.Dispose();
            gp.Dispose();
        }
    }
}
