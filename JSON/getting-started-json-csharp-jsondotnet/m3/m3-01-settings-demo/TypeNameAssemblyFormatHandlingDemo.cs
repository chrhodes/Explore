using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace m3_01_settings_demo
{
    public static class TypeNameAssemblyFormatHandlingDemo
    {
        /// <summary>
        /// Controls how type names are written during serialization.
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** TypeNameAssemblyFormatHandling ***");

            Author xavier = new Author();
            xavier.name = "Xavier Morera";
            xavier.courses = new List<string>() { "Solr", "Spark", "Jira" };
            xavier.car = new Car() { model = "Land Rover", year = 1976 };

            Console.WriteLine("- TypeNameAssemblyFormatHandling.Simple");
            string xavierTNAS = JsonConvert.SerializeObject(xavier, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                // TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                Formatting = Formatting.Indented
            });
            Console.WriteLine(xavierTNAS);

            Console.WriteLine("- TypeNameAssemblyFormatHandling.Full");
            string xavierTNAF = JsonConvert.SerializeObject(xavier, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full,
                Formatting = Formatting.Indented
            });
            Console.WriteLine(xavierTNAF);
        }
    }
}
