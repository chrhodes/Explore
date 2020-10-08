using System;
using System.Collections.Concurrent;
using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class Concurrent_M6_EnumerateConcurrentDictionary : Page
    {
        public Concurrent_M6_EnumerateConcurrentDictionary(Program program) : base("Concurrent_M6_EnumerateConcurrentDictionary", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Concurrent_M6_EnumerateConcurrentDictionary");

            EnumerateDictionary();

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }

        void EnumerateDictionary()
        {
            // change this to a Dictionary<string, int> to see that enumerating while modifying
            // throws an exception for the standard dictionary
            var stock = new ConcurrentDictionary<string, int>();
            stock.TryAdd("jDays", 0);
            stock.TryAdd("Code School", 0);
            stock.TryAdd("Buddhist Geeks", 0);

            foreach (var shirt in stock)
            {
                stock.AddOrUpdate("jDays", 0, (key, value) => value + 1);
                Console.WriteLine(shirt.Key + ": " + shirt.Value);
            }

        }
    }
}
