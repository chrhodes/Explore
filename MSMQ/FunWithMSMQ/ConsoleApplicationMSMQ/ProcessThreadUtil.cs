using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplicationMSMQ
{
    class ProcessThreadUtil
    {
        public static void DisplayInfo(string location)
        {
            Console.WriteLine(string.Format("{0,50} Tds={3,2} PID={1,5} TID={2,2}", 
                location,
                System.Diagnostics.Process.GetCurrentProcess().Id,
                System.Threading.Thread.CurrentThread.ManagedThreadId,
                System.Diagnostics.Process.GetCurrentProcess().Threads.Count));
        }
    }
}
