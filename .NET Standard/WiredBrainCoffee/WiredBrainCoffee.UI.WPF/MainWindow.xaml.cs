using System.Windows;

using WiredBrainCoffee.Simulators;

namespace WiredBrainCoffee.UI.WPF
{
    public partial class MainWindow : Window
    {
        private CoffeeMachine _coffeeMachine;

        public MainWindow()
        {
            InitializeComponent();
            _coffeeMachine = new CoffeeMachine();
            txtCappuccinoCounter.Text = _coffeeMachine.CounterCappuccino.ToString();
        }

        private void btnMakeCappuccinoClick(object sender, RoutedEventArgs e)
        {
            _coffeeMachine.MakeCappuccion();
            txtCappuccinoCounter.Text = _coffeeMachine.CounterCappuccino.ToString();
        }
    }
}
