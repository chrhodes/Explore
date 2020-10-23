using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m3_01_settings_demo
{
    public static class MetadataHandlingDemo
    {
        /// <summary>
        /// Specifies metadata property handling options 
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** Metadata Handling ***");

            // Default metadata handling
            string xavierAuthor = @"{ 'name': 'Xavier Morera', 
                '$type': 'm3_01_settings_demo.Author, m3-01-settings-demo'}";

            object xavierObjectNoSetting = JsonConvert.DeserializeObject(xavierAuthor);
            Console.WriteLine("- xavierObjectNoSetting is of type "
                                + xavierObjectNoSetting.GetType().ToString());

            // Read all metadata properties first before deserializing
            object xavierWithMetadataReadAhead = JsonConvert.DeserializeObject(xavierAuthor, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                // $type no longer needs to be first
                MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
            });
            Console.WriteLine("- xavierWithMetadataReadAhead is of type "
                                + xavierWithMetadataReadAhead.GetType().ToString());
        }
    }
}
