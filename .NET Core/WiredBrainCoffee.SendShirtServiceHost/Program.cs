using System;
using System.ServiceModel;
using WiredBrainCoffee.SendShirtServiceHost.Service;

namespace WiredBrainCoffee.SendShirtServiceHost
{
  class Program
  {
    static void Main(string[] args)
    {
      using (var host = new ServiceHost(typeof(SendShirtService)))
      {
        host.Open();

        Console.WriteLine("Service is running.");
        foreach (var baseAddress in host.BaseAddresses)
        {
          Console.WriteLine($"- BaseAddress: {baseAddress}");
        }

        Console.WriteLine();
        Console.WriteLine("Waiting for incoming shirt requests");
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();

        host.Close();
      }
    }
  }
}
