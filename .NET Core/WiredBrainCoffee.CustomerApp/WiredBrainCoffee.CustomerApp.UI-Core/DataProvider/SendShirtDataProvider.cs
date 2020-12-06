using System.Threading.Tasks;

using WiredBrainCoffee.CustomerApp.Models;

namespace WiredBrainCoffee.CustomerApp.UI.DataProvider
{
  public interface ISendShirtDataProvider
  {
    Task SendShirtToCustomerAsync(Customer customer, string colorCode);
  }

  public class SendShirtDataProvider : ISendShirtDataProvider
  {
    public async Task SendShirtToCustomerAsync(Customer customer, string colorCode)
    {
      using (var client = new SendShirtServiceReference.SendShirtServiceClient())
      {
        await client.SendShirtToCustomerAsync(customer.Id, $"{customer.FirstName} {customer.LastName}", colorCode);
      }
    }
  }
}
