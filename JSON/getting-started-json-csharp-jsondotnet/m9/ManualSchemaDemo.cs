using JsonSamples;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m9_01_json_schema_demo
{
    public static class ManualSchemaDemo
    {
        /// <summary>
        /// Create a Schema manually
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** Json.NET Create Schema Manually ***");

            // Parse the JSON text 
            string xavierAuthor = Generate.SingleJson();
            JObject author = JObject.Parse(xavierAuthor);

            // Generate the Schema  
            JSchema manualSchema = ManuallyCreateSchema();
            Console.WriteLine(manualSchema.ToString());

            // Check the Schema
            IList<string> why;
            bool validateManual = author.IsValid(manualSchema, out why);
            Console.WriteLine("Author is valid: " + validateManual);         
        }

        public static JSchema ManuallyCreateSchema()
        {
            Console.WriteLine("- Manually generated schema");

            JSchema schema = new JSchema
            {
                Type = JSchemaType.Object,
                Properties =    {
                    { "name", new JSchema { Type = JSchemaType.String } },
                    { "courses", new JSchema{
                         Type = JSchemaType.Array,
                         Items = { new JSchema { Type = JSchemaType.String } }
                        }
                    },
                    { "since", new JSchema { Type = JSchemaType.String } },
                    { "happy", new JSchema { Type = JSchemaType.Boolean } },
                    { "issues", new JSchema { Type = JSchemaType.Object } },
                    { "car", new JSchema { Type = JSchemaType.Object } },
                    { "authorRelationship", new JSchema { Type = JSchemaType.Integer } },
                }
            };

            return schema;
        }
    }
}
