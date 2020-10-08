using System;
using System.Collections.Concurrent;
using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class Concurrent_M2_End : Page
    {
        public Concurrent_M2_End(Program program) : base("Concurrent_M2_End", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Concurrent_M2_End");

			EndOfModule();

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }

		private static void EndOfModule()
		{
			var stock = new ConcurrentDictionary<string, int>();
			stock.TryAdd("jDays", 4);
			stock.TryAdd("technologyhour", 3);
			Console.WriteLine(string.Format("No. of shirts in stock = {0}", stock.Count));

			bool success = stock.TryAdd("pluralsight", 6);
			Console.WriteLine("Added succeeded? " + success);
			success = stock.TryAdd("pluralsight", 6);
			Console.WriteLine("Added succeeded? " + success);

			stock["buddhistgeeks"] = 5;

			// stock["pluralsight"]++;
			int psStock = stock.AddOrUpdate("pluralsight", 1, (key, oldValue) => oldValue + 1);
			Console.WriteLine("New value is " + psStock);

			Console.WriteLine(string.Format("stock[pluralsight] = {0}", stock.GetOrAdd("pluralsight", 0)));

			int jDaysValue;
			success = stock.TryRemove("jDays", out jDaysValue);
			if (success)
				Console.WriteLine("value removed was: " + jDaysValue);

			Console.WriteLine("\r\nEnumerating:");
			foreach (var keyValPair in stock)
			{
				Console.WriteLine("{0}: {1}", keyValPair.Key, keyValPair.Value);
			}
		}
	}
}
