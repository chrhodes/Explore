using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m3_02_attributes_demo
{
    public static class JsonConverterAttributeDemo
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** [JsonConverterAttribute] ***");

            AuthorNoConverterAttribute xavierNoAttribute = new AuthorNoConverterAttribute()
            {
                author = "Xavier",
                relationship = Relationship.IndependentAuthor,
                since = new DateTime(2015, 02, 15)
            };
            Console.WriteLine(JsonConvert.SerializeObject(xavierNoAttribute, Formatting.Indented));

            AuthorWithConverterAttribute xavierWithAttribute = new AuthorWithConverterAttribute()
            {
                author = "Xavier",
                relationship = Relationship.IndependentAuthor,
                since = new DateTime(2015, 02, 15)
            };
            Console.WriteLine(JsonConvert.SerializeObject(xavierWithAttribute, Formatting.Indented));
            //And of course it can be used at class level with the appropriate converter
        }
    }


    public class AuthorNoConverterAttribute
    {
        public string author { get; set; }

        public Relationship relationship { get; set; }

        public DateTime since { get; set; }
    }


    public class AuthorWithConverterAttribute
    {
        public string author { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Relationship relationship { get; set; }

        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTime since { get; set; }
    }

    public enum Relationship
    {
        EmployeeAuthor,
        IndependentAuthor
    }
}
