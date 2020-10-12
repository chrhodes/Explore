using System;
using WiredBrainCoffee.Simulators;

namespace WiredBrainCoffee.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var coffeeMachine = new CoffeeMachine();
            coffeeMachine.MakeCappuccion();
            coffeeMachine.MakeCappuccion();

            Console.WriteLine($"Counter cappuccino: {coffeeMachine.CounterCappuccino}");

            Console.ReadLine();
        }
    }
}
