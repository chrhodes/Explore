
using JsonSamples;
using System;
using System.Text.Json;

namespace m10_01_future_json
{
    public static class SystemTextJsonDemo
    {
        public static void Show()
        {

            Console.Clear();
            Console.WriteLine("*** System.Text.Json in .NET Core and .NET 5 ***");

            string authorJSON = Generate.SingleJson();
            System.Console.WriteLine(authorJSON);

            Author xavierAuthor = JsonSerializer.Deserialize<Author>(authorJSON);
            Console.WriteLine("- Deserialize");
            System.Console.WriteLine(xavierAuthor.name);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            
            string xavierString = JsonSerializer.Serialize(xavierAuthor, options);
            Console.WriteLine("- Serialize");
            System.Console.WriteLine(xavierString);           
        }
    }
}
