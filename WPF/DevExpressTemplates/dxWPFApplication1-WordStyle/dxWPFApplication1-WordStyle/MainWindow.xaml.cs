using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Layout.Core;
using DevExpress.Xpf.Docking;
using DevExpress.XtraRichEdit;
using DevExpress.Xpf.RichEdit;


namespace dxWPFApplication1_WordStyle
{
    public partial class MainWindow : DXRibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ribbonControl1.SelectedPage = pageHome;
        }
        void richEditControl1_StartHeaderFooterEditing(object sender, HeaderFooterEditingEventArgs e)
        {
            catHeaderFooterTools.IsVisible = true;
            ribbonControl1.SelectedPage = pageHeaderFooterToolsDesign;
        }

        void richEditControl1_FinishHeaderFooterEditing(object sender, HeaderFooterEditingEventArgs e)
        {
            catHeaderFooterTools.IsVisible = false;
        }

        void richEditControl1_SelectionChanged(object sender, EventArgs e)
        {
            bool isSelectionInTable = richEditControl1.IsSelectionInTable();
            if (catTableTools.IsVisible != isSelectionInTable)
            {
                catTableTools.IsVisible = isSelectionInTable;
                if (isSelectionInTable)
                    ribbonControl1.SelectedPage = pageTableToolsDesign;
            }

            bool isSelectionInFloatingObject = richEditControl1.IsFloatingObjectSelected;
            if (catPictureTools.IsVisible != isSelectionInFloatingObject)
            {
                catPictureTools.IsVisible = isSelectionInFloatingObject;
                if (isSelectionInFloatingObject)
                    ribbonControl1.SelectedPage = pagePictureToolsFormat;
            }
        }

    }


}
