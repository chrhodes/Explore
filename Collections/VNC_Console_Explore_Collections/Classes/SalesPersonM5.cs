using System;
using System.Collections.Generic;
using System;
using System.Threading;

using VNC_Console_Explore_Collections.Pages;

namespace VNC_Console_Explore_Collections.Classes
{
	public class SalesPersonM5
	{
		public string Name { get; private set; }

		public SalesPersonM5(string id)
		{
			this.Name = id;
		}

		public void Work(StockControllerM5 stockController, TimeSpan workDay)
		{
			Random rand = new Random(Name.GetHashCode());
			DateTime start = DateTime.Now;
			while (DateTime.Now - start < workDay)
			{
				Thread.Sleep(rand.Next(100));
				bool buy = (rand.Next(6) == 0);
				string itemName = Concurrent_M5_SalesBonuses.AllShirtNames[rand.Next(Concurrent_M5_SalesBonuses.AllShirtNames.Count)];
				if (buy)
				{
					int quantity = rand.Next(9) + 1;
					stockController.BuyStock(this, itemName, quantity);
					DisplayPurchase(itemName, quantity);
				}
				else
				{
					bool success = stockController.TrySellItem(this, itemName);
					DisplaySaleAttempt(success, itemName);
				}
			}
			Console.WriteLine("SalesPerson {0} signing off", this.Name);
		}

		public void DisplayPurchase(string itemName, int quantity)
		{
			Console.WriteLine("Thread {0}: {1} bought {2} of {3}", Thread.CurrentThread.ManagedThreadId, this.Name, quantity, itemName);
		}

		public void DisplaySaleAttempt(bool success, string itemName)
		{
			int threadId = Thread.CurrentThread.ManagedThreadId;
			if (success)
				Console.WriteLine(string.Format("Thread {0}: {1} sold {2}", threadId, this.Name, itemName));
			else
				Console.WriteLine(string.Format("Thread {0}: {1}: Out of stock of {2}", threadId, this.Name, itemName));
		}
	}
}
