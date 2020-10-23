using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m2_02json_serializer_demo
{
    public static class JsonSerializerDemo
    {
        /// <summary>
        /// In this demo I will show you how to write serialized JSON objects 
        /// using different settings (indentation and ignore nulls) to compare 
        /// the size of each generated file
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** JsonSerializer Demo ***");

            Author xavier = new Author()
            {
                name = "Xavier Morera",
                courses = new string[] { "Solr", "Spark", "Jira" },
                happy = true,
                since = new DateTime(2014, 01, 15)
            };

            Console.WriteLine("Step 1: Serialize JSON with default settings");
            
            using (StreamWriter sw = new StreamWriter(@"..\..\1-defaultsettings.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(sw, xavier);
            }

            Console.WriteLine("Step 2: Serialize JSON with indented true");

            using (StreamWriter sw = new StreamWriter(@"..\..\2-indented.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(sw, xavier);
            }

            Console.WriteLine("Step 3: Serialize JSON with ignore nulls");

            using (StreamWriter sw = new StreamWriter(@"..\..\3-ignorenulls.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.NullValueHandling = NullValueHandling.Ignore;
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(sw, xavier);
            }

            Console.WriteLine("Curious about size difference?");

            FileInfo defaultsettings = new FileInfo(@"..\..\1-defaultsettings.json");
            Console.WriteLine("Size of file 1-defaultsettings.json: {0} bytes", defaultsettings.Length);

            FileInfo indented = new FileInfo(@"..\..\2-indented.json");
            Console.WriteLine("Size of file 2-indented.json: {0} bytes", indented.Length);

            FileInfo ignorenulls = new FileInfo(@"..\..\3-ignorenulls.json");
            Console.WriteLine("Size of file 3-ignorenulls.json: {0} bytes", ignorenulls.Length);
        }

    }
}
