namespace SketcherControlLib
{
    partial class ColorSelectorDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.colorSelector1 = new CustomColorSelectorLib.ColorSelector();
            this.SuspendLayout();
            // 
            // colorSelector1
            // 
            this.colorSelector1.ControlBackColor = System.Drawing.SystemColors.Control;
            this.colorSelector1.ControlBorder = false;
            this.colorSelector1.Location = new System.Drawing.Point(0, 0);
            this.colorSelector1.Name = "colorSelector1";
            this.colorSelector1.PickerBackColor = System.Drawing.Color.LightSteelBlue;
            this.colorSelector1.PrimaryColor = System.Drawing.Color.White;
            this.colorSelector1.SecondaryColor = System.Drawing.Color.Black;
            this.colorSelector1.Size = new System.Drawing.Size(197, 307);
            this.colorSelector1.TabIndex = 0;
            // 
            // ColorSelectorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(199, 310);
            this.Controls.Add(this.colorSelector1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColorSelectorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Color Selector Dialog";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ColorSelectorDialog_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ColorSelectorDialog_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        public CustomColorSelectorLib.ColorSelector colorSelector1;

    }
}