using System.Data.Entity.Migrations;
using WiredBrainCoffee.CustomerApp.Models;

namespace WiredBrainCoffee.CustomerApp.DataAccess.Migrations
{
  internal sealed class Configuration : DbMigrationsConfiguration<CustomerDbContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(CustomerDbContext context)
    {
      context.Customers.AddOrUpdate(
        c => new { c.FirstName, c.LastName },
        new Customer { FirstName = "Thomas Claudius", LastName = "Huber", FavoriteColor = "#FF0080FF" },
        new Customer { FirstName = "Anna", LastName = "Baier", FavoriteColor = "#FFFF00FF" },
        new Customer { FirstName = "Sara", LastName = "Ramone", FavoriteColor = "#FFFFFF80" },
        new Customer { FirstName = "Julia", LastName = "Master", FavoriteColor = "#FF00FF00" }
      );
    }
  }
}
