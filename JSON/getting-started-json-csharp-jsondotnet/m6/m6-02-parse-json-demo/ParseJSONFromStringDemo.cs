using JsonSamples;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m6_02_parse_json_demo
{
    class ParseJSONFromStringDemo
    {
        /// <summary>
        /// Parse JSON from string
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** Parse JSON From String ***");

            string xavierJSON = Generate.SingleJson();

            string happyJSON = " { 'happy' : true }";

            string coursesJSON = @"['Solr','Jira','dotTrace',]";

            JObject xavier = JObject.Parse(xavierJSON);
            JToken happy = JToken.Parse(happyJSON);
            JArray courses = JArray.Parse(coursesJSON);

            Console.WriteLine(xavier.Children().ElementAt(0));
            Console.WriteLine(xavier.Children().Last());

            Console.WriteLine((string)happy["happy"]);

            Console.WriteLine(courses.Count);
        }
    }
}
