using JsonSamples;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m6_03_query_json_linq
{
    public static class GetJsonValuesLinqDemo
    {
        /// <summary>
        /// Read JSON using the attribute name
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** LINQ to JSON ***");

            string xavierJSON = Generate.SingleJson();

            JObject xavierJObject = JObject.Parse(xavierJSON);

            Console.WriteLine(xavierJObject["name"]);

            Console.WriteLine(xavierJObject["since"]);

            Console.WriteLine(xavierJObject["issues"]);

            Console.WriteLine(xavierJObject["car"]["model"]);
        }
    }
}
