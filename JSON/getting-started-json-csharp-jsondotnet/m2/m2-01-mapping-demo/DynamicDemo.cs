using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m2_01_mapping_demo
{
    public static class DynamicDemo
    {
        /// <summary>
        /// In this demo I show how Json.NET can serialize and deserialize dynamic objects
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("***  Dynamic and ExpandoObject Demo ***");
            dynamic author = new ExpandoObject();
            author.FriendlyName = "Xavier Morera";
            author.Courses = new List<string>() { "Solr", "Spark", "Python", "Jira" };
            author.Happy = true;

            Console.WriteLine("- Serialize");
            /*---------------------------------------------------------*/
            string jsonDynamicAuthor = JsonConvert.SerializeObject(author, Formatting.Indented);
            Console.WriteLine(jsonDynamicAuthor);

            Console.WriteLine("- Deserialize");
            /*---------------------------------------------------------*/
            dynamic authorDeserialized = JsonConvert.DeserializeObject(jsonDynamicAuthor);
            Console.WriteLine("Friendly Name: " + authorDeserialized.FriendlyName);
        }
    }
}
