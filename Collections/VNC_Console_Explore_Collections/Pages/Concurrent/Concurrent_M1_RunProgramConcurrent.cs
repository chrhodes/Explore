using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class Concurrent_M1_RunProgramConcurrent : Page
    {
        public Concurrent_M1_RunProgramConcurrent(Program program) : base("Concurrent_RunProgramConcurrent", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page Concurrent_RunProgramConcurrent");

            // multi-threaded but NOT thread-safe:
            // This method will give unpredictable results and may crash
            // Note that the stability of this function depends to some extent on 
            // your hardware. I put a Thread.Sleep(1) in the code used in the module
            // because that seemed to time the threads to maximize contention and so maximuze
            // the likelihood of showing errors on my computer.
            // You may need to adjust this time to get similar results on your hardware.
            RunProgramConcurrent();

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }

        static void RunProgramConcurrent()
        {
            var orders = new ConcurrentQueue<string>();
            Task task1 = Task.Run(() => PlaceOrders2(orders, "Mark"));
            Task task2 = Task.Run(() => PlaceOrders2(orders, "Ramdevi"));
            Task.WaitAll(task1, task2);

            foreach (string order in orders)
                Console.WriteLine("ORDER: " + order);
        }

        static void PlaceOrders2(ConcurrentQueue<string> orders, string customerName)
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
