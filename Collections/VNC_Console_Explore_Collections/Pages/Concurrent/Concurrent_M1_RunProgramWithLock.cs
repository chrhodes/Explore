using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class Concurrent_M1_RunProgramWithLock : Page
    {
        public Concurrent_M1_RunProgramWithLock(Program program) : base("Concurrent_M1_RunProgramWithLock", program)
        {
        }
        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page Concurrent_M1_RunProgramWithLock");

            // multi-threaded but NOT thread-safe:
            // This method will give unpredictable results and may crash
            // Note that the stability of this function depends to some extent on 
            // your hardware. I put a Thread.Sleep(1) in the code used in the module
            // because that seemed to time the threads to maximize contention and so maximize
            // the likelihood of showing errors on my computer.
            // You may need to adjust this time to get similar results on your hardware.
            RunProgramWithLock();

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }

        static void RunProgramWithLock()
        {
            var orders = new Queue<string>();
            Task task1 = Task.Run(() => PlaceOrders3(orders, "Mark"));
            Task task2 = Task.Run(() => PlaceOrders3(orders, "Ramdevi"));
            Task.WaitAll(task1, task2);

            foreach (string order in orders)
                Console.WriteLine("ORDER: " + order);
        }

        static object _lockObj = new object();
        static void PlaceOrders3(Queue<string> orders, string customerName)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1);
                string orderName = string.Format("{0} wants t-shirt {1}", customerName, i + 1);
                lock (_lockObj)
                {
                    orders.Enqueue(orderName);
                }
            }
        }
    }
}
