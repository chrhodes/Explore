using System.Data.Entity.Migrations;

using FriendOrganizer.Domain;

namespace FriendOrganizer.FriendOrganizer.DataAccess10.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<FriendOrganizerDbContext10>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FriendOrganizerDbContext10 context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Friends05.AddOrUpdate(
              f => f.FirstName,
              new Friend05 { FirstName = "Vikki", LastName = "Schanz" },
              new Friend05 { FirstName = "Natalie", LastName = "Rhodes" },
              new Friend05 { FirstName = "Christopher", LastName = "Rhodes" },
              new Friend05 { FirstName = "George", LastName = "Lachow" }
              );
        }
    }
}
