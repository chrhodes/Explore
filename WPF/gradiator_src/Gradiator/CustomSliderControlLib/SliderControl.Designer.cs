namespace CustomSliderControlLib
{
    partial class SliderControl
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
            this.SuspendLayout();
            // 
            // SliderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.DoubleBuffered = true;
            this.Name = "SliderControl";
            this.Size = new System.Drawing.Size(300, 24);
            this.MouseLeave += new System.EventHandler(this.SliderControl_MouseLeave);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SliderControl_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SliderControl_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SliderControl_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SliderControl_MouseUp);
            this.EnabledChanged += new System.EventHandler(this.SliderControl_EnabledChanged);
            this.SizeChanged += new System.EventHandler(this.SliderControl_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
