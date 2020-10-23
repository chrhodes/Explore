using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m4_01_conditional_serialization_demo
{
    public static class ShowIContractResolverDemo
    {
        public static void Show ()
        {
            Console.Clear();
            Console.WriteLine("*** ContractResolver ***");

            Author xavierAuthor = new Author();
            xavierAuthor.Name = "Xavier Morera";
            xavierAuthor.Courses = new string[] { "Solr", "Spark", "Python" };
            xavierAuthor.car = new Car() { model = "Land Rover", year = 1976 };
            xavierAuthor.happy = true;
            xavierAuthor.since = new DateTime(2014, 01, 15);
            xavierAuthor.issues = null;
            xavierAuthor.IsActive = true;

            List<string> propertiesToSerialize = new List<string>(new string[] { "Name", "Courses" });

            // Create contractResolver
            SelectiveContractResolver contractResolver = new SelectiveContractResolver(propertiesToSerialize);
            string xavierJson = JsonConvert.SerializeObject(xavierAuthor, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = contractResolver
            });
            Console.WriteLine(xavierJson);
        }
    }
}
