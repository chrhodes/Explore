namespace TasksWinForm
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
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnBlink = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtLoop = new System.Windows.Forms.TextBox();
            this.nudLoop = new System.Windows.Forms.NumericUpDown();
            this.nudDelayRed = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nudDelayGreen = new System.Windows.Forms.NumericUpDown();
            this.nudDelayBlue = new System.Windows.Forms.NumericUpDown();
            this.nudRepeatBlue = new System.Windows.Forms.NumericUpDown();
            this.nudRepeatGreen = new System.Windows.Forms.NumericUpDown();
            this.nudRepeatRed = new System.Windows.Forms.NumericUpDown();
            this.btnBlinkAsync = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ckAbort = new System.Windows.Forms.CheckBox();
            this.ucLightGrid1 = new TasksWinForm.ucLightGrid();
            ((System.ComponentModel.ISupportInitialize)(this.nudLoop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelayRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelayGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelayBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepeatBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepeatGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepeatRed)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(174, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(255, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnBlink
            // 
            this.btnBlink.Location = new System.Drawing.Point(150, 189);
            this.btnBlink.Name = "btnBlink";
            this.btnBlink.Size = new System.Drawing.Size(75, 21);
            this.btnBlink.TabIndex = 4;
            this.btnBlink.Text = "Blink";
            this.btnBlink.UseVisualStyleBackColor = true;
            this.btnBlink.Click += new System.EventHandler(this.btnBlink_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.Location = new System.Drawing.Point(12, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(75, 35);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Green;
            this.panel2.Location = new System.Drawing.Point(93, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(75, 35);
            this.panel2.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Blue;
            this.panel3.Location = new System.Drawing.Point(174, 56);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(75, 35);
            this.panel3.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Location = new System.Drawing.Point(255, 56);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(75, 35);
            this.panel4.TabIndex = 6;
            // 
            // txtLoop
            // 
            this.txtLoop.Location = new System.Drawing.Point(231, 203);
            this.txtLoop.Name = "txtLoop";
            this.txtLoop.Size = new System.Drawing.Size(38, 20);
            this.txtLoop.TabIndex = 7;
            // 
            // nudLoop
            // 
            this.nudLoop.Location = new System.Drawing.Point(69, 190);
            this.nudLoop.Name = "nudLoop";
            this.nudLoop.Size = new System.Drawing.Size(75, 20);
            this.nudLoop.TabIndex = 8;
            this.nudLoop.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // nudDelayRed
            // 
            this.nudDelayRed.Increment = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.nudDelayRed.Location = new System.Drawing.Point(12, 107);
            this.nudDelayRed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudDelayRed.Name = "nudDelayRed";
            this.nudDelayRed.Size = new System.Drawing.Size(75, 20);
            this.nudDelayRed.TabIndex = 9;
            this.nudDelayRed.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 192);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Repeats";
            // 
            // nudDelayGreen
            // 
            this.nudDelayGreen.Increment = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.nudDelayGreen.Location = new System.Drawing.Point(93, 107);
            this.nudDelayGreen.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudDelayGreen.Name = "nudDelayGreen";
            this.nudDelayGreen.Size = new System.Drawing.Size(75, 20);
            this.nudDelayGreen.TabIndex = 12;
            this.nudDelayGreen.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // nudDelayBlue
            // 
            this.nudDelayBlue.Increment = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.nudDelayBlue.Location = new System.Drawing.Point(174, 107);
            this.nudDelayBlue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudDelayBlue.Name = "nudDelayBlue";
            this.nudDelayBlue.Size = new System.Drawing.Size(75, 20);
            this.nudDelayBlue.TabIndex = 13;
            this.nudDelayBlue.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // nudRepeatBlue
            // 
            this.nudRepeatBlue.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudRepeatBlue.Location = new System.Drawing.Point(174, 133);
            this.nudRepeatBlue.Name = "nudRepeatBlue";
            this.nudRepeatBlue.Size = new System.Drawing.Size(75, 20);
            this.nudRepeatBlue.TabIndex = 16;
            this.nudRepeatBlue.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // nudRepeatGreen
            // 
            this.nudRepeatGreen.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudRepeatGreen.Location = new System.Drawing.Point(93, 133);
            this.nudRepeatGreen.Name = "nudRepeatGreen";
            this.nudRepeatGreen.Size = new System.Drawing.Size(75, 20);
            this.nudRepeatGreen.TabIndex = 15;
            this.nudRepeatGreen.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // nudRepeatRed
            // 
            this.nudRepeatRed.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudRepeatRed.Location = new System.Drawing.Point(12, 133);
            this.nudRepeatRed.Name = "nudRepeatRed";
            this.nudRepeatRed.Size = new System.Drawing.Size(75, 20);
            this.nudRepeatRed.TabIndex = 14;
            this.nudRepeatRed.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // btnBlinkAsync
            // 
            this.btnBlinkAsync.Location = new System.Drawing.Point(150, 216);
            this.btnBlinkAsync.Name = "btnBlinkAsync";
            this.btnBlinkAsync.Size = new System.Drawing.Size(75, 21);
            this.btnBlinkAsync.TabIndex = 17;
            this.btnBlinkAsync.Text = "Blink Async";
            this.btnBlinkAsync.UseVisualStyleBackColor = true;
            this.btnBlinkAsync.Click += new System.EventHandler(this.btnBlinkAsync_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Task Delay (ms)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(255, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Task Loops";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(275, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Repeat #";
            // 
            // ckAbort
            // 
            this.ckAbort.AutoSize = true;
            this.ckAbort.Location = new System.Drawing.Point(231, 243);
            this.ckAbort.Name = "ckAbort";
            this.ckAbort.Size = new System.Drawing.Size(74, 17);
            this.ckAbort.TabIndex = 0;
            this.ckAbort.Text = "Abort Run";
            this.ckAbort.UseVisualStyleBackColor = true;
            // 
            // ucLightGrid1
            // 
            this.ucLightGrid1.Location = new System.Drawing.Point(464, 42);
            this.ucLightGrid1.Name = "ucLightGrid1";
            this.ucLightGrid1.Size = new System.Drawing.Size(333, 368);
            this.ucLightGrid1.TabIndex = 21;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 468);
            this.Controls.Add(this.ucLightGrid1);
            this.Controls.Add(this.ckAbort);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBlinkAsync);
            this.Controls.Add(this.nudRepeatBlue);
            this.Controls.Add(this.nudRepeatGreen);
            this.Controls.Add(this.nudRepeatRed);
            this.Controls.Add(this.nudDelayBlue);
            this.Controls.Add(this.nudDelayGreen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudDelayRed);
            this.Controls.Add(this.nudLoop);
            this.Controls.Add(this.txtLoop);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnBlink);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudLoop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelayRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelayGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelayBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepeatBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepeatGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepeatRed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnBlink;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtLoop;
        private System.Windows.Forms.NumericUpDown nudLoop;
        private System.Windows.Forms.NumericUpDown nudDelayRed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudDelayGreen;
        private System.Windows.Forms.NumericUpDown nudDelayBlue;
        private System.Windows.Forms.NumericUpDown nudRepeatBlue;
        private System.Windows.Forms.NumericUpDown nudRepeatGreen;
        private System.Windows.Forms.NumericUpDown nudRepeatRed;
        private System.Windows.Forms.Button btnBlinkAsync;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ckAbort;
        private ucLightGrid ucLightGrid1;
    }
}

