using JsonSamples;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m6_03_query_json_linq
{
    public static class QueryingJSONWithLINQDemo
    {
        /// <summary>
        /// Query JSON with LINQ
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** LINQ to JSON Functions ***");

            string courseViews = Generate.CourseViewsString(100);

            // courseViews is composed as { views: [...] }
            JObject courseViewsObject = JObject.Parse(courseViews);

            JArray views = courseViewsObject.Value<JArray>("views");

            // secondsWatched 
            List<int> secondsWatchedList = (from s in views select (int)s["secondsWatched"]).ToList();
            int sumWatchedList = 0;
            foreach (int s in secondsWatchedList)
            {
                sumWatchedList += s;
            }
            Console.WriteLine(sumWatchedList);

            // Aggregated functions like Sum and Average  
            int secondsWatchedSUM = views.Sum(sS => (int)sS["secondsWatched"]);
            Console.WriteLine(sumWatchedList);

            Console.WriteLine(views.Average(sS => (int)sS["secondsWatched"]));

            // Group by clause 
            var watchedByDay = from v in views
                               group v by v["course"] into vGroup
                               select new
                               {
                                   courseGrouping = vGroup.Key,
                                   courseCount = vGroup.Count()
                               };

            foreach (dynamic vD in watchedByDay)
            {
                Console.WriteLine(vD.courseGrouping + " " + vD.courseCount);
            }

            // JSON array 
            JArray solrEntries = new JArray(
                views.Where(c => c["course"].Value<string>() == "Solr").Select(v => new JObject{
                        {
                            "Course", v["course"].Value<string>()
                        },
                        {
                            "Date", v["watchedDate"].Value<DateTime>()
                        },
                        {
                            "Seconds", v["secondsWatched"].Value<int>()
                        }
                    }).Take(10)
                );
            Console.WriteLine(solrEntries.ToString());
        }
    }
}
