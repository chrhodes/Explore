using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m2_01_mapping_demo
{
    public static class DeserializeObjectsDemo
    {
        /// <summary>
        /// In this demo I will deserialize different .NET objects from JSON text
        /// </summary>
        public static void Show()
        {

            Console.Clear();
            Console.WriteLine("*** Deserialize Objects Demo ***");

            string jsonAuthor = @"{
                'name': 'Xavier Morera', 
                'courses': ['Solr', 'Spark', 'Jira'], 
                'happy': true }";

            /*---------------------------------------------------------*/
            Console.WriteLine("Step 1: Deserialize to a typed object");
            Author authorObj = JsonConvert.DeserializeObject<Author>(jsonAuthor);
            Console.WriteLine(authorObj.name);

            /*---------------------------------------------------------*/
            Console.WriteLine("Step 2: Deserialize into a var");
            var authorVar = JsonConvert.DeserializeObject(jsonAuthor);
            Console.WriteLine(authorVar);

            /*---------------------------------------------------------*/
            Console.WriteLine("Step 3: Deserialize into an anonymous type");
            var authorAnonymousDefinition = new
            {
                author = string.Empty,
                happy = false,
                courses = new string[] { },
                anotherproperty = string.Empty
            };
            var authorAnonymous = JsonConvert.DeserializeAnonymousType(jsonAuthor, authorAnonymousDefinition);
            Console.WriteLine(authorAnonymous);

            /*---------------------------------------------------------*/
            Console.WriteLine("Step 4: Deserialize a Collection");
            string jsonCollection = @"['Solr', 'Spark', 'Jira']";
            List<string> coursesList = JsonConvert.DeserializeObject<List<string>>(jsonCollection);
            Console.WriteLine(coursesList.ToString());
            foreach (string course in coursesList)
            {
                Console.WriteLine(course);
            }

            /*---------------------------------------------------------*/
            Console.WriteLine("Step 5: Deserialize a Dictionary");
            string jsonDictionary = @"{ 'Solr': 1500, 'Spark': 300, 'Jira': 2000 }";
            Dictionary<string, int> coursesDict = JsonConvert.DeserializeObject<Dictionary<string, int>>(jsonDictionary);
            foreach (KeyValuePair<string, int> kvp in coursesDict)
            {
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }

            /*---------------------------------------------------------*/
            Console.WriteLine("Step 6: Deserialize JSON from a file");
            Author xavierAuthor = JsonConvert.DeserializeObject<Author>(File.ReadAllText(@"xavier.json"));
            Console.WriteLine(xavierAuthor.name);
        }

    }
}
