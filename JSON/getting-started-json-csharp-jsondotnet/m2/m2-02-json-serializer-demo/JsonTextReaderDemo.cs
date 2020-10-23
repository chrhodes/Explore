using JsonSamples;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m2_02json_serializer_demo
{
    public static class JsonTextReaderDemo
    {
        /// <summary>
        /// Demonstrate JsonTextReader
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** JsonTextReader Demo ***");
            string jsonSample = Generate.SingleJson();

            JsonTextReader jsonReader = new JsonTextReader(new StringReader(jsonSample));
            while (jsonReader.Read())
            {
                if (jsonReader.Value != null)
                    Console.WriteLine("Token: {0}, Value: {1}", jsonReader.TokenType, jsonReader.Value);
                else
                    Console.WriteLine("Token: {0}", jsonReader.TokenType);
            }
            
        }
    }
}
