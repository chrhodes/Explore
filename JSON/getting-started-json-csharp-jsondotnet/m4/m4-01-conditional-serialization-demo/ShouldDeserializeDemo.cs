using System;
using JsonSamples;
using Newtonsoft.Json;

namespace m4_01_conditional_serialization_demo
{
    public static class ShouldDeserializeDemo
    {
        /// <summary>
        /// ShouldDeserializeAttributeName
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** ShouldDeserialize ***");

            string json = Generate.SingleJson();

            Author xavier = JsonConvert.DeserializeObject<Author>(json);
            Console.WriteLine(xavier.Courses);
        }
    }
}
