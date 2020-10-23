using Newtonsoft.Json;
using System;
using System.IO;

namespace m2_02json_serializer_demo
{
    public static class JsonTextWriterDemo
    {

        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** JsonTextWriter Demo ***");

            StringWriter sw = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);

            writer.Formatting = Formatting.Indented; //try at the beginning and at the end

            writer.WriteStartObject();
            writer.WritePropertyName("name");
            writer.WriteValue("Xavier Morera");
            writer.WritePropertyName("courses");
            writer.WriteStartArray();
            writer.WriteValue("Solr");
            writer.WriteValue("Spark");
            writer.WriteEndArray();
            writer.WritePropertyName("since");
            writer.WriteValue(new DateTime(2014, 01, 14));
            writer.WritePropertyName("happy");
            writer.WriteValue(true);
            writer.WritePropertyName("issues");
            writer.WriteNull();
            writer.WritePropertyName("car"); // what happens if you forget this one? Exception!
            writer.WriteStartObject();
            writer.WritePropertyName("model");
            writer.WriteValue("Land Rover Series III");
            writer.WritePropertyName("year");
            writer.WriteValue(1976);
            writer.WriteEndObject();
            writer.WriteEndObject();
            writer.Flush();

            string jsonText = sw.GetStringBuilder().ToString();

            Console.WriteLine(jsonText);
        }

    }
}
