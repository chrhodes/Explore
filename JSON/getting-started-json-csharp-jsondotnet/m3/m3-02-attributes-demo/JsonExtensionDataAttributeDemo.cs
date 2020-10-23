using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m3_02_attributes_demo
{
    public static class JsonExtensionDataAttributeDemo
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine(" *** [JsonExtensionData] *** ");

            string authorJson = @"{'authorName': 'Xavier Morera in JSON'}";

            Console.WriteLine(authorJson);

            AuthorJsonExtension xavierJsonExtension = JsonConvert.DeserializeObject<AuthorJsonExtension>(authorJson);

            Console.WriteLine("author -> " + xavierJsonExtension.author);
            foreach (KeyValuePair<string, JToken> fld in xavierJsonExtension.unMappedFields)
            {
                Console.WriteLine("Unmapped field: " + fld.Key + " -> " + ((JValue)fld.Value).Value);
            }
        }
    }

    /// <summary>
    /// JsonExtensionDataAttribute
    /// </summary>
    public class AuthorJsonExtension
    {
        public string author { get; set; }

        public DateTime since { get; set; }

        public bool happy { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JToken> unMappedFields;
    }
}
