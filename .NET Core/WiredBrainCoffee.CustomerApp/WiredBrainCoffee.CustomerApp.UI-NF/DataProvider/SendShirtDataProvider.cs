using WiredBrainCoffee.CustomerApp.Models;

namespace WiredBrainCoffee.CustomerApp.UI.DataProvider
{
  public interface ISendShirtDataProvider
  {
    void SendShirtToCustomer(Customer customer, string colorCode);
  }

  public class SendShirtDataProvider : ISendShirtDataProvider
  {
    public void SendShirtToCustomer(Customer customer, string colorCode)
    {
      using (var client = new SendShirtServiceReference.SendShirtServiceClient())
      {
        client.SendShirtToCustomer(customer.Id, $"{customer.FirstName} {customer.LastName}", colorCode);
      }
    }
  }
}
