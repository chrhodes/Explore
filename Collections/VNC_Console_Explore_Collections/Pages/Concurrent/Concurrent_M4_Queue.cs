using System;
using System.Collections.Generic;
using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class Concurrent_M4_Queue : Page
    {
        public Concurrent_M4_Queue(Program program) : base("Concurrent_M4_Queue", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page Concurrent_M4_Queue");

            DemoQueue();

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }

        static void DemoQueue()
        {
            var shirts = new Queue<string>();
            shirts.Enqueue("Pluralsight");
            shirts.Enqueue("WordPress");
            shirts.Enqueue("Code School");

            Console.WriteLine("After enqueuing, count = " + shirts.Count.ToString());

            string item1 = shirts.Dequeue();
            Console.WriteLine("\r\nRemoving " + item1);

            string item2 = shirts.Peek();
            Console.WriteLine("Peeking   " + item2);

            Console.WriteLine("\r\nEnumerating:");
            foreach (string item in shirts)
                Console.WriteLine(item);

            Console.WriteLine("\r\nAfter enumerating, count = " + shirts.Count.ToString());

        }
    }
}
