namespace VNC_Console_Explore_Collections.Classes
{
	public static class WorkerM6
	{
		public static int DoSomethingTimeConsuming()
		{
			int total = 0;
			for (int i = 0; i < 1000; i++)
				total += i;
			return total;
		}
	}
}
