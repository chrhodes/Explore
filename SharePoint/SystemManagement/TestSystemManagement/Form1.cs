using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace TestSystemManagementWindowsForm
{
    public partial class Form1 : Form
    {
        ConnectionOptions _options;

        ManagementScope _scope;

        string[] _hosts =
        {
            "localhost",
            "A097805C3P",
            // Development Servers
            "ldspide01v.devlifeint.devpl01.net",
            "ldspide02v.devlifeint.devpl01.net",
            "ldspide03v.devlifeint.devpl01.net",
            "ldspide04v.devlifeint.devpl01.net",
            "ldspide05v.devlifeint.devpl01.net",
            // iDev Servers
            "lifesps601.devlifeint.devpl01.net",
            "lifesps701.devlifeint.devpl01.net",
            "lifesrch601.devlifeint.devpl01.net",
            "lifesrch701.devlifeint.devpl01.net",
            // iTest Servers
            "lifesps401.tstlifeint.tstpl01.net",
            "lifesps501.tstlifeint.tstpl01.net",
            "lifesrch401.tstlifeint.tstpl01.net",
            "lifesrch501.tstlifeint.tstpl01.net",
            // Staging Servers
            "lsspa01v.life.pacificlife.net",
            "lsspa02v.life.pacificlife.net",
            "lssps01v.life.pacificlife.net",
            "lssps02v.life.pacificlife.net",
            // Production Servers
            "lpspa01v.life.pacificlife.net",
            "lpspa02v.life.pacificlife.net",
            "lpsps01v.life.pacificlife.net",
            "lpsps02v.life.pacificlife.net",
        };

        string[] _WMIServices =
        {
            "Win32_Account",
            "Win32_BIOS",
            "Win32_BaseBoard",
            "Win32_DiskDrive",
            "Win32_Environment",
            "Win32_Group",
            "Win32_LogicalDisk",
            "Win32_MappedLogicalDisk",
            "Win32_MemoryDevice",
            "Win32_NetworkAdapter",
            "Win32_OperatingSystem",
            "Win32_PhysicalMedia",
            "Win32_PhysicalMemory",
            "Win32_Processor",
            "Win32_Service",
            "Win32_SystemAccount",
            "Win32_SystemBIOS",
            "Win32_UserAccount"
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void ClearControls()
        {
            this.txtTime.Clear();
            this.txtOutput.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearControls();   
         
            long startTicks;

            startTicks = Stopwatch.GetTimestamp();

            string service = cbWMIService.SelectedItem.ToString();

            lblWMIService.Text = service;

            // TODO: May want to move some of this out of the button click.

            // Load the Assembly containing the class we want
            Assembly a = Assembly.Load("SystemManagement");
            // Get and create the class
            Type wmiClass = a.GetType("SystemManagement.WMIInfo");
            object obj = Activator.CreateInstance(wmiClass);
            // Go find the method that was selected
            MethodInfo mi = wmiClass.GetMethod(service);

            // Prepare the arguments for the call
            object[] paramArray = new object[1];
            paramArray[0] = _scope;
            // and finally invoke the method
            txtOutput.Text = (string)mi.Invoke(obj, paramArray);

            //switch (service)
            //{
            //    // Learn how to do this with reflection.

            //    case "Win32_Processor":
            //        txtOutput.Text = SystemManagement.WMIInfo.Win32_Processor(_scope);
            //        break;

            //    case "Win32_MemoryDevice":
            //        txtOutput.Text = SystemManagement.WMIInfo.Win32_MemoryDevice(_scope);
            //        break;
            //    case "Win32_LogicalDisk":
            //        txtOutput.Text = SystemManagement.WMIInfo.Win32_LogicalDisk(_scope);
            //        break;
            //    case "Win32_OperatingSystem":
            //        txtOutput.Text = SystemManagement.WMIInfo.Win32_OperatingSystem(_scope);
            //        break;
            //    case "Win32_Service":
            //        txtOutput.Text = SystemManagement.WMIInfo.Win32_Service(_scope);
            //        break;
            //    case "Win32_BaseBoard":
            //        txtOutput.Text = SystemManagement.WMIInfo.Win32_BaseBoard(_scope);
            //        break;
            //    case "Win32_DiskDrive":
            //        txtOutput.Text = SystemManagement.WMIInfo.Win32_DiskDrive(_scope);
            //        break;
            //    case "Win32_MappedLogicalDisk":
            //        txtOutput.Text = SystemManagement.WMIInfo.Win32_MappedLogicalDisk(_scope);
            //        break;
            //    case "Win32_BIOS":
            //        txtOutput.Text = SystemManagement.WMIInfo.Win32_BIOS(_scope);
            //        break;
            //    case "Win32_SystemAccount":
            //        txtOutput.Text = SystemManagement.WMIInfo.Win32_SystemAccount(_scope);
            //        break;
            //    case "Win32_Group":
            //        txtOutput.Text = SystemManagement.WMIInfo.Win32_Group(_scope);
            //        break;
            //    case "Win32_UserAccount":
            //        txtOutput.Text = SystemManagement.WMIInfo.Win32_UserAccount(_scope);
            //        break;
            //    case "Win32_Account":
            //        txtOutput.Text = SystemManagement.WMIInfo.Win32_Account(_scope);
            //        break;
            //    case "Win32_NetworkAdapter":
            //        txtOutput.Text = SystemManagement.WMIInfo.Win32_NetworkAdapter(_scope);
            //        break;
            //    case "Win32_PhysicalMemory":
            //        txtOutput.Text = SystemManagement.WMIInfo.Win32_PhysicalMemory(_scope);
            //        break;
            //    case "Win32_PhysicalMedia":
            //        txtOutput.Text = SystemManagement.WMIInfo.Win32_PhysicalMedia(_scope);
            //        break;
            //}

            txtTime.Text = ((Stopwatch.GetTimestamp() - startTicks) / (Stopwatch.Frequency / 1000)).ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string host in _hosts)
            {
                cbHost.Items.Add(host);
            }
            
            _options = new ConnectionOptions();
            // TODO: Handle security crossing domains.
            // Default to localhost
            _scope = new ManagementScope(string.Format(@"\\{0}\root\cimv2", "localhost", _options));

            _scope.Connect();
            
            foreach (string service in _WMIServices)
            {
                cbWMIService.Items.Add(service);
            }
        }

        private void cbHost_SelectedIndexChanged(object sender, EventArgs e)
        {
            _scope = new ManagementScope(
                string.Format(@"\\{0}{1}", 
                    this.cbHost.SelectedItem.ToString(),
                    this.cbWMIProvider.SelectedItem.ToString()),
                _options);

            _scope.Connect();
        }
    }
}
