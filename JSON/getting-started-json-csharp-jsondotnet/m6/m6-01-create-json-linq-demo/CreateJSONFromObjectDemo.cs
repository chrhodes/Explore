using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m6_01_create_json_linq_demo
{
    public static class CreateJSONFromObjectDemo
    {
        /// <summary>
        /// FromObject
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** From Object ***");

            List<string> courseList = new List<string>() { "Solr", "Jira", "dotTrace" };

            dynamic xavierDynamic = new ExpandoObject();
            xavierDynamic.name = "Xavier Morera";
            xavierDynamic.courses = from c in courseList
                                    orderby c
                                    select new JValue(c);
            xavierDynamic.since = new DateTime(2015, 01, 14);
            xavierDynamic.happy = true;
            xavierDynamic.issues = null;

            JObject xavierAuthor = JObject.FromObject(xavierDynamic);

            Console.WriteLine(xavierAuthor.ToString());
        }
    }
}
