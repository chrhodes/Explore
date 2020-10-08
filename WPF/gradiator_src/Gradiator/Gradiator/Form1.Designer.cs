namespace Gradiator
{
    partial class Form1
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
            this.sketcherControl1 = new SketcherControlLib.SketcherControl();
            this.SuspendLayout();
            // 
            // sketcherControl1
            // 
            this.sketcherControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(251)))), ((int)(((byte)(239)))));
            this.sketcherControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sketcherControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sketcherControl1.Location = new System.Drawing.Point(0, 0);
            this.sketcherControl1.Name = "sketcherControl1";
            this.sketcherControl1.ShapeSelectionList = null;
            this.sketcherControl1.Size = new System.Drawing.Size(806, 527);
            this.sketcherControl1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 527);
            this.Controls.Add(this.sketcherControl1);
            this.DoubleBuffered = true;
            this.HelpButton = true;
            this.MinimumSize = new System.Drawing.Size(750, 460);
            this.Name = "Form1";
            this.Text = "Gradiator Version 2.0 \"Gradients Made Easy\" by Mike Hankey";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private SketcherControlLib.SketcherControl sketcherControl1;
    }
}

