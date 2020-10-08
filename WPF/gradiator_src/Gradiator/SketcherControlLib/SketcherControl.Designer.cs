namespace SketcherControlLib
{
    partial class SketcherControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SketcherControl));
            ShapesClassLib.ShapeBrush shapeBrush1 = new ShapesClassLib.ShapeBrush();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsmiFileOptions = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiNewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPointer = new System.Windows.Forms.ToolStripButton();
            this.tsbRectangle = new System.Windows.Forms.ToolStripButton();
            this.tsbRoundedRectangle = new System.Windows.Forms.ToolStripButton();
            this.tsbEllipse = new System.Windows.Forms.ToolStripButton();
            this.tsbText = new System.Windows.Forms.ToolStripButton();
            this.tsbImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbTextEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbImportImage = new System.Windows.Forms.ToolStripButton();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEditorColor = new System.Windows.Forms.ToolStripButton();
            this.tslCaption = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbBringToFront = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbSendToBack = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbClone = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveBrush = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSwatchFillColor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSwatchOuterColor = new System.Windows.Forms.Button();
            this.btnSwatchCenterColor = new System.Windows.Forms.Button();
            this.cbPathSelectBoth = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.scPathFocusScalesX = new CustomSliderControlLib.SliderControl();
            this.scPathFocus = new CustomSliderControlLib.SliderControl();
            this.scPathFocusScalesY = new CustomSliderControlLib.SliderControl();
            this.scPathScale = new CustomSliderControlLib.SliderControl();
            this.cbPathTriangular = new System.Windows.Forms.CheckBox();
            this.cbPathSigma = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSwatchEndColor = new System.Windows.Forms.Button();
            this.btnSwatchStartColor = new System.Windows.Forms.Button();
            this.cbSelectBoth = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.scFocus = new CustomSliderControlLib.SliderControl();
            this.scScale = new CustomSliderControlLib.SliderControl();
            this.scAngle = new CustomSliderControlLib.SliderControl();
            this.cbGamma = new System.Windows.Forms.CheckBox();
            this.cbTriangular = new System.Windows.Forms.CheckBox();
            this.cbSigma = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSwatchBorder = new System.Windows.Forms.Button();
            this.rbLinear = new System.Windows.Forms.RadioButton();
            this.cbHasBorder = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbPath = new System.Windows.Forms.RadioButton();
            this.rbSolid = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.tpBrush = new System.Windows.Forms.TabPage();
            this.brushManager1 = new SketcherControlLib.MiscControls.BrushManager();
            this.tpBlend = new System.Windows.Forms.TabPage();
            this.blendManager1 = new SketcherControlLib.MiscControls.BlendManager();
            this.statusStripControl1 = new SketcherControlLib.MiscControls.StatusStripControl();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.tpBrush.SuspendLayout();
            this.tpBlend.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFileOptions,
            this.toolStripSeparator2,
            this.tsbPointer,
            this.tsbRectangle,
            this.tsbRoundedRectangle,
            this.tsbEllipse,
            this.tsbText,
            this.tsbImage,
            this.toolStripSeparator1,
            this.tsbTextEdit,
            this.tsbImportImage,
            this.tsbHelp,
            this.toolStripSeparator5,
            this.tsbEditorColor,
            this.toolStripSeparator6,
            this.tslCaption,
            this.toolStripSeparator7});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(517, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsmiFileOptions
            // 
            this.tsmiFileOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsmiFileOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewProject,
            this.tsmiLoad,
            this.tsmiSave,
            this.tsmiImport,
            this.toolStripSeparator3,
            this.tsmiClear,
            this.toolStripSeparator4,
            this.tsmiOptions});
            this.tsmiFileOptions.Image = ((System.Drawing.Image)(resources.GetObject("tsmiFileOptions.Image")));
            this.tsmiFileOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiFileOptions.Name = "tsmiFileOptions";
            this.tsmiFileOptions.Size = new System.Drawing.Size(33, 24);
            this.tsmiFileOptions.ToolTipText = "Workspace options";
            // 
            // tsmiNewProject
            // 
            this.tsmiNewProject.Name = "tsmiNewProject";
            this.tsmiNewProject.Size = new System.Drawing.Size(143, 22);
            this.tsmiNewProject.Text = "New Project";
            this.tsmiNewProject.Click += new System.EventHandler(this.genericTookStripMenuItem_Click);
            // 
            // tsmiLoad
            // 
            this.tsmiLoad.Name = "tsmiLoad";
            this.tsmiLoad.Size = new System.Drawing.Size(143, 22);
            this.tsmiLoad.Text = "Load";
            this.tsmiLoad.Click += new System.EventHandler(this.genericTookStripMenuItem_Click);
            // 
            // tsmiSave
            // 
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Size = new System.Drawing.Size(143, 22);
            this.tsmiSave.Text = "Save";
            this.tsmiSave.Click += new System.EventHandler(this.genericTookStripMenuItem_Click);
            // 
            // tsmiImport
            // 
            this.tsmiImport.Name = "tsmiImport";
            this.tsmiImport.Size = new System.Drawing.Size(143, 22);
            this.tsmiImport.Text = "Import";
            this.tsmiImport.Click += new System.EventHandler(this.genericTookStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(140, 6);
            // 
            // tsmiClear
            // 
            this.tsmiClear.Name = "tsmiClear";
            this.tsmiClear.Size = new System.Drawing.Size(143, 22);
            this.tsmiClear.Text = "Clear";
            this.tsmiClear.Click += new System.EventHandler(this.genericTookStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(140, 6);
            // 
            // tsmiOptions
            // 
            this.tsmiOptions.Name = "tsmiOptions";
            this.tsmiOptions.Size = new System.Drawing.Size(143, 22);
            this.tsmiOptions.Text = "Options";
            this.tsmiOptions.Click += new System.EventHandler(this.genericTookStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbPointer
            // 
            this.tsbPointer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPointer.Image = ((System.Drawing.Image)(resources.GetObject("tsbPointer.Image")));
            this.tsbPointer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPointer.Name = "tsbPointer";
            this.tsbPointer.Size = new System.Drawing.Size(24, 24);
            this.tsbPointer.ToolTipText = "Tool: Selection\r\n\r\nUsed to Move and Resize\r\nelsments!";
            this.tsbPointer.Click += new System.EventHandler(this.genericToolStrip_Click);
            // 
            // tsbRectangle
            // 
            this.tsbRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRectangle.Image = ((System.Drawing.Image)(resources.GetObject("tsbRectangle.Image")));
            this.tsbRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRectangle.Name = "tsbRectangle";
            this.tsbRectangle.Size = new System.Drawing.Size(24, 24);
            this.tsbRectangle.Text = "toolStripButton1";
            this.tsbRectangle.ToolTipText = "Tool: Rectangle\r\n\r\nCreate a rectangular object";
            this.tsbRectangle.Click += new System.EventHandler(this.genericToolStrip_Click);
            // 
            // tsbRoundedRectangle
            // 
            this.tsbRoundedRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRoundedRectangle.Image = ((System.Drawing.Image)(resources.GetObject("tsbRoundedRectangle.Image")));
            this.tsbRoundedRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRoundedRectangle.Name = "tsbRoundedRectangle";
            this.tsbRoundedRectangle.Size = new System.Drawing.Size(24, 24);
            this.tsbRoundedRectangle.Text = "toolStripButton2";
            this.tsbRoundedRectangle.ToolTipText = "Tool: RoundedRectangle\r\n\r\nCreate a rectangular object with rounded corners";
            this.tsbRoundedRectangle.Click += new System.EventHandler(this.genericToolStrip_Click);
            // 
            // tsbEllipse
            // 
            this.tsbEllipse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEllipse.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbEllipse.Image = ((System.Drawing.Image)(resources.GetObject("tsbEllipse.Image")));
            this.tsbEllipse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEllipse.Name = "tsbEllipse";
            this.tsbEllipse.Size = new System.Drawing.Size(24, 24);
            this.tsbEllipse.Text = "toolStripButton3";
            this.tsbEllipse.ToolTipText = "Tool: Ellipse\r\n\r\nCreate an elliptical object";
            this.tsbEllipse.Click += new System.EventHandler(this.genericToolStrip_Click);
            // 
            // tsbText
            // 
            this.tsbText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbText.Image = ((System.Drawing.Image)(resources.GetObject("tsbText.Image")));
            this.tsbText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbText.Name = "tsbText";
            this.tsbText.Size = new System.Drawing.Size(24, 24);
            this.tsbText.Text = "toolStripButton1";
            this.tsbText.ToolTipText = "Tool: Text\r\n\r\nCreates a Text object";
            this.tsbText.Click += new System.EventHandler(this.genericToolStrip_Click);
            // 
            // tsbImage
            // 
            this.tsbImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImage.Image = global::SketcherControlLib.Properties.Resources.InsertPictureHS;
            this.tsbImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImage.Name = "tsbImage";
            this.tsbImage.Size = new System.Drawing.Size(24, 24);
            this.tsbImage.Text = "toolStripButton1";
            this.tsbImage.ToolTipText = "Tool: Image\r\n\r\nCreates and Image object";
            this.tsbImage.Click += new System.EventHandler(this.genericToolStrip_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(10, 27);
            // 
            // tsbTextEdit
            // 
            this.tsbTextEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTextEdit.Enabled = false;
            this.tsbTextEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsbTextEdit.Image")));
            this.tsbTextEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTextEdit.Name = "tsbTextEdit";
            this.tsbTextEdit.Size = new System.Drawing.Size(24, 24);
            this.tsbTextEdit.Text = "Text Properties";
            this.tsbTextEdit.ToolTipText = "Editor: Text Properties\r\n\r\nIf an Text object is selected clicking this \r\nbutton w" +
                "ill display the Text Properties Editor.";
            this.tsbTextEdit.Click += new System.EventHandler(this.genericToolStrip_Click);
            // 
            // tsbImportImage
            // 
            this.tsbImportImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImportImage.Enabled = false;
            this.tsbImportImage.Image = ((System.Drawing.Image)(resources.GetObject("tsbImportImage.Image")));
            this.tsbImportImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImportImage.Name = "tsbImportImage";
            this.tsbImportImage.Size = new System.Drawing.Size(24, 24);
            this.tsbImportImage.Text = "Import Image";
            this.tsbImportImage.ToolTipText = "Editor: Image Properties\r\n\r\nWhen clicked and an Image object selected will displa" +
                "y\r\nthe OpenFileDialog for getting an Image.";
            this.tsbImportImage.Click += new System.EventHandler(this.genericToolStrip_Click);
            // 
            // tsbHelp
            // 
            this.tsbHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Image = global::SketcherControlLib.Properties.Resources.question;
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(24, 24);
            this.tsbHelp.Text = "toolStripButton1";
            this.tsbHelp.ToolTipText = "What about it!";
            this.tsbHelp.Click += new System.EventHandler(this.genericToolStrip_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbEditorColor
            // 
            this.tsbEditorColor.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbEditorColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEditorColor.Image = global::SketcherControlLib.Properties.Resources.ColorHS;
            this.tsbEditorColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEditorColor.Name = "tsbEditorColor";
            this.tsbEditorColor.Size = new System.Drawing.Size(24, 24);
            this.tsbEditorColor.Text = "toolStripButton2";
            this.tsbEditorColor.ToolTipText = "Display the Color selector dialog";
            this.tsbEditorColor.Click += new System.EventHandler(this.genericToolStrip_Editor_ButtonClick);
            // 
            // tslCaption
            // 
            this.tslCaption.AutoSize = false;
            this.tslCaption.BackColor = System.Drawing.Color.MistyRose;
            this.tslCaption.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tslCaption.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslCaption.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.tslCaption.Name = "tslCaption";
            this.tslCaption.Size = new System.Drawing.Size(220, 24);
            this.tslCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 27);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRemove,
            this.tsbBringToFront,
            this.tsbSendToBack,
            this.tsbClone,
            this.tsmiSaveBrush});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(154, 114);
            // 
            // tsmiRemove
            // 
            this.tsmiRemove.Name = "tsmiRemove";
            this.tsmiRemove.Size = new System.Drawing.Size(153, 22);
            this.tsmiRemove.Text = "Remove";
            this.tsmiRemove.Click += new System.EventHandler(this.genericTookStripMenuItem_Click);
            // 
            // tsbBringToFront
            // 
            this.tsbBringToFront.Name = "tsbBringToFront";
            this.tsbBringToFront.Size = new System.Drawing.Size(153, 22);
            this.tsbBringToFront.Text = "Bring To Front";
            this.tsbBringToFront.Click += new System.EventHandler(this.tsbBringToFront_Click);
            // 
            // tsbSendToBack
            // 
            this.tsbSendToBack.Name = "tsbSendToBack";
            this.tsbSendToBack.Size = new System.Drawing.Size(153, 22);
            this.tsbSendToBack.Text = "Send To Back";
            this.tsbSendToBack.Click += new System.EventHandler(this.tsbSendToBack_Click);
            // 
            // tsbClone
            // 
            this.tsbClone.Name = "tsbClone";
            this.tsbClone.Size = new System.Drawing.Size(153, 22);
            this.tsbClone.Text = "Clone";
            this.tsbClone.Click += new System.EventHandler(this.tsbClone_Click);
            // 
            // tsmiSaveBrush
            // 
            this.tsmiSaveBrush.Name = "tsmiSaveBrush";
            this.tsmiSaveBrush.Size = new System.Drawing.Size(153, 22);
            this.tsmiSaveBrush.Text = "Save Brush";
            this.tsmiSaveBrush.Click += new System.EventHandler(this.genericTookStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btnSwatchFillColor);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(6, 124);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 84);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Solid Brush Properties";
            // 
            // btnSwatchFillColor
            // 
            this.btnSwatchFillColor.BackColor = System.Drawing.Color.White;
            this.btnSwatchFillColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwatchFillColor.Location = new System.Drawing.Point(22, 30);
            this.btnSwatchFillColor.Name = "btnSwatchFillColor";
            this.btnSwatchFillColor.Size = new System.Drawing.Size(14, 14);
            this.btnSwatchFillColor.TabIndex = 48;
            this.btnSwatchFillColor.UseVisualStyleBackColor = false;
            this.btnSwatchFillColor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.genericSwatchButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "Fill Color";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSwatchOuterColor);
            this.groupBox4.Controls.Add(this.btnSwatchCenterColor);
            this.groupBox4.Controls.Add(this.cbPathSelectBoth);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.scPathFocusScalesX);
            this.groupBox4.Controls.Add(this.scPathFocus);
            this.groupBox4.Controls.Add(this.scPathFocusScalesY);
            this.groupBox4.Controls.Add(this.scPathScale);
            this.groupBox4.Controls.Add(this.cbPathTriangular);
            this.groupBox4.Controls.Add(this.cbPathSigma);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox4.Location = new System.Drawing.Point(6, 124);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 289);
            this.groupBox4.TabIndex = 59;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Path Brush Properties";
            this.groupBox4.Visible = false;
            // 
            // btnSwatchOuterColor
            // 
            this.btnSwatchOuterColor.BackColor = System.Drawing.Color.Black;
            this.btnSwatchOuterColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwatchOuterColor.Location = new System.Drawing.Point(45, 65);
            this.btnSwatchOuterColor.Name = "btnSwatchOuterColor";
            this.btnSwatchOuterColor.Size = new System.Drawing.Size(14, 14);
            this.btnSwatchOuterColor.TabIndex = 68;
            this.btnSwatchOuterColor.UseVisualStyleBackColor = false;
            this.btnSwatchOuterColor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.genericSwatchButton_Click);
            // 
            // btnSwatchCenterColor
            // 
            this.btnSwatchCenterColor.BackColor = System.Drawing.Color.White;
            this.btnSwatchCenterColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwatchCenterColor.Location = new System.Drawing.Point(45, 43);
            this.btnSwatchCenterColor.Name = "btnSwatchCenterColor";
            this.btnSwatchCenterColor.Size = new System.Drawing.Size(14, 14);
            this.btnSwatchCenterColor.TabIndex = 67;
            this.btnSwatchCenterColor.UseVisualStyleBackColor = false;
            this.btnSwatchCenterColor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.genericSwatchButton_Click);
            // 
            // cbPathSelectBoth
            // 
            this.cbPathSelectBoth.AutoSize = true;
            this.cbPathSelectBoth.Checked = true;
            this.cbPathSelectBoth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPathSelectBoth.Location = new System.Drawing.Point(18, 24);
            this.cbPathSelectBoth.Name = "cbPathSelectBoth";
            this.cbPathSelectBoth.Size = new System.Drawing.Size(112, 17);
            this.cbPathSelectBoth.TabIndex = 66;
            this.cbPathSelectBoth.Text = "Select both Colors";
            this.cbPathSelectBoth.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(66, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 65;
            this.label7.Text = "Outer Color";
            // 
            // scPathFocusScalesX
            // 
            this.scPathFocusScalesX.BackColor = System.Drawing.Color.Transparent;
            this.scPathFocusScalesX.Caption = "Focus Scales X";
            this.scPathFocusScalesX.CurrentValue = 50F;
            this.scPathFocusScalesX.IsValueFloat = false;
            this.scPathFocusScalesX.Location = new System.Drawing.Point(16, 221);
            this.scPathFocusScalesX.MaxValue = 100F;
            this.scPathFocusScalesX.MinValue = 0F;
            this.scPathFocusScalesX.Name = "scPathFocusScalesX";
            this.scPathFocusScalesX.Size = new System.Drawing.Size(172, 24);
            this.scPathFocusScalesX.TabIndex = 62;
            this.scPathFocusScalesX.OnValueChange += new CustomSliderControlLib.SliderControl.OnValueChangeEventHandler(this.genericSliderControl_OnValueChange);
            // 
            // scPathFocus
            // 
            this.scPathFocus.BackColor = System.Drawing.Color.Transparent;
            this.scPathFocus.Caption = "Focal Point";
            this.scPathFocus.CurrentValue = 50F;
            this.scPathFocus.IsValueFloat = false;
            this.scPathFocus.Location = new System.Drawing.Point(16, 150);
            this.scPathFocus.MaxValue = 100F;
            this.scPathFocus.MinValue = 0F;
            this.scPathFocus.Name = "scPathFocus";
            this.scPathFocus.Size = new System.Drawing.Size(172, 24);
            this.scPathFocus.TabIndex = 61;
            this.scPathFocus.Tag = "linear,rest";
            this.scPathFocus.OnValueChange += new CustomSliderControlLib.SliderControl.OnValueChangeEventHandler(this.genericSliderControl_OnValueChange);
            // 
            // scPathFocusScalesY
            // 
            this.scPathFocusScalesY.BackColor = System.Drawing.Color.Transparent;
            this.scPathFocusScalesY.Caption = "Focus Scales Y";
            this.scPathFocusScalesY.CurrentValue = 50F;
            this.scPathFocusScalesY.IsValueFloat = false;
            this.scPathFocusScalesY.Location = new System.Drawing.Point(17, 247);
            this.scPathFocusScalesY.MaxValue = 100F;
            this.scPathFocusScalesY.MinValue = 0F;
            this.scPathFocusScalesY.Name = "scPathFocusScalesY";
            this.scPathFocusScalesY.Size = new System.Drawing.Size(171, 24);
            this.scPathFocusScalesY.TabIndex = 63;
            this.scPathFocusScalesY.OnValueChange += new CustomSliderControlLib.SliderControl.OnValueChangeEventHandler(this.genericSliderControl_OnValueChange);
            // 
            // scPathScale
            // 
            this.scPathScale.BackColor = System.Drawing.Color.Transparent;
            this.scPathScale.Caption = "Dropoff Scale";
            this.scPathScale.CurrentValue = 50F;
            this.scPathScale.IsValueFloat = false;
            this.scPathScale.Location = new System.Drawing.Point(16, 180);
            this.scPathScale.MaxValue = 100F;
            this.scPathScale.MinValue = 0F;
            this.scPathScale.Name = "scPathScale";
            this.scPathScale.Size = new System.Drawing.Size(172, 24);
            this.scPathScale.TabIndex = 60;
            this.scPathScale.Tag = "linear,rest";
            this.scPathScale.OnValueChange += new CustomSliderControlLib.SliderControl.OnValueChangeEventHandler(this.genericSliderControl_OnValueChange);
            // 
            // cbPathTriangular
            // 
            this.cbPathTriangular.AutoSize = true;
            this.cbPathTriangular.BackColor = System.Drawing.Color.Transparent;
            this.cbPathTriangular.Location = new System.Drawing.Point(25, 119);
            this.cbPathTriangular.Name = "cbPathTriangular";
            this.cbPathTriangular.Size = new System.Drawing.Size(153, 17);
            this.cbPathTriangular.TabIndex = 58;
            this.cbPathTriangular.Tag = "linear,rest";
            this.cbPathTriangular.Text = "Use TriangularBlendShape";
            this.cbPathTriangular.UseVisualStyleBackColor = false;
            this.cbPathTriangular.CheckedChanged += new System.EventHandler(this.generic_CheckBox_CheckChange);
            // 
            // cbPathSigma
            // 
            this.cbPathSigma.AutoSize = true;
            this.cbPathSigma.BackColor = System.Drawing.Color.Transparent;
            this.cbPathSigma.Location = new System.Drawing.Point(25, 96);
            this.cbPathSigma.Name = "cbPathSigma";
            this.cbPathSigma.Size = new System.Drawing.Size(125, 17);
            this.cbPathSigma.TabIndex = 59;
            this.cbPathSigma.Tag = "linear,rest";
            this.cbPathSigma.Text = "Use SigmaBellShape";
            this.cbPathSigma.UseVisualStyleBackColor = false;
            this.cbPathSigma.CheckedChanged += new System.EventHandler(this.generic_CheckBox_CheckChange);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(65, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 57;
            this.label5.Text = "Center Color";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.btnSwatchEndColor);
            this.groupBox3.Controls.Add(this.btnSwatchStartColor);
            this.groupBox3.Controls.Add(this.cbSelectBoth);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.scFocus);
            this.groupBox3.Controls.Add(this.scScale);
            this.groupBox3.Controls.Add(this.scAngle);
            this.groupBox3.Controls.Add(this.cbGamma);
            this.groupBox3.Controls.Add(this.cbTriangular);
            this.groupBox3.Controls.Add(this.cbSigma);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Location = new System.Drawing.Point(6, 124);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 295);
            this.groupBox3.TabIndex = 51;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Linear Brush Properties";
            this.groupBox3.Visible = false;
            // 
            // btnSwatchEndColor
            // 
            this.btnSwatchEndColor.BackColor = System.Drawing.Color.Black;
            this.btnSwatchEndColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwatchEndColor.Location = new System.Drawing.Point(45, 68);
            this.btnSwatchEndColor.Name = "btnSwatchEndColor";
            this.btnSwatchEndColor.Size = new System.Drawing.Size(14, 14);
            this.btnSwatchEndColor.TabIndex = 60;
            this.btnSwatchEndColor.UseVisualStyleBackColor = false;
            this.btnSwatchEndColor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.genericSwatchButton_Click);
            // 
            // btnSwatchStartColor
            // 
            this.btnSwatchStartColor.BackColor = System.Drawing.Color.White;
            this.btnSwatchStartColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwatchStartColor.Location = new System.Drawing.Point(45, 47);
            this.btnSwatchStartColor.Name = "btnSwatchStartColor";
            this.btnSwatchStartColor.Size = new System.Drawing.Size(14, 14);
            this.btnSwatchStartColor.TabIndex = 59;
            this.btnSwatchStartColor.UseVisualStyleBackColor = false;
            this.btnSwatchStartColor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.genericSwatchButton_Click);
            // 
            // cbSelectBoth
            // 
            this.cbSelectBoth.AutoSize = true;
            this.cbSelectBoth.Checked = true;
            this.cbSelectBoth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSelectBoth.Location = new System.Drawing.Point(18, 27);
            this.cbSelectBoth.Name = "cbSelectBoth";
            this.cbSelectBoth.Size = new System.Drawing.Size(112, 17);
            this.cbSelectBoth.TabIndex = 58;
            this.cbSelectBoth.Text = "Select both Colors";
            this.cbSelectBoth.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(66, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 57;
            this.label6.Text = "End Color (Sec,)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(66, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 55;
            this.label4.Text = "Start Color (Pri.)";
            // 
            // scFocus
            // 
            this.scFocus.BackColor = System.Drawing.Color.Transparent;
            this.scFocus.Caption = "Focal Point";
            this.scFocus.CurrentValue = 50F;
            this.scFocus.IsValueFloat = false;
            this.scFocus.Location = new System.Drawing.Point(14, 224);
            this.scFocus.MaxValue = 100F;
            this.scFocus.MinValue = 0F;
            this.scFocus.Name = "scFocus";
            this.scFocus.Size = new System.Drawing.Size(172, 24);
            this.scFocus.TabIndex = 53;
            this.scFocus.Tag = "linear,rest";
            this.scFocus.OnValueChange += new CustomSliderControlLib.SliderControl.OnValueChangeEventHandler(this.genericSliderControl_OnValueChange);
            // 
            // scScale
            // 
            this.scScale.BackColor = System.Drawing.Color.Transparent;
            this.scScale.Caption = "Dropoff Scale";
            this.scScale.CurrentValue = 50F;
            this.scScale.IsValueFloat = false;
            this.scScale.Location = new System.Drawing.Point(14, 254);
            this.scScale.MaxValue = 100F;
            this.scScale.MinValue = 0F;
            this.scScale.Name = "scScale";
            this.scScale.Size = new System.Drawing.Size(172, 24);
            this.scScale.TabIndex = 52;
            this.scScale.Tag = "linear,rest";
            this.scScale.OnValueChange += new CustomSliderControlLib.SliderControl.OnValueChangeEventHandler(this.genericSliderControl_OnValueChange);
            // 
            // scAngle
            // 
            this.scAngle.BackColor = System.Drawing.Color.Transparent;
            this.scAngle.Caption = "Focus Angle ";
            this.scAngle.CurrentValue = 90F;
            this.scAngle.IsValueFloat = false;
            this.scAngle.Location = new System.Drawing.Point(14, 182);
            this.scAngle.MaxValue = 360F;
            this.scAngle.MinValue = 0F;
            this.scAngle.Name = "scAngle";
            this.scAngle.Size = new System.Drawing.Size(172, 24);
            this.scAngle.TabIndex = 51;
            this.scAngle.Tag = "linear,rest";
            this.scAngle.OnValueChange += new CustomSliderControlLib.SliderControl.OnValueChangeEventHandler(this.genericSliderControl_OnValueChange);
            // 
            // cbGamma
            // 
            this.cbGamma.AutoSize = true;
            this.cbGamma.BackColor = System.Drawing.Color.Transparent;
            this.cbGamma.Location = new System.Drawing.Point(18, 99);
            this.cbGamma.Name = "cbGamma";
            this.cbGamma.Size = new System.Drawing.Size(135, 17);
            this.cbGamma.TabIndex = 50;
            this.cbGamma.Tag = "linear,rest";
            this.cbGamma.Text = "Use Gamma Correction";
            this.cbGamma.UseVisualStyleBackColor = false;
            this.cbGamma.CheckedChanged += new System.EventHandler(this.generic_CheckBox_CheckChange);
            // 
            // cbTriangular
            // 
            this.cbTriangular.AutoSize = true;
            this.cbTriangular.BackColor = System.Drawing.Color.Transparent;
            this.cbTriangular.Location = new System.Drawing.Point(18, 153);
            this.cbTriangular.Name = "cbTriangular";
            this.cbTriangular.Size = new System.Drawing.Size(153, 17);
            this.cbTriangular.TabIndex = 48;
            this.cbTriangular.Tag = "linear,rest";
            this.cbTriangular.Text = "Use TriangularBlendShape";
            this.cbTriangular.UseVisualStyleBackColor = false;
            this.cbTriangular.CheckedChanged += new System.EventHandler(this.generic_CheckBox_CheckChange);
            // 
            // cbSigma
            // 
            this.cbSigma.AutoSize = true;
            this.cbSigma.BackColor = System.Drawing.Color.Transparent;
            this.cbSigma.Location = new System.Drawing.Point(18, 130);
            this.cbSigma.Name = "cbSigma";
            this.cbSigma.Size = new System.Drawing.Size(125, 17);
            this.cbSigma.TabIndex = 49;
            this.cbSigma.Tag = "linear,rest";
            this.cbSigma.Text = "Use SigmaBellShape";
            this.cbSigma.UseVisualStyleBackColor = false;
            this.cbSigma.CheckedChanged += new System.EventHandler(this.generic_CheckBox_CheckChange);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSwatchBorder);
            this.groupBox2.Controls.Add(this.rbLinear);
            this.groupBox2.Controls.Add(this.cbHasBorder);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.rbPath);
            this.groupBox2.Controls.Add(this.rbSolid);
            this.groupBox2.Location = new System.Drawing.Point(6, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 85);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Shape Properties";
            // 
            // btnSwatchBorder
            // 
            this.btnSwatchBorder.BackColor = System.Drawing.Color.Black;
            this.btnSwatchBorder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwatchBorder.Location = new System.Drawing.Point(70, 51);
            this.btnSwatchBorder.Name = "btnSwatchBorder";
            this.btnSwatchBorder.Size = new System.Drawing.Size(14, 14);
            this.btnSwatchBorder.TabIndex = 47;
            this.btnSwatchBorder.UseVisualStyleBackColor = false;
            this.btnSwatchBorder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.genericSwatchButton_Click);
            // 
            // rbLinear
            // 
            this.rbLinear.AutoSize = true;
            this.rbLinear.Location = new System.Drawing.Point(68, 19);
            this.rbLinear.Name = "rbLinear";
            this.rbLinear.Size = new System.Drawing.Size(54, 17);
            this.rbLinear.TabIndex = 46;
            this.rbLinear.Text = "Linear";
            this.rbLinear.UseVisualStyleBackColor = true;
            this.rbLinear.CheckedChanged += new System.EventHandler(this.genericRadioButton_CheckChange);
            // 
            // cbHasBorder
            // 
            this.cbHasBorder.AutoSize = true;
            this.cbHasBorder.BackColor = System.Drawing.Color.Transparent;
            this.cbHasBorder.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbHasBorder.Checked = true;
            this.cbHasBorder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHasBorder.Location = new System.Drawing.Point(11, 51);
            this.cbHasBorder.Name = "cbHasBorder";
            this.cbHasBorder.Size = new System.Drawing.Size(51, 17);
            this.cbHasBorder.TabIndex = 40;
            this.cbHasBorder.Tag = "all";
            this.cbHasBorder.Text = "Draw";
            this.cbHasBorder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbHasBorder.UseVisualStyleBackColor = false;
            this.cbHasBorder.CheckedChanged += new System.EventHandler(this.generic_CheckBox_CheckChange);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(88, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Border Color";
            // 
            // rbPath
            // 
            this.rbPath.AutoSize = true;
            this.rbPath.Location = new System.Drawing.Point(128, 19);
            this.rbPath.Name = "rbPath";
            this.rbPath.Size = new System.Drawing.Size(47, 17);
            this.rbPath.TabIndex = 43;
            this.rbPath.Text = "Path";
            this.rbPath.UseVisualStyleBackColor = true;
            this.rbPath.CheckedChanged += new System.EventHandler(this.genericRadioButton_CheckChange);
            // 
            // rbSolid
            // 
            this.rbSolid.AutoSize = true;
            this.rbSolid.Checked = true;
            this.rbSolid.Location = new System.Drawing.Point(14, 19);
            this.rbSolid.Name = "rbSolid";
            this.rbSolid.Size = new System.Drawing.Size(48, 17);
            this.rbSolid.TabIndex = 45;
            this.rbSolid.TabStop = true;
            this.rbSolid.Text = "Solid";
            this.rbSolid.UseVisualStyleBackColor = true;
            this.rbSolid.CheckedChanged += new System.EventHandler(this.genericRadioButton_CheckChange);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpGeneral);
            this.tabControl1.Controls.Add(this.tpBrush);
            this.tabControl1.Controls.Add(this.tpBlend);
            this.tabControl1.Location = new System.Drawing.Point(517, -1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(227, 547);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tpGeneral
            // 
            this.tpGeneral.BackColor = System.Drawing.Color.Transparent;
            this.tpGeneral.Controls.Add(this.groupBox1);
            this.tpGeneral.Controls.Add(this.groupBox4);
            this.tpGeneral.Controls.Add(this.groupBox3);
            this.tpGeneral.Controls.Add(this.groupBox2);
            this.tpGeneral.Location = new System.Drawing.Point(4, 4);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tpGeneral.Size = new System.Drawing.Size(219, 521);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General Properties";
            // 
            // tpBrush
            // 
            this.tpBrush.BackColor = System.Drawing.Color.Transparent;
            this.tpBrush.Controls.Add(this.brushManager1);
            this.tpBrush.Location = new System.Drawing.Point(4, 4);
            this.tpBrush.Name = "tpBrush";
            this.tpBrush.Padding = new System.Windows.Forms.Padding(3);
            this.tpBrush.Size = new System.Drawing.Size(219, 521);
            this.tpBrush.TabIndex = 1;
            this.tpBrush.Text = "Brushes";
            // 
            // brushManager1
            // 
            shapeBrush1.Angle = 90F;
            shapeBrush1.BrushName = null;
            shapeBrush1.CenterPoint = ((System.Drawing.PointF)(resources.GetObject("shapeBrush1.CenterPoint")));
            shapeBrush1.Color1 = System.Drawing.Color.Black;
            shapeBrush1.Color2 = System.Drawing.Color.Black;
            shapeBrush1.Factors = ((System.Collections.Generic.List<float>)(resources.GetObject("shapeBrush1.Factors")));
            shapeBrush1.FocusScale = ((System.Drawing.PointF)(resources.GetObject("shapeBrush1.FocusScale")));
            shapeBrush1.LinearFocus = 0.5F;
            shapeBrush1.LinearScale = 0.5F;
            shapeBrush1.Positions = ((System.Collections.Generic.List<float>)(resources.GetObject("shapeBrush1.Positions")));
            shapeBrush1.ReferenceRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            shapeBrush1.ShapeColor = System.Drawing.Color.White;
            shapeBrush1.ShapeFillType = ShapesClassLib.FillType.solid;
            shapeBrush1.SurroundingColors = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("shapeBrush1.SurroundingColors")));
            shapeBrush1.UseBlend = false;
            shapeBrush1.UseBlendTriangle = false;
            shapeBrush1.UseColorBlend = false;
            shapeBrush1.UseGammaCorrection = false;
            shapeBrush1.UseSigmaBell = false;
            this.brushManager1.CurrentBrush = shapeBrush1;
            this.brushManager1.Location = new System.Drawing.Point(6, 6);
            this.brushManager1.Name = "brushManager1";
            this.brushManager1.Size = new System.Drawing.Size(200, 462);
            this.brushManager1.TabIndex = 0;
            this.brushManager1.OnBrushSelected += new SketcherControlLib.MiscControls.BrushManager.OnBrushSelectedEventHandler(this.bm_OnBrushSelected);
            // 
            // tpBlend
            // 
            this.tpBlend.BackColor = System.Drawing.Color.Transparent;
            this.tpBlend.Controls.Add(this.blendManager1);
            this.tpBlend.Location = new System.Drawing.Point(4, 4);
            this.tpBlend.Name = "tpBlend";
            this.tpBlend.Size = new System.Drawing.Size(219, 521);
            this.tpBlend.TabIndex = 2;
            this.tpBlend.Text = "Blend";
            // 
            // blendManager1
            // 
            this.blendManager1.ColorDialogReference = null;
            this.blendManager1.CurrentShape = null;
            this.blendManager1.Location = new System.Drawing.Point(0, 3);
            this.blendManager1.Name = "blendManager1";
            this.blendManager1.Size = new System.Drawing.Size(219, 393);
            this.blendManager1.TabIndex = 2;
            this.blendManager1.OnBlendEditorStyleChange += new SketcherControlLib.MiscControls.BlendManager.OnBlendEditorStyleChangeEventHandler(this.blendManager1_OnBlendEditorStyleChange);
            this.blendManager1.OnBlendAction += new SketcherControlLib.MiscControls.BlendManager.OnBlendActionEventHandler(this.blendEd_OnBlendAction);
            // 
            // statusStripControl1
            // 
            this.statusStripControl1.AutoSize = true;
            this.statusStripControl1.BackColor = System.Drawing.Color.Transparent;
            this.statusStripControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusStripControl1.CurrentShape = null;
            this.statusStripControl1.Enabled = false;
            this.statusStripControl1.Location = new System.Drawing.Point(-1, 519);
            this.statusStripControl1.Name = "statusStripControl1";
            this.statusStripControl1.Size = new System.Drawing.Size(518, 27);
            this.statusStripControl1.TabIndex = 2;
            this.statusStripControl1.OnShapeScroll += new SketcherControlLib.MiscControls.StatusStripControl.OnShapeScrollEventHandler(this.statusStripControl1_OnShapeScroll);
            this.statusStripControl1.OnStatusControlValueChange += new SketcherControlLib.MiscControls.StatusStripControl.OnStatusControlValueChangeEventHandler(this.statusStripControl1_OnStatusControlValueChange);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 27);
            // 
            // SketcherControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(251)))), ((int)(((byte)(239)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStripControl1);
            this.DoubleBuffered = true;
            this.Name = "SketcherControl";
            this.Size = new System.Drawing.Size(743, 545);
            this.Load += new System.EventHandler(this.SketcherControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SketcherControl_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.control_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.control_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.control_MouseUp);
            this.SizeChanged += new System.EventHandler(this.SketcherControl_SizeChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.tpBrush.ResumeLayout(false);
            this.tpBlend.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbPointer;
        private System.Windows.Forms.ToolStripButton tsbRectangle;
        private System.Windows.Forms.ToolStripButton tsbRoundedRectangle;
        private System.Windows.Forms.ToolStripButton tsbEllipse;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbText;
        private System.Windows.Forms.ToolStripButton tsbTextEdit;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemove;
        private System.Windows.Forms.ToolStripMenuItem tsbBringToFront;
        private System.Windows.Forms.ToolStripMenuItem tsbSendToBack;
        private System.Windows.Forms.ToolStripMenuItem tsbClone;
        private System.Windows.Forms.ToolStripButton tsbImage;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripButton tsbImportImage;
        private SketcherControlLib.MiscControls.StatusStripControl statusStripControl1;
        private System.Windows.Forms.ToolStripDropDownButton tsmiFileOptions;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewProject;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoad;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiImport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmiOptions;
        private System.Windows.Forms.ToolStripButton tsbEditorColor;
        private System.Windows.Forms.RadioButton rbSolid;
        private System.Windows.Forms.RadioButton rbPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbHasBorder;
        private System.Windows.Forms.RadioButton rbLinear;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbSigma;
        private System.Windows.Forms.CheckBox cbGamma;
        private System.Windows.Forms.CheckBox cbTriangular;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private CustomSliderControlLib.SliderControl scAngle;
        private CustomSliderControlLib.SliderControl scFocus;
        private CustomSliderControlLib.SliderControl scScale;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbSelectBoth;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private CustomSliderControlLib.SliderControl scPathFocusScalesX;
        private CustomSliderControlLib.SliderControl scPathFocusScalesY;
        private CustomSliderControlLib.SliderControl scPathFocus;
        private CustomSliderControlLib.SliderControl scPathScale;
        private System.Windows.Forms.CheckBox cbPathTriangular;
        private System.Windows.Forms.CheckBox cbPathSigma;
        private System.Windows.Forms.CheckBox cbPathSelectBoth;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSwatchBorder;
        private System.Windows.Forms.Button btnSwatchCenterColor;
        private System.Windows.Forms.Button btnSwatchOuterColor;
        private System.Windows.Forms.Button btnSwatchFillColor;
        private System.Windows.Forms.Button btnSwatchEndColor;
        private System.Windows.Forms.Button btnSwatchStartColor;
        private System.Windows.Forms.ToolStripButton tsbHelp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveBrush;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.TabPage tpBrush;
        private SketcherControlLib.MiscControls.BrushManager brushManager1;
        private System.Windows.Forms.TabPage tpBlend;
        private SketcherControlLib.MiscControls.BlendManager blendManager1;
        private System.Windows.Forms.ToolStripLabel tslCaption;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    }
}
