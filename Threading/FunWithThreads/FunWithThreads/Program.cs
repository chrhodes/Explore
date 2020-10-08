using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  System.Threading;

namespace FunWithThreads
{
    class Program
    {
        static void Main(string[] args)
        {
            // Queue the task.
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc));

            Console.WriteLine("Main thread does some work, then sleeps.");
            // If you comment out the Sleep, the main thread exits before
            // the thread pool task runs.  The thread pool uses background
            // threads, which do not keep the application running.  (This
            // is a simple example of a race condition.)

            if (args.Length > 0)
            {
                Thread.Sleep(2000);            	;
            }

            int availableCompletionPortThreads;
            int availableWorkerThreads;
            int maxCompletionPortThreads;
            int maxWorkerThreads;    
            int minCompletionPortThreads;
            int minWorkerThreads;

            ThreadPool.GetAvailableThreads(out availableWorkerThreads, out availableCompletionPortThreads);
            ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxCompletionPortThreads);
            ThreadPool.GetMinThreads(out minWorkerThreads, out minCompletionPortThreads);
            
            Console.WriteLine(string.Format("Main thread exits. AvailableThreads {0}:{1} MaxThreads {2}:{3} MinThreads {4}:{5}",
                availableCompletionPortThreads,
                availableWorkerThreads,
                maxCompletionPortThreads,
                maxWorkerThreads,
                minCompletionPortThreads,
                minWorkerThreads
                ));
        }

        // This thread procedure performs the task.
        static void ThreadProc(Object stateInfo)
        {
            // No state object was passed to QueueUserWorkItem, so 
            // stateInfo is null.
            Thread.Sleep(500);
            Console.WriteLine("Hello from the thread pool.");
        }
    }
}
