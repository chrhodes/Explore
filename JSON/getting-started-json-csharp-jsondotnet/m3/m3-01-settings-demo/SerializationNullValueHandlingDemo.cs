using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m3_01_settings_demo
{
    public static class SerializationNullValueHandlingDemo
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** Null Value Handling ***");

            Author xavierPocoWithNullValue = new Author() { 
                name = "Xavier Morera", 
                happy = true
            };
            
            // Serialize class with null values without specifying how to handle null values
            Console.WriteLine("- Serialize object with null values");
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            string xavierNullValue = JsonConvert.SerializeObject(xavierPocoWithNullValue, jsonSettings);
            Console.WriteLine(xavierNullValue);


            // Serialize using NullValueHandling.Ignore
            Console.WriteLine("- Serialize object with setting to ignore null values");
            JsonSerializerSettings jsonSettingsIgnore = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };

            string xavierNullValueIgnore = JsonConvert.SerializeObject(xavierPocoWithNullValue, jsonSettingsIgnore);
            Console.WriteLine(xavierNullValueIgnore);
        }
    }
}
