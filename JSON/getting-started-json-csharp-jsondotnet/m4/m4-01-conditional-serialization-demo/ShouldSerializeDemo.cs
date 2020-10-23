using System;
using Newtonsoft.Json;

namespace m4_01_conditional_serialization_demo
{
    public static class ShouldSerializeDemo
    {
        /// <summary>
        /// ShouldSerializeAttributeName
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** ShouldSerialize ***");

            Author xavier = new Author()
            {
                Name = "Xavier Morera",
                Courses = new string[] { "Solr", "Spark", "Python" }
            };

            xavier.IsActive = true;
            string xavierActiveTrue = JsonConvert.SerializeObject(xavier, Formatting.Indented);
            Console.WriteLine(xavierActiveTrue);

            xavier.IsActive = false;
            string xavierActiveFalse = JsonConvert.SerializeObject(xavier, Formatting.Indented);
            Console.WriteLine(xavierActiveFalse);
        }
    }
}
