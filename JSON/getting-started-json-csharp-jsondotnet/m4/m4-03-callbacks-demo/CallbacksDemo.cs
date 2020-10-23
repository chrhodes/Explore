using JsonSamples;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m4_03_callbacks_demo
{
    public static class CallbacksDemo
    {
        /// <summary>
        /// OnSerializing, OnSerialized, OnDeserializing, OnDeserialized
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** Serialization Call Backs ***");

            string xavierJson = Generate.SingleJson(); 
            
            Console.WriteLine("Deserialize");
            Author xavierAuthor = JsonConvert.DeserializeObject<Author>(xavierJson);
            Console.WriteLine(xavierAuthor.name);

            Console.WriteLine("Serialize");
            string xavierJsonSerialized = JsonConvert.SerializeObject(xavierAuthor);
            Console.WriteLine(xavierJsonSerialized);
        }
    }
}
