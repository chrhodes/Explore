using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace VNC_Console_Explore_Collections.Classes
{
	class SingleThreadBenchmarkM6
	{
		static void PopulateDict(IDictionary<int, int> dict, int dictSize)
		{
			for (int i = 0; i < dictSize; i++)
			{
				dict.Add(i, 0);
			}

			for (int i = 0; i < dictSize; i++)
			{
				dict[i] += 1;
				WorkerM6.DoSomethingTimeConsuming();
			}
		}
		static int GetTotalValue(IDictionary<int, int> dict)
		{
			int total = 0;
			foreach (var item in dict)
			{
				total += dict[item.Value];
				WorkerM6.DoSomethingTimeConsuming();
			}
			return total;
		}

		public static void TimeDict(IDictionary<int, int> dict, int dictSize)
		{
			Stopwatch stopwatch = new Stopwatch();

			stopwatch.Start();
			PopulateDict(dict, dictSize);
			stopwatch.Stop();
			Console.WriteLine(string.Format("Time taken to build dictionary (ms):     {0}", stopwatch.ElapsedMilliseconds));

			stopwatch.Restart();
			int total = GetTotalValue(dict);
			stopwatch.Stop();
			Console.WriteLine(string.Format("Time taken to enumerate dictionary (ms): {0}", stopwatch.ElapsedMilliseconds));

			Console.WriteLine("total is " + total.ToString());
			if (total != dictSize)
				Console.WriteLine("ERROR IN TOTAL!");

		}

	}
}
