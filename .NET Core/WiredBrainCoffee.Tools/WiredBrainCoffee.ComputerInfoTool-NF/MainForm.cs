using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;

namespace WiredBrainCoffee.ComputerInfoTool
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
     
      txtUserName.Text = SystemInformation.UserName;
      txtComputerName.Text = SystemInformation.ComputerName;

      var ipAddresses = GetIPAddresses();
      txtIPAddresses.Text = string.Join(Environment.NewLine, ipAddresses);
    }

    private void BtnCopyToClipboard_Click(object sender, EventArgs e)
    {
      Clipboard.SetText(
$@"Username: {txtUserName.Text}
Computername: {txtComputerName.Text}

IP addresses: 
{txtIPAddresses.Text}");

      MessageBox.Show("Copied to Clipboard!");
    }

    private IEnumerable<string> GetIPAddresses()
    {
      var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces()
        .Where(ni => ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211
                 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet);

      foreach (var networkInterface in networkInterfaces)
      {
        var unicastAddresses = networkInterface.GetIPProperties().UnicastAddresses
          .Where(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork);
        foreach (var ip in unicastAddresses)
        {
          yield return $"{ip.Address} - {networkInterface.Name}";
        }
      }
    }
  }
}
