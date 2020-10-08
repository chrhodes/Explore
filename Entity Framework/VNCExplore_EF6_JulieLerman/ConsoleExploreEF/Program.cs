using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

using NinjaDomain.Classes;
using NinjaDomain.DataModel;

namespace ConsoleExploreEF
{
    class Program
    {
        static void Main(string[] args)
        {
            // Have EF skip DB state check.
            Database.SetInitializer(new NullDatabaseInitializer<NinjaContext>());

            //InsertNinja();

            //InsertMultipleNinjas();

            //SimpleNinjaQueries();

            //QueryAndUpdateNinja();

            //QueryAndUpdateNinjaDisconnected();

            //RetrieveDataWithFind();

            //RetrieveDataWithSqlQuery();

            //DeleteNinja();

            //DeleteNinjaWithKeyValue();

            //DeleteNinjaViaStoredProcedure();

            InsertNinjaWithEquipment();

            //SimpleNinjaGraphQuery();

            //ProjectionQuery();

            //QueryAndUpdateNinjaDisconnected();

            //ReseedDatabase();

            //DataHelpers.NewDbWithSeed();

            Console.ReadKey();
        }

        private static void InsertNinja()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);

            // Create a Ninja

            var ninja = new Ninja
            {
                Name = "SampsonNew",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(2000, 11, 28),
                DateOfDeath = null,
                ClanId = 1

            };

            // And use EF to insert it.

            using (var context = new NinjaContext())
            {
                // This displays SQL activity to Console
                context.Database.Log = Console.WriteLine;
                // Add new ninja to Ninjas DbSet
                // so context is aware of it.
                context.Ninjas.Add(ninja);

                //context.Database.Log = s =>
                //{
                //    Debug.Print(s);
                //};
                // SaveChanges will execute all tracked changes
                // which in this case is just one new ninja
                context.SaveChanges();
            }
        }

        private static void InsertMultipleNinjas()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);

            var ninja1 = new Ninja
            {
                Name = "Leonardo",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1984, 1, 1),
                ClanId = 1
            };
            var ninja2 = new Ninja
            {
                Name = "Raphael",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1985, 1, 1),
                ClanId = 1
            };

            var ninja3 = new Ninja
            {
                Name = "Vikki",
                ServedInOniwaban = true,
                DateOfBirth = new DateTime(1954, 3, 12),
                ClanId = 1
            };

            var ninja4 = new Ninja
            {
                Name = "Christopher",
                ServedInOniwaban = true,
                DateOfBirth = new DateTime(1956, 11, 22),
                ClanId = 1
            };

            // EF 6 will only send one command to database at a time

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.AddRange(new List<Ninja> { ninja1, ninja2, ninja3, ninja4 });
                context.SaveChanges();
            }
        }

        private static void SimpleNinjaQueries()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                // Define and execute (ToList) query

                var ninjas = context.Ninjas.ToList();

                //var ninjas = context.Ninjas
                //     .Where(n => n.DateOfBirth >= new DateTime(1984, 1, 1))
                //     .FirstOrDefault();

                // FirstOrDefault only returns a single ninja
                // so next will not work as not enumerable
                // First throws exception, FirstOrDefault does not.

                //foreach (var ninja in ninjas)
                //{
                //    console.writeline(ninjas.name);
                //}

                //Console.WriteLine(ninjas.Name);

                // Query with some filters
                // Name will be hard coded.

                //var ninjas = context.Ninjas
                //    .Where(n => n.Name == "Raphael");

                //var theName = "Raphael";

                // Name will be parameterized

                //var ninjas = context.Ninjas
                //    .Where(n => n.Name == theName);

                //var ninjas = context.Ninjas
                //    .Where(n => n.DateOfBirth >= new DateTime(1984, 1, 1));

                // Query with some filters and paging

                //var ninjas = context.Ninjas
                //    .Where(n => n.DateOfBirth >= new DateTime(1984, 1, 1))
                //    .OrderBy(n => n.Name)
                //    .Skip(1).Take(1);

                // Define query first

                //var query = context.Ninjas;

                // Then execute it

                //foreach (var ninja in query)
                //{
                //    Console.WriteLine(ninja.Name);
                //}

                // Force query evaluation

                //var ninjas = query.ToList();

                // Avoid doing lots of work in iteration as connection
                // is opened and closed after all rows enumerated

                foreach (var ninja in ninjas)
                {
                    Console.WriteLine(ninja.Name);
                }
            }
        }

        private static void QueryAndUpdateNinja()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.FirstOrDefault();
                ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);
                context.SaveChanges();
            }
        }

        private static void QueryAndUpdateNinjaDisconnected()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);

            Ninja ninja;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                ninja = context.Ninjas.FirstOrDefault();
            }

            ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);

            // Nothing happens here because new context 
            // doesn't know about changes

            //using (var context = new NinjaContext())
            //{
            //    context.Database.Log = Console.WriteLine;
            //    context.SaveChanges();
            //}

            // Add is not the answer as it will do an insert

            //using (var context = new NinjaContext())
            //{
            //    context.Database.Log = Console.WriteLine;
            //    context.Ninjas.Add(ninja);
            //    context.SaveChanges();
            //}

            // This tells context that we have a Ninja - Attach
            // and it is new modified Ninja.

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.Attach(ninja);
                context.Entry(ninja).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        private static void RetrieveDataWithFind()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);

            var keyval = 8;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.Find(keyval);
                Console.WriteLine("After Find#1:" + ninja.Name);

                // This should not hit DB again

                var someNinja = context.Ninjas.Find(keyval);
                Console.WriteLine("After Find#2:" + someNinja.Name);
                ninja = null;
            }

            // Will check context first and not query if present
            // Expects to find value (SingleOrDefault Linq style)
        }

        private static void RetrieveDataWithSqlQuery()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);

            // Query needs to match schema of type.

            //using (var context = new NinjaContext())
            //{
            //    context.Database.Log = Console.WriteLine;
            //    var ninjas = context.Ninjas.SqlQuery("select * from Ninjas").ToList();

            //    foreach (var ninja in ninjas)
            //    {
            //        Console.WriteLine(ninja.Name);
            //    }
            //}

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninjas = context.Ninjas.SqlQuery("exec GetOldNinjas").ToList();

                foreach (var ninja in ninjas)
                {
                    Console.WriteLine(ninja.Name);
                }
            }
        }

        private static void DeleteNinja()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);

            Ninja ninja;
            //using (var context = new NinjaContext())
            //{
            //    context.Database.Log = Console.WriteLine;
            //    ninja = context.Ninjas.FirstOrDefault();
            //    context.Ninjas.Remove(ninja);
            //    context.SaveChanges();
            //}

            // This is more typical of what happens.

            // Get a ninja (one context)
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                ninja = context.Ninjas.FirstOrDefault();
            }

            //// Delete it (another context)
            //using (var context = new NinjaContext())
            //{
            //    context.Database.Log = Console.WriteLine;
            //    //context.Ninjas.Attach(ninja);
            //    context.Ninjas.Remove(ninja);   // Exception not in context
            //    context.SaveChanges();
            //}

            // Works if attach to context then remove
            // but yucky syntax
            //using (var context = new NinjaContext())
            //{
            //    context.Database.Log = Console.WriteLine;
            //    context.Ninjas.Attach(ninja);
            //    context.Ninjas.Remove(ninja);
            //    context.SaveChanges();
            //}

            // Tells EF about the object and sets its state
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Entry(ninja).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        private static void DeleteNinjaWithKeyValue()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);

            var keyval = 13;

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.Find(keyval);
                context.Ninjas.Remove(ninja);
                context.SaveChanges();
            }
        }

        private static void DeleteNinjaViaStoredProcedure()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);

            var keyval = 14;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Database.ExecuteSqlCommand(
                    "exec DeleteNinjaById {0}", keyval);
            }
        }

        private static void InsertNinjaWithEquipment()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var ninja = new Ninja
                {
                    Name = "Kacy Catanzaro",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1990, 1, 14),
                    ClanId = 1
                };

                var muscles = new NinjaEquipment
                {
                    Name = "Muscles",
                    Type = EquipmentType.Tool
                };

                var spunk = new NinjaEquipment
                {
                    Name = "Spunk",
                    Type = EquipmentType.Weapon
                };

                // Add the equipment to the List<Equipment>
                ninja.EquipmentOwned.Add(muscles);
                ninja.EquipmentOwned.Add(spunk);

                // NB. The equipment was not explicity added to context
                // but EF will apply the Added state to them because
                // we are adding the Ninja

                context.Ninjas.Add(ninja);
                context.SaveChanges();
            }
        }

        private static void SimpleNinjaGraphQuery()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                //var ninjas = context.Ninjas.Include(n => n.EquipmentOwned)
                //    .FirstOrDefault(n => n.Name.StartsWith("Kacy"));

                var ninja = context.Ninjas
                           .FirstOrDefault(n => n.Name.StartsWith("Kacy"));
                Console.WriteLine("Ninja Retrieved:" + ninja.Name);
                context.Entry(ninja).Collection(n => n.EquipmentOwned).Load();
            }
        }

        private static void ProjectionQuery()
        { 
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninjas = context.Ninjas
                    .Select(n => new { n.Name, n.DateOfBirth, n.EquipmentOwned })
                    .ToList();

            }
        }

        private static void ReseedDatabase()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);

            Database.SetInitializer(new DropCreateDatabaseAlways<NinjaContext>());

            using (var context = new NinjaContext())
            {
                context.Clans.Add(new Clan { ClanName = "Vermont Clan" });

                var j = new Ninja
                {
                    Name = "JulieSan",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1980, 1, 1),
                    ClanId = 1

                };
                var s = new Ninja
                {
                    Name = "SampsonSan",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(2008, 1, 28),
                    ClanId = 1

                };
                var l = new Ninja
                {
                    Name = "Leonardo",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1984, 1, 1),
                    ClanId = 1
                };
                var r = new Ninja
                {
                    Name = "Raphael",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1985, 1, 1),
                    ClanId = 1
                };
                context.Ninjas.AddRange(new List<Ninja> { j, s, l, r });
                context.SaveChanges();
                context.Database.ExecuteSqlCommand(
                  @"CREATE PROCEDURE GetOldNinjas
                    AS  SELECT * FROM Ninjas WHERE DateOfBirth<='1/1/1980'");

                context.Database.ExecuteSqlCommand(
                   @"CREATE PROCEDURE DeleteNinjaViaId
                     @Id int
                     AS
                     DELETE from Ninjas Where Id = @id
                     RETURN @@rowcount");
            }

        }
    }
}
