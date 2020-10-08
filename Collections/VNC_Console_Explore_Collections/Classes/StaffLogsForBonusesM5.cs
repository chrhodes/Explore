using System;
using System.Collections.Concurrent;
using System.Threading;

namespace VNC_Console_Explore_Collections.Classes
{
	public class StaffLogsForBonusesM5
	{
		private ConcurrentDictionary<SalesPersonM5, int> _salesByPerson = new ConcurrentDictionary<SalesPersonM5, int>();
		private ConcurrentDictionary<SalesPersonM5, int> _purchasesByPerson =
			 new ConcurrentDictionary<SalesPersonM5, int>();

		public void ProcessTrade(TradeM5 sale)
		{
			Thread.Sleep(300);
			if (sale.QuantitySold > 0)
				_salesByPerson.AddOrUpdate(
					sale.Person,
					sale.QuantitySold,
					(key, oldValue) => oldValue + sale.QuantitySold);
			else
				_purchasesByPerson.AddOrUpdate(
					sale.Person,
					-sale.QuantitySold,
					(key, oldValue) => oldValue - sale.QuantitySold);
		}

		public void DisplayReport(SalesPersonM5[] people)
		{
			Console.WriteLine();
			Console.WriteLine("Transactions by salesperson:");

			foreach (SalesPersonM5 person in people)
			{
				int sales = _salesByPerson.GetOrAdd(person, 0);
				int purchases = _purchasesByPerson.GetOrAdd(person, 0);
				Console.WriteLine("{0,15} sold {1,3}, bought {2,3} items, total {3}", person.Name, sales, purchases, sales + purchases);
			}
		}
	}
}
