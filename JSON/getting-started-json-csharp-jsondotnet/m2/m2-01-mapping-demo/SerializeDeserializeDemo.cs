using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonSamples;
using Newtonsoft.Json;

namespace m2_01_mapping_demo
{
    public static class SerializeDeserializeDemo
    {
        /// <summary>
        /// In this demo I will show you how to serialize and deserialize 
        /// </summary>
        public static void Show()
        {
            string jsonSample = Generate.SingleJson();

            /*---------------------------------------------------------*/
            Console.WriteLine("Step 1: Output JSON");
            Console.WriteLine(jsonSample);

            /*---------------------------------------------------------*/
            Console.WriteLine(Environment.NewLine + "Step 2: Output property Author.Name from deserialized class");
            Author xavierAuthor = JsonConvert.DeserializeObject<Author>(jsonSample);
            Console.WriteLine(xavierAuthor.name);

            /*---------------------------------------------------------*/
            Console.WriteLine(Environment.NewLine + "Step 3: Output serialized Author class");
            string xavierJsonSerialized = JsonConvert.SerializeObject(xavierAuthor);
            Console.WriteLine(xavierJsonSerialized);

            /*---------------------------------------------------------*/
            Console.WriteLine(Environment.NewLine + "Step 4: Output serialized Author class with indentation");
            string xavierJsonSerializedIndented = JsonConvert.SerializeObject(xavierAuthor, Formatting.Indented);
            Console.WriteLine(xavierJsonSerializedIndented);
        }

    }
}
