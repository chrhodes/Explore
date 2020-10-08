namespace VNC_Console_Explore_Collections.Classes
{
	public class TradeM5
	{
		public SalesPersonM5 Person { get; private set; }

		//  QuantitySold is negative if the trade was a purchase
		public int QuantitySold { get; private set; }

		public TradeM5(SalesPersonM5 person, int quantitySold)
		{
			this.Person = person;
			this.QuantitySold = quantitySold;
		}
	}
}
