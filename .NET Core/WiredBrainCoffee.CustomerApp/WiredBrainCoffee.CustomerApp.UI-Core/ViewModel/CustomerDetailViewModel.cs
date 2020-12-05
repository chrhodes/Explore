using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using System.Windows.Media;
using WiredBrainCoffee.CustomerApp.Models;
using WiredBrainCoffee.CustomerApp.UI.DataProvider;
using WiredBrainCoffee.CustomerApp.UI.Dialogs;
using WiredBrainCoffee.CustomerApp.UI.Events;
using WiredBrainCoffee.CustomerApp.UI.ViewModel.Base;

namespace WiredBrainCoffee.CustomerApp.UI.ViewModel
{
  public class CustomerDetailViewModel : NotifyDataErrorInfoViewModelBase
  {
    private readonly ICustomerDataProvider _customerDataProvider;
    private readonly IColorDialogService _colorDialogService;
    private readonly IMessageBoxService _messageBoxService;
    private readonly IEventAggregator _eventAggregator;
    private readonly ISendShirtDataProvider _sendShirtDataProvider;
    private Customer _customer;
    private bool _hasChanges;
    private Color? _favoriteColor;

    public CustomerDetailViewModel(ICustomerDataProvider customerDataProvider,
      IColorDialogService colorDialogService,
      IMessageBoxService messageBoxService,
      IEventAggregator eventAggregator,
      ISendShirtDataProvider sendShirtDataProvider)
    {
      _customerDataProvider = customerDataProvider;
      _colorDialogService = colorDialogService;
      _messageBoxService = messageBoxService;
      _eventAggregator = eventAggregator;
      _sendShirtDataProvider = sendShirtDataProvider;
      SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
      DeleteCommand = new DelegateCommand(OnDeleteExecute, OnDeleteCanExecute);
      SendShirtToCustomerCommand = new DelegateCommand(OnSendShirtToCustomerExecute, OnSendShirtToCustomerCanExecute);
      ChangeFavoriteColorCommand = new DelegateCommand(OnChangeFavoriteColorExecute);
    }
    
    public int CustomerId => _customer.Id;

    public string FirstName
    {
      get { return _customer.FirstName; }
      set
      {
        _customer.FirstName = value;
        OnPropertyChanged();
        HasChanges = true;
        ValidateProperty(_customer);
        SaveCommand.RaiseCanExecuteChanged();
      }
    }

    public string LastName
    {
      get { return _customer.LastName; }
      set
      {
        _customer.LastName = value;
        OnPropertyChanged();
        HasChanges = true;
        ValidateProperty(_customer);
        SaveCommand.RaiseCanExecuteChanged();
      }
    }

    public Color? FavoriteColor => _favoriteColor;

    public bool HasChanges
    {
      get => _hasChanges;
      set
      {
        _hasChanges = value;
        OnPropertyChanged();
        SaveCommand.RaiseCanExecuteChanged();
      }
    }

    public DelegateCommand SaveCommand { get; }

    public DelegateCommand DeleteCommand { get; }

    public DelegateCommand SendShirtToCustomerCommand { get; }

    public DelegateCommand ChangeFavoriteColorCommand { get; }

    public async Task LoadAsync(int customerId)
    {
      _customer = await _customerDataProvider.LoadCustomerByIdAsync(customerId);
      if (!string.IsNullOrEmpty(_customer.FavoriteColor))
      {
        _favoriteColor = (Color)ColorConverter.ConvertFromString(_customer.FavoriteColor);
        SendShirtToCustomerCommand.RaiseCanExecuteChanged();
      }
      OnPropertyChanged("");
    }

    public void CreateNewCustomer()
    {
      _customer = new Customer();
      ValidateProperty(_customer, nameof(_customer.FirstName));
    }

    private void OnChangeFavoriteColorExecute()
    {
      if (_colorDialogService.ShowDialog(ref _favoriteColor))
      {
        _customer.FavoriteColor = _favoriteColor.ToString();
        OnPropertyChanged(nameof(FavoriteColor));
        HasChanges = true;
        SendShirtToCustomerCommand.RaiseCanExecuteChanged();
      }
    }

    private async void OnSaveExecute()
    {
      await _customerDataProvider.SaveCustomerAsync(_customer);
      OnPropertyChanged(nameof(CustomerId));
      HasChanges = false;
      DeleteCommand.RaiseCanExecuteChanged();
      _eventAggregator.GetEvent<AfterCustomerSavedEvent>().Publish(
        new AfterCustomerSavedEventArgs(_customer));
    }

    private bool OnSaveCanExecute()
    {
      return HasChanges && !HasErrors;
    }

    private async void OnDeleteExecute()
    {
      var result = _messageBoxService.ShowOkCancelBox($"Do you want to delete the customer '{_customer.FirstName} {_customer.LastName}'?", "Question");
      if (result == System.Windows.MessageBoxResult.OK)
      {
        await _customerDataProvider.DeleteCustomerAsync(_customer);
        _eventAggregator.GetEvent<AfterCustomerDeletedEvent>().Publish(
          new AfterCustomerDeletedEventArgs(_customer.Id));
      }
    }

    private bool OnDeleteCanExecute()
    {
      return CustomerId > 0;
    }

    private async void OnSendShirtToCustomerExecute()
    {
      if (FavoriteColor.HasValue)
      {
        await _sendShirtDataProvider.SendShirtToCustomerAsync(_customer, FavoriteColor.ToString());
      }
    }

    private bool OnSendShirtToCustomerCanExecute()
    {
      return FavoriteColor.HasValue;
    }
  }
}
