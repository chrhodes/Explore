using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m6_02_parse_json_demo
{
    public static class ParseJSONFromStreamDemo
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("***  Parse JSON From Stream ***");

            JObject xavier;
            using (StreamReader reader = new StreamReader(@".\..\..\AuthorSingle.json"))
            {
                xavier = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
            }

            Console.WriteLine(xavier.First);
            Console.WriteLine(xavier.SelectToken("name"));
            Console.WriteLine(xavier["car"]);
        }
    }
}
