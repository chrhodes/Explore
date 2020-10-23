using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m4_02_custom_jsonconverter_demo
{
    public static class CustomJsonConverterDemo
    {
        /// <summary>
        /// Custom JSON converter
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** Custom JsonConverter ***");

            Author xavierAuthor = new Author()
            {
                name = "Xavier Morera",
                courses = new string[] { "Solr",
                    "Spark",
                    "Python"
                },
                happy = true,
                issues = null
            };

            string xavierJson = JsonConvert.SerializeObject(xavierAuthor, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                Converters = new List<JsonConverter>(new JsonConverter[]{
                    new RemoveNullsJsonConverter(typeof(Author))})
            });
            Console.WriteLine(xavierJson);
        }
    }
}
