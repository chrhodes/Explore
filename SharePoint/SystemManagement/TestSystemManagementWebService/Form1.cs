using System;
using System.Collections;
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

namespace TestSystemManagementWebServiceWinForm
{
    public partial class Form1 : Form
    {
        static long cMilliSeconds = Stopwatch.Frequency / 1000;

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

        // TODO: Reflect on WebService so don't have to maintain this list.

        string[] _WebServiceMethods =
        {
            "HelloWorld",
            "GetWin32Account",
            "GetWin32BIOS",
            "GetWin32BaseBoard",
            "GetWin32DiskDrive",
            "GetWin32Environment",
            "GetWin32Group",
            "GetWin32LogicalDisk",
            "GetWin32MappedLogicalDevice",
            "GetWin32MemoryDevice",
            "GetWin32NetworkAdapter",
            "GetWin32OperatingSystem",
            "GetWin32PhysicalMedia",
            "GetWin32PhysicalMemory",
            "GetWin32Processor",
            "GetWin32Service",
            "GetWin32SystemAccount",
            "GetWin32SystemBIOS",
            "GetWin32UserAccount"
        };

        SystemManagementWS.WMIInfoWS _webservice = new SystemManagementWS.WMIInfoWS();

        public Form1()
        {
            InitializeComponent();
        }

        private void displayHelloWorld(System.Windows.Forms.TextBox control, string text)
        {
            control.Text = text;
        }

        private void displayOutput(TextBox control, object obj, long duration)
        {
            StringBuilder sb = new StringBuilder(1024);
            long reflectTime = Stopwatch.GetTimestamp();

            Type wmiObjectType = obj.GetType();

            // Find the "GetEnumerator" method.  The underlying type should be a xyz[] which has a "GetEnumerator" method.
            System.Reflection.MethodInfo getEnumerator = wmiObjectType.GetMethod("GetEnumerator");
            System.Collections.IEnumerator iEnum = (IEnumerator)getEnumerator.Invoke(obj, null);
            Type wmiItemType;
            PropertyInfo property;
            object wmiItem;

            while (iEnum.MoveNext())
            {
                wmiItem = iEnum.Current;
                wmiItemType = wmiItem.GetType();

                foreach (PropertyInfo propertyInfo in wmiItemType.GetProperties())
                {
                    property = wmiItemType.GetProperty(propertyInfo.Name);

                    sb.AppendLine(string.Format("{0}:{1}", propertyInfo.Name, property.GetValue(wmiItem, null)));
                }
            }

            control.Text = sb.ToString();

            txtTime.Text = (duration / cMilliSeconds).ToString();
            txtReflectTime.Text = ((Stopwatch.GetTimestamp() - reflectTime) / cMilliSeconds).ToString();
        }
        
        private void ClearControls()
        {
            txtOutput.Clear();
            txtReflectTime.Clear();
            txtTime.Clear();

            this.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ClearControls();

            //getWMIinfo(_webservice);

            ClearControls();

            long startTicks;

            startTicks = Stopwatch.GetTimestamp();

            string method = cbMethod.SelectedItem.ToString();

            lblWMIService.Text = method;

            // Get the class
            Type wmiClass = _webservice.GetType();
            // Go find the method that was selected
            MethodInfo mi = wmiClass.GetMethod(method);
            // And call it.

            object obj = null;

            try
            {
                obj = mi.Invoke(_webservice, null);
            }
            catch (Exception ex)
            {
                txtOutput.Text = "Error Invoking Web Service\r\n" + ex.ToString();
                return;
            }

            try
            {
                if ("HelloWorld" == method)
                {
                    txtOutput.Text = (string)obj;
                    txtTime.Text = ((Stopwatch.GetTimestamp() - startTicks) / cMilliSeconds).ToString();
                }
                else
                {
                    displayOutput(txtOutput, obj, Stopwatch.GetTimestamp() - startTicks);
                }
            }
            catch (Exception ex)
            {
                txtOutput.Text = "Error Displaying Output\r\n" + ex.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string host in _hosts)
            {
                cbHost.Items.Add(host);
            }

            foreach (string service in _WebServiceMethods)
            {
                cbMethod.Items.Add(service);
            }

            System.Net.CredentialCache cache = new System.Net.CredentialCache();

            foreach (string host in _hosts)
            {
                string url = string.Format("http://{0}/SystemManagement/WMIInfoWS.asmx", host);

                // Now build the credential cache so we can get to all the places we need to.

                switch (host)
                {
                    // Development Servers
                    case "ldspide01v.devlifeint.devpl01.net":
                    case "ldspide02v.devlifeint.devpl01.net":
                    case "ldspide03v.devlifeint.devpl01.net":
                    case "ldspide04v.devlifeint.devpl01.net":
                    case "ldspide05v.devlifeint.devpl01.net":
                    // iDev Servers
                    case "lifesps601.devlifeint.devpl01.net":
                    case "lifesps701.devlifeint.devpl01.net":
                    case "lifesrch601.devlifeint.devpl01.net":
                    case "lifesrch701.devlifeint.devpl01.net":

                        cache.Add(new Uri(url), "NTLM", new System.Net.NetworkCredential("dspappca", "Development2007", "DEVLIFEINT"));
                        break;

                     // iTest Servers
                    case "lifesps401.tstlifeint.tstpl01.net":
                    case "lifesps501.tstlifeint.tstpl01.net":
                    case "lifesrch401.tstlifeint.tstpl01.net":
                    case "lifesrch501.tstlifeint.tstpl01.net":

                        cache.Add(new Uri(url), "NTLM", new System.Net.NetworkCredential("tspappca", "Testing2007", "TSTLIFEINT"));
                        break;

                    // Staging Servers
                    case "lsspa01v.life.pacificlife.net":
                    case "lsspa02v.life.pacificlife.net":
                    case "lssps01v.life.pacificlife.net":
                    case "lssps02v.life.pacificlife.net":

                        cache.Add(new Uri(url), "NTLM", new System.Net.NetworkCredential("sspappca", "Staging2007", "PACIFICMUTUAL"));
                        break;

                    // Production Servers
                    case "lpspa01v.life.pacificlife.net":
                    case "lpspa02v.life.pacificlife.net":
                    case "lpsps01v.life.pacificlife.net":
                    case "lpsps02v.life.pacificlife.net":

                        cache.Add(new Uri(url), "NTLM", new System.Net.NetworkCredential("pspappca", "Production2007", "PACIFICMUTUAL"));
                        break;
                }
            }

            _webservice.Credentials = cache;
        }

        private void cbHost_SelectedIndexChanged(object sender, EventArgs e)
        {
            string url = string.Format("http://{0}/SystemManagement/WMIInfoWS.asmx", this.cbHost.SelectedItem.ToString());
            _webservice.Url = url;
        }
    }
}
