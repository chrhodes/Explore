using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WiredBrainCoffee.CustomerApp.Models;

namespace WiredBrainCoffee.CustomerApp.DataAccess
{
  public class CustomerDbContext : DbContext
  {
    public CustomerDbContext() : base("CustomerDatabase")
    {
    }

    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
    }
  }
}
