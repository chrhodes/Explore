using Prism.Events;

namespace WiredBrainCoffee.CustomerApp.UI.Events
{
  public class OpenCustomerDetailViewEvent : PubSubEvent<OpenCustomerDetailViewEventArgs>
  {
  }

  public class OpenCustomerDetailViewEventArgs
  {
    public OpenCustomerDetailViewEventArgs(int customerId)
    {
      CustomerId = customerId;
    }

    public int CustomerId { get; }
  }
}
