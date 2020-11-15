namespace PatternsOfParallelProgrammingWinForms
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInclusiveLowerBound = new System.Windows.Forms.TextBox();
            this.txtExclusiveUpperBound = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnGoParallel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "To";
            // 
            // txtInclusiveLowerBound
            // 
            this.txtInclusiveLowerBound.Location = new System.Drawing.Point(69, 8);
            this.txtInclusiveLowerBound.Name = "txtInclusiveLowerBound";
            this.txtInclusiveLowerBound.Size = new System.Drawing.Size(61, 20);
            this.txtInclusiveLowerBound.TabIndex = 2;
            // 
            // txtExclusiveUpperBound
            // 
            this.txtExclusiveUpperBound.Location = new System.Drawing.Point(216, 8);
            this.txtExclusiveUpperBound.Name = "txtExclusiveUpperBound";
            this.txtExclusiveUpperBound.Size = new System.Drawing.Size(69, 20);
            this.txtExclusiveUpperBound.TabIndex = 3;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(292, 48);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(212, 372);
            this.txtOutput.TabIndex = 4;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(16, 97);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(93, 23);
            this.btnGo.TabIndex = 5;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnGoParallel
            // 
            this.btnGoParallel.Location = new System.Drawing.Point(16, 126);
            this.btnGoParallel.Name = "btnGoParallel";
            this.btnGoParallel.Size = new System.Drawing.Size(93, 23);
            this.btnGoParallel.TabIndex = 6;
            this.btnGoParallel.Text = "Go Parallel";
            this.btnGoParallel.UseVisualStyleBackColor = true;
            this.btnGoParallel.Click += new System.EventHandler(this.btnGoParallel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 448);
            this.Controls.Add(this.btnGoParallel);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.txtExclusiveUpperBound);
            this.Controls.Add(this.txtInclusiveLowerBound);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInclusiveLowerBound;
        private System.Windows.Forms.TextBox txtExclusiveUpperBound;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnGoParallel;
    }
}

