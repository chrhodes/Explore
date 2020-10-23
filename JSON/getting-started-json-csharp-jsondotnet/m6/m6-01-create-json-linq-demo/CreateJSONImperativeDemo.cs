using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m6_01_create_json_linq_demo
{
    public static class CreateJSONImperativeDemo
    {
        /// <summary>
        /// Imperative
        /// </summary>
        public static void Show ()
        {
            Console.Clear();
            Console.WriteLine("*** Imperative ***");

            JObject xavierAuthor = new JObject();

            xavierAuthor.Add("author", "Xavier Morera");

            JArray courses = new JArray();
            JValue solr = new JValue("Solr");
            JValue jira = new JValue("Jira");
            JValue dotTrace = new JValue("dotTrace");
            courses.Add(solr);
            courses.Add(jira);
            jira.AddBeforeSelf(dotTrace);
            xavierAuthor.Add("courses", courses);

            // Add properties as string
            JValue dateSince = new JValue(new DateTime(2015, 01, 14));
            xavierAuthor.Add("since", dateSince);

            Console.WriteLine(xavierAuthor.ToString());

            // Dynamic properties: instead of xavierAuthor.Add("happy", true); you can use
            dynamic xavierDynamic = xavierAuthor;
            xavierDynamic.happy = true;
            xavierDynamic.issues = null;

            Console.WriteLine(xavierDynamic);
        }
    }
}
