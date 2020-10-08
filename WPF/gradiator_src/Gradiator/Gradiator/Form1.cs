using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using ShapesClassLib;
using System.Text.RegularExpressions;
using System.Diagnostics;

//=========================================================================
//
//  Version: 1.0.4
//
//  Created: ?? Todays date 8/20/07 Monday
//
//  This started out as an CP article on GDI+ and Gradients, then I included
//      support for images and have the ability for screen capture and also
//      want to add support for putting pic clips in a slide control.  I tried
//      adding 8 (I think) images, reduced in size, to the edit area and it 
//      VERY progressively got slower so don't know if  the way I'm doing it
//      will work.  Reason for pic clips and screen capture is for Keeper
//      application.  I think it would be a good addtion!
//  I would also like to add support for generating code.
//
//  Version 1.0.5 I've striped everything out of here so what remains is just a shell.
//
//=========================================================================

namespace Gradiator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                sketcherControl1.HideColorSelector();
        }
    }
}