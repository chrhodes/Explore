using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows;
using WiredBrainCoffee.CustomerApp.UI.DataProvider;
using WiredBrainCoffee.CustomerApp.UI.Dialogs;
using WiredBrainCoffee.CustomerApp.UI.Events;
using WiredBrainCoffee.CustomerApp.UI.ViewModel.Base;

namespace WiredBrainCoffee.CustomerApp.UI.ViewModel
{
  public class MainViewModel : ViewModelBase
  {
    private readonly Func<CustomerDetailViewModel> _customerDetailViewModelCreator;
    private readonly IEventAggregator _eventAggregator;
    private readonly IMessageBoxService _messageBoxService;
    private readonly IRegistryDataProvider _registryDataProvider;
    private CustomerDetailViewModel _customerDetailViewModel;
    private NavigationAlignment _navigationAlignment;
    private bool _isInitializing;

    public MainViewModel(NavigationViewModel navigationViewModel,
      Func<CustomerDetailViewModel> customerDetailViewModelCreator,
      IEventAggregator eventAggregator,
      IMessageBoxService messageBoxService,
      IRegistryDataProvider registryDataProvider)
    {
      NavigationViewModel = navigationViewModel;
      _customerDetailViewModelCreator = customerDetailViewModelCreator;
      _eventAggregator = eventAggregator;
      _messageBoxService = messageBoxService;
      _registryDataProvider = registryDataProvider;
      _eventAggregator.GetEvent<OpenCustomerDetailViewEvent>()
       .Subscribe(OnOpenCustomerDetailView);
      _eventAggregator.GetEvent<AfterCustomerDeletedEvent>().Subscribe(AfterCustomerDeleted);
      MoveNavigationCommand = new DelegateCommand(OnMoveNavigationExecute);
      AddCustomerCommand = new DelegateCommand(OnAddCustomerExecute, OnAddCustomerCanExecute);
    }

    public NavigationViewModel NavigationViewModel { get; }

    public CustomerDetailViewModel CustomerDetailViewModel
    {
      get => _customerDetailViewModel;
      private set
      {
        _customerDetailViewModel = value;
        OnPropertyChanged();
      }
    }

    public NavigationAlignment NavigationAlignment
    {
      get { return _navigationAlignment; }
      set
      {
        _navigationAlignment = value;
        OnPropertyChanged();
      }
    }

    public bool IsInitializing
    {
      get => _isInitializing;
      set
      {
        _isInitializing = value;
        OnPropertyChanged();
        AddCustomerCommand.RaiseCanExecuteChanged();
      }
    }

    public DelegateCommand MoveNavigationCommand { get; }

    public DelegateCommand AddCustomerCommand { get; }

    public async Task InitializeAsync()
    {
      IsInitializing = true;
      try
      {
        LoadRegistryValueForNavigation();
        await NavigationViewModel.LoadAsync();
      }
      finally
      {
        IsInitializing = false;
      }
    }

    private void AfterCustomerDeleted(AfterCustomerDeletedEventArgs obj)
    {
      CustomerDetailViewModel = null;
    }

    private async void OnOpenCustomerDetailView(OpenCustomerDetailViewEventArgs args)
    {
      if (CustomerDetailViewModel?.CustomerId == args.CustomerId)
      {
        return;
      }

      bool openNewView = CheckIfShouldOpenNewView();

      if (openNewView)
      {
        var customerDetailViewModel = _customerDetailViewModelCreator();
        try
        {
          await customerDetailViewModel.LoadAsync(args.CustomerId);
          CustomerDetailViewModel = customerDetailViewModel;
          NavigationViewModel.SelectCustomerInNavigation(CustomerDetailViewModel.CustomerId);
        }
        catch
        {
          _messageBoxService.ShowInfoBox("Could not load the entity, " +
             "maybe it was deleted in the meantime by another user. " +
             "The navigation is refreshed for you.");
          await NavigationViewModel.LoadAsync();
          return;
        }
      }
    }

    private bool CheckIfShouldOpenNewView()
    {
      var openNewView = true;

      if (CustomerDetailViewModel?.HasChanges == true)
      {
        var result = _messageBoxService.ShowOkCancelBox("When you navigate away, you will lose your changes. Continue?", "Question");
        if (result == MessageBoxResult.Cancel)
        {
          openNewView = false;
        }
      }

      return openNewView;
    }

    private void OnAddCustomerExecute()
    {
      if (CustomerDetailViewModel?.CustomerId == 0)
      {
        return;
      }

      bool openNewView = CheckIfShouldOpenNewView();

      if (openNewView)
      {
        var customerDetailViewModel = _customerDetailViewModelCreator();
        customerDetailViewModel.CreateNewCustomer();
        CustomerDetailViewModel = customerDetailViewModel;
        NavigationViewModel.SelectCustomerInNavigation(CustomerDetailViewModel.CustomerId);
      }
    }

    private bool OnAddCustomerCanExecute()
    {
      return !IsInitializing;
    }

    private void OnMoveNavigationExecute()
    {
      NavigationAlignment = NavigationAlignment == NavigationAlignment.Left
        ? NavigationAlignment.Right
        : NavigationAlignment.Left;

      _registryDataProvider.SaveValue(nameof(NavigationAlignment), NavigationAlignment.ToString());
    }

    private void LoadRegistryValueForNavigation()
    {
      var value = _registryDataProvider.GetValue(nameof(NavigationAlignment));
      if (value != null
       && Enum.TryParse(value.ToString(), out NavigationAlignment result))
      {
        NavigationAlignment = result;
      }
    }
  }

  public enum NavigationAlignment
  {
    Left,
    Right
  }
}
