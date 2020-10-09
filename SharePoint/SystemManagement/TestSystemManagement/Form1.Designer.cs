namespace TestSystemManagementWindowsForm
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
            this.lblWMIService = new System.Windows.Forms.Label();
            this.cbHost = new System.Windows.Forms.ComboBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.cbWMIProvider = new System.Windows.Forms.ComboBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.cbWMIService = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(805, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Get WMI Info";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblWMIService
            // 
            this.lblWMIService.AutoSize = true;
            this.lblWMIService.Location = new System.Drawing.Point(12, 59);
            this.lblWMIService.Name = "lblWMIService";
            this.lblWMIService.Size = new System.Drawing.Size(69, 13);
            this.lblWMIService.TabIndex = 15;
            this.lblWMIService.Text = "WMI Service";
            // 
            // cbHost
            // 
            this.cbHost.FormattingEnabled = true;
            this.cbHost.Location = new System.Drawing.Point(493, 20);
            this.cbHost.Name = "cbHost";
            this.cbHost.Size = new System.Drawing.Size(306, 21);
            this.cbHost.TabIndex = 20;
            this.cbHost.SelectedIndexChanged += new System.EventHandler(this.cbHost_SelectedIndexChanged);
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(805, 49);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(104, 20);
            this.txtTime.TabIndex = 41;
            // 
            // cbWMIProvider
            // 
            this.cbWMIProvider.FormattingEnabled = true;
            this.cbWMIProvider.Items.AddRange(new object[] {
            "\\root\\cimv2",
            "\\root\\MicrosoftIISv2"});
            this.cbWMIProvider.Location = new System.Drawing.Point(15, 20);
            this.cbWMIProvider.Name = "cbWMIProvider";
            this.cbWMIProvider.Size = new System.Drawing.Size(233, 21);
            this.cbWMIProvider.TabIndex = 57;
            // 
            // txtOutput
            // 
            this.txtOutput.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.Location = new System.Drawing.Point(12, 75);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(897, 865);
            this.txtOutput.TabIndex = 58;
            // 
            // cbWMIService
            // 
            this.cbWMIService.FormattingEnabled = true;
            this.cbWMIService.Location = new System.Drawing.Point(254, 20);
            this.cbWMIService.Name = "cbWMIService";
            this.cbWMIService.Size = new System.Drawing.Size(233, 21);
            this.cbWMIService.TabIndex = 59;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 60;
            this.label1.Text = "WMI Provider";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 61;
            this.label2.Text = "WMI Class";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(490, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 62;
            this.label3.Text = "Host";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(730, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 63;
            this.label4.Text = "Duration (ms)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 952);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbWMIService);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.cbWMIProvider);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.cbHost);
            this.Controls.Add(this.lblWMIService);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblWMIService;
        private System.Windows.Forms.ComboBox cbHost;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.ComboBox cbWMIProvider;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.ComboBox cbWMIService;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

