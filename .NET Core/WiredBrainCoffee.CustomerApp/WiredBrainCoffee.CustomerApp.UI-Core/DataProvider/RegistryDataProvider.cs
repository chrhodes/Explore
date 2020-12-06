using Microsoft.Win32;

namespace WiredBrainCoffee.CustomerApp.UI.DataProvider
{
  public interface IRegistryDataProvider
  {
    object GetValue(string name);
    void SaveValue(string name, object value);
  }

  public class RegistryDataProvider : IRegistryDataProvider
  {
    public void SaveValue(string name, object value)
    {
      using (RegistryKey key = CreateKeyIfNotExists())
      {
        key.SetValue(name, value);
      }
    }
    public object GetValue(string name)
    {
      using (RegistryKey key = CreateKeyIfNotExists())
      {
        return key.GetValue(name);
      }
    }

    private static RegistryKey CreateKeyIfNotExists()
    {
      var keyName = @"Software\WiredBrainCoffee\CustomerApp";

      var key = Registry.CurrentUser.OpenSubKey(keyName, true);
      if (key == null)
      {
        key = Registry.CurrentUser.CreateSubKey(keyName);
      }

      return key;
    }
  }
}
