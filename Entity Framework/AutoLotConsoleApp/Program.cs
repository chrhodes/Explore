using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLotConsoleApp.EF;
using AutoLotConsoleApp.Models;
using static System.Console;

namespace AutoLotConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("***** CHR explores ADO.NET EF *****\n");

            PrintAllInventory("Before AddNewRecord()");

            int carId = AddNewRecord();
            
            //RemoveRecordUsingEntityState(carId);
            WriteLine(carId);

            PrintAllInventory("After AddNewRecord()");

            //RemoveRecord(carId);

            //PrintAllInventory("After RemoveRecord(carId)");

            //FunWithLinqQueries();
            //RemoveRecordsWithLinq();
            //UpdateRecord(carId);
            //WriteLine("*******************************\n");
            //PrintAllInventory();
            WriteLine("Press Enter to Exit ...");
            ReadLine();
        }

        private static int AddNewRecord()
        {
            // Add record to the Inventory table of the AutoLot database. 
            using (var context = new AutoLotEntities())
            {
                try
                {
                    var car = new Car() { Make = "Yugo", Color = "Brown", CarNickName = "Brownie" };

                    context.Cars.Add(car);
                    context.SaveChanges();

                    // On a successful save, EF populates the database generated identity field
                    return car.CarId;
                }
                catch (Exception ex)
                {
                    WriteLine(ex.InnerException?.Message);
                    return 0;
                }
            }
        }

        private static void AddNewRecords(IEnumerable<Car> carsToAdd)
        {
            using (var context = new AutoLotEntities())
            {
                context.Cars.AddRange(carsToAdd);
                context.SaveChanges();
            }
        }

        private static void PrintAllInventory(string message = "")
        {
            WriteLine(message);

            // Select all items from the Inventory table of AutoLot,
            // and print out the data using our custom ToString()
            // of the Car entity class. 
            using (var context = new AutoLotEntities())
            {
                foreach (Car c in context.Cars)
                {
                    WriteLine(c);
                }

                // Use SQL to Query DbSet
                // NB. the alias on PetName

                //foreach (Car c in context.Cars.SqlQuery(
                //    "Select CarId,Make,Color,PetName as CarNickName from Inventory where Make=@p0", "BMW"))
                //{
                //    WriteLine(c);
                //}

                // Use Can also use Database property of context.
                // NB. using a different class to get fewer columns

                //foreach (ShortCar c in context.Database.SqlQuery(typeof(ShortCar), "Select CarId,Make from Inventory"))
                //{
                //    WriteLine(c);
                //}

                // Use Linq to get records.  Use logging to see the SQL
                // Select * from Inventory where Make = 'BMW'

                //foreach (Car c in context.Cars.Where(c => c.Make == "BMW"))
                //{
                //    WriteLine(c);
                //}

                //Find() is a special LINQ method, in that it first looks in the DbChangeTracker for the entity being requested
                //and, if found, returns that instance to the calling method.If it isn’t found, EF does a database call to create
                //the requested instance.
                //Find() only searches by primary key(simple or complex), so you must keep that in mind when using it.
                //An example of Find() in action looks like this:

                //WriteLine(context.Cars.Find(5));

                foreach (Car c in context.Cars)
                {
                    foreach (Order o in c.Orders)
                    {
                        WriteLine(o.OrderId);
                    }
                }

                foreach (Car c in context.Cars.Include(c => c.Orders))
                {
                    foreach (Order o in c.Orders)
                    {
                        WriteLine(o.OrderId);
                    }
                }

                context.Configuration.LazyLoadingEnabled = false;

                foreach (Car c in context.Cars)
                {
                    context.Entry(c).Collection(x => x.Orders).Load();
                    foreach (Order o in c.Orders)
                    {
                        WriteLine(o.OrderId);
                    }
                }

                foreach (Order o in context.Orders)
                {
                    context.Entry(o).Reference(x => x.Car).Load();
                }
            }
        }

        private static void FunWithLinqQueries()
        {
            using (var context = new AutoLotEntities())
            {
                // Get all data from the Inventory table.
                // could also write:
                // var allData = (from item in context.Inventories select item).ToArray();
                var allData = context.Cars.ToArray();

                // Get a projection of new data. 
                var colorsMakes = from item in allData select new { item.Color, item.Make };

                foreach (var item in colorsMakes)
                {
                    WriteLine(item);
                }

                // Get only items where Color == "Black"
                var blackCars = from item in allData where item.Color == "Black" select item;

                foreach (var item in blackCars)
                {
                    WriteLine(item);
                }
            }
        }

        private static void ChainingLinqQueries()
        {
            using (var context = new AutoLotEntities())
            {
                //Not executed
                DbSet<Car> allData = context.Cars;

                //Not Executed. 
                var colorsMakes = from item in allData select new { item.Color, item.Make };

                //Now it's executed
                foreach (var item in colorsMakes)
                {
                    WriteLine(item);
                }
            }
        }

        private static void RemoveRecord(int carId)
        {
            // Find a car to delete by primary key.
            using (var context = new AutoLotEntities())
            {
                // See if we have it.  NB. This makes a trip to the database
                Car carToDelete = context.Cars.Find(carId);

                if (carToDelete != null)
                {
                    context.Cars.Remove(carToDelete);
                    //This code is purely demonstrative to show the entity state changed to Deleted
                    if (context.Entry(carToDelete).State != EntityState.Deleted)
                    {
                        throw new Exception("Unable to delete the record");
                    }
                    context.SaveChanges();
                }
            }
        }

        private static void RemoveMultipleRecords(IEnumerable<Car> carsToRemove)
        {
            using (var context = new AutoLotEntities())
            {
                //Each record must be loaded in the DbChangeTracker
                context.Cars.RemoveRange(carsToRemove);
                context.SaveChanges();
            }
        }

        // This does not hit the database first.

        private static void RemoveRecordUsingEntityState(int carId)
        {
            using (var context = new AutoLotEntities())
            {
                Car carToDelete = new Car() { CarId = carId };
                context.Entry(carToDelete).State = EntityState.Deleted;
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    WriteLine(ex);
                }
            }
        }

        private static void UpdateRecord(int carId)
        {
            using (var context = new AutoLotEntities())
            {
                // Grab the car, change it, save! 
                Car carToUpdate = context.Cars.Find(carId);
                if (carToUpdate != null)
                {
                    WriteLine(context.Entry(carToUpdate).State);
                    carToUpdate.Color = "Blue";
                    WriteLine(context.Entry(carToUpdate).State);
                }
            }
        }
    }
}
