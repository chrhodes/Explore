using JsonSamples;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace m3_01_settings_demo
{
    public static class ObjectCreationHandlingDemo
    {
        /// <summary>
        /// controls how objects are created and deserialized to during deserialization.
        /// </summary>
        public static void Show()
        {

            Console.Clear();
            Console.WriteLine("*** ObjectCreationHandling ***");

            string xavierJson = Generate.SmallJson();

           // Json.NET will append values to existing collections during deserialization
            Console.WriteLine("- No setting");
            Author xavierOBH = JsonConvert.DeserializeObject<Author>(xavierJson);
            Console.WriteLine(xavierOBH.courses.Count());
            Console.WriteLine(string.Join(",", xavierOBH.courses));

            // Json.NET will replace existing values on collections during deserialization
            Console.WriteLine("- ObjectCreationHandling.Replace");
            Author xavierOBHReplace = JsonConvert.DeserializeObject<Author>(xavierJson, new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace
            });
            Console.WriteLine(xavierOBHReplace.courses.Count());
            Console.WriteLine(string.Join(",", xavierOBHReplace.courses));

        }
    }
}
