namespace EACApplicationTemplateCSharp1.User_Interface
{
    partial class ucApplicationMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucApplicationMain));
            this.dataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.applicationDataSet = new EACApplicationTemplateCSharp1.Data.ApplicationDataSet();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLoadFromFile = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataColumn1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataColumn2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataColumn3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSaveToFile = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGotoStarterScreenWPF = new System.Windows.Forms.Button();
            this.btnGotoStarterScreen = new System.Windows.Forms.Button();
            this.btnGoToSS1 = new System.Windows.Forms.Button();
            this.btnReturnToSender = new System.Windows.Forms.Button();
            this.btnGoToSS2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtCat = new System.Windows.Forms.TextBox();
            this.btnClickMe = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblURL = new System.Windows.Forms.Label();
            this.txtDog = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.cbEnvironments = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnInfo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationDataSet)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataTable1BindingSource
            // 
            this.dataTable1BindingSource.DataMember = "DataTable1";
            this.dataTable1BindingSource.DataSource = this.applicationDataSet;
            // 
            // applicationDataSet
            // 
            this.applicationDataSet.DataSetName = "ApplicationDataSet";
            this.applicationDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoadFromFile);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.btnSaveToFile);
            this.groupBox1.Location = new System.Drawing.Point(20, 503);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(615, 157);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Loading and Saving dataTables from XML files";
            // 
            // btnLoadFromFile
            // 
            this.btnLoadFromFile.Location = new System.Drawing.Point(9, 118);
            this.btnLoadFromFile.Name = "btnLoadFromFile";
            this.btnLoadFromFile.Size = new System.Drawing.Size(96, 23);
            this.btnLoadFromFile.TabIndex = 33;
            this.btnLoadFromFile.Text = "Load from File";
            this.btnLoadFromFile.UseVisualStyleBackColor = true;
            this.btnLoadFromFile.Click += new System.EventHandler(this.btnLoadFromFile_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataColumn1DataGridViewTextBoxColumn,
            this.dataColumn2DataGridViewTextBoxColumn,
            this.dataColumn3DataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.dataTable1BindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(9, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(599, 96);
            this.dataGridView1.TabIndex = 32;
            // 
            // dataColumn1DataGridViewTextBoxColumn
            // 
            this.dataColumn1DataGridViewTextBoxColumn.DataPropertyName = "DataColumn1";
            this.dataColumn1DataGridViewTextBoxColumn.HeaderText = "DataColumn1";
            this.dataColumn1DataGridViewTextBoxColumn.Name = "dataColumn1DataGridViewTextBoxColumn";
            // 
            // dataColumn2DataGridViewTextBoxColumn
            // 
            this.dataColumn2DataGridViewTextBoxColumn.DataPropertyName = "DataColumn2";
            this.dataColumn2DataGridViewTextBoxColumn.HeaderText = "DataColumn2";
            this.dataColumn2DataGridViewTextBoxColumn.Name = "dataColumn2DataGridViewTextBoxColumn";
            // 
            // dataColumn3DataGridViewTextBoxColumn
            // 
            this.dataColumn3DataGridViewTextBoxColumn.DataPropertyName = "DataColumn3";
            this.dataColumn3DataGridViewTextBoxColumn.HeaderText = "DataColumn3";
            this.dataColumn3DataGridViewTextBoxColumn.Name = "dataColumn3DataGridViewTextBoxColumn";
            // 
            // btnSaveToFile
            // 
            this.btnSaveToFile.Location = new System.Drawing.Point(518, 125);
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.Size = new System.Drawing.Size(91, 23);
            this.btnSaveToFile.TabIndex = 31;
            this.btnSaveToFile.Text = "Save to File";
            this.btnSaveToFile.UseVisualStyleBackColor = true;
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGotoStarterScreenWPF);
            this.groupBox2.Controls.Add(this.btnGotoStarterScreen);
            this.groupBox2.Controls.Add(this.btnGoToSS1);
            this.groupBox2.Controls.Add(this.btnReturnToSender);
            this.groupBox2.Controls.Add(this.btnGoToSS2);
            this.groupBox2.Location = new System.Drawing.Point(20, 335);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(309, 162);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Screen Swapping";
            // 
            // btnGotoStarterScreenWPF
            // 
            this.btnGotoStarterScreenWPF.Location = new System.Drawing.Point(9, 131);
            this.btnGotoStarterScreenWPF.Name = "btnGotoStarterScreenWPF";
            this.btnGotoStarterScreenWPF.Size = new System.Drawing.Size(137, 23);
            this.btnGotoStarterScreenWPF.TabIndex = 30;
            this.btnGotoStarterScreenWPF.Text = "GoTo StarterScreenWPF";
            this.btnGotoStarterScreenWPF.UseVisualStyleBackColor = true;
            this.btnGotoStarterScreenWPF.Click += new System.EventHandler(this.btnGotoStarterScreenWPF_Click);
            // 
            // btnGotoStarterScreen
            // 
            this.btnGotoStarterScreen.Location = new System.Drawing.Point(9, 104);
            this.btnGotoStarterScreen.Name = "btnGotoStarterScreen";
            this.btnGotoStarterScreen.Size = new System.Drawing.Size(137, 23);
            this.btnGotoStarterScreen.TabIndex = 29;
            this.btnGotoStarterScreen.Text = "GoTo StarterScreen";
            this.btnGotoStarterScreen.UseVisualStyleBackColor = true;
            this.btnGotoStarterScreen.Click += new System.EventHandler(this.btnGotoStarterScreen_Click);
            // 
            // btnGoToSS1
            // 
            this.btnGoToSS1.Location = new System.Drawing.Point(9, 19);
            this.btnGoToSS1.Name = "btnGoToSS1";
            this.btnGoToSS1.Size = new System.Drawing.Size(123, 23);
            this.btnGoToSS1.TabIndex = 28;
            this.btnGoToSS1.Text = "GoTo SS1";
            this.btnGoToSS1.UseVisualStyleBackColor = true;
            this.btnGoToSS1.Click += new System.EventHandler(this.btnGoToSS1_Click);
            // 
            // btnReturnToSender
            // 
            this.btnReturnToSender.Location = new System.Drawing.Point(200, 19);
            this.btnReturnToSender.Name = "btnReturnToSender";
            this.btnReturnToSender.Size = new System.Drawing.Size(103, 23);
            this.btnReturnToSender.TabIndex = 26;
            this.btnReturnToSender.Text = "Return to Sender";
            this.btnReturnToSender.UseVisualStyleBackColor = true;
            this.btnReturnToSender.Click += new System.EventHandler(this.btnReturnToSender_Click);
            // 
            // btnGoToSS2
            // 
            this.btnGoToSS2.Location = new System.Drawing.Point(9, 48);
            this.btnGoToSS2.Name = "btnGoToSS2";
            this.btnGoToSS2.Size = new System.Drawing.Size(123, 23);
            this.btnGoToSS2.TabIndex = 27;
            this.btnGoToSS2.Text = "GoTo SS2";
            this.btnGoToSS2.UseVisualStyleBackColor = true;
            this.btnGoToSS2.Click += new System.EventHandler(this.btnGoToSS2_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtCat);
            this.groupBox3.Controls.Add(this.btnClickMe);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.lblURL);
            this.groupBox3.Controls.Add(this.txtDog);
            this.groupBox3.Controls.Add(this.lblDescription);
            this.groupBox3.Controls.Add(this.cbEnvironments);
            this.groupBox3.Location = new System.Drawing.Point(335, 335);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(300, 162);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Loading Config Data";
            // 
            // txtCat
            // 
            this.txtCat.Location = new System.Drawing.Point(6, 19);
            this.txtCat.Name = "txtCat";
            this.txtCat.Size = new System.Drawing.Size(100, 20);
            this.txtCat.TabIndex = 31;
            // 
            // btnClickMe
            // 
            this.btnClickMe.Location = new System.Drawing.Point(155, 31);
            this.btnClickMe.Name = "btnClickMe";
            this.btnClickMe.Size = new System.Drawing.Size(99, 23);
            this.btnClickMe.TabIndex = 25;
            this.btnClickMe.Text = "Click Me";
            this.btnClickMe.UseVisualStyleBackColor = true;
            this.btnClickMe.Click += new System.EventHandler(this.btnClickMe_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Dog";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Environments";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Cat";
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Location = new System.Drawing.Point(7, 114);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(20, 13);
            this.lblURL.TabIndex = 35;
            this.lblURL.Text = "Url";
            // 
            // txtDog
            // 
            this.txtDog.Location = new System.Drawing.Point(6, 45);
            this.txtDog.Name = "txtDog";
            this.txtDog.Size = new System.Drawing.Size(100, 20);
            this.txtDog.TabIndex = 32;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(7, 136);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 34;
            this.lblDescription.Text = "Description";
            // 
            // cbEnvironments
            // 
            this.cbEnvironments.FormattingEnabled = true;
            this.cbEnvironments.Location = new System.Drawing.Point(6, 90);
            this.cbEnvironments.Name = "cbEnvironments";
            this.cbEnvironments.Size = new System.Drawing.Size(264, 21);
            this.cbEnvironments.TabIndex = 33;
            this.cbEnvironments.SelectedIndexChanged += new System.EventHandler(this.cbEnvironments_SelectedIndexChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(20, 40);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(615, 289);
            this.textBox2.TabIndex = 23;
            this.textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 34;
            this.label1.Text = "label1";
            // 
            // btnInfo
            // 
            this.btnInfo.BackgroundImage = global::EACApplicationTemplateCSharp1.Properties.Resources.Info;
            this.btnInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnInfo.Location = new System.Drawing.Point(618, 8);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(17, 23);
            this.btnInfo.TabIndex = 35;
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // ucApplicationMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.textBox2);
            this.Name = "ucApplicationMain";
            this.Size = new System.Drawing.Size(666, 668);
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationDataSet)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource dataTable1BindingSource;
        private Data.ApplicationDataSet applicationDataSet;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnClickMe;
        private System.Windows.Forms.Button btnReturnToSender;
        private System.Windows.Forms.Button btnGoToSS2;
        private System.Windows.Forms.Button btnGoToSS1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCat;
        private System.Windows.Forms.TextBox txtDog;
        private System.Windows.Forms.ComboBox cbEnvironments;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLoadFromFile;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataColumn1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataColumn2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataColumn3DataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnSaveToFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGotoStarterScreenWPF;
        private System.Windows.Forms.Button btnGotoStarterScreen;
        private System.Windows.Forms.Button btnInfo;
    }
}
