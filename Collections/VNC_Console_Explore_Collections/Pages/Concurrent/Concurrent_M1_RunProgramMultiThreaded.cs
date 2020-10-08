using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class Concurrent_M1_RunProgramMultiThreaded : Page
    {
        static int _numberOrders = 5;
        static int _sleepDelay = 1;

        public Concurrent_M1_RunProgramMultiThreaded(Program program) : base("Concurrent_RunProgramMultiThreaded", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page Concurrent_RunProgramMultiThreaded");

            _numberOrders = Input.ReadInt("Enter number of orders: ", 5, 100);
            _sleepDelay = Input.ReadInt("Enter sleepDelay: ", 0, 10);

            // multi-threaded but NOT thread-safe:
            // This method will give unpredictable results and may crash
            // Note that the stability of this function depends to some extent on 
            // your hardware. I put a Thread.Sleep(1) in the code used in the module
            // because that seemed to time the threads to maximize contention and so maximize
            // the likelihood of showing errors on my computer.
            // You may need to adjust this time to get similar results on your hardware.
            RunProgramMultithreaded();

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }

        static void RunProgramMultithreaded()
        {
            var orders = new Queue<string>();
            Task task1 = Task.Run(() => PlaceOrders(orders, "Mark"));
            Task task2 = Task.Run(() => PlaceOrders(orders, "Ramdevi"));
            Task.WaitAll(task1, task2);

            foreach (string order in orders)
                Console.WriteLine("ORDER: " + order);
        }

        static void PlaceOrders(Queue<string> orders, string customerName)
        {
            for (int i = 0; i < _numberOrders; i++)
            {
                Thread.Sleep(_sleepDelay);
                string orderName = string.Format("{0} wants t-shirt {1}", customerName, i + 1);
                orders.Enqueue(orderName);
            }
        }
    }
}
