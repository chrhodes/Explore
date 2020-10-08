using Prism.Interactivity.InteractionRequest;
using System;
using System.Windows;
using System.Windows.Controls;

namespace VNCExplore_LearnPrism_BrianLagunas.Views
{
    /// <summary>
    /// Interaction logic for CustomPopupView.xaml
    /// </summary>
    public partial class CustomPopupView : UserControl, IInteractionRequestAware
    {
        public CustomPopupView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (FinishInteraction != null)
                FinishInteraction();
        }

        public Action FinishInteraction { get; set; }
        public INotification Notification { get; set; }
    }
}
