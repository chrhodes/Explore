using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m6_01_create_json_linq_demo
{
    public static class CreateJSONDeclarativeDemo
    {
        /// <summary>
        /// Declarative
        /// </summary>
        public static void Show ()
        {
            Console.Clear();
            Console.WriteLine("*** Declarative ***");

            List<string> coursesForSorting = new List<string>() { "Solr", "Jira", "dotTrace" };

            JObject xavierAuthor = new JObject(
                new JProperty("name", "Xavier Morera"),
                new JProperty("courses", new JArray("Solr", "Jira", "dotTrace")),
                new JProperty("sorted", new JArray(
                    from c in coursesForSorting
                    orderby c
                    select new JValue(c))),
                new JProperty("since", new DateTime(2015, 01, 14)),
                new JProperty("happy", true),
                new JProperty("issues", null)
            );

            Console.WriteLine(xavierAuthor.ToString());
        }
    }
}
