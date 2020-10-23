using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m3_01_settings_demo
{
    public static class DefaultValueHandlingDemo
    {
        /// <summary>
        /// Controls how Json.NET uses default values using the .NET DefaultValueAttribute 
        /// when serializing and deserializing
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** Default Value Handling ***");

            AuthorDefaults author = new AuthorDefaults();

            author.name = "Xavier Morera"; // No default
            author.courses = 4; //Default is 4
            author.happy = true; //Default is true
            author.resting = false; //Default is true
            //'state' not set, but has a default of 'Passionate about teaching'

            // Serialize with default values
            Console.WriteLine("- Serialize with default values but no setting");
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            string authorJsonDefaults = JsonConvert.SerializeObject(author, settings);
            Console.WriteLine(authorJsonDefaults);

            // Ignore properties with default values when serializing 
            Console.WriteLine("- DefaultValueHandling.Ignore");
            JsonSerializerSettings settingsIgnore = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.Indented
            };
            string authorJsonDefaultsIgnore = JsonConvert.SerializeObject(author, settingsIgnore);
            Console.WriteLine(authorJsonDefaultsIgnore);

            // Include properties with default values when deserializing 
            Console.WriteLine("- Deserialize default");
            string authorOnlyName = "{'name': 'Xavier Morera'}";

            AuthorDefaults authorDVH = JsonConvert.DeserializeObject<AuthorDefaults>(authorOnlyName);
            Console.WriteLine(authorDVH.state);

            Console.WriteLine("- DefaultValueHandling.Populate");
            JsonSerializerSettings settingsPopulate = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Populate,
                Formatting = Formatting.Indented
            };

            AuthorDefaults authorDVHPopulate = JsonConvert.DeserializeObject<AuthorDefaults>(authorOnlyName, settingsPopulate);
            Console.WriteLine(authorDVHPopulate.state);

            //Populate and Ignore can be used together with IgnoreAndPopulate
        }
    }
}
