using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

//===================================================================================================
//
//	Module Name: SliderControl
//
//	Author: Mike Hankey
//
//	Create Date: 11/12/07
//
//	Copyright: WoodWare 2002-2007
//
//	Version History: 1.0
//
//	Notes:
//
//====================================================================================================

namespace CustomSliderControlLib
{
    [DefaultEvent("OnValueChange")]
    public partial class SliderControl : UserControl
    {
        public SliderControl()
        {
            InitializeComponent();
            ConfigureControl();
        }

        private enum ButtonStatus
        {
            idle,
            moving,
            selected
        };

        private string caption;
        [Category("WoodWare")]
        public string Caption
        {
            get { return caption; }
            set { caption = value; }
        }

        private float currentValue = 0;
        [Category("WoodWare")]
        public float CurrentValue
        {
            get { return currentValue; }
            set 
            {
                currentValue = value;
                currentPos = GivenValueCalcPosition(value);
                Invalidate();
            }
        }

        private float minValue = 0;
        [Category("WoodWare")]
        public float MinValue
        {
            get { return minValue; }
            set { minValue = value; }
        }

        private float maxValue = 255;
        [Category("WoodWare")]
        public float MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }

        private const float BAR_HT = 24.0f;
        private const float LABEL_WIDTH = 40.0f;
        private const float LABEL_HT = 18.0f;
        private const float GAP = 5.0f;
        private const float SELECTOR_WIDTH = 20.0f;

        private RectangleF barRct = Rectangle.Empty;
        private RectangleF labelRct = Rectangle.Empty;
        private RectangleF textRct = Rectangle.Empty;
        private RectangleF selectRct = Rectangle.Empty;
        private RectangleF selectorRct = Rectangle.Empty;
        private RectangleF moveRct = Rectangle.Empty;

        private float currentPos = 0;
        private int opacity = 200;
        private bool mouseDown = false;
        private float offset = 0;

        private ButtonStatus btnStatus = ButtonStatus.idle;

        private bool outputFloat = false;
        [Category("WoodWare")]
        public bool IsValueFloat
        {
            get { return outputFloat; }
            set { outputFloat = value; }
        }

        public delegate void OnValueChangeEventHandler(object sender, SliderEventArgs e);
        [Category("WoodWare")]
        public event OnValueChangeEventHandler OnValueChange;

        /// <summary>
        /// Paint the control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            DrawBar(e);
            DrawValueLabel(e);
            DrawValueString(e);
            DrawMoveArea(e);
            DrawCaption(e);
            DrawSelector(e);
        }

        private void ConfigureControl()
        {
            //Center the bar in the controls client area
            barRct = new RectangleF(
                0,
                (ClientRectangle.Height - BAR_HT) / 2,
                Width,
                BAR_HT);

            //Position the label 
            labelRct = new RectangleF(
                barRct.Width - LABEL_WIDTH - GAP,
                (ClientRectangle.Height - LABEL_HT) / 2,
                LABEL_WIDTH,
                LABEL_HT);

            //The total area that the selector travels
            selectRct = new RectangleF(
                GAP,
                barRct.Location.Y,
                labelRct.X - GAP,
                this.Height);

            //Duh
            textRct = new RectangleF(
                selectRct.X + GAP,
                barRct.Y + 2,
                selectRct.Width,
                LABEL_HT);

            //The area that the selector can actually move in
            moveRct = new RectangleF(
                selectRct.X,
                barRct.Location.Y,
                selectRct.Width - SELECTOR_WIDTH,
                selectRct.Height);

            //The moveable part of the control
            selectorRct = new RectangleF(
                moveRct.X,
                labelRct.Y,
                SELECTOR_WIDTH,
                labelRct.Height);
        }

        /// <summary>
        /// As the name implies Converts a value to a position
        ///     Both conversion methods calculate the percentage and scales  to the other.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private float GivenValueCalcPosition(float value)
        {
            float delta = maxValue - minValue;
            return (value * moveRct.Width) / delta;
        }

        /// <summary>
        /// Converts the Position to a value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private float GivenPositionCalcValue(float value)
        {
            float delta = maxValue - minValue;
            float vd = value * delta;
            return vd / moveRct.Width;
        }
        
        /// <summary>
        /// Draws the bar portion of the control
        /// </summary>
        /// <param name="e"></param>
        private void DrawBar(PaintEventArgs e)
        {
            GraphicsPath gp = CreateRoundedRectangle(barRct, 10);
            
            LinearGradientBrush lgb = new LinearGradientBrush(
                barRct, 
                Color.FromArgb(opacity, 91, 122, 202),
                Color.FromArgb(opacity, 255, 255, 255),
                90f);
            lgb.SetBlendTriangularShape(.35f, 1.0f);

            e.Graphics.FillPath(lgb, gp);

            //NOTE For some reason I have to do this to get border to draw?
            RectangleF tmp = barRct;
            Pen p = new Pen(Color.FromArgb(opacity, Color.Black));
            tmp.Inflate(-1, -1);
            gp.Reset();
            gp = CreateRoundedRectangle(tmp, 10);
            e.Graphics.DrawPath(p, gp);

            p.Dispose();
            gp.Dispose();
            lgb.Dispose();
        }

        /// <summary>
        /// Draw the ellipse where the value is displayed
        /// </summary>
        /// <param name="e"></param>
        private void DrawValueLabel(PaintEventArgs e)
        {
            GraphicsPath gp = CreateRoundedRectangle(labelRct, 10);
            SolidBrush br = new SolidBrush(Color.FromArgb(180, 255, 255, 255));
            Pen p = new Pen(Color.FromArgb(opacity, Color.Black));

            e.Graphics.FillPath(br, gp);
            e.Graphics.DrawPath(p, gp);

            p.Dispose();
            gp.Dispose();
            br.Dispose();
        }

        /// <summary>
        /// Draws the value in the value area
        /// </summary>
        /// <param name="e"></param>
        private void DrawValueString(PaintEventArgs e)
        {
            Font fnt = new Font("Palatino Linotype", 10);
            RectangleF rct = labelRct;
            rct.Offset(6, -1);

            string str = string.Empty;
            if (IsValueFloat)
                str = ((float)currentValue).ToString();
            else
                str = ((int)CurrentValue).ToString();

            e.Graphics.DrawString(str, fnt, new SolidBrush(Color.FromArgb(opacity, Color.Black)), rct);

            fnt.Dispose();
        }

        /// <summary>
        /// Draws the moveable area (long RoundedRectangle) of the control where the selector travels
        /// </summary>
        /// <param name="e"></param>
        private void DrawMoveArea(PaintEventArgs e)
        {
            RectangleF rct = new RectangleF(GAP, labelRct.Y, moveRct.Width + SELECTOR_WIDTH, labelRct.Height);
            GraphicsPath gp = CreateRoundedRectangle(rct, 10);
            SolidBrush br = new SolidBrush(Color.FromArgb(opacity, Color.White));
            Pen p = new Pen(Color.FromArgb(opacity, Color.Black));

            e.Graphics.FillPath(br, gp);
            e.Graphics.DrawPath(p, gp);
            p.Dispose();

            br.Dispose();
            gp.Dispose();
        }

        /// <summary>
        /// Draw the selector at the current position and color based on activity
        /// </summary>
        /// <param name="e"></param>
        private void DrawSelector(PaintEventArgs e)
        {
            selectorRct.X = GivenValueCalcPosition(CurrentValue) + moveRct.X;

            GraphicsPath gp = CreateRoundedRectangle(selectorRct, 10);

            Pen p = new Pen(Color.FromArgb(opacity, Color.Black));

            e.Graphics.DrawPath(p, gp);

            PathGradientBrush pgb = new PathGradientBrush(gp);

            Color[] surroundColor = new Color[] {
                Color.FromArgb(opacity, 227, 243, 255),
                Color.FromArgb(opacity, 189, 205, 233),
                Color.FromArgb(opacity, 103, 150, 161),
                Color.FromArgb(opacity, 76, 118, 146)
            };
            //If cursor is within selector boundry accent/highlight it
            switch (btnStatus)
            {
                case ButtonStatus.moving:
                    surroundColor[1] = Color.FromArgb(140, Color.Green);
                    break;
                case ButtonStatus.selected:
                    surroundColor[1] = Color.FromArgb(100, 255, 100, 0);
                    break;
            }
               
            float[] positions = new float[] { 0f, .82f, .85f, 1.0f };
            
            ColorBlend blnd = new ColorBlend();
            blnd.Positions = positions;
            blnd.Colors = surroundColor;
            pgb.InterpolationColors = blnd;
            pgb.SurroundColors = surroundColor;

            e.Graphics.FillPath(pgb, gp);

            p.Dispose();
            gp.Dispose();
            pgb.Dispose();
        }

        /// <summary>
        /// Draw the caption in the moveable area
        /// </summary>
        /// <param name="e"></param>
        private void DrawCaption(PaintEventArgs e)
        {
            Font fnt = new Font("Palatino Linotype", 10);
            SolidBrush br = new SolidBrush(Color.FromArgb(opacity, Color.DimGray));
            RectangleF rct = moveRct;
            
            rct.Offset(3, 2);
            e.Graphics.DrawString(caption, fnt, br, rct);

            br.Dispose();
            fnt.Dispose();
        }

        /// <summary>
        /// Creates and returns a GraphicsPath containing a Rounded Rectangle (duh?)
        /// </summary>
        /// <param name="rct"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static GraphicsPath CreateRoundedRectangle(RectangleF rct, int radius)
        {
            GraphicsPath gp = new GraphicsPath();
            PointF pt1 = new PointF(rct.X + radius, rct.Y);
            PointF pt2 = new PointF(rct.X + rct.Width - radius, rct.Y);
            RectangleF rct1 = new RectangleF(rct.X, rct.Y, radius, radius);

            gp.AddArc(rct1, 180f, 90f);
            gp.AddLine(pt1, pt2);

            rct1.Location = pt2;
            gp.AddArc(rct1, 270f, 90f);

            pt1 = new PointF(rct.X + rct.Width, rct.Y + radius);
            pt2 = new PointF(rct.X + rct.Width, rct.Y + rct.Height - radius);
            gp.AddLine(pt1, pt2);

            rct1.Location = new PointF(rct.X + rct.Width - radius, rct.Y + rct.Height - radius);
            gp.AddArc(rct1, 0f, 90f);

            pt1 = new PointF(rct.X + rct.Width - radius, rct.Y + rct.Height);
            pt2 = new PointF(rct.X + radius, rct.Y + rct.Height);
            gp.AddLine(pt1, pt2);

            rct1.Location = new PointF(rct.X, rct.Y + rct.Height - radius);
            gp.AddArc(rct1, 90f, 90f);

            gp.CloseFigure();

            return gp;
        }

        /// <summary>
        /// Handles the mouse down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Enabled)
                return;

            //If we have clicked on the selector we;
            //  1) Get the mouses offset from the top, left of the control so it doesn't jump when we go to move
            //  2) Set the ButtonStatus to selected
            //  3) set flag indicating that we have selected the selector
            //  4) Invalidate so we can draw changes
            if (selectorRct.Contains(e.Location))
            {
                offset = e.X - selectorRct.X;
                btnStatus = ButtonStatus.selected;
                mouseDown = true;
                Invalidate();
            }
            else
                HandleClick(e.Location);
        }

        /// <summary>
        /// This method is used to handle when we have clicked on the control but not in the selector
        ///     i.e. we have chosen to move the selector incrementally.
        /// </summary>
        /// <param name="pt"></param>
        private void HandleClick(Point pt)
        {
            if (barRct.Contains(pt) && !selectorRct.Contains(pt))
            {
                if (pt.X < selectorRct.X)
                {
                    if (--currentValue < minValue)
                        CurrentValue = minValue; ;
                }
                else
                {
                    if (++currentValue > maxValue)
                        CurrentValue = maxValue;
                }

                currentPos = GivenValueCalcPosition(currentValue);
                
                if (OnValueChange != null)
                    OnValueChange(this, new SliderEventArgs(CurrentValue));
                
                Invalidate();
            }
        }

        /// <summary>
        /// Handle mouse up, basically just reset flags and indicators
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderControl_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            btnStatus = ButtonStatus.idle;
            Invalidate();
        }

        /// <summary>
        /// Handle mouse move events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderControl_MouseMove(object sender, MouseEventArgs e)
        {
            //Accents the button
            if (selectorRct.Contains(e.Location))
            {
                if (mouseDown)
                    btnStatus = ButtonStatus.moving;
                else
                    btnStatus = ButtonStatus.selected;

                Invalidate();

                if (!mouseDown)
                    return;

                float pt = e.X - offset - moveRct.X;

                //Make sure the value stays in bounds, and thus the selector in the move area.
                CheckBounds(pt);

                if (OnValueChange != null)
                    OnValueChange(this, new SliderEventArgs(CurrentValue));

            }
            else
                btnStatus = ButtonStatus.idle;

            Invalidate();
        }

        /// <summary>
        /// This method makes sure that we don't go under or over the min/max values.
        /// </summary>
        /// <param name="pt"></param>
        private void CheckBounds(float pt)
        {
            if (pt < moveRct.X)
            {
                currentValue = minValue;
                currentPos = 0;
            }
            else
            {
                if (pt > (moveRct.Width - moveRct.X))
                {
                    currentValue = maxValue;
                    currentPos = GivenValueCalcPosition(CurrentValue);
                }
                else
                {
                    currentPos = pt;
                    currentValue = GivenPositionCalcValue(pt);
                }
            }   
        }

        /// <summary>
        /// When the size changes we reconfigure the control and recalculate the new position.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderControl_SizeChanged(object sender, EventArgs e)
        {
            ConfigureControl();
            currentPos = GivenValueCalcPosition(currentValue);
        }

        /// <summary>
        /// When disabled just dim the control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderControl_EnabledChanged(object sender, EventArgs e)
        {
            if (Enabled)
                opacity = 200;
            else
                opacity = 80;

            Invalidate();
        }

        private void SliderControl_MouseLeave(object sender, EventArgs e)
        {
            btnStatus = ButtonStatus.idle;
            Invalidate();
        }
    }

    /// <summary>
    /// SlideEventArgs - Just pass the new value to subscriber
    /// </summary>
    public class SliderEventArgs : EventArgs
    {
        public float value;

        public SliderEventArgs(float val)
        {
            value = val;
        }
    }
}