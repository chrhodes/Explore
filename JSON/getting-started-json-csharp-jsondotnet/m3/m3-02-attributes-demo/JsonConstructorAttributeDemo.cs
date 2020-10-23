using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m3_02_attributes_demo
{
    public static class JsonConstructorAttributeDemo
    {
        /// <summary>
        /// Specify which constructor should be used during deserialization
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** [JsonConstructor] ***");

            string authorJson = @"{'authorName': 'Xavier Morera As Parameter'}";

            AuthorConstructor xavierNoAttribute = JsonConvert.DeserializeObject<AuthorConstructor>(authorJson);
            Console.WriteLine(xavierNoAttribute.author);

            AuthorConstructorAttribute xavierWithAttribute = JsonConvert.DeserializeObject<AuthorConstructorAttribute>(authorJson);
            Console.WriteLine(xavierWithAttribute.author);
        }
    }

    /// <summary>
    /// Deserialize uses the default constructor 
    /// </summary>
    public class AuthorConstructor
    {
        public AuthorConstructor()
        {
            author = "Xavier Morera In Constructor";
            happy = false;
        }

        AuthorConstructor(string authorName)
        {
            author = authorName;
            happy = true;
        }

        public string author { get; set; }

        public DateTime since { get; set; }

        public bool happy { get; set; }
    }

    /// <summary>
    /// Deserialize uses the constructor with the JsonConstructor attribute
    /// </summary>
    public class AuthorConstructorAttribute
    {
        public AuthorConstructorAttribute()
        {
            author = "Xavier Morera in Constructor";
            happy = false;
        }

        [JsonConstructor]
        AuthorConstructorAttribute(string authorName)
        {
            author = authorName;
            happy = true;
        }

        public string author { get; set; }

        public DateTime since { get; set; }

        public bool happy { get; set; }
    }
}
