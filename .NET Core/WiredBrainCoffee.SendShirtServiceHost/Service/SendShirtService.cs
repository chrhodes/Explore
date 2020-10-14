using System;
using System.ServiceModel;

namespace WiredBrainCoffee.SendShirtServiceHost.Service
{
  [ServiceContract]
  public interface ISendShirtService
  {
    [OperationContract(IsOneWay = true)]
    void SendShirtToCustomer(int id, string fullName, string colorCode);
  }

  public class SendShirtService : ISendShirtService
  {
    public void SendShirtToCustomer(int id, string fullName, string colorCode)
    {
      Console.WriteLine($"Id: {id}; FullName: {fullName}; Color: {colorCode}");
    }
  }
}
