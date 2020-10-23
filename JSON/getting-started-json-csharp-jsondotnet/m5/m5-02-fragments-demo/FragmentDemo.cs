using JsonSamples;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m5_02_fragments_demo
{
    public static class FragmentDemo
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** JSON Fragments ***");
            //Extract courses and minutes viewed
            List<UserInteraction> userLogs = Generate.UserInteractions(100000);
            string bigLog = JsonConvert.SerializeObject(userLogs);

            //Used to generate sample file when required
            //System.IO.File.WriteAllText(@"..\..\..\m5-18-memory-demo\logs.json", bigLog);

            // Deserialize using JsonConverter 
            DeserializeUsingJsonConverter(bigLog);

            // Deserialize using fragments
            DeserializeUsingFragments(bigLog);
        }

        public static void DeserializeUsingFragments (string bigLog)
        {
            Console.WriteLine("- Deserialize with JSON fragments");
            JArray logs = JArray.Parse(bigLog);
            int fragmentCount = 0;

            foreach (JObject logEntry in logs)
            {
                fragmentCount += logEntry["courseView"]["secondsWatched"].Value<int>();

                //Another way would be:
                //CourseView course = logEntry["courseView"].ToObject<CourseView>();
                //fragmentCount += course.secondsWatched;
            }
            Console.WriteLine("Total watched: " + fragmentCount);
        }

        public static void DeserializeUsingJsonConverter(string bigLog)
        {
            Console.WriteLine("- Deserialize The entire JSON text");
            try
            {
                List<UserInteraction> userInteractions = JsonConvert.DeserializeObject<List<UserInteraction>>(bigLog);
                int secondsViewed = 0;

                foreach (UserInteraction ui in userInteractions)
                {
                    secondsViewed += ui.courseView.secondsWatched;
                }

                Console.WriteLine("Total watched: " + secondsViewed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
