using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplicationMSMQ
{
	public class Order
	{
        public int orderId;
        public int processId;
        public int threadId;
		public DateTime orderTime;

        public Order()
        {
            orderId = 0;
            processId = System.Diagnostics.Process.GetCurrentProcess().Id;
            threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            orderTime = DateTime.Now;
        }

        public Order(int id)
        {
            orderId = id;
            processId = System.Diagnostics.Process.GetCurrentProcess().Id;
            threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            orderTime = DateTime.Now;
        }

        public override string ToString()
        {
            return string.Format("OrderID {0}:{1}:{2}:{3}",
                orderId, processId, threadId, orderTime);
        }
	};
}
