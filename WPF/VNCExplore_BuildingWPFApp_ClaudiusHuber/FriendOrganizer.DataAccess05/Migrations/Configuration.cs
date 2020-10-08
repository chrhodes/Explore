using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using FriendOrganizer.Domain;

namespace FriendOrganizer.DataAccess05.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<FriendOrganizerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FriendOrganizerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Friends05.AddOrUpdate
            (
                f => f.FirstName,
                new Friend05 { FirstName = "Vikki", LastName = "Schanz" }
                , new Friend05 { FirstName = "Natalie", LastName = "Rhodes" }
                , new Friend05 { FirstName = "Christopher", LastName = "Rhodes" }
                , new Friend05 { FirstName = "George", LastName = "Lachow" }
            );

            context.Friends12.AddOrUpdate
            (
                f => f.FirstName,
                new Friend12 { FirstName = "Vikki", LastName = "Schanz" }
                , new Friend12 { FirstName = "Natalie", LastName = "Rhodes" }
                , new Friend12 { FirstName = "Christopher", LastName = "Rhodes" }
                , new Friend12 { FirstName = "George", LastName = "Lachow" }
            );

            context.Friends13.AddOrUpdate
            (
                f => f.FirstName,
                new Friend13 { FirstName = "Vikki", LastName = "Schanz" }
                , new Friend13 { FirstName = "Natalie", LastName = "Rhodes" }
                , new Friend13 { FirstName = "Christopher", LastName = "Rhodes" }
                , new Friend13 { FirstName = "George", LastName = "Lachow" }
            );

            context.Friends15.AddOrUpdate
            (
                f => f.FirstName,
                new Friend15 { FirstName = "Vikki", LastName = "Schanz" }
                , new Friend15 { FirstName = "Natalie", LastName = "Rhodes" }
                , new Friend15 { FirstName = "Christopher", LastName = "Rhodes" }
                , new Friend15 { FirstName = "George", LastName = "Lachow" }
            );

            context.Friends19.AddOrUpdate
            (
                f => f.FirstName,
                new Friend19 { FirstName = "Vikki", LastName = "Schanz" }
                , new Friend19 { FirstName = "Natalie", LastName = "Rhodes" }
                , new Friend19 { FirstName = "Christopher", LastName = "Rhodes" }
                , new Friend19 { FirstName = "George", LastName = "Lachow" }
            );

            context.ProgrammingLanguages12.AddOrUpdate
            (
                pl => pl.Name,
                new ProgrammingLanguage12 { Name="C#"}
                , new ProgrammingLanguage12 { Name = "VB.NET" }
                , new ProgrammingLanguage12 { Name = "Forth" }
                , new ProgrammingLanguage12 { Name = "Assembly" }
                , new ProgrammingLanguage12 { Name = "APL" }
            );

            context.SaveChanges();

            context.FriendPhoneNumbers13.AddOrUpdate
            (
                pn => pn.Number,
                new FriendPhoneNumber13
                {
                    Number = "+49 12345678",
                    FriendId = context.Friends13.First().Id
                }
            );

            context.Meetings15.AddOrUpdate
            (
                m => m.Title,
                new Meeting15
                {
                    Title = "Watching Star Wars",
                    DateFrom = new System.DateTime(2019, 06, 27),
                    DateTo = new System.DateTime(2019, 06, 27),
                    Friends = new List<Friend15>
                    {
                        context.Friends15.Single(f => f.FirstName == "Christopher" && f.LastName == "Rhodes"),
                        context.Friends15.Single(f => f.FirstName == "Natalie" && f.LastName == "Rhodes"),
                        context.Friends15.Single(f => f.FirstName == "Vikki" && f.LastName == "Schanz")
                    }
                }
            );

            context.Meetings19.AddOrUpdate
            (
                m => m.Title,
                new Meeting19
                {
                    Title = "Watching American Ninja Warrior",
                    DateFrom = new System.DateTime(2019, 07, 04),
                    DateTo = new System.DateTime(2019, 07, 04),
                    Friends = new List<Friend19>
                    {
                        context.Friends19.Single(f => f.FirstName == "Christopher" && f.LastName == "Rhodes"),
                        context.Friends19.Single(f => f.FirstName == "Natalie" && f.LastName == "Rhodes")
                    }
                }
            );

        }
    }
}
