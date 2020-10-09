namespace TestSystemManagementWebServiceWinForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.cbMethod = new System.Windows.Forms.ComboBox();
            this.cbHost = new System.Windows.Forms.ComboBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.lblWMIService = new System.Windows.Forms.Label();
            this.txtReflectTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(755, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Get WMI Info";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbMethod
            // 
            this.cbMethod.FormattingEnabled = true;
            this.cbMethod.Location = new System.Drawing.Point(12, 22);
            this.cbMethod.Name = "cbMethod";
            this.cbMethod.Size = new System.Drawing.Size(233, 21);
            this.cbMethod.TabIndex = 62;
            // 
            // cbHost
            // 
            this.cbHost.FormattingEnabled = true;
            this.cbHost.Location = new System.Drawing.Point(251, 22);
            this.cbHost.Name = "cbHost";
            this.cbHost.Size = new System.Drawing.Size(361, 21);
            this.cbHost.TabIndex = 60;
            this.cbHost.Text = "localhost";
            this.cbHost.SelectedIndexChanged += new System.EventHandler(this.cbHost_SelectedIndexChanged);
            // 
            // txtOutput
            // 
            this.txtOutput.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.Location = new System.Drawing.Point(12, 86);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(852, 877);
            this.txtOutput.TabIndex = 65;
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(755, 37);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(109, 20);
            this.txtTime.TabIndex = 64;
            // 
            // lblWMIService
            // 
            this.lblWMIService.AutoSize = true;
            this.lblWMIService.Location = new System.Drawing.Point(9, 70);
            this.lblWMIService.Name = "lblWMIService";
            this.lblWMIService.Size = new System.Drawing.Size(69, 13);
            this.lblWMIService.TabIndex = 63;
            this.lblWMIService.Text = "WMI Service";
            // 
            // txtReflectTime
            // 
            this.txtReflectTime.Location = new System.Drawing.Point(755, 63);
            this.txtReflectTime.Name = "txtReflectTime";
            this.txtReflectTime.Size = new System.Drawing.Size(109, 20);
            this.txtReflectTime.TabIndex = 66;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 67;
            this.label1.Text = "WebService";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 68;
            this.label2.Text = "Host";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(663, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 69;
            this.label4.Text = "WS Duration (ms)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(660, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 70;
            this.label3.Text = "Post Duration (ms)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 975);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtReflectTime);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.lblWMIService);
            this.Controls.Add(this.cbMethod);
            this.Controls.Add(this.cbHost);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbMethod;
        private System.Windows.Forms.ComboBox cbHost;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Label lblWMIService;
        private System.Windows.Forms.TextBox txtReflectTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}

