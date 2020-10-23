using JsonSamples;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m5_03_json_populate_demo
{
    class PopulateDemo
    {
        /// <summary>
        /// PopulateObject  
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** PopulateObject ***");

            //Generate test data
            List<UserInteraction> userLogs = Generate.UserInteractions(100);

            string jsonReviewed = @"{
                'reviewed': true,
                'processedBy': ['ReviewerProcess'],
                'reviewedDate': '" + DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssK") + @"' 
            }";
            Console.WriteLine(jsonReviewed);

            Console.WriteLine("- Populate values");
            foreach (UserInteraction log in userLogs)
            {
                JsonConvert.PopulateObject(jsonReviewed, log);
            }

            // Check change
            Console.WriteLine("Reviewed: " + userLogs[0].reviewed);
            Console.WriteLine("Reviewed Date: " + userLogs[0].reviewedDate);
            Console.WriteLine("Processed By: " + String.Join(" | ", userLogs[0].processedBy));
        }
    }
}
