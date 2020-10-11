namespace WiredBrainCoffee.ComputerInfoTool
{
  partial class MainForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.lblUserName = new System.Windows.Forms.Label();
      this.txtUserName = new System.Windows.Forms.TextBox();
      this.lblComputerName = new System.Windows.Forms.Label();
      this.txtComputerName = new System.Windows.Forms.TextBox();
      this.lblIPAddresses = new System.Windows.Forms.Label();
      this.txtIPAddresses = new System.Windows.Forms.TextBox();
      this.btnCopyToClipboard = new System.Windows.Forms.Button();
      this.pnlHeader = new System.Windows.Forms.Panel();
      this.lblTitle = new System.Windows.Forms.Label();
      this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
      this.pnlMainArea = new System.Windows.Forms.Panel();
      this.pnlHeader.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
      this.pnlMainArea.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblUserName
      // 
      this.lblUserName.AutoSize = true;
      this.lblUserName.ForeColor = System.Drawing.Color.White;
      this.lblUserName.Location = new System.Drawing.Point(34, 18);
      this.lblUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblUserName.Name = "lblUserName";
      this.lblUserName.Size = new System.Drawing.Size(102, 24);
      this.lblUserName.TabIndex = 1;
      this.lblUserName.Text = "Username:";
      // 
      // txtUserName
      // 
      this.txtUserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
      this.txtUserName.ForeColor = System.Drawing.Color.White;
      this.txtUserName.Location = new System.Drawing.Point(147, 14);
      this.txtUserName.Margin = new System.Windows.Forms.Padding(4);
      this.txtUserName.Name = "txtUserName";
      this.txtUserName.ReadOnly = true;
      this.txtUserName.Size = new System.Drawing.Size(257, 29);
      this.txtUserName.TabIndex = 2;
      // 
      // lblComputerName
      // 
      this.lblComputerName.AutoSize = true;
      this.lblComputerName.ForeColor = System.Drawing.Color.White;
      this.lblComputerName.Location = new System.Drawing.Point(444, 18);
      this.lblComputerName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblComputerName.Name = "lblComputerName";
      this.lblComputerName.Size = new System.Drawing.Size(146, 24);
      this.lblComputerName.TabIndex = 3;
      this.lblComputerName.Text = "Computername:";
      // 
      // txtComputerName
      // 
      this.txtComputerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtComputerName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
      this.txtComputerName.ForeColor = System.Drawing.Color.White;
      this.txtComputerName.Location = new System.Drawing.Point(599, 14);
      this.txtComputerName.Margin = new System.Windows.Forms.Padding(4);
      this.txtComputerName.Name = "txtComputerName";
      this.txtComputerName.ReadOnly = true;
      this.txtComputerName.Size = new System.Drawing.Size(310, 29);
      this.txtComputerName.TabIndex = 4;
      // 
      // lblIPAddresses
      // 
      this.lblIPAddresses.AutoSize = true;
      this.lblIPAddresses.ForeColor = System.Drawing.Color.White;
      this.lblIPAddresses.Location = new System.Drawing.Point(11, 76);
      this.lblIPAddresses.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblIPAddresses.Name = "lblIPAddresses";
      this.lblIPAddresses.Size = new System.Drawing.Size(123, 24);
      this.lblIPAddresses.TabIndex = 5;
      this.lblIPAddresses.Text = "IP addresses:";
      // 
      // txtIPAddresses
      // 
      this.txtIPAddresses.AcceptsReturn = true;
      this.txtIPAddresses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtIPAddresses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
      this.txtIPAddresses.ForeColor = System.Drawing.Color.White;
      this.txtIPAddresses.Location = new System.Drawing.Point(147, 70);
      this.txtIPAddresses.Margin = new System.Windows.Forms.Padding(4);
      this.txtIPAddresses.Multiline = true;
      this.txtIPAddresses.Name = "txtIPAddresses";
      this.txtIPAddresses.ReadOnly = true;
      this.txtIPAddresses.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtIPAddresses.Size = new System.Drawing.Size(762, 195);
      this.txtIPAddresses.TabIndex = 6;
      // 
      // btnCopyToClipboard
      // 
      this.btnCopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnCopyToClipboard.Location = new System.Drawing.Point(147, 295);
      this.btnCopyToClipboard.Margin = new System.Windows.Forms.Padding(4);
      this.btnCopyToClipboard.Name = "btnCopyToClipboard";
      this.btnCopyToClipboard.Size = new System.Drawing.Size(308, 50);
      this.btnCopyToClipboard.TabIndex = 0;
      this.btnCopyToClipboard.Text = "Copy to Clipboard";
      this.btnCopyToClipboard.UseVisualStyleBackColor = true;
      this.btnCopyToClipboard.Click += new System.EventHandler(this.BtnCopyToClipboard_Click);
      // 
      // pnlHeader
      // 
      this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(90)))), ((int)(((byte)(40)))));
      this.pnlHeader.Controls.Add(this.lblTitle);
      this.pnlHeader.Controls.Add(this.pictureBoxLogo);
      this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlHeader.Location = new System.Drawing.Point(0, 0);
      this.pnlHeader.Margin = new System.Windows.Forms.Padding(4);
      this.pnlHeader.Name = "pnlHeader";
      this.pnlHeader.Size = new System.Drawing.Size(931, 108);
      this.pnlHeader.TabIndex = 0;
      // 
      // lblTitle
      // 
      this.lblTitle.AutoSize = true;
      this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblTitle.ForeColor = System.Drawing.Color.White;
      this.lblTitle.Location = new System.Drawing.Point(190, 36);
      this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new System.Drawing.Size(380, 46);
      this.lblTitle.TabIndex = 0;
      this.lblTitle.Text = "Computer Info Tool";
      // 
      // pictureBoxLogo
      // 
      this.pictureBoxLogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.pictureBoxLogo.Image = global::WiredBrainCoffee.ComputerInfoTool.Properties.Resources.logo;
      this.pictureBoxLogo.Location = new System.Drawing.Point(11, 11);
      this.pictureBoxLogo.Margin = new System.Windows.Forms.Padding(4);
      this.pictureBoxLogo.Name = "pictureBoxLogo";
      this.pictureBoxLogo.Size = new System.Drawing.Size(136, 88);
      this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBoxLogo.TabIndex = 7;
      this.pictureBoxLogo.TabStop = false;
      // 
      // pnlMainArea
      // 
      this.pnlMainArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.pnlMainArea.Controls.Add(this.lblUserName);
      this.pnlMainArea.Controls.Add(this.txtUserName);
      this.pnlMainArea.Controls.Add(this.btnCopyToClipboard);
      this.pnlMainArea.Controls.Add(this.lblComputerName);
      this.pnlMainArea.Controls.Add(this.txtIPAddresses);
      this.pnlMainArea.Controls.Add(this.txtComputerName);
      this.pnlMainArea.Controls.Add(this.lblIPAddresses);
      this.pnlMainArea.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMainArea.Location = new System.Drawing.Point(0, 108);
      this.pnlMainArea.Margin = new System.Windows.Forms.Padding(4);
      this.pnlMainArea.Name = "pnlMainArea";
      this.pnlMainArea.Size = new System.Drawing.Size(931, 365);
      this.pnlMainArea.TabIndex = 1;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(931, 473);
      this.Controls.Add(this.pnlMainArea);
      this.Controls.Add(this.pnlHeader);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(4);
      this.Name = "MainForm";
      this.Text = "Wired Brain Coffee - Computer Info Tool    :::    Pluralsight & Thomas Claudius H" +
    "uber";
      this.pnlHeader.ResumeLayout(false);
      this.pnlHeader.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
      this.pnlMainArea.ResumeLayout(false);
      this.pnlMainArea.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lblUserName;
    private System.Windows.Forms.TextBox txtUserName;
    private System.Windows.Forms.Label lblComputerName;
    private System.Windows.Forms.TextBox txtComputerName;
    private System.Windows.Forms.Label lblIPAddresses;
    private System.Windows.Forms.TextBox txtIPAddresses;
    private System.Windows.Forms.Button btnCopyToClipboard;
    private System.Windows.Forms.PictureBox pictureBoxLogo;
    private System.Windows.Forms.Panel pnlHeader;
    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.Panel pnlMainArea;
  }
}

