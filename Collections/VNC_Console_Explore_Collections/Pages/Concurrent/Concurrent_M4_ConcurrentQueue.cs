﻿using System;
using System.Collections.Concurrent;
using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class Concurrent_M4_ConcurrentQueue : Page
    {
        public Concurrent_M4_ConcurrentQueue(Program program) : base("Concurrent_M4_ConcurrentQueue", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Concurrent_M4_ConcurrentQueue");

            DemoConcurrentQueue();

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }

		private static void DemoConcurrentQueue()
		{
			var shirts = new ConcurrentQueue<string>();
			shirts.Enqueue("Pluralsight");
			shirts.Enqueue("WordPress");
			shirts.Enqueue("Code School");

			Console.WriteLine("After enqueuing, count = " + shirts.Count.ToString());

			string item1; //= shirts.Dequeue();
			bool success = shirts.TryDequeue(out item1);
			if (success)
				Console.WriteLine("\r\nRemoving " + item1);
			else
				Console.WriteLine("queue was empty");

			string item2; //= shirts.Peek();
			success = shirts.TryPeek(out item2);
			if (success)
				Console.WriteLine("Peeking   " + item2);
			else
				Console.WriteLine("queue was empty");

			Console.WriteLine("\r\nEnumerating:");
			foreach (string item in shirts)
				Console.WriteLine(item);

			Console.WriteLine("\r\nAfter enumerating, count = " + shirts.Count.ToString());
		}
	}
}
