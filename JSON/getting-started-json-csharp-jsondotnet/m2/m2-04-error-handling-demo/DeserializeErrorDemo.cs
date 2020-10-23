using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using JsonSamples;

namespace m2_04_error_handling_demo
{
    public static class DeserializeErrorDemo
    {
        /// <summary>
        /// In this demo I will show you what happens when Json.NET 
        /// raise an error when deserializing a JSON text
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** Deserialize Error ***");
            try
            {
                var jsonDates = Generate.DatesJson();

                List<DateTime> deserializedDates = JsonConvert.DeserializeObject<List<DateTime>>(jsonDates);
                Console.WriteLine(deserializedDates.Count());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to deserialize object: " + ex.Message);
            }
        }
    }
}
