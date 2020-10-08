namespace EACApplicationTemplateCSharp1.User_Interface
{
    partial class frmDebugWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClearOutput = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.chkDebugLevel1 = new System.Windows.Forms.CheckBox();
            this.chkDebugLevel2 = new System.Windows.Forms.CheckBox();
            this.chkDebugSQL = new System.Windows.Forms.CheckBox();
            this.gbDebugOptions = new System.Windows.Forms.GroupBox();
            this.btnGetAllConfigInfo = new System.Windows.Forms.Button();
            this.gbDebugOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClearOutput
            // 
            this.btnClearOutput.Location = new System.Drawing.Point(12, 12);
            this.btnClearOutput.Name = "btnClearOutput";
            this.btnClearOutput.Size = new System.Drawing.Size(178, 23);
            this.btnClearOutput.TabIndex = 7;
            this.btnClearOutput.Text = "Clear Output";
            this.btnClearOutput.UseVisualStyleBackColor = true;
            this.btnClearOutput.Click += new System.EventHandler(this.btnClearOutput_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(196, 12);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(576, 538);
            this.txtOutput.TabIndex = 6;
            // 
            // chkDebugLevel1
            // 
            this.chkDebugLevel1.AutoSize = true;
            this.chkDebugLevel1.Location = new System.Drawing.Point(16, 48);
            this.chkDebugLevel1.Name = "chkDebugLevel1";
            this.chkDebugLevel1.Size = new System.Drawing.Size(96, 17);
            this.chkDebugLevel1.TabIndex = 2;
            this.chkDebugLevel1.Text = "Debug Level 1";
            this.chkDebugLevel1.UseVisualStyleBackColor = true;
            this.chkDebugLevel1.CheckedChanged += new System.EventHandler(this.chkDebugLevel1_CheckedChanged);
            // 
            // chkDebugLevel2
            // 
            this.chkDebugLevel2.AutoSize = true;
            this.chkDebugLevel2.Location = new System.Drawing.Point(16, 71);
            this.chkDebugLevel2.Name = "chkDebugLevel2";
            this.chkDebugLevel2.Size = new System.Drawing.Size(96, 17);
            this.chkDebugLevel2.TabIndex = 1;
            this.chkDebugLevel2.Text = "Debug Level 2";
            this.chkDebugLevel2.UseVisualStyleBackColor = true;
            this.chkDebugLevel2.CheckedChanged += new System.EventHandler(this.chkDebugLevel2_CheckedChanged);
            // 
            // chkDebugSQL
            // 
            this.chkDebugSQL.AutoSize = true;
            this.chkDebugSQL.Location = new System.Drawing.Point(16, 25);
            this.chkDebugSQL.Name = "chkDebugSQL";
            this.chkDebugSQL.Size = new System.Drawing.Size(82, 17);
            this.chkDebugSQL.TabIndex = 3;
            this.chkDebugSQL.Text = "Debug SQL";
            this.chkDebugSQL.UseVisualStyleBackColor = true;
            this.chkDebugSQL.CheckedChanged += new System.EventHandler(this.chkDebugSQL_CheckedChanged);
            // 
            // gbDebugOptions
            // 
            this.gbDebugOptions.Controls.Add(this.chkDebugLevel1);
            this.gbDebugOptions.Controls.Add(this.chkDebugLevel2);
            this.gbDebugOptions.Controls.Add(this.chkDebugSQL);
            this.gbDebugOptions.Location = new System.Drawing.Point(12, 53);
            this.gbDebugOptions.Name = "gbDebugOptions";
            this.gbDebugOptions.Size = new System.Drawing.Size(178, 130);
            this.gbDebugOptions.TabIndex = 8;
            this.gbDebugOptions.TabStop = false;
            this.gbDebugOptions.Text = "Debug Options";
            // 
            // btnGetAllConfigInfo
            // 
            this.btnGetAllConfigInfo.Location = new System.Drawing.Point(12, 189);
            this.btnGetAllConfigInfo.Name = "btnGetAllConfigInfo";
            this.btnGetAllConfigInfo.Size = new System.Drawing.Size(178, 23);
            this.btnGetAllConfigInfo.TabIndex = 10;
            this.btnGetAllConfigInfo.Text = "Get All Config Info";
            this.btnGetAllConfigInfo.UseVisualStyleBackColor = true;
            this.btnGetAllConfigInfo.Click += new System.EventHandler(this.btnGetAllConfigInfo_Click);
            // 
            // frmDebugWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.btnGetAllConfigInfo);
            this.Controls.Add(this.btnClearOutput);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.gbDebugOptions);
            this.Name = "frmDebugWindow";
            this.Text = "frmDebugWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDebugWindow_FormClosing);
            this.gbDebugOptions.ResumeLayout(false);
            this.gbDebugOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnClearOutput;
        internal System.Windows.Forms.TextBox txtOutput;
        internal System.Windows.Forms.CheckBox chkDebugLevel1;
        internal System.Windows.Forms.CheckBox chkDebugLevel2;
        internal System.Windows.Forms.CheckBox chkDebugSQL;
        internal System.Windows.Forms.GroupBox gbDebugOptions;
        private System.Windows.Forms.Button btnGetAllConfigInfo;
    }
}