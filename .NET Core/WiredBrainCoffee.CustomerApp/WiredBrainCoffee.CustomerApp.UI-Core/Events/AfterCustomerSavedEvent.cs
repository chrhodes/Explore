using Prism.Events;
using WiredBrainCoffee.CustomerApp.Models;

namespace WiredBrainCoffee.CustomerApp.UI.Events
{
  public class AfterCustomerSavedEvent : PubSubEvent<AfterCustomerSavedEventArgs>
  {
  }

  public class AfterCustomerSavedEventArgs
  {
    public AfterCustomerSavedEventArgs(Customer savedCustomer)
    {
      SavedCustomer = savedCustomer;
    }

    public Customer SavedCustomer { get; }
  }
}
