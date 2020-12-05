using Prism.Events;

namespace WiredBrainCoffee.CustomerApp.UI.Events
{
  public class AfterCustomerDeletedEvent : PubSubEvent<AfterCustomerDeletedEventArgs>
  {
  }

  public class AfterCustomerDeletedEventArgs
  {
    public AfterCustomerDeletedEventArgs(int customerId)
    {
      CustomerId = customerId;
    }

    public int CustomerId { get; }
  }
}
