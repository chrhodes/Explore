using EasyConsole;
using System;
using System.Collections.Generic;

namespace VNC_Console_Explore_Collections.Pages
{
    class Concurrent_M6_EnumerateDictionary : Page
    {
        public Concurrent_M6_EnumerateDictionary(Program program) : base("Concurrent_M6_EnumerateDictionary", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Concurrent_M6_EnumerateDictionary");

            EnumerateDictionary();

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }

        void EnumerateDictionary()
        {
            // change this to a Dictionary<string, int> to see that enumerating while modifying
            // throws an exception for the standard dictionary
            var stock = new Dictionary<string, int>();
            stock.Add("jDays", 0);
            stock.Add("Code School", 0);
            stock.Add("Buddhist Geeks", 0);

            foreach (var shirt in stock)
            {
                //stock.Add("jDays", 0, (key, value) => value + 1);
                Console.WriteLine(shirt.Key + ": " + shirt.Value);
            }

        }
    }
}
