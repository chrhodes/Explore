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
    public static class BsonSerializationDemo
    {
        /// <summary>
        /// Convert JSON to BSON and back
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** BSON Serialization ***");

            string xavierJson = Generate.SingleJson();

            Author xavierObject = JsonConvert.DeserializeObject<Author>(xavierJson);

            MemoryStream bsonDataMemoryStream = new MemoryStream();
            using (BsonWriter bsonWriter = new BsonWriter(bsonDataMemoryStream))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(bsonWriter, xavierObject);
            }

            string bsonDataString = Convert.ToBase64String(bsonDataMemoryStream.ToArray());

            Console.WriteLine(bsonDataString);

            // Deserialize

            byte[] bsonDataFromString = Convert.FromBase64String(bsonDataString);

            MemoryStream bsonDataMemoryStreamTwo = new MemoryStream(bsonDataFromString);

            Author xavierDeserialized;
            using (BsonReader bsonReader = new BsonReader(bsonDataMemoryStreamTwo))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                xavierDeserialized = jsonSerializer.Deserialize<Author>(bsonReader);
            }

            Console.WriteLine(xavierDeserialized.name);
        }
    }
}
