using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using WiredBrainCoffee.CustomerApp.Models;
using WiredBrainCoffee.CustomerApp.DataAccess;
using System.Linq;

namespace WiredBrainCoffee.CustomerApp.UI.DataProvider
{
  public interface ICustomerDataProvider
  {
    Task<IEnumerable<Customer>> GetAllAsync();
    Task SaveCustomerAsync(Customer customer);
    Task DeleteCustomerAsync(Customer customer);
    Task<Customer> LoadCustomerByIdAsync(int id);
  }

  public class CustomerDbDataProvider : ICustomerDataProvider
  {
    private readonly Func<CustomerDbContext> _contextFactory;

    public CustomerDbDataProvider(Func<CustomerDbContext> contextFactory)
    {
      _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
      using (var ctx = _contextFactory())
      {
        // Create explicitly a new Thread with Task.Run,
        // as this is the first database call that takes a few seconds
        // and as ToListAsync doesn't do the job, as it blocks the UI thread on that first database call
        var customerList = await Task.Run(() => ctx.Customers.AsNoTracking().ToList());
        return customerList;
      }
    }

    public async Task<Customer> LoadCustomerByIdAsync(int id)
    {
      using (var ctx = _contextFactory())
      {
        return await ctx.Customers.FindAsync(id);
      }
    }

    public async Task SaveCustomerAsync(Customer customer)
    {
      using (var ctx = _contextFactory())
      {
        ctx.Customers.Add(customer);
        if (customer.Id > 0)
        {
          ctx.Entry(customer).State = EntityState.Modified;
        }
        await ctx.SaveChangesAsync();
      }
    }

    public async Task DeleteCustomerAsync(Customer customer)
    {
      using (var ctx = _contextFactory())
      {
        ctx.Customers.Add(customer);
        ctx.Entry(customer).State = EntityState.Deleted;
        await ctx.SaveChangesAsync();
      }
    }
  }
}
