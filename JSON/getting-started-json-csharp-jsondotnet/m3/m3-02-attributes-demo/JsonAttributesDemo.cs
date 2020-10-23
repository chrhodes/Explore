using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m3_02_attributes_demo
{
    public static class JsonAttributesDemo
    {
        /// <summary>
        /// How to serialize a .NET object to different json objects: 
        /// JsonObject, JsonArray, JsonDictionary
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** [JsonObject] ***");

            Console.WriteLine("- No attribute");
            Author xavierAuthor = new Author();
            xavierAuthor.courses.Add(new Course() { courseName = "Solr", duration = 182 });
            xavierAuthor.courses.Add(new Course() { courseName = "Spark", duration = 183 });
            string xavierAuthorSerialized = JsonConvert.SerializeObject(xavierAuthor, Formatting.Indented);
            Console.WriteLine(xavierAuthorSerialized);

            Console.WriteLine("- With [JsonObject] attribute");
            AuthorJsonObject xavierAuthorAttribute = new AuthorJsonObject();
            xavierAuthorAttribute.courses.Add(new Course() { courseName = "Solr", duration = 180 });
            xavierAuthorAttribute.courses.Add(new Course() { courseName = "Spark", duration = 181 });
            string xavierAuthorSerializedAttribute = JsonConvert.SerializeObject(xavierAuthorAttribute, Formatting.Indented);
            Console.WriteLine(xavierAuthorSerializedAttribute);

            Console.WriteLine("- With [JsonArray] attribute");
            AuthorJsonArray xavierAuthorArray = new AuthorJsonArray();
            xavierAuthorArray.courses.Add(new Course() { courseName = "Solr", duration = 180 });
            xavierAuthorArray.courses.Add(new Course() { courseName = "Spark", duration = 181 });
            string xavierAuthorSerializedArray = JsonConvert.SerializeObject(xavierAuthorArray, Formatting.Indented);
            Console.WriteLine(xavierAuthorSerializedArray);

            Console.WriteLine("[JsonArray] and [JsonDictionary]");

            dictionaryWithDictionaryAttribute<string, string> dictionaryAttribute = new dictionaryWithDictionaryAttribute<string, string>();
            dictionaryWithArrayAttribute<string, string> arrayAttribute = new dictionaryWithArrayAttribute<string, string>();

            Console.WriteLine("- JsonDictionary");
            dictionaryAttribute.Add("key1", "value1");
            dictionaryAttribute.Add("key2", "value2");
            Console.WriteLine(JsonConvert.SerializeObject(dictionaryAttribute, Formatting.Indented));

            Console.WriteLine("- JsonArray");
            arrayAttribute.Add("key1", "value1");
            arrayAttribute.Add("key2", "value2");
            Console.WriteLine(JsonConvert.SerializeObject(arrayAttribute, Formatting.Indented));
        }
    }

    public class Course
    {
        public string courseName { get; set; }
        public int duration { get; set; }
    }

    public class Author : IEnumerable<Course>
    {
        public List<Course> courses { get; set; }

        public Author()
        {
            courses = new List<Course>();
        }
        public IEnumerator<Course> GetEnumerator()
        {
            return courses.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// JsonObjectAttribute 
    /// </summary>
    [JsonObject()]
    public class AuthorJsonObject : Author
    {
        public AuthorJsonObject()
        {
            courses = new List<Course>();
        }
    }

    /// <summary>
    /// JsonArrayAttribute 
    /// </summary>
    [JsonArray()]
    public class AuthorJsonArray : Author
    {
        public AuthorJsonArray()
        {
            courses = new List<Course>();
        }
    }

    /// <summary>
    /// JsonDictionaryAttribute 
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    [JsonDictionary]
    public class dictionaryWithDictionaryAttribute<K, V> : Dictionary<K, V>
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// JsonArrayAttribute 
    /// </summary>
    [JsonArray]
    public class dictionaryWithArrayAttribute<K, V> : Dictionary<K, V>
    {
        public string Name { get; set; }
    }

}
