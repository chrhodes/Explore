namespace EACApplicationTemplateCSharp1.User_Interface
{
    partial class ucStarterScreen
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
            if(disposing && (components != null))
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnReturnToSender = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // btnReturnToSender
            // 
            this.btnReturnToSender.Location = new System.Drawing.Point(541, 454);
            this.btnReturnToSender.Name = "btnReturnToSender";
            this.btnReturnToSender.Size = new System.Drawing.Size(96, 23);
            this.btnReturnToSender.TabIndex = 0;
            this.btnReturnToSender.Text = "Return to Sender";
            this.btnReturnToSender.UseVisualStyleBackColor = true;
            this.btnReturnToSender.Click += new System.EventHandler(this.btnReturnToSender_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.BackgroundImage = Properties.Resources.Info;
            this.btnInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnInfo.Location = new System.Drawing.Point(611, 19);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(17, 23);
            this.btnInfo.TabIndex = 36;
            this.btnInfo.UseVisualStyleBackColor = true;
            // 
            // ucStarterScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReturnToSender);
            this.Name = "ucStarterScreen";
            this.Size = new System.Drawing.Size(640, 480);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReturnToSender;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnInfo;

    }
}
