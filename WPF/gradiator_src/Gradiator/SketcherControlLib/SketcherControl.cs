using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Diagnostics;
using ShapesClassLib;
using SketcherControlLib.MiscControls;

//===================================================================================
//
//  Created: ?? Been working on it for some time in the AreaEstimator application
//      but updated for article on 8/20/07 entitled "Gradients made easy".
//
//  Author: Mike Hankey
//
//  Notes and Comments:  This is a crude implementation of a drawing routine but
//      I wanted to learn about GDI+ and graphics in general so I took this and
//      other projects that I probably could have got off one of the sites like
//      CodeProject but instead did it myself.  I originally started it as part
//      of the AreaEstimator application but at the time did not really have the
//      knowledge to finish/refine it to make it a usable control so have been
//      working on it off and on for some time in hopes that it will develop into
//      something I can use in several applications I have in mind to do.
//  
//      9/18/07 Added rotate stuff to Elliptical shape.  Almost works except when
//          if go to select, it uses where the control region started. i.e. The
//          RotateControl is in far right and mid ht.!
//      10/21/07 Removed rotation func. to finish another day. Projects 1.0.12 contains
//          the code with the rotate stuff in there.  Gonna clean up, move and rename to 
//          make it an official project and call it Gradiator.
//      10/26/07 Cleaned up Shapes and ShapeBase, but in ShapeBase I need to move
//          Control stuff somewhere!
//      11/14/07 Wedsday  About midway through the conversion and all is going well except what
//          I want to do about the BlendEditor...I'd thought about bundling it with the
//          BrushEditor that I created to save brushes.  This is a workable version and
//          looks good so far but still have a few problems to iron out.  Also need to rewrite
//          the CP artcle associated with this app and I want to write a help file for using
//          Gradiator.  Will provide examples on how to create different effects and so forth.
//
//  Version 2.0.1 Migrated ShapeControl to its own class in preparation to do some major
//      over hauling.  Very workable version and am only continuing with it because I want
//      to use SketchControl in Keeper and maybe AreaEstimator???? if I ever get back on it!
//
//====================================================================================

namespace SketcherControlLib
{
    public partial class SketcherControl : UserControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SketcherControl()
        {
            InitializeComponent();
        }

        #region Properties and fields


        private ColorSelectorDialog selectorDialog = new ColorSelectorDialog();

        private Rectangle clientArea = new Rectangle();

        private List<ShapeBase> shapeList = new List<ShapeBase>();
        public List<ShapeBase> ShapeList
        {
            get { return shapeList; }
        }

        private List<ShapeBase> shapeSelectionList;
        public List<ShapeBase> ShapeSelectionList
        {
            get { return shapeSelectionList; }
            set { shapeSelectionList = value; }
        }

        private ShapeBase currentShape = null;
        public ShapeBase CurrentShape
        {
            get { return currentShape; }
        }

        #endregion

        #region Delegates and Events

        public delegate void OnShapeSelectionChangeEventhandler(object sender, SketcherEventArgs e);
        [Category("WoodWare")]
        [Description("Fired when an item is selected (On MouseUp)")]
        public event OnShapeSelectionChangeEventhandler OnShapeSelectionChange;

        public delegate void OnShapeSizeChangeEventHandler(object sender, SketcherEventArgs e);
        [Category("WoodWare")]
        [Description("Fired when an item is being resized (On MouseMove and resizing)")]
        public event OnShapeSizeChangeEventHandler OnShapeSizeChange;

        public delegate void OnWorkspaceStatusChangeEventHandler(object sender, SketcherEventArgs e);
        [Category("WoodWare")]
        [Description("Fired when an theres a change in the workspace status (such as Clearing)")]
        public event OnWorkspaceStatusChangeEventHandler OnWorkspaceStatusChange;

        #endregion

        #region Form level event handlers

        /// <summary>
        /// yer typical load handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SketcherControl_Load(object sender, EventArgs e)
        {
            //Got this from MSDN - a way to tell if in design mode since DesignMode is flackey!
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
                return;

            clientArea = this.DisplayRectangle;
            clientArea.Y += toolStrip1.Height;
            clientArea.Height -= statusStripControl1.Height;

            Point pt = PointToScreen(clientArea.Location);
            pt.Offset(clientArea.Width - selectorDialog.Width + 200, clientArea.Height - selectorDialog.Height);
            selectorDialog.Location = pt;
            selectorDialog.Show();

            blendManager1.ColorDialogReference = selectorDialog;

            tslCaption.Text = "default.xml";
        }

        /// <summary>
        /// size change handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SketcherControl_SizeChanged(object sender, EventArgs e)
        {
            if (this.Height < 400)
                Height = 400;

            tabControl1.Height = this.Height;

            clientArea = this.DisplayRectangle;
            clientArea.Y += toolStrip1.Height;
            clientArea.Height -= statusStripControl1.Height;
            clientArea.Width -= tabControl1.Width;

            toolStrip1.Width = this.Width - tabControl1.Width;
            tslCaption.Width = toolStrip1.Width - tslCaption.Bounds.X - 66;

            statusStripControl1.Location = new Point(0, this.Height - statusStripControl1.Height);
            statusStripControl1.Width = this.Width - tabControl1.Width;
        }

        /// <summary>
        /// Draw the ShapeList objects
        ///     z-order Text
        ///             CurrentShape
        ///             All other shapes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SketcherControl_Paint(object sender, PaintEventArgs e)
        {
            List<ShapeBase> tmpList = new List<ShapeBase>();

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (ShapeBase sb in shapeList)
            {
                if (sb.ShapeType == Shapes.Text)
                    tmpList.Add(sb);
                else
                    sb.Draw(e, 1f);
            }

            //The temporary list is used for items that are to be drawn after all others
            if (tmpList.Count > 0)
            {
                foreach (ShapeBase sb in tmpList)
                    sb.Draw(e, 1f);
            }
        }
        
        /// <summary>
        /// Used by main form to tell this control that its is being minimize and it needs to hide the
        ///     ColorSelector dialog.
        /// </summary>
        public void HideColorSelector()
        {
            selectorDialog.Hide();
        }
        
        #endregion

        #region BlendManager handlers

        //NOTE Instead of keeping currentBrush here and in BlendManager in sync BlendManager only relays
        //  messages indicating the action to be perform and passes the data collected from the manager
        //  that is needed to create, update, remove, move or clear the list of colors, positions and factors!

        /// <summary>
        /// BlendStyle has changed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void blendManager1_OnBlendEditorStyleChange(object sender, BlendEditorStyleChangeEventArgs e)
        {
            if (currentShape == null)
                return;

            switch (e.style)
            {
                case BlendStyle.disabled:
                    currentShape.CurrentBrush.UseBlend = false;
                    currentShape.CurrentBrush.UseColorBlend = false;
                    break;
                case BlendStyle.enabled:
                    currentShape.CurrentBrush.UseBlend = true;
                    currentShape.CurrentBrush.UseColorBlend = false;
                    break;
                case BlendStyle.regular:
                    currentShape.CurrentBrush.UseBlend = true;
                    currentShape.CurrentBrush.UseColorBlend = false;
                    break;
                case BlendStyle.color:
                    currentShape.CurrentBrush.UseBlend = false;
                    currentShape.CurrentBrush.UseColorBlend = true;
                    break;
            }
            blendManager1.CurrentShape = currentShape;
            Invalidate();
        }

        /// <summary>
        /// Performs the associated Blend action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void blendEd_OnBlendAction(object sender, SketcherControlLib.MiscControls.BlendActionEventArgs e)
        {
            if (currentShape == null)
                return;

            switch (e.act)
            {
                case BlendAction.add:
                    currentShape.CurrentBrush.SurroundingColors.Insert(e.index, e.item.color);
                    currentShape.CurrentBrush.Positions.Insert(e.index, e.item.position);
                    currentShape.CurrentBrush.Factors.Insert(e.index, e.item.factor);
                    break;
                case BlendAction.update:
                    currentShape.CurrentBrush.Factors[e.index] = e.item.factor;
                    currentShape.CurrentBrush.Positions[e.index] = e.item.position;
                    currentShape.CurrentBrush.SurroundingColors[e.index] = e.item.color;
                    break;
                case BlendAction.remove:
                    currentShape.CurrentBrush.SurroundingColors.RemoveAt(e.index);
                    currentShape.CurrentBrush.Factors.RemoveAt(e.index);
                    currentShape.CurrentBrush.Positions.RemoveAt(e.index);
                    break;
                case BlendAction.reset:
                    //Clear all the lists
                    currentShape.CurrentBrush.SurroundingColors.Clear();
                    currentShape.CurrentBrush.Positions.Clear();
                    currentShape.CurrentBrush.Factors.Clear();

                    //Now we need to add the two required list items
                    currentShape.CurrentBrush.SurroundingColors.Add(Color.White);
                    currentShape.CurrentBrush.SurroundingColors.Add(Color.Black);
                    currentShape.CurrentBrush.Positions.Add(0f);
                    currentShape.CurrentBrush.Positions.Add(1.0f);
                    currentShape.CurrentBrush.Factors.Add(.2f);
                    currentShape.CurrentBrush.Factors.Add(.8f);
                    break;
                case BlendAction.moveUp:
                    MoveBlendItem(e.act, e.index);
                    break;
                case BlendAction.moveDown:
                    MoveBlendItem(BlendAction.moveDown, e.index);;
                    break;
            }
            blendManager1.CurrentShape = currentShape;
            Invalidate();
        }

        /// <summary>
        /// Moves the Blend group up or down as allowed within the list.
        /// NOTE If we try to move to the first or last spot we change everything but the position
        /// </summary>
        /// <param name="ba"></param>
        /// <param name="index"></param>
        private void MoveBlendItem(BlendAction ba, int index)
        {
            if (ba == BlendAction.moveUp)
            {
                if (index == 0)
                    MessageBox.Show("No can do bucka-roo 0");
                else
                    if (index == 1)
                        SwapBlendItems(ba, index, false);
                    else
                        if (index == (currentShape.CurrentBrush.SurroundingColors.Count - 1))
                            SwapBlendItems(ba, index, false);
                        else
                            SwapBlendItems(ba, index, true);
            }
            else
            {
                if (index == 0)
                    SwapBlendItems(ba, index, false);
                else
                    if (index == (currentShape.CurrentBrush.SurroundingColors.Count - 2))
                        SwapBlendItems(ba, index, false);
                    else
                        if (index == (currentShape.CurrentBrush.SurroundingColors.Count - 1))
                            MessageBox.Show("Not today!");
                        else
                            SwapBlendItems(ba, index, true);
            }
        }

        /// <summary>
        /// Performs the actual swapping
        /// </summary>
        /// <param name="ba"></param>
        /// <param name="index"></param>
        /// <param name="includePos"></param>
        private void SwapBlendItems(BlendAction ba, int index, bool includePos)
        {
            Color tclr;
            float tval;

            if (ba == BlendAction.moveUp)
            {
                tclr = currentShape.CurrentBrush.SurroundingColors[index];
                currentShape.CurrentBrush.SurroundingColors.RemoveAt(index);
                currentShape.CurrentBrush.SurroundingColors.Insert(index - 1, tclr);

                tval = currentShape.CurrentBrush.Factors[index];
                currentShape.CurrentBrush.Factors.RemoveAt(index);
                currentShape.CurrentBrush.Factors.Insert(index - 1, tval);

                if (includePos)
                {
                    tval = currentShape.CurrentBrush.Positions[index];
                    currentShape.CurrentBrush.Positions.RemoveAt(index);
                    currentShape.CurrentBrush.Positions.Insert(index - 1, tval);
                }
            }
            else
            {
                tclr = currentShape.CurrentBrush.SurroundingColors[index];
                currentShape.CurrentBrush.SurroundingColors.RemoveAt(index);
                currentShape.CurrentBrush.SurroundingColors.Insert(index + 1, tclr);

                tval = currentShape.CurrentBrush.Factors[index];
                currentShape.CurrentBrush.Factors.RemoveAt(index);
                currentShape.CurrentBrush.Factors.Insert(index + 1, tval);

                if (includePos)
                {
                    tval = currentShape.CurrentBrush.Positions[index];
                    currentShape.CurrentBrush.Positions.RemoveAt(index);
                    currentShape.CurrentBrush.Positions.Insert(index + 1, tval);
                }
            }
            blendManager1.CurrentShape = currentShape;
        }

#endregion

        #region BrushManager handlers

        /// <summary>
        /// A brush has been selected and is associated with the currentShape.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bm_OnBrushSelected(object sender, BrushManagerEventArgs e)
        {
            if (currentShape == null)
                return;

            currentShape.CurrentBrush = e.brush;
            currentShape.CurrentBrush.ReferenceRectangle = currentShape.ShapeRectangle;
            UpdateControls();
            Invalidate();
        }

        #endregion

        #region Shape List manipulation methods

        /// <summary>
        /// Adds a Shape to the ShapeList
        /// </summary>
        /// <param name="sb"></param>
        public void AddShape(ShapeBase sb)
        {
            shapeList.Add(sb);
        }

        /// <summary>
        /// Removes a Shape from the ShapeList
        /// </summary>
        /// <param name="sb"></param>
        public void RemoveShape(ShapeBase sb)
        {
            shapeList.Remove(sb);
            currentShape = null;
            statusStripControl1.Clear();
        }

        /// <summary>
        /// Clears all entries from the ShapeList
        /// </summary>
        public void Clear()
        {
            if (OnWorkspaceStatusChange != null)
                OnWorkspaceStatusChange(this, new SketcherEventArgs(null));

            shapeList.Clear();
            Invalidate();
        }

        #endregion

        #region Selection methods

        /// <summary>
        /// Select a Shape based on the coordinates (x, y)
        ///     Note: I select the smallest one that pos (x, y) is contained in unless the
        ///             mouse is in one of the current shapes control area!
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private ShapeBase SelectShape(int x, int y)
        {
            ShapeBase sb = null;

            // If the mouse has been clicked within one of the current shape control areas leave as current
            if (currentShape != null &&
                currentShape.ShapeControlObject.ControlHitCheck(x, y) > 0)
            {
                currentShape.ShapeSelected = true;
                return currentShape;
            }

            //Deselect previous shape if selected
            if (currentShape != null)
            {
                currentShape.ShapeSelected = false;
                currentShape = null;
            }

            foreach (ShapeBase s in shapeList)
            {
                if (s.ShapeRectangle.Contains(x, y))
                {
                    if (sb == null)
                        sb = s;
                    else
                        if (s.ShapeArea < sb.ShapeArea)
                            sb = s;
                }
            }

            if (sb != null)
            {
                //Found a shape so set as current
                statusStripControl1.CurrentShape = sb;
                sb.ShapeSelected = true;
                currentShape = sb;
                blendManager1.CurrentShape = sb;
                UpdateControls();
            }
            else
            {
                //Click no active object selected
                statusStripControl1.Clear();
                blendManager1.CurrentShape = null;
            }

            return sb;
        }

        /// <summary>
        /// Selects the specified shape and deselects previous shape
        /// </summary>
        /// <param name="sb"></param>
        private void SelectShape(ShapeBase sb)
        {
            //Deselect previous shape if selected
            if (currentShape != null)
            {
                currentShape.ShapeSelected = false;
                currentShape = null;
            }

            if (sb != null)
            {
                //Found a shape so set as current
                statusStripControl1.CurrentShape = sb;
                sb.ShapeSelected = true;
                currentShape = sb;
                blendManager1.CurrentShape = sb;
                UpdateControls();
            }
            else
            {
                //Click no active object selected
                statusStripControl1.Clear();
                blendManager1.CurrentShape = null;
            }
        }

        /// <summary>
        /// Update the controls with current values
        /// </summary>
        private void UpdateControls()
        {
            //Generic properties
            cbHasBorder.Checked = currentShape.HasBorder;
            btnSwatchBorder.BackColor = currentShape.CurrentBrush.Color1;

            switch (currentShape.CurrentBrush.ShapeFillType)
            {
                case FillType.solid:
                    //Solid Brush Properties
                    btnSwatchFillColor.BackColor = currentShape.CurrentBrush.ShapeColor;
                    rbSolid.Checked = true;
                    break;
                case FillType.linearGradient:
                    //Linear Brush Properties
                    btnSwatchStartColor.BackColor = currentShape.CurrentBrush.ShapeColor;
                    btnSwatchEndColor.BackColor = currentShape.CurrentBrush.Color2;
                    cbGamma.Checked = currentShape.CurrentBrush.UseGammaCorrection;
                    cbSigma.Checked = currentShape.CurrentBrush.UseSigmaBell;
                    cbTriangular.Checked = currentShape.CurrentBrush.UseBlendTriangle;
                    scAngle.CurrentValue = currentShape.CurrentBrush.Angle;
                    scFocus.CurrentValue = currentShape.CurrentBrush.LinearFocus * 100f;
                    scScale.CurrentValue = currentShape.CurrentBrush.LinearScale * 100f;
                    rbLinear.Checked = true;
                    break;
                case FillType.pathGradient:
                    //Path Brush Properties
                    btnSwatchCenterColor.BackColor = currentShape.CurrentBrush.ShapeColor;
                    btnSwatchOuterColor.BackColor = currentShape.CurrentBrush.Color2;
                    cbPathSigma.Checked = currentShape.CurrentBrush.UseSigmaBell;
                    cbPathTriangular.Checked = currentShape.CurrentBrush.UseBlendTriangle;
                    scPathFocus.CurrentValue = currentShape.CurrentBrush.LinearFocus * 100f;
                    scPathScale.CurrentValue = currentShape.CurrentBrush.LinearScale * 100f;
                    scPathFocusScalesX.CurrentValue = currentShape.CurrentBrush.FocusScale.X * 100;
                    scPathFocusScalesY.CurrentValue = currentShape.CurrentBrush.FocusScale.Y * 100f;
                    rbPath.Checked = true;
                    break;
            }
        }

        #endregion

        #region Mouse event handlers and helpers

        private bool mouseDown = false;
        private bool resizing = false;
        private bool centering = false;
        private Point offset = new Point(0);
        private Shapes currentShapeType = Shapes.pointer;
        private int ctrlPt = 5;

        /// <summary>
        /// Handles mouse down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void control_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;

            if (e.Button == MouseButtons.Left)
            {
                if (currentShape != null)
                    currentShape.ShapeSelected = false;

                //Its a new element we're workin with
                if (currentShapeType != Shapes.pointer)
                {
                    switch (currentShapeType)
                    {
                        case Shapes.Rectangular:
                            currentShape = new Rectangular(new Rectangle(e.X, e.Y, 10, 10));
                            break;
                        case Shapes.select:
                            currentShape = new Rectangular(new Rectangle(e.X, e.Y, 10, 10));
                            currentShape.CurrentBrush.ShapeColor = Color.FromArgb(0, 0, 0, 0);
                            break;
                        case Shapes.RoundedRectangle:
                            currentShape = new RoundedRectangle(new Rectangle(e.X, e.Y, 10, 10));
                            break;
                        case Shapes.Elliptical:
                            currentShape = new Elliptical(new Rectangle(e.X, e.Y, 10, 10));
                            break;
                        case Shapes.Text:
                            currentShape = new Text(new Rectangle(e.X, e.Y, 10, 10));
                            break;
                        case Shapes.Image:
                            currentShape = new ShapeImage(new Rectangle(e.X, e.Y, 10, 10));
                            break;
                    }
                    AddShape(currentShape);
                    SelectShape(currentShape);
                    resizing = true;
                    SetCursor(5);
                }

                //We're in select mode
                if (currentShapeType == Shapes.pointer)
                {
                    currentShape = SelectShape(e.X, e.Y);

                    if (currentShape != null)
                    {
                        //The offset is the delta value of mouse coords. and itemRct.Location
                        offset.X = e.X - currentShape.ShapeRectangle.X;
                        offset.Y = e.Y - currentShape.ShapeRectangle.Y;

                        //Perform a hit check
                        ctrlPt = currentShape.ShapeControlObject.ControlHitCheck(e.X, e.Y);
                        if (ctrlPt > 0)
                        {
                            if (ctrlPt < 9)
                                resizing = true;
                            else
                                centering = true;
                        }
                        SetCursor(ctrlPt);
                    }
                    else
                        currentShape = null;
                }
            }
            else
                contextMenuStrip1.Show(MousePosition);

            //Set the TextProperites and ImageProperties toolStrip button status
            SetToolStripButtonStatus();

            this.Invalidate();
        }

        /// <summary>
        /// Set the enabled status of the ToolStripButtons on the main Toolstrip
        /// </summary>
        private void SetToolStripButtonStatus()
        {
            if (currentShape == null)
            {
                tsbTextEdit.Enabled = false;
                tsbImportImage.Enabled = false;
                statusStripControl1.Enabled = false;
                return;
            }

            statusStripControl1.Enabled = true;

            if (currentShape.ShapeType == Shapes.Text)
                tsbTextEdit.Enabled = true;
            else
                tsbTextEdit.Enabled = false;

            if (currentShape.ShapeType == Shapes.Image)
                tsbImportImage.Enabled = true;
            else
                tsbImportImage.Enabled = false;
        }

        /// <summary>
        /// MouseUp 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void control_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            resizing = false;
            centering = false;

            //Reset to Pointer after done with creating shape
            ResetToolStripButtonState();
            tsbPointer.Checked = true;

            //If the current object being created is text then Display properties dialog.
            if (currentShapeType == Shapes.Text)
            {
                GetTextProperties();
                this.Invalidate();
            }

            //If the current shape is an image load an image.
            if (currentShapeType == Shapes.Image)
                GetImage();

            //If anyones listening let them know that shape selection has changed
            if (OnShapeSelectionChange != null)
                OnShapeSelectionChange(this, new SketcherEventArgs(currentShape));

            currentShapeType = Shapes.pointer;
            SetCursor(0);
        }

        /// <summary>
        /// MouseMove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void control_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentShape == null)
                return;

            if (mouseDown)
            {
                Point pt = new Point(0, 0);

                //Calcualte objects offset relative to current mouse coords.
                pt.X = e.X - offset.X;
                pt.Y = e.Y - offset.Y;

                //We're risizing the shape
                if (resizing)
                {
                    currentShape.Resize(ctrlPt,
                        e.X,
                        e.Y,
                        clientArea);
                    this.Invalidate();

                    //Informs the StatusStrip to update its controls
                    statusStripControl1.UpdateStatus(currentShape);

                    //Resizing the shape
                    if (OnShapeSizeChange != null)
                        OnShapeSizeChange(this, new SketcherEventArgs(currentShape));
                }
                else
                {
                    //We're centering
                    if (centering)
                    {
                        //Convert Point to a percentage of overall width/height
                        //  i.e. if e.x = 200 and width = 200 and loc.x = 100
                        //          (200 - 100) / 200 = .5
                        currentShape.CurrentBrush.CenterPoint = new PointF(
                            (float)(e.Location.X - currentShape.ShapeRectangle.X) / (float)currentShape.ShapeRectangle.Width,
                            (float)(e.Location.Y - currentShape.ShapeRectangle.Y) / (float)currentShape.ShapeRectangle.Height);
                        this.Invalidate();
                    }
                    else
                    {
                        //We're moving the shape
                        currentShape.Move(pt.X, pt.Y, clientArea);

                        //Informs the StatusStrip to update its controls
                        statusStripControl1.UpdateStatus(currentShape);

                        this.Invalidate();
                    }
                }
            }
            else
                SetCursor(currentShape.ShapeControlObject.ControlHitCheck(e.X, e.Y));
        }

        /// <summary>
        /// Set the appropriate cursor given the position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="ctrlNew"></param>
        private void SetCursor(int ctrlNew)
        {
            ctrlPt = ctrlNew;
            switch (ctrlNew)
            {
                case 0:
                    Cursor.Current = Cursors.Default;
                    break;
                case 1:
                    Cursor.Current = Cursors.SizeNWSE;
                    break;
                case 2:
                    Cursor.Current = Cursors.SizeNS;
                    break;
                case 3:
                    Cursor.Current = Cursors.SizeNESW;
                    break;
                case 4:
                    Cursor.Current = Cursors.SizeWE;
                    break;
                case 5:
                    Cursor.Current = Cursors.SizeNWSE;
                    break;
                case 6:
                    Cursor.Current = Cursors.SizeNS;
                    break;
                case 7:
                    Cursor.Current = Cursors.SizeNESW;
                    break;
                case 8:
                    Cursor.Current = Cursors.SizeWE;
                    break;
                case 9: //CenterPointControl
                    Cursor.Current = Cursors.Hand;
                    break;
            }
        }

        #endregion

        #region Miscellaneous methods

        /// <summary>
        /// Used to control Toolbox ToolStrip button state - Resets buttons to unchecked
        /// </summary>
        private void ResetToolStripButtonState()
        {
            tsbRectangle.Checked = false;
            tsbRoundedRectangle.Checked = false;
            tsbEllipse.Checked = false;
            tsbText.Checked = false;
            tsbImage.Checked = false;
            tsbPointer.Checked = false;
            tsbTextEdit.Checked = false;
            tsbImportImage.Checked = false;
        }

        /// <summary>
        /// Gets Text properties from dialog
        /// </summary>
        private void GetTextProperties()
        {
            if (currentShape == null || currentShape.ShapeType != Shapes.Text)
                return;

            frmText ft = new frmText();
            ft.Content = currentShape.Text;
            ft.TextFont = new Font(currentShape.FontFamily, currentShape.FontSize);
            ft.TextColor = currentShape.CurrentBrush.ShapeColor;
            ft.Location = PointToScreen(this.Location);
            ft.Content = currentShape.Text;
            if (ft.ShowDialog() == DialogResult.OK)
            {
                currentShape.Text = (string)ft.Content.Clone();
                currentShape.FontFamily = ft.TextFont.FontFamily.Name;
                currentShape.FontSize = ft.TextFont.Size;
            }
            ft.Dispose();
        }

        /// <summary>
        /// Loads am Image from file
        /// </summary>
        public void GetImage()
        {
            if (currentShape == null)
                return;

            openFileDialog1.DefaultExt = "jpg";
            openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG;*.ICO)|*.BMP;*.JPG;*.GIF;*.png;*.ico|All files (*.*)|*.*";
            DialogResult dr = openFileDialog1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                this.currentShape.Text = openFileDialog1.FileName;
                try
                {
                    if (File.Exists(this.currentShape.Text))
                        this.currentShape.ShapeImage = Image.FromFile(this.currentShape.Text);
                    else
                    {
                        int index = this.currentShape.Text.LastIndexOf('\\');
                        if (index > 0)
                        {
                            string file = this.currentShape.Text.Substring(index);
                            file = Application.StartupPath + "\\" + file;
                            this.currentShape.ShapeImage = Image.FromFile(file);
                        }
                    }
                    this.Invalidate();
                    SelectShape(this.currentShape);
                }
                catch
                {
                    MessageBox.Show("Could not load for some reason");
                    this.currentShape.ShapeImage = null;
                }
            }
        }

        /// <summary>
        /// Context menu item to remove an object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentShape == null)
                return;

            this.Invalidate();
        }

        /// <summary>
        /// Specific to Gradiator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbClone_Click(object sender, EventArgs e)
        {
            if (currentShape == null)
                return;

            Random rnd = new Random(-1);

            //Deselect current shape
            currentShape.ShapeSelected = false;

            //Clone the current shape
            ShapeBase sb = currentShape.Clone() as ShapeBase;

            //Offset and Select the cloned item
            Rectangle rct = sb.ShapeRectangle;
            rct.Offset(rnd.Next(40), rnd.Next(40));
            sb.ShapeRectangle = rct;
            sb.ShapeSelected = true;

            //Add item and set it as current
            AddShape(sb);
            currentShape = sb;

            this.Invalidate();
        }

        #endregion

        #region Methods to control z-order

        /// <summary>
        /// Bring the selected shape to top of z-order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbBringToFront_Click(object sender, EventArgs e)
        {
            if (currentShape == null)
                return;

            shapeList.Remove(currentShape);
            //Append to list so it will be drawn last
            AddShape(currentShape);
            this.Invalidate();
        }

        /// <summary>
        /// Sends the selected shape to the bottom of the z-order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbSendToBack_Click(object sender, EventArgs e)
        {
            if (currentShape == null)
                return;

            shapeList.Remove(currentShape);
            //Insert at zero so it will be drawn first
            shapeList.Insert(0, currentShape);
            this.Invalidate();
        }

        #endregion

        #region Document manipulation methods

        /// <summary>
        /// Load a document/project
        /// </summary>
        /// <param name="newProject"></param>
        private void LoadDocument(bool newProject)
        {
            XmlDataDocument dom = new XmlDataDocument();
            ShapeBase sb = null;
            XmlNodeList nodeList;

            openFileDialog1.DefaultExt = "xml";
            openFileDialog1.FileName = "default.xml";
            openFileDialog1.Filter = "Xml files (*.xml)|*.xml";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            if (newProject)
                Clear();

            int index = openFileDialog1.FileName.LastIndexOf('\\');
            string str = openFileDialog1.FileName.Substring(++index);
            tslCaption.Text = str;
            dom.Load(openFileDialog1.FileName);
            nodeList = dom.DocumentElement.SelectNodes("ShapeObject");
            foreach (XmlNode node in nodeList)
            {
                switch (node["Type"].InnerText)
                {
                    case "Rectangular":
                        sb = new Rectangular(node);
                        break;
                    case "RoundedRectangle":
                        sb = new RoundedRectangle(node);
                        break;
                    case "Elliptical":
                        sb = new Elliptical(node);
                        break;
                    case "Text":
                        sb = new Text(node);
                        break;
                    case "Image":
                        sb = new ShapeImage(node);
                        LoadImage(sb);
                        break;
                }

                //If there wasn't a problem with conversion then go on
                ConvertXmlToGDI(ref sb, node);
                AddShape(sb);
            }
            Invalidate();
        }

        /// <summary>
        /// Save a document/project
        /// </summary>
        private void SaveDocument()
        {
            XmlDataDocument dom = new XmlDataDocument();
            XmlElement elem = dom.CreateElement("ShapeObjects");
            dom.AppendChild(elem);

            ConvertGDIToXML(ref dom);
            saveFileDialog1.DefaultExt = "xml";
            saveFileDialog1.FileName = "default.xml";
            saveFileDialog1.Filter = "Xml files (*.xml)|*.xml";
            saveFileDialog1.ShowDialog();
            dom.Save(saveFileDialog1.FileName);

            //Display the name in the ToolStrip tslCaption
            int index = saveFileDialog1.FileName.LastIndexOf('\\');
            string str = saveFileDialog1.FileName.Substring(++index);
            tslCaption.Text = str;
        }

        /// <summary>
        /// Load an image
        /// </summary>
        /// <param name="sb"></param>
        private void LoadImage(ShapeBase sb)
        {
            try
            {
                if (File.Exists(sb.Text))
                    sb.ShapeImage = Image.FromFile(sb.Text);
                else
                {
                    int index = sb.Text.LastIndexOf('\\');
                    if (index > 0)
                    {
                        string file = sb.Text.Substring(index);
                        file = Application.StartupPath + "\\" + file;
                        sb.ShapeImage = Image.FromFile(file);
                    }
                }
                this.Invalidate();
            }
            catch
            {
                MessageBox.Show("Could not load, might have been deleted or moved?");
                sb.ShapeImage = null;
            }
        }

        /// <summary>
        /// Convert the Xml data coming in to GDI (Brush)
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="node"></param>
        private void ConvertXmlToGDI(ref ShapeBase sb, XmlNode node)
        {
            try
            {
                sb.HasBorder = bool.Parse(node["HasBorder"].InnerText);

                //Delegate responsibility to the Brush class
                sb.CurrentBrush.ConvertXmlToGDI(ref sb, node);
            }
            catch
            {
                throw new Exception("Could load document specified");
            }
        }

        /// <summary>
        /// Convert from GDI (Brush) to Xml
        /// </summary>
        /// <param name="dom"></param>
        public void ConvertGDIToXML(ref XmlDataDocument dom)
        {
            XmlNode node = null;

            XmlNode root = dom.SelectSingleNode("//ShapeObjects");

            foreach (ShapeBase sb in shapeList)
            {
                node = sb.ObjectToXML(ref dom);
                root.AppendChild(node);
            }
        }

        #endregion

        #region generic event handlers

        /// <summary>
        /// Toobox ToolStrip generic button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void genericToolStrip_Click(object sender, EventArgs e)
        {
            ToolStripButton tsb = (ToolStripButton)sender;

            ResetToolStripButtonState();
            switch (tsb.Name)
            {
                case "tsbPointer":
                    currentShapeType = Shapes.pointer;
                    tsbPointer.Checked = true;
                    break;
                case "tsbRectangle":
                    currentShapeType = Shapes.Rectangular;
                    tsbRectangle.Checked = true;
                    break;
                case "tsbRoundedRectangle":
                    tsbRoundedRectangle.Checked = true;
                    currentShapeType = Shapes.RoundedRectangle;
                    break;
                case "tsbEllipse":
                    currentShapeType = Shapes.Elliptical;
                    tsbEllipse.Checked = true;
                    break;
                case "tsbText":
                    currentShapeType = Shapes.Text;
                    tsbText.Checked = true;
                    break;
                case "tsbTextEdit":
                    GetTextProperties();
                    tsbTextEdit.Checked = true;
                    this.Invalidate();
                    break;
                case "tsbImage":
                    currentShapeType = Shapes.Image;
                    tsbImage.Checked = true;
                    break;
                case "tsbImportImage":
                    GetImage();
                    tsbImportImage.Checked = true;
                    break;
                case "tsbHelp":
                    AboutGradiator ag = new AboutGradiator();
                    ag.ShowDialog();
                    ag.Dispose();
                    break;
            }
        }

        private void genericTookStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;

            switch (tsmi.Name)
            {
                case "tsmiRemove":
                    if (currentShape != null)
                        RemoveShape(currentShape);
                    Invalidate();
                    break;
                case "tsmiNewProject":
                    //TODO Check Dirty
                    Clear();
                    break;
                case "tsmiLoad":
                    LoadDocument(true);
                    break;
                case "tsmiSave":
                    SaveDocument();
                    break;
                case "tsmiImport":
                    LoadDocument(false);
                    break;
                case "tsmiClear":
                    Clear();
                    break;
                case "tsmiSaveBrush":
                    if (currentShape != null)
                        brushManager1.AddBrush((ShapeBrush)currentShape.CurrentBrush.Clone());
                    break;
                case "tsmiOptions":
                    //TODO Move to method where I can display options dialog and persist options
                    //TODO Eventually use ColorPicker when I get it all the way done.
                    break;
            }
        }
        
        /// <summary>
        /// Handles ToolstripButton editor events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void genericToolStrip_Editor_ButtonClick(object sender, EventArgs e)
        {
            ToolStripButton tsb = (ToolStripButton)sender;

            switch (tsb.Name)
            {
                case "tsbEditorColor":
                    selectorDialog.Show();
                    break;
            }
        }

        private bool inEvent = false;
        /// <summary>
        /// Generic handler to handle CheckBox clicks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generic_CheckBox_CheckChange(object sender, EventArgs e)
        {
            if (inEvent || currentShape == null)
                return;

            inEvent = true;
            CheckBox cb = (CheckBox)sender;

            switch (cb.Name)
            {
                case "cbHasBorder":
                    currentShape.HasBorder = cb.Checked;
                    break;
                case "cbGamma":
                    currentShape.CurrentBrush.UseGammaCorrection = cb.Checked;
                    break;
                case "cbSigma":
                    currentShape.CurrentBrush.UseSigmaBell = cb.Checked;
                    break;
                case "cbTriangular":
                    currentShape.CurrentBrush.UseBlendTriangle = cb.Checked;
                    break;
                case "cbPathSigma":
                    currentShape.CurrentBrush.UseSigmaBell = cb.Checked;
                    break;
                case "cbPathTriangular":
                    currentShape.CurrentBrush.UseBlendTriangle = cb.Checked;
                    break;
            }
            inEvent = false;
            Invalidate();
        }

        /// <summary>
        /// Generic method to handle slider value change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void genericSliderControl_OnValueChange(object sender, CustomSliderControlLib.SliderEventArgs e)
        {
            if (currentShape == null)
                return;

            CustomSliderControlLib.SliderControl sc = (CustomSliderControlLib.SliderControl)sender;

            switch (sc.Name)
            {
                case "scAngle":
                    currentShape.CurrentBrush.Angle = scAngle.CurrentValue;
                    break;
                case "scFocus":
                    currentShape.CurrentBrush.LinearFocus = scFocus.CurrentValue / 100f;
                    break;
                case "scScale":
                    currentShape.CurrentBrush.LinearScale = scScale.CurrentValue / 100f;
                    break;
                case "scPathFocus":
                    currentShape.CurrentBrush.LinearFocus = scPathFocus.CurrentValue / 100f;
                    break;
                case "scPathScale":
                    currentShape.CurrentBrush.LinearScale = scPathScale.CurrentValue / 100f;
                    break;
                case "scPathFocusScalesX":
                    currentShape.CurrentBrush.FocusScale = new PointF(
                        scPathFocusScalesX.CurrentValue / 100f,
                        scPathFocusScalesY.CurrentValue / 100f);
                    break;
                case "scPathFocusScalesY":
                    currentShape.CurrentBrush.FocusScale = new PointF(
                        scPathFocusScalesX.CurrentValue / 100f,
                        scPathFocusScalesY.CurrentValue / 100f);
                    break;
            }
            Invalidate();
        }

        private bool inRadioButtonCheckChange = false;
        /// <summary>
        /// Generic RadioButton check change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void genericRadioButton_CheckChange(object sender, EventArgs e)
        {
            if (currentShape == null || inRadioButtonCheckChange)
                return;

            inRadioButtonCheckChange = true;

            RadioButton rb = (RadioButton)sender;

            switch (rb.Name)
            {
                case "rbSolid":
                    currentShape.CurrentBrush.ShapeFillType = FillType.solid; ;
                    groupBox1.Show();
                    groupBox3.Hide();
                    groupBox4.Hide();
                    break;
                case "rbLinear":
                    currentShape.CurrentBrush.ShapeFillType = FillType.linearGradient;
                    groupBox1.Hide();
                    groupBox3.Show();
                    groupBox4.Hide();
                    break;
                case "rbPath":
                    currentShape.CurrentBrush.ShapeFillType = FillType.pathGradient;
                    groupBox1.Hide();
                    groupBox3.Hide();
                    groupBox4.Show();
                    break;
            }
            UpdateControls();
            blendManager1.CurrentShape = currentShape;
            inRadioButtonCheckChange = false;
            Invalidate();
        }

        /// <summary>
        /// Generic color swatch (label) color change event
        /// NOTE left click and color swatch is desctination, right click and color selector is destinatioin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void genericSwatchButton_Click(object sender, MouseEventArgs e)
        {
            if (currentShape == null)
                return;

            Button btn = (Button)sender;
            string btnName = btn.Name;

            if ((cbSelectBoth.CheckState == CheckState.Checked) &&
                (btnName == "btnSwatchStartColor" || btnName == "btnSwatchEndColor"))
                btnName = "retreiveBoth";

            if ((cbPathSelectBoth.CheckState == CheckState.Checked) &&
                (btnName == "btnSwatchCenterColor" || btnName == "btnSwatchOuterColor"))
                btnName = "retreiveBothPath";

            switch (btnName)
            {
                case "btnSwatchFillColor":
                    if (e.Button == MouseButtons.Left)
                    {
                        btnSwatchFillColor.BackColor = selectorDialog.colorSelector1.PrimaryColor;
                        currentShape.CurrentBrush.ShapeColor = selectorDialog.colorSelector1.PrimaryColor;
                    }
                    else
                        selectorDialog.colorSelector1.PrimaryColor = btnSwatchFillColor.BackColor;
                    break;
                case "btnSwatchBorder":
                    if (e.Button == MouseButtons.Left)
                    {
                        btnSwatchBorder.BackColor = selectorDialog.colorSelector1.PrimaryColor;
                        currentShape.CurrentBrush.Color1 = selectorDialog.colorSelector1.PrimaryColor;
                    }
                    else
                        selectorDialog.colorSelector1.PrimaryColor = btnSwatchBorder.BackColor;
                    break;
                case "btnSwatchStartColor":
                    if (e.Button == MouseButtons.Left)
                    {
                        btnSwatchStartColor.BackColor = selectorDialog.colorSelector1.PrimaryColor;
                        currentShape.CurrentBrush.ShapeColor = selectorDialog.colorSelector1.PrimaryColor;
                    }
                    else
                        selectorDialog.colorSelector1.PrimaryColor = btnSwatchStartColor.BackColor;
                    break;
                case "btnSwatchEndColor":
                    if (e.Button == MouseButtons.Left)
                    {
                        btnSwatchEndColor.BackColor = selectorDialog.colorSelector1.SecondaryColor;
                        currentShape.CurrentBrush.Color2 = selectorDialog.colorSelector1.SecondaryColor;
                    }
                    else
                        selectorDialog.colorSelector1.SecondaryColor = btnSwatchEndColor.BackColor;
                    break;
                case "retreiveBoth":
                    if (e.Button == MouseButtons.Left)
                    {
                        btnSwatchStartColor.BackColor = selectorDialog.colorSelector1.PrimaryColor;
                        currentShape.CurrentBrush.ShapeColor = selectorDialog.colorSelector1.PrimaryColor;
                        btnSwatchEndColor.BackColor = selectorDialog.colorSelector1.SecondaryColor;
                        currentShape.CurrentBrush.Color2 = selectorDialog.colorSelector1.SecondaryColor;
                    }
                    else
                    {
                        selectorDialog.colorSelector1.PrimaryColor = btnSwatchStartColor.BackColor;
                        selectorDialog.colorSelector1.SecondaryColor = btnSwatchEndColor.BackColor;
                    }
                    break;
                case "btnSwatchCenterColor":
                    if (e.Button == MouseButtons.Left)
                    {
                        btnSwatchCenterColor.BackColor = selectorDialog.colorSelector1.PrimaryColor;
                        currentShape.CurrentBrush.ShapeColor = selectorDialog.colorSelector1.PrimaryColor;
                    }
                    else
                        selectorDialog.colorSelector1.PrimaryColor = btnSwatchCenterColor.BackColor;
                    break;
                case "btnSwatchOuterColor":
                    if (e.Button == MouseButtons.Left)
                    {
                        btnSwatchOuterColor.BackColor = selectorDialog.colorSelector1.SecondaryColor;
                        currentShape.CurrentBrush.Color2 = selectorDialog.colorSelector1.SecondaryColor;
                    }
                    else
                        selectorDialog.colorSelector1.SecondaryColor = btnSwatchOuterColor.BackColor;
                    break;
                case "retreiveBothPath":
                    if (e.Button == MouseButtons.Left)
                    {
                        btnSwatchCenterColor.BackColor = selectorDialog.colorSelector1.PrimaryColor;
                        currentShape.CurrentBrush.ShapeColor = selectorDialog.colorSelector1.PrimaryColor;
                        btnSwatchOuterColor.BackColor = selectorDialog.colorSelector1.SecondaryColor;
                        currentShape.CurrentBrush.Color2 = selectorDialog.colorSelector1.SecondaryColor;
                    }
                    else
                    {
                        selectorDialog.colorSelector1.PrimaryColor = btnSwatchCenterColor.BackColor;
                        selectorDialog.colorSelector1.SecondaryColor = btnSwatchOuterColor.BackColor;
                    }
                    break;
            }
            Invalidate();
        }

        /// <summary>
        /// Tab control tab change event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentShape == null)
                return;

            //Need to do this because if tab is not showing it doesn't get updated
            if (tabControl1.SelectedIndex == 2)
                blendManager1.CurrentShape = currentShape;
        }    

        #endregion

        #region StatusStrip Handlers

        /// <summary>
        /// One of the StatusStrip values has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void statusStripControl1_OnStatusControlValueChange(object sender, StatusStripControlEventArgs e)
        {
            if (currentShape == null)
                return;

            Point pt = PointToClient(new Point(e.value, 0));
            switch (e.type)
            {
                case StatusControlChangeType.x:
                    currentShape.Move(e.value, currentShape.ShapeRectangle.Y, this.clientArea);
                    break;
                case StatusControlChangeType.y:
                    currentShape.Move(currentShape.ShapeRectangle.X, e.value, this.clientArea);
                    break;
                case StatusControlChangeType.width:
                    currentShape.Resize(e.value, currentShape.ShapeRectangle.Height);
                    break;
                case StatusControlChangeType.height:
                    currentShape.Resize(currentShape.ShapeRectangle.Width, e.value);
                    break;
            }
            this.Invalidate();
        }

        /// <summary>
        /// Handle scrolling through shapes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void statusStripControl1_OnShapeScroll(object sender, StatusStripControlEventArgs e)
        {
            if (currentShape == null)
                return;

            int index = shapeList.IndexOf(currentShape);
            ScrollShape(index, e.direction);
        }

        /// <summary>
        /// Does the actual scrolling
        /// </summary>
        /// <param name="index"></param>
        /// <param name="scd"></param>
        private void ScrollShape(int index, ShapeScrollDirection scd)
        {
            if (shapeList.Count < 1)
                return;

            if (scd == ShapeScrollDirection.up)
                ++index;
            else
                --index;

            if (index < 0)
                index = shapeList.Count;
            else
                if (index >= shapeList.Count)
                    index = 0;

            currentShape.ShapeSelected = false;
            currentShape = shapeList[index];
            currentShape.ShapeSelected = true;
            statusStripControl1.CurrentShape = currentShape;
            UpdateControls();
            Invalidate();
        }

        #endregion

    }
}
