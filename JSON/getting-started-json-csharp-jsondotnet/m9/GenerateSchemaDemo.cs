using JsonSamples;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m9_01_json_schema_demo
{
    public static class GenerateSchemaDemo
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** Json.NET Create Schema Automatically ***");

            // Parse the JSON text 
            string xavierAuthor = Generate.SingleJson();
            JObject author = JObject.Parse(xavierAuthor);

            // Generate the Schema 
            JSchemaGenerator jsonSchemaGenerator = new JSchemaGenerator();
            JSchema generatedSchema = jsonSchemaGenerator.Generate(typeof(Author));
            Console.WriteLine(generatedSchema.ToString());

            // Check the Schema 
            bool validateGenerated = author.IsValid(generatedSchema);
            Console.WriteLine("Author is valid: " + validateGenerated);
        }
    }
}
