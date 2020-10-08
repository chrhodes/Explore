using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.VisualBasic;
using Microsoft.Practices.EnterpriseLibrary.AppSettings.Configuration.Design.Properties;
using Microsoft.Practices.EnterpriseLibrary.Configuration;



namespace ApplicationSettings
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGetSettings_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            foreach (ConfigurationSectionGroup configSectionGroup in config.SectionGroups)
            {
                txtOutput.Text += ControlChars.CrLf + configSectionGroup.Name;
                txtOutput.Text += ControlChars.CrLf + configSectionGroup.SectionGroupName;
            }

            ConfigurationSectionGroup configGroup = config.SectionGroups["userSettings"];
            ClientSettingsSection settingsSection = (ClientSettingsSection)configGroup.Sections["ApplicationSettings.Properties.Settings"];
            SettingElementCollection elements = settingsSection.Settings;

            foreach (SettingElement element in elements)
            {
                txtOutput.Text += element.Name + ":" + element.Value.ValueXml.InnerText + Environment.NewLine;
            }

        }
    }
}