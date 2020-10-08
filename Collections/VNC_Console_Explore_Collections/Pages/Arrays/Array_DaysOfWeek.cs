using EasyConsole;
using System;

namespace VNC_Console_Explore_Collections.Pages
{
    class Array_DaysOfWeek : Page
    {
        public Array_DaysOfWeek(Program program) : base("Array_DaysOfWeek", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page Array_DaysOfWeek");

			DemoCode();

			Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }

		void DemoCode()
		{
			string[] daysOfWeek = {
				"Monday",
				"Tuesday",
				"Wensday",
				"Thursday",
				"Friday",
				"Saturday",
				"Sunday"
			};

			Console.WriteLine("Before:");
			foreach (string day in daysOfWeek)
				Console.WriteLine(day);

			daysOfWeek[2] = "Wednesday";

			Console.WriteLine("\r\nBefore:");
			foreach (string day in daysOfWeek)
				Console.WriteLine(day);
		}
	}
}
