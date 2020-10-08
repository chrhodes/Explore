using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyConsole;
using VNC_Console_Explore_Collections.Classes;

namespace VNC_Console_Explore_Collections.Pages
{
    class Concurrent_M3_BuyAndSell : Page
    {

        public static readonly List<string> AllShirtNames =
            new List<string> { "technologyhour", "Code School", "jDays", "buddhistgeeks", "iGeek" };

        public Concurrent_M3_BuyAndSell(Program program) : base("Concurrent_M3_BuyAndSell", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Concurrent_M3_BuyAndSell");

            BuyAndSell();

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }

        void BuyAndSell()
        {
            StockControllerM3 controller = new StockControllerM3();
            TimeSpan workDay = new TimeSpan(0, 0, 2);

            Task t1 = Task.Run(() => new SalesPersonM3("Sahil").Work(controller, workDay));
            Task t2 = Task.Run(() => new SalesPersonM3("Peter").Work(controller, workDay));
            Task t3 = Task.Run(() => new SalesPersonM3("Juliette").Work(controller, workDay));
            Task t4 = Task.Run(() => new SalesPersonM3("Xavier").Work(controller, workDay));

            Task.WaitAll(t1, t2, t3, t4);
            controller.DisplayStatus();
        }
    }
}
