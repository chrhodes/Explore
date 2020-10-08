using System;
using System.Collections.Concurrent;

using EasyConsole;

using VNC_Console_Explore_Collections.Classes;

namespace VNC_Console_Explore_Collections.Pages
{
    class M6_ParallelBenchmarkConcurrentDictionary : Page
    {
        public M6_ParallelBenchmarkConcurrentDictionary(Program program) : base("M6_ParallelBenchmarkConcurrentDictionary", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from M6_ParallelBenchmarkConcurrentDictionary");

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

            Console.WriteLine("\r\nConcurrentDictionary, multiple threads:");
            var dict2 = new ConcurrentDictionary<int, int>();
            ParallelBenchmarkM6.TimeDictParallel(dict2, dictSize);
        }
    }
}
