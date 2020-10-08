using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using EasyConsole;
using VNC_Console_Explore_Collections.Classes;

namespace VNC_Console_Explore_Threading.Pages
{
    class M6_SingleThreadBenchmarkConcurrentDictionary : Page
    {
        public M6_SingleThreadBenchmarkConcurrentDictionary(Program program) : base("M6_SingleThreadBenchmarkConcurrentDictionary", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from M6_SingleThreadBenchmarkConcurrentDictionary");

            Benchmark();

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }

        void Benchmark()
        {
            // comment out all calls to Worker.DoSomethingTimeConsuming() 
            // throughout project to see how the benchmark works when threads spend
            // most of their time dealing with the concurrent dictionary
            int dictSize = 1000000;

            Console.WriteLine("\r\nConcurrentDictionary, single thread:");
            var dict2 = new ConcurrentDictionary<int, int>();
            SingleThreadBenchmarkM6.TimeDict(dict2, dictSize);
        }
    }
}
