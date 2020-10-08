using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SketcherControlLib
{
    public partial class ColorSelectorDialog : Form
    {
        public ColorSelectorDialog()
        {
            InitializeComponent();
        }

        private void ColorSelectorDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void ColorSelectorDialog_Load(object sender, EventArgs e)
        {
            colorSelector1.LoadColorPalette(Application.StartupPath + @"\default color palette.xml");
        }
    }
}