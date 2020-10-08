using System;
using System.Collections.Generic;
using System.Threading;

using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class Concurrent_M1_RunProgramOneThread : Page
    {
        public Concurrent_M1_RunProgramOneThread(Program program) : base("Concurrent_RunProgramOneThread", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page Concurrent_RunProgramOneThread");

            RunProgramOneThread();

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }

        static void RunProgramOneThread()
        {
            var orders = new Queue<string>();

            PlaceOrders(orders, "Mark");
            PlaceOrders(orders, "Ramdevi");

            foreach (string order in orders)
                Console.WriteLine("ORDER: " + order);
        }

        static void PlaceOrders(Queue<string> orders, string customerName)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1);
                string orderName = string.Format("{0} wants t-shirt {1}", customerName, i + 1);
                orders.Enqueue(orderName);
            }
        }
    }
}
