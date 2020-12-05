using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiredBrainCoffee.CustomerApp.Models;

namespace WiredBrainCoffee.CustomerApp.UI.DataProvider
{
  //
  // If you don't want to set up a database,
  // you can use this CustomerInMemoryDataProvider
  //
  // USAGE:
  // Go to the Startup\Bootstrapper.cs file and use it
  // instead of the CustomerDbDataProvider. You find more
  // information in the Startup\Bootstrapper.cs file
  //
  public class CustomerInMemoryDataProvider : ICustomerDataProvider
  {
    private static readonly List<Customer> _storageList;

    static CustomerInMemoryDataProvider()
    {
      _storageList = new List<Customer>
      {
        new Customer { Id = 1, FirstName = "Thomas Claudius", LastName = "Huber", FavoriteColor = "#FF0080FF" },
        new Customer { Id = 2, FirstName = "Anna", LastName = "Baier", FavoriteColor = "#FFFF00FF" },
        new Customer { Id = 3, FirstName = "Julia", LastName = "Master", FavoriteColor = "#FFFFFF80" },
        new Customer { Id = 4, FirstName = "Sara", LastName = "Ramone", FavoriteColor = "#FF00FF00" }
      };
    }
    public Task DeleteCustomerAsync(Customer customer)
    {
      var customerToDelete = _storageList.Single(c => c.Id == customer.Id);
      _storageList.Remove(customerToDelete);

      return Task.Delay(0);
    }

    public Task<IEnumerable<Customer>> GetAllAsync()
    {
      return Task.FromResult(GetCopyOfStorageList());
    }

    public Task<Customer> LoadCustomerByIdAsync(int id)
    {
      var customer = _storageList.Single(c => c.Id == id);
      return Task.FromResult(CopyCustomer(customer));
    }

    public Task SaveCustomerAsync(Customer customer)
    {
      Customer customerToUpdate;
      if (customer.Id == 0)
      {
        customerToUpdate = new Customer
        {
          Id = _storageList.Max(c => c.Id) + 1
        };
        _storageList.Add(customerToUpdate);

        customer.Id = customerToUpdate.Id;
      }
      else
      {
        customerToUpdate = _storageList.Single(c => c.Id == customer.Id);
      }

      customerToUpdate.FirstName = customer.FirstName;
      customerToUpdate.LastName = customer.LastName;
      customerToUpdate.FavoriteColor = customer.FavoriteColor;

      return Task.Delay(0);
    }

    private static IEnumerable<Customer> GetCopyOfStorageList()
    {
      return _storageList.Select(CopyCustomer);
    }

    private static Customer CopyCustomer(Customer customer)
    {
      return new Customer
      {
        Id = customer.Id,
        FirstName = customer.FirstName,
        LastName = customer.LastName,
        FavoriteColor = customer.FavoriteColor
      };
    }
  }
}
