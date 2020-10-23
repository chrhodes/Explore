using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m3_01_settings_demo
{
    public static class TypeNameHandlingDemo
    {
        /// <summary>
        /// Controls whether Json.NET includes .NET type names during serialization 
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** TypeNameHandling ***");

            Author xavier = new Author();
            xavier.name = "Xavier Morera";
            xavier.courses = new List<string>() { "Solr", "Spark", "Jira" };
            xavier.car = new Car() { model = "Land Rover", year = 1976 };

            // Include the type on all 
            Console.WriteLine("- TypeNameHandling.All");
            JsonSerializerSettings settingsAll = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented
            };
            string xavierTNHAll = JsonConvert.SerializeObject(xavier, settingsAll);
            Console.WriteLine(xavierTNHAll);

            // Include type informaiton only on arrays
            Console.WriteLine("- TypeNameHandling.Arrays");
            JsonSerializerSettings settingsArrays = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Arrays,
                Formatting = Formatting.Indented
            };
            string xavierTNHArrays = JsonConvert.SerializeObject(xavier, settingsArrays);
            Console.WriteLine(xavierTNHArrays);

            // Also available: None, Objects or Auto
        }
    }
}
