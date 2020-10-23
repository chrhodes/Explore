using JsonSamples;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m2_04_error_handling_demo
{
    public static class OnErrorAttributeDemo
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** On Error Attribute Demo ***");

            Author xavier = new Author()
            {
                name = "Xavier Morera",
                courses = new string[] { "Solr", "Spark", "Jira" },
                happy = true,
                since = new DateTime(2014, 01, 15),
                car = null
            };

            string json = JsonConvert.SerializeObject(xavier, Formatting.Indented);

            Console.WriteLine(json);
        }
    }
}
