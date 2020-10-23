using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m3_02_attributes_demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // JsonObjectAttribute, JsonArrayAttribute, JsonDictionaryAttribute
            JsonAttributesDemo.Show();

            // JsonObject, JsonProperty, NonSerialized, JsonIgnoreAttribute
            JsonOptInOptOutDemo.Show();

            // JsonPropertyAttribute
            JsonPropertyAttributeDemo.Show();

            // JsonConverterAttribute 
            JsonConverterAttributeDemo.Show();

            // JsonConstructorAttribute
            JsonConstructorAttributeDemo.Show();

            // JsonExtensionDataAttribute
            JsonExtensionDataAttributeDemo.Show();

        }
    }
}
