using System;

namespace WiredBrainCoffee.Simulators
{
    public class CoffeeMachine
    {
        public int CounterCappuccino { get; private set; }

        public void MakeCappuccion()
        {
            CounterCappuccino++;

            Console.WriteLine($"Make Cappuccino number {CounterCappuccino}");
        }
    }
}