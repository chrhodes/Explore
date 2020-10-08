using System;
using System.Collections.Generic;
using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class Concurrent_M2_Start : Page
    {
        public Concurrent_M2_Start(Program program) : base("Concurrent_M2_Start", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Concurrent_M2_Start");

            StartOfModule();

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }

		private static void StartOfModule()
		{
			var stock = new Dictionary<string, int>()
			{
				{"jDays", 4},
				{"technologyhour", 3}
			};

			Console.WriteLine(string.Format("No. of shirts in stock = {0}", stock.Count));

			stock.Add("pluralsight", 6);
			stock["buddhistgeeks"] = 5;

			stock["pluralsight"] = 7; // up from 6 - we just bought one			
			Console.WriteLine(string.Format("\r\nstock[pluralsight] = {0}", stock["pluralsight"]));

			stock.Remove("jDays");

			Console.WriteLine("\r\nEnumerating:");
			foreach (var keyValPair in stock)
			{
				Console.WriteLine("{0}: {1}", keyValPair.Key, keyValPair.Value);
			}
		}
	}
}
