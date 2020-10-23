using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m3_01_settings_demo
{
    public static class BinderDemo
    {
        /// <summary>
        /// Creates a custom SerializationBinder 
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** Binder ***");

            Author xavierBinder = new Author()
            {
                name = "Xavier Morera",
                courses = new List<string>() { "Solr", "Spark", "Jira" },
                happy = true,
                car = new Car
                {
                    model = "Land Rover",
                    year = 1976
                }
            };

            // Baseline
            Console.WriteLine("- With TypeNameHandling");
            string jsonFullClass = JsonConvert.SerializeObject(xavierBinder, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            Console.WriteLine(jsonFullClass);

            // With TypeSerializationBinder
            Console.WriteLine("- With SerializationBinder");
            TypeSerializationBinder binder = new TypeSerializationBinder
            {
                KnownTypes = new List<Type> { typeof(Car) }
            };

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                SerializationBinder = binder
                // [deprecated] Binder = binder
            };

            string json = JsonConvert.SerializeObject(xavierBinder, Formatting.Indented, settings);

            Console.WriteLine(json);
        }
    }
}
