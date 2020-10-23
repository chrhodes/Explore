using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m3_01_settings_demo
{
    public static class SerializationCircularReferencesDemo
    {
        /// <summary>
        /// Controls serialization when circular references are present
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** Circular References ***");

            Author xavier = new Author { name = "Xavier Morera" };
            Author alba = new Author { name = "J. Alba" };
            Author simon = new Author { name = "Simon Robinson" };

            xavier.favoriteAuthors = new List<Author>() { xavier, alba, simon };
            
            // Serialize with a circular reference  
            try
            {
                Console.WriteLine("- Serialize Author with circular reference");
                string xavierJson = JsonConvert.SerializeObject(xavier);
                Console.WriteLine(xavierJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Serialize ignoring the circular reference 
            Console.WriteLine("- Serialize Ignoring Author circular reference with ReferenceLoopHandling");
            JsonSerializerSettings settingsIgnore = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            string xavierJsonReferenceLoopHandling = JsonConvert.SerializeObject(xavier, settingsIgnore);
            Console.WriteLine(xavierJsonReferenceLoopHandling);

            // Serialize managing the circular reference  
            Console.WriteLine("- Serialize Managing Author circular reference with ReferenceLoopHandling");
            JsonSerializerSettings settingsAll = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                Formatting = Formatting.Indented
            };
            string xavierJsonPreserveReferencesHandling = JsonConvert.SerializeObject(xavier, settingsAll);
            Console.WriteLine(xavierJsonPreserveReferencesHandling);
        }
    }
}
