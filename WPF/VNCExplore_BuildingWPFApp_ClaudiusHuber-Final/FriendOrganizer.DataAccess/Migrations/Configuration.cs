using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using FriendOrganizer.Domain;
using VNC;

namespace FriendOrganizer.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<FriendOrganizerDbContext>
    {
        public Configuration()
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            AutomaticMigrationsEnabled = false;

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        protected override void Seed(FriendOrganizerDbContext context)
        {
            Int64 startTicks = Log.PERSISTENCE("Enter", Common.LOG_APPNAME);

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Friends.AddOrUpdate
            (
                f => f.FirstName,
                new Friend { FirstName = "Vikki", LastName = "Schanz" }
                , new Friend { FirstName = "Natalie", LastName = "Rhodes" }
                , new Friend { FirstName = "Christopher", LastName = "Rhodes" }
                , new Friend { FirstName = "George", LastName = "Lachow" }
            );


            context.ProgrammingLanguages.AddOrUpdate
            (
                pl => pl.Name,
                new ProgrammingLanguage { Name="C#"}
                , new ProgrammingLanguage { Name = "VB.NET" }
                , new ProgrammingLanguage { Name = "Forth" }
                , new ProgrammingLanguage { Name = "Assembly" }
                , new ProgrammingLanguage { Name = "APL" }
            );

            context.SaveChanges();

            context.FriendPhoneNumbers.AddOrUpdate
            (
                pn => pn.Number,
                new FriendPhoneNumber
                {
                    Number = "+49 12345678",
                    FriendId = context.Friends.First().Id
                }
            );

            context.Meetings.AddOrUpdate
            (
                m => m.Title,
                new Meeting
                {
                    Title = "Watching Star Wars",
                    DateFrom = new System.DateTime(2019, 06, 27),
                    DateTo = new System.DateTime(2019, 06, 27),
                    Friends = new List<Friend>
                    {
                        context.Friends.Single(f => f.FirstName == "Christopher" && f.LastName == "Rhodes"),
                        context.Friends.Single(f => f.FirstName == "Natalie" && f.LastName == "Rhodes"),
                        context.Friends.Single(f => f.FirstName == "Vikki" && f.LastName == "Schanz")
                    }
                }
                , new Meeting
                {
                    Title = "Watching American Ninja Warrior",
                    DateFrom = new System.DateTime(2019, 07, 04),
                    DateTo = new System.DateTime(2019, 07, 04),
                    Friends = new List<Friend>
                    {
                        context.Friends.Single(f => f.FirstName == "Christopher" && f.LastName == "Rhodes"),
                        context.Friends.Single(f => f.FirstName == "Natalie" && f.LastName == "Rhodes")
                    }
                }
            );

            Log.PERSISTENCE("Exit", Common.LOG_APPNAME, startTicks);
        }
    }
}
