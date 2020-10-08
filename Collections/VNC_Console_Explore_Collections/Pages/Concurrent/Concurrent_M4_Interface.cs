﻿using System;
using System.Collections.Concurrent;
using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class Concurrent_M4_Interface : Page
    {
        public Concurrent_M4_Interface(Program program) : base("Concurrent_M4_Interface", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Concurrent_M4_Interface");

			DemoInterface();

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }

		private static void DemoInterface()
		{
			// can change this to concurrent stack or bag
			IProducerConsumerCollection<string> shirts = new ConcurrentQueue<string>();
			shirts.TryAdd("Pluralsight");
			shirts.TryAdd("WordPress");
			shirts.TryAdd("Code School");

			Console.WriteLine("After enqueuing, count = " + shirts.Count.ToString());

			string item1; // = shirts.Dequeue();
			bool success = shirts.TryTake(out item1);
			if (success)
				Console.WriteLine("\r\nRemoving " + item1);
			else
				Console.WriteLine("queue was empty");

			Console.WriteLine("\r\nEnumerating:");
			foreach (string item in shirts)
				Console.WriteLine(item);

			Console.WriteLine("\r\nAfter enumerating, count = " + shirts.Count.ToString());
		}
	}
}
