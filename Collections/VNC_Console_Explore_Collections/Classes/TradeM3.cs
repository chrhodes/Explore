namespace VNC_Console_Explore_Collections.Classes
{
	public class TradeM3
	{
		public SalesPersonM3 Person { get; private set; }

		//  QuantitySold is negative if the trade was a purchase
		public int QuantitySold { get; private set; }

		public TradeM3(SalesPersonM3 person, int quantitySold)
		{
			this.Person = person;
			this.QuantitySold = quantitySold;
		}
	}
}
