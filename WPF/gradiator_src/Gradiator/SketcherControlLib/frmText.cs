using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SketcherControlLib
{
    public partial class frmText : Form
    {
        public frmText()
        {
            InitializeComponent();
        }

        private string  _text;
        public string  Content
        {
            get { return _text; }
            set 
            { 
                _text = value;
                textBox1.Text = value;
            }
        }

        private Font _font = new Font("Comic Sans MS", 10f);
        public Font TextFont
        {
            get { return _font; }
            set { _font = value; }
        }

        private Color _color = Color.Black;
        public Color TextColor
        {
            get { return _color; }
            set { _color = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _text = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _text = textBox1.Text;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _text = textBox1.Text;
        }

        private void frmText_Load(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();

            FontFamily[] ffs = FontFamily.GetFamilies(g);
            foreach (FontFamily ff in ffs)
                comboBox1.Items.Add(ff.Name);
            
            comboBox1.Text = _font.FontFamily.Name;
            comboBox2.Text = _font.Size.ToString();

            textBox1.Text = _text;
            textBox1.Font = _font;
            textBox1.ForeColor = _color;
            
            g.Dispose();
        }

        private void frmText_FormClosing(object sender, FormClosingEventArgs e)
        {
            _font = new Font(comboBox1.Text, float.Parse(comboBox2.Text));
            _text = textBox1.Text;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != string.Empty && comboBox2.Text != string.Empty)
                _font = new Font(comboBox1.Text, float.Parse(comboBox2.Text));
               
            textBox1.Font = _font;
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font fnt = new Font(comboBox1.Items[e.Index].ToString(), 10f);
            e.Graphics.DrawString(comboBox1.Items[e.Index].ToString(), fnt, new SolidBrush(Color.Black), e.Bounds);
        }

        private void comboBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            Font fnt = new Font(comboBox1.Items[e.Index].ToString(), 10f);
            e.ItemHeight = fnt.Height;
        }
    }
}