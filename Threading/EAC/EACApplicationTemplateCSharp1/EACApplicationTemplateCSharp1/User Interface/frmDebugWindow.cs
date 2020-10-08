using System;
using System.Windows.Forms;

namespace EACApplicationTemplateCSharp1.User_Interface
{
    public partial class frmDebugWindow : Form
    {
        public frmDebugWindow()
        {
            InitializeComponent();
        }

        private void btnClearOutput_Click(System.Object sender, System.EventArgs e)
        {
            this.txtOutput.Clear();
        }

        private void chkDebugSQL_CheckedChanged(object sender, EventArgs e)
        {
            Common.DebugSQL = ((System.Windows.Forms.CheckBox)sender).Checked;
        }

        private void chkDebugLevel1_CheckedChanged(object sender, EventArgs e)
        {
            Common.DebugLevel1 = ((System.Windows.Forms.CheckBox)sender).Checked;
        }

        private void chkDebugLevel2_CheckedChanged(object sender, EventArgs e)
        {
            Common.DebugLevel2 = ((System.Windows.Forms.CheckBox)sender).Checked;
        }

        private void frmDebugWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Common.DeveloperMode = false;
            e.Cancel = true;
        }

        private void btnGetAllConfigInfo_Click(object sender, EventArgs e)
        {
            Common.WriteToDebugWindow("ConfigData Data");
            Common.WriteToDebugWindow(ConfigData.GetAllConfigInfo());
        }

    }
}
