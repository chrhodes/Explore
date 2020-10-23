using JsonSamples;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m8_01_bson
{
    public static class BsonInternallyDemo
    {
        /// <summary>
        /// BSON
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** BSON Internally ***");

            //http://bsonspec.org/spec.html

            string simpleJson = Generate.SingleJson();

            var simpleObject = JsonConvert.DeserializeObject(simpleJson);

            JsonSerializer jsonSerializer = new JsonSerializer();
            MemoryStream bsonMemoryStream = new MemoryStream();

            using (BsonWriter bsonWriter = new BsonWriter(bsonMemoryStream))
            {
                jsonSerializer.Serialize(bsonWriter, simpleObject);
            }

            int i = 0;
            foreach (byte b in bsonMemoryStream.ToArray()) //X2 means two uppercase HEX chars
            {
                Console.WriteLine(i + ":" + b.ToString("X2") + "(" + b + ")" +
                    " -> " + System.Text.Encoding.ASCII.GetString(new byte[] { b }));
                i++;
            }

            Console.WriteLine(Encoding.ASCII.GetString(bsonMemoryStream.ToArray()));

            Console.WriteLine(Convert.ToBase64String(bsonMemoryStream.ToArray()));
        }
    }
}
