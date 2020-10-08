namespace CustomColorSelectorLib
{
    partial class ColorSelector
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.scSaturation = new CustomSliderControlLib.SliderControl();
            this.scBrightness = new CustomSliderControlLib.SliderControl();
            this.scHue = new CustomSliderControlLib.SliderControl();
            this.scAlpha = new CustomSliderControlLib.SliderControl();
            this.scGreen = new CustomSliderControlLib.SliderControl();
            this.scBlue = new CustomSliderControlLib.SliderControl();
            this.scRed = new CustomSliderControlLib.SliderControl();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.btnSavePalette = new System.Windows.Forms.Button();
            this.btnLoadPalette = new System.Windows.Forms.Button();
            this.btnNewPalette = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // scSaturation
            // 
            this.scSaturation.BackColor = System.Drawing.Color.Transparent;
            this.scSaturation.Caption = "Saturation";
            this.scSaturation.CurrentValue = 255F;
            this.scSaturation.IsValueFloat = false;
            this.scSaturation.Location = new System.Drawing.Point(13, 243);
            this.scSaturation.MaxValue = 255F;
            this.scSaturation.MinValue = 0F;
            this.scSaturation.Name = "scSaturation";
            this.scSaturation.Size = new System.Drawing.Size(172, 24);
            this.scSaturation.TabIndex = 7;
            this.scSaturation.Visible = false;
            this.scSaturation.OnValueChange += new CustomSliderControlLib.SliderControl.OnValueChangeEventHandler(this.genericHSVSlider_OnValueChage);
            // 
            // scBrightness
            // 
            this.scBrightness.BackColor = System.Drawing.Color.Transparent;
            this.scBrightness.Caption = "Brightness";
            this.scBrightness.CurrentValue = 255F;
            this.scBrightness.IsValueFloat = false;
            this.scBrightness.Location = new System.Drawing.Point(13, 273);
            this.scBrightness.MaxValue = 255F;
            this.scBrightness.MinValue = 0F;
            this.scBrightness.Name = "scBrightness";
            this.scBrightness.Size = new System.Drawing.Size(172, 24);
            this.scBrightness.TabIndex = 6;
            this.scBrightness.Visible = false;
            this.scBrightness.OnValueChange += new CustomSliderControlLib.SliderControl.OnValueChangeEventHandler(this.genericHSVSlider_OnValueChage);
            // 
            // scHue
            // 
            this.scHue.BackColor = System.Drawing.Color.Transparent;
            this.scHue.Caption = "Hue";
            this.scHue.CurrentValue = 360F;
            this.scHue.IsValueFloat = false;
            this.scHue.Location = new System.Drawing.Point(13, 213);
            this.scHue.MaxValue = 360F;
            this.scHue.MinValue = 0F;
            this.scHue.Name = "scHue";
            this.scHue.Size = new System.Drawing.Size(172, 24);
            this.scHue.TabIndex = 5;
            this.scHue.Visible = false;
            this.scHue.OnValueChange += new CustomSliderControlLib.SliderControl.OnValueChangeEventHandler(this.genericHSVSlider_OnValueChage);
            // 
            // scAlpha
            // 
            this.scAlpha.BackColor = System.Drawing.Color.Transparent;
            this.scAlpha.Caption = "Alpha";
            this.scAlpha.CurrentValue = 255F;
            this.scAlpha.IsValueFloat = false;
            this.scAlpha.Location = new System.Drawing.Point(13, 166);
            this.scAlpha.MaxValue = 255F;
            this.scAlpha.MinValue = 0F;
            this.scAlpha.Name = "scAlpha";
            this.scAlpha.Size = new System.Drawing.Size(172, 24);
            this.scAlpha.TabIndex = 4;
            this.scAlpha.OnValueChange += new CustomSliderControlLib.SliderControl.OnValueChangeEventHandler(this.AlphaSlider_OnValueChange);
            // 
            // scGreen
            // 
            this.scGreen.BackColor = System.Drawing.Color.Transparent;
            this.scGreen.Caption = "Green";
            this.scGreen.CurrentValue = 255F;
            this.scGreen.IsValueFloat = false;
            this.scGreen.Location = new System.Drawing.Point(13, 243);
            this.scGreen.MaxValue = 255F;
            this.scGreen.MinValue = 0F;
            this.scGreen.Name = "scGreen";
            this.scGreen.Size = new System.Drawing.Size(172, 24);
            this.scGreen.TabIndex = 3;
            this.scGreen.OnValueChange += new CustomSliderControlLib.SliderControl.OnValueChangeEventHandler(this.genericRGBSlider_OnValueChange);
            // 
            // scBlue
            // 
            this.scBlue.BackColor = System.Drawing.Color.Transparent;
            this.scBlue.Caption = "Blue";
            this.scBlue.CurrentValue = 255F;
            this.scBlue.IsValueFloat = false;
            this.scBlue.Location = new System.Drawing.Point(13, 273);
            this.scBlue.MaxValue = 255F;
            this.scBlue.MinValue = 0F;
            this.scBlue.Name = "scBlue";
            this.scBlue.Size = new System.Drawing.Size(172, 24);
            this.scBlue.TabIndex = 2;
            this.scBlue.OnValueChange += new CustomSliderControlLib.SliderControl.OnValueChangeEventHandler(this.genericRGBSlider_OnValueChange);
            // 
            // scRed
            // 
            this.scRed.BackColor = System.Drawing.Color.Transparent;
            this.scRed.Caption = "Red";
            this.scRed.CurrentValue = 255F;
            this.scRed.IsValueFloat = false;
            this.scRed.Location = new System.Drawing.Point(13, 213);
            this.scRed.MaxValue = 255F;
            this.scRed.MinValue = 0F;
            this.scRed.Name = "scRed";
            this.scRed.Size = new System.Drawing.Size(172, 24);
            this.scRed.TabIndex = 1;
            this.scRed.OnValueChange += new CustomSliderControlLib.SliderControl.OnValueChangeEventHandler(this.genericRGBSlider_OnValueChange);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.BackColor = System.Drawing.Color.Transparent;
            this.radioButton1.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(100, 193);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(49, 20);
            this.radioButton1.TabIndex = 8;
            this.radioButton1.Text = "HSB";
            this.radioButton1.UseVisualStyleBackColor = false;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.RadioButton_CheckChange);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.BackColor = System.Drawing.Color.Transparent;
            this.radioButton2.Checked = true;
            this.radioButton2.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(31, 193);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(49, 20);
            this.radioButton2.TabIndex = 9;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "RGB";
            this.radioButton2.UseVisualStyleBackColor = false;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.RadioButton_CheckChange);
            // 
            // btnSavePalette
            // 
            this.btnSavePalette.BackColor = System.Drawing.SystemColors.Control;
            this.btnSavePalette.Image = global::CustomColorSelectorLib.Properties.Resources.save1;
            this.btnSavePalette.Location = new System.Drawing.Point(13, 56);
            this.btnSavePalette.Name = "btnSavePalette";
            this.btnSavePalette.Size = new System.Drawing.Size(24, 24);
            this.btnSavePalette.TabIndex = 12;
            this.btnSavePalette.UseVisualStyleBackColor = false;
            this.btnSavePalette.Click += new System.EventHandler(this.genericButton_Click);
            // 
            // btnLoadPalette
            // 
            this.btnLoadPalette.Image = global::CustomColorSelectorLib.Properties.Resources.open1;
            this.btnLoadPalette.Location = new System.Drawing.Point(13, 33);
            this.btnLoadPalette.Name = "btnLoadPalette";
            this.btnLoadPalette.Size = new System.Drawing.Size(24, 24);
            this.btnLoadPalette.TabIndex = 11;
            this.btnLoadPalette.UseVisualStyleBackColor = true;
            this.btnLoadPalette.Click += new System.EventHandler(this.genericButton_Click);
            // 
            // btnNewPalette
            // 
            this.btnNewPalette.BackColor = System.Drawing.SystemColors.Control;
            this.btnNewPalette.Image = global::CustomColorSelectorLib.Properties.Resources._new;
            this.btnNewPalette.Location = new System.Drawing.Point(13, 10);
            this.btnNewPalette.Name = "btnNewPalette";
            this.btnNewPalette.Size = new System.Drawing.Size(24, 24);
            this.btnNewPalette.TabIndex = 10;
            this.btnNewPalette.UseVisualStyleBackColor = false;
            this.btnNewPalette.Click += new System.EventHandler(this.genericButton_Click);
            // 
            // ColorSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSavePalette);
            this.Controls.Add(this.btnLoadPalette);
            this.Controls.Add(this.btnNewPalette);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.scSaturation);
            this.Controls.Add(this.scBrightness);
            this.Controls.Add(this.scHue);
            this.Controls.Add(this.scAlpha);
            this.Controls.Add(this.scGreen);
            this.Controls.Add(this.scBlue);
            this.Controls.Add(this.scRed);
            this.DoubleBuffered = true;
            this.Name = "ColorSelector";
            this.Size = new System.Drawing.Size(197, 307);
            this.Load += new System.EventHandler(this.ColorSelector_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ColorSelector_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ColorSelector_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private CustomSliderControlLib.SliderControl scRed;
        private CustomSliderControlLib.SliderControl scBlue;
        private CustomSliderControlLib.SliderControl scGreen;
        private CustomSliderControlLib.SliderControl scAlpha;
        private CustomSliderControlLib.SliderControl scSaturation;
        private CustomSliderControlLib.SliderControl scBrightness;
        private CustomSliderControlLib.SliderControl scHue;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Button btnNewPalette;
        private System.Windows.Forms.Button btnLoadPalette;
        private System.Windows.Forms.Button btnSavePalette;
    }
}
