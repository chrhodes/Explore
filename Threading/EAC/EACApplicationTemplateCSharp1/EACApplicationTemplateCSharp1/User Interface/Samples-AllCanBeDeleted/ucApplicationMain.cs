using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using EACApplicationTemplateCSharp1.Data;

namespace EACApplicationTemplateCSharp1.User_Interface
{
    public partial class ucApplicationMain : ucScreenBase
    {
        const string CONTROL_NAME = "ucApplicationMain";

        #region Initialization

        public ucApplicationMain()
        {
#if TRACE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            InitializeComponent();
        }

        /// <summary>
        /// Perform any initialization that is wanted before much else happens.
        /// By now the controls should be present and ready to go.  This method
        /// typically does not need to change.
        /// </summary>
        override public void InitialLoad()
        {
#if TRACE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            // Load another instance of this control back into the Controls collection
            // so the control can be swapped in and out. Do not delete this line.
            base.ShowControl(this.GetType());

            // Add any one time initialization code.
            InitializeUI();
        }

        /// <summary>
        /// Initialize the UI.  This is called from InitialLoad and has been pulled out into a separate
        /// method in case it needs to be called again.  
        /// 
        /// Typically this method changes depending on the needs of the application.
        /// 
        /// Also see the BeforeSwappingOut and AfterSwappingIn methods.
        /// </summary>
        public override void InitializeUI()
        {
#if TRACE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            // TODO: Load up combo boxes, initialize data, etc.

            #region Template Sample

            Type me = this.GetType();
            StringBuilder sb = new StringBuilder();
            Type baseType = me;

            do
            {
                sb.AppendFormat("{0}:", baseType.Name);
                baseType = baseType.BaseType;
            } while(baseType.Name != "UserControl");

            sb.AppendFormat("UserControl");

            label1.Text = sb.ToString();

            txtCat.Text = ConfigData.Cat;
            txtDog.Text = ConfigData.Dog;

            foreach(ConfigData.urlinfo item in ConfigData.URLs)
            {
                cbEnvironments.Items.Add(item.envCode);
            }

            lblDescription.Text = "";
            lblURL.Text = "";

            #endregion

        }

        #endregion

        #region ApplicationLevel Event Handlers

        /// <summary>
        /// Intialize the Application Level Event Handlers.
        /// 
        /// Typically this does not need to change unless the events are not desired
        /// or more events need to be added.
        /// </summary>
        protected override void InitializeApplicationEventHandlers()
        {
#if TRACE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            ApplicationEventHandler.ToggleF11Event += this.ToggleDebugMode;
            ApplicationEventHandler.ToggleF12Event += this.ToggleDeveloperMode;
        }

        /// <summary>
        /// Peform any operations that are needed when DebugMode switches state.
        /// </summary>
        protected override void ToggleDebugMode()
        {
#if TRACE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif

            Common.DebugMode = !Common.DebugMode;

            if(Common.DebugMode)
            {
                // TODO:
            }
            else
            {
                // TODO:
            }
        }

        /// <summary>
        /// Peform any operations that are needed when DeveloperMode switches state.
        /// </summary>
        protected override void ToggleDeveloperMode()
        {
#if TRACE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif

            Common.DeveloperMode = !Common.DeveloperMode;

            if(Common.DeveloperMode)
            {
                // TODO:
                this.BackColor = Color.LightBlue;
                Common.DebugWindow.Show();
            }
            else
            {
                // TODO:
                this.BackColor = SystemColors.ControlLight;
                Common.DebugWindow.Hide();
            }
        }

        /// <summary>
        /// Remove the Application Level Event Handlers.
        /// 
        /// Typically this does not need to change unless the events are not desired
        /// or more events need to be added.
        /// </summary>
        protected override void RemoveApplicationEventHandlers()
        {
#if TRACE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            ApplicationEventHandler.ToggleF11Event -= this.ToggleDebugMode;
            ApplicationEventHandler.ToggleF12Event -= this.ToggleDeveloperMode;
        }

        #endregion

        #region Swapping Methods

        /// These methods may be commented out if not used.
        /// 
        /// <summary>
        /// Perform any operations before the screen is swapped out.
        /// </summary>
        protected override void BeforeSwappingOut()
        {
#if TRACE_BASE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
        }

        /// <summary>
        /// Perform any operations after the screen is swapped in.
        /// </summary>
        protected override void AfterSwappingIn()
        {
#if TRACE_BASE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
        }

        #endregion

        #region Event Handlers

        #region Template Sample

        private void btnClickMe_Click(object sender, EventArgs e)
        {
            SurelyYouJest();
        }

        private void btnGoToSS1_Click(object sender, EventArgs e)
        {
            GoToSS1();
        }

        private void btnGoToSS2_Click(object sender, EventArgs e)
        {
            GoToSS2();
        }

        private void btnGotoStarterScreen_Click(object sender, EventArgs e)
        {
            GoToStarterScreen();
        }

        private void btnGotoStarterScreenWPF_Click(object sender, EventArgs e)
        {
            GoToStarterScreenWPF();
        }

        private void btnReturnToSender_Click(object sender, EventArgs e)
        {
            ReturnToSender();
        }

        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            AddRequestsFromXMLFile();
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            SaveRequestsToXMLFile();
        }

        private void cbEnvironments_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currentSelection = ((ComboBox)sender).Text;

            var envs = from env in ConfigData.URLs
                       where env.envCode == currentSelection
                       select env;

            foreach(ConfigData.urlinfo env in envs)
            {
                lblURL.Text = env.url;
                lblDescription.Text = env.description;
            }
        }

        #endregion

        private void btnInfo_Click(object sender, EventArgs e)
        {
            DisplayAssemblyInfo();
        }

        #endregion

        #region Main Function Routines

        #region Template Sample

        private void AddRequestsFromXMLFile()
        {
            openFileDialog1.Filter = "XML Files (*.xml)|*.xml|All files (*.*)|(*.*)";

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                applicationDataSet.DataTable1.InputFilePath = openFileDialog1.FileName;
                applicationDataSet.DataTable1.LoadFromFile(applicationDataSet.DataTable1);
            }
        }

        private void GoToSS1()
        {
#if TRACE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif

            SwapUserControl(this, System.Type.GetType("EACApplicationTemplateCSharp1.User_Interface.ucApplicationMainSS1"));
        }

        private void GoToSS2()
        {
#if TRACE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif

            SwapUserControl(this, System.Type.GetType("EACApplicationTemplateCSharp1.User_Interface.ucApplicationMainSS2"));
        }

        private void GoToStarterScreen()
        {
#if TRACE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif

            SwapUserControl(this, System.Type.GetType("EACApplicationTemplateCSharp1.User_Interface.ucStarterScreen"));
        }

        private void GoToStarterScreenWPF()
        {
#if TRACE
            Trace.WriteLine(string.Format("{0}:{1}()", CONTROL_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif

            SwapUserControl(this, System.Type.GetType("EACApplicationTemplateCSharp1.User_Interface.ucStarterScreenWPF"));
        }

        private static void SurelyYouJest()
        {
            MessageBox.Show("Surely you jest");
        }

        private void SaveRequestsToXMLFile()
        {
            saveFileDialog1.Filter = "XML Files (*.xml)|*.xml|All files (*.*)|(*.*)";

            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                applicationDataSet.DataTable1.OutputFilePath = saveFileDialog1.FileName;
                applicationDataSet.DataTable1.SaveToFile(applicationDataSet.DataTable1);
            }
        }

        #endregion

        private void DisplayAssemblyInfo()
        {
            Assembly asmb  = Assembly.GetExecutingAssembly();
            AssemblyHelper.AssemblyInformation info = new AssemblyHelper.AssemblyInformation(asmb);
            MessageBox.Show(info.ToString(), "Assembly Information");
        }

        #endregion

    }
}
