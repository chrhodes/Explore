using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using WiredBrainCoffee.CustomerApp.Models;
using WiredBrainCoffee.CustomerApp.UI.DataProvider;
using WiredBrainCoffee.CustomerApp.UI.Events;
using WiredBrainCoffee.CustomerApp.UI.ViewModel.Base;

namespace WiredBrainCoffee.CustomerApp.UI.ViewModel
{
  public class NavigationViewModel
  {
    private readonly ICustomerDataProvider _customerDataProvider;
    private readonly IEventAggregator _eventAggregator;

    public NavigationViewModel(ICustomerDataProvider customerDataProvider,
      IEventAggregator eventAggregator)
    {
      Customers = new ObservableCollection<NavigationItemViewModel>();

      _customerDataProvider = customerDataProvider;
      _eventAggregator = eventAggregator;
      _eventAggregator.GetEvent<AfterCustomerSavedEvent>().Subscribe(AfterCustomerSaved);
      _eventAggregator.GetEvent<AfterCustomerDeletedEvent>().Subscribe(AfterCustomerDeleted);
    }

    public ObservableCollection<NavigationItemViewModel> Customers { get; }

    public async Task LoadAsync()
    {
      Customers.Clear();

      var customers = await _customerDataProvider.GetAllAsync();

      foreach (var customer in customers)
      {
        Customers.Add(new NavigationItemViewModel(customer.Id, GetCustomerDisplayString(customer),
          _eventAggregator));
      }
    }

    public void SelectCustomerInNavigation(int customerId)
    {
      foreach (var navigationItemViewModel in Customers)
      {
        navigationItemViewModel.IsSelected = navigationItemViewModel.CustomerId == customerId;
      }
    }

    private void AfterCustomerDeleted(AfterCustomerDeletedEventArgs args)
    {
      var navigationItem = Customers.SingleOrDefault(x => x.CustomerId == args.CustomerId);
      if (navigationItem != null)
      {
        Customers.Remove(navigationItem);
      }
    }

    private void AfterCustomerSaved(AfterCustomerSavedEventArgs args)
    {
      var navigationItem = Customers.SingleOrDefault(x => x.CustomerId == args.SavedCustomer.Id);
      if (navigationItem == null)
      {
        Customers.Add(new NavigationItemViewModel(args.SavedCustomer.Id,
          GetCustomerDisplayString(args.SavedCustomer), _eventAggregator)
        {
          IsSelected = true
        });
      }
      else
      {
        navigationItem.DisplayMember = GetCustomerDisplayString(args.SavedCustomer);
      }
    }

    private string GetCustomerDisplayString(Customer customer)
    {
      return $"{customer.FirstName} {customer.LastName}";
    }
  }

  public class NavigationItemViewModel : ViewModelBase
  {
    private bool _isSelected;
    private string _displayMember;
    private IEventAggregator _eventAggregator;

    public NavigationItemViewModel(int customerId, string displayMember,
      IEventAggregator eventAggregator)
    {
      _eventAggregator = eventAggregator;
      CustomerId = customerId;
      DisplayMember = displayMember;
      OpenCustomerDetailViewCommand = new DelegateCommand(OnOpenCustomerDetailViewExecute);
    }

    public int CustomerId { get; }

    public string DisplayMember
    {
      get { return _displayMember; }
      set
      {
        _displayMember = value;
        OnPropertyChanged();
      }
    }

    public bool IsSelected
    {
      get { return _isSelected; }
      set
      {
        _isSelected = value;
        OnPropertyChanged();
      }
    }

    public ICommand OpenCustomerDetailViewCommand { get; }

    private void OnOpenCustomerDetailViewExecute()
    {
      _eventAggregator.GetEvent<OpenCustomerDetailViewEvent>()
            .Publish(new OpenCustomerDetailViewEventArgs(CustomerId));
    }
  }
}
