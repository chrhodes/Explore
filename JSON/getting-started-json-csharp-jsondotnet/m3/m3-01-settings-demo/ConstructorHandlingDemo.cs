using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m3_01_settings_demo
{
    public static class ConstructorHandlingDemo
    {
        /// <summary>
        /// Specifies how constructors are used when initializing objects during deserialization
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** Constructor Handling ***");

            string jsonConstructor = "{'name': 'Xavier Morera', 'happy': true }";

            // Use the default constructor
            try
            {
                Console.WriteLine("- Deserialize normally");
                AuthorConstructor author = JsonConvert.DeserializeObject<AuthorConstructor>(jsonConstructor);
                Console.WriteLine(author.name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Use the non-public constructor
            Console.WriteLine("- Deserialize with AllowNonPublicDefaultConstructor");
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };

            AuthorConstructor authorConstructorHandling = JsonConvert.DeserializeObject<AuthorConstructor>(jsonConstructor, settings);
            Console.WriteLine(authorConstructorHandling.name);
        }
    }

    public class AuthorConstructor
    {
        private AuthorConstructor()
        {
            //Private constructor
        }

        public AuthorConstructor(string authorName)
        {
            if (string.IsNullOrEmpty(authorName))
            {
                throw new ArgumentNullException("authorName is a required value");
            }
            //Public constructor
            name = authorName;
        }

        public string name { get; set; }

        public bool happy { get; set; }
    }
}
