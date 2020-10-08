using System;
using System.Collections.Concurrent;
using System.Threading;

namespace VNC_Console_Explore_Collections.Classes
{

	public class ToDoQueueM5
	{
		private readonly ConcurrentQueue<TradeM5> _queue = new ConcurrentQueue<TradeM5>();
		private bool _workingDayComplete = false;
		private readonly StaffLogsForBonusesM5 _staffLogs;

		public ToDoQueueM5(StaffLogsForBonusesM5 staffResults)
		{
			_staffLogs = staffResults;
		}

		public void AddTrade(TradeM5 transaction)
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
				TradeM5 nextTrade;
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
