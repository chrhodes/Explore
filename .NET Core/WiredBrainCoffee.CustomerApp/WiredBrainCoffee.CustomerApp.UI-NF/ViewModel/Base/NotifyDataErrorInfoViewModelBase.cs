using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WiredBrainCoffee.CustomerApp.UI.ViewModel.Base
{
  public class NotifyDataErrorInfoViewModelBase : ViewModelBase, INotifyDataErrorInfo
  {
    private readonly Dictionary<string, List<string>> _errorsByProperty = new Dictionary<string, List<string>>();

    public bool HasErrors => _errorsByProperty.Any();

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    public IEnumerable GetErrors(string propertyName)
    {
      return _errorsByProperty.ContainsKey(propertyName)
         ? _errorsByProperty[propertyName]
         : Enumerable.Empty<string>();
    }

    protected virtual void RaiseErrorsChanged(string propertyName)
    {
      ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    protected virtual void ValidateProperty(object model,[CallerMemberName] string propertyName = null)
    {
      if (_errorsByProperty.ContainsKey(propertyName))
      {
        _errorsByProperty.Remove(propertyName);
      }

      var propertyValue = model.GetType().GetProperty(propertyName).GetValue(model, null);

      var validationResults = new List<ValidationResult>();
      var ctx = new ValidationContext(model) { MemberName = propertyName };
      Validator.TryValidateProperty(propertyValue, ctx, validationResults);

      if (validationResults.Any())
      {
        _errorsByProperty[propertyName] = new List<string>();
        foreach (var validationResult in validationResults)
        {
          _errorsByProperty[propertyName].Add(validationResult.ErrorMessage);
        }
      }

      RaiseErrorsChanged(propertyName);
      OnPropertyChanged(nameof(HasErrors));
    }
  }
}
