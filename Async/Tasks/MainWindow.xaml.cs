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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.Red);
            SolidColorBrush greenBrush = new SolidColorBrush(Colors.Green);
            SolidColorBrush blueBrush = new SolidColorBrush(Colors.Blue);
            SolidColorBrush whiteBrush = new SolidColorBrush(Colors.White);
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ellipse1.Fill = whiteBrush;
            ellipse2.Fill = redBrush;
            ellipse3.Fill = greenBrush;
            ellipse4.Fill = blueBrush;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            ellipse1.Fill = blueBrush;
            ellipse2.Fill = whiteBrush;
            ellipse3.Fill = redBrush;
            ellipse4.Fill = greenBrush;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            ellipse1.Fill = greenBrush;
            ellipse2.Fill = blueBrush;
            ellipse3.Fill = whiteBrush;
            ellipse4.Fill = redBrush;
        }

        private void blinkEllipse(Ellipse e, Brush color, int delay)
        {
            e.Fill = whiteBrush;
            System.Threading.Thread.Sleep(delay);
            e.Fill = color;
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                txtLoop.Text = i.ToString();
                txtLoop.InvalidateVisual();
                blinkEllipse(ellipse1, redBrush, 1000);
                blinkEllipse(ellipse2, greenBrush, 1000); 
                blinkEllipse(ellipse3, blueBrush, 1000);
            }
        }
    }
}
