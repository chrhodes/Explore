using EasyConsole;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VNC_Console_Explore_Collections.Classes;

namespace VNC_Console_Explore_Collections.Pages
{
    class Concurrent_M5_SalesBonusesAfter : Page
    {
		public static readonly List<string> AllShirtNames = new List<string>
		{ "technologyhour", "Code School", "jDays", "buddhistgeeks", "iGeek" };

		public Concurrent_M5_SalesBonusesAfter(Program program) : base("Concurrent_M5_SalesBonusesAfter", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Concurrent_M5_SalesBonuses After");

            SalesBonuses();

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }

        void SalesBonuses()
        {
			StaffLogsForBonusesM5 staffLogs = new StaffLogsForBonusesM5();
			ToDoQueueM5 toDoQueue = new ToDoQueueM5(staffLogs);

			SalesPersonM5[] people = {   new SalesPersonM5("Sahil"),
									   new SalesPersonM5("Peter"),
									   new SalesPersonM5("Juliette"),
									   new SalesPersonM5("Xavier") };

			StockControllerM5 controller = new StockControllerM5(toDoQueue);

			TimeSpan workDay = new TimeSpan(0, 0, 1);

			Task t1 = Task.Run(() => people[0].Work(controller, workDay));
			Task t2 = Task.Run(() => people[1].Work(controller, workDay));
			Task t3 = Task.Run(() => people[2].Work(controller, workDay));
			Task t4 = Task.Run(() => people[3].Work(controller, workDay));

			Task bonusLogger = Task.Run(() => toDoQueue.MonitorAndLogTrades());
			Task bonusLogger2 = Task.Run(() => toDoQueue.MonitorAndLogTrades());

			Task.WaitAll(t1, t2, t3, t4);
			toDoQueue.CompleteAdding();
			Task.WaitAll(bonusLogger, bonusLogger2);

			controller.DisplayStatus();
			staffLogs.DisplayReport(people);

		}
    }
}
