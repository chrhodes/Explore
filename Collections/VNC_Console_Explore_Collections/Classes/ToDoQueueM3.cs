using System;
using System.Collections.Concurrent;
using System.Threading;

namespace VNC_Console_Explore_Collections.Classes
{

	public class ToDoQueueM3
	{
		private readonly ConcurrentQueue<TradeM3> _queue = new ConcurrentQueue<TradeM3>();
		private bool _workingDayComplete = false;
		private readonly StaffLogsForBonusesM3 _staffLogs;

		public ToDoQueueM3(StaffLogsForBonusesM3 staffResults)
		{
			_staffLogs = staffResults;
		}

		public void AddTrade(TradeM3 transaction)
		{
			_queue.Enqueue(transaction);
		}

		public void CompleteAdding()
		{
			_workingDayComplete = true;
		}

		public void MonitorAndLogTrades()
		{
			while (true)
			{
				TradeM3 nextTrade;
				bool done = _queue.TryDequeue(out nextTrade);
				if (done)
				{
					_staffLogs.ProcessTrade(nextTrade);
					Console.WriteLine("Processing transaction from " + nextTrade.Person.Name);
				}
				else if (_workingDayComplete)
				{
					Console.WriteLine("No more sales to log - exiting");
					return;
				}
				else
				{
					Console.WriteLine("No transactions available");
					Thread.Sleep(500);
				}
			}
		}

	}
}
