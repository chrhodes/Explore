using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using JsonSamples;
using Newtonsoft.Json;

namespace m2_01_mapping_demo
{
    public static class SerializeObjectsDemo
    {

        /// <summary>
        /// In this demo I will serialize different .NET objects into JSON text
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** Serialize Objects Demo ***");

            Author xavier = new Author()
            {
                name = "Xavier Morera",
                courses = new string[] { "Solr", "Spark", "Python", "Jira" },
                happy = true
            };

            /*---------------------------------------------------------*/
            Console.WriteLine("Step 1: Serialize an Object");
            string objectSerialized = JsonConvert.SerializeObject(xavier, Formatting.Indented);
            Console.WriteLine(objectSerialized);

            /*---------------------------------------------------------*/
            Console.WriteLine("Step 2: Serialize a Collection");
            List<string> courses = new List<string> { "Solr", "Spark", "Python", "Jira" };
            string collectionSerialized = JsonConvert.SerializeObject(courses);
            Console.WriteLine(collectionSerialized);

            /*---------------------------------------------------------*/
            Console.WriteLine("Step 3: Serialize a Dictionary");
            Dictionary<string, int> views = new Dictionary<string, int>
            {
                { "Solr", 1500 },
                { "Spark", 300 },
                { "Jira", 2000 }
            };
            string dictionarySerialized = JsonConvert.SerializeObject(views, Formatting.Indented);
            Console.WriteLine(dictionarySerialized);

            /*---------------------------------------------------------*/
            Console.WriteLine("Step 4: Serialize a DataSet");
            DataSet coursesDataSet = Generate.CoursesDataSet();
            string datasetSerialized = JsonConvert.SerializeObject(coursesDataSet, Formatting.Indented);
            Console.WriteLine(datasetSerialized);

            /*---------------------------------------------------------*/
            Console.WriteLine("Step 5: Serialize JSON to a file");
            File.WriteAllText(@"xavier.json", JsonConvert.SerializeObject(xavier));

        }

    }
}
