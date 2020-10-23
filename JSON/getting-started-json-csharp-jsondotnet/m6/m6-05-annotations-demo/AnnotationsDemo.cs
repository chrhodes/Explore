using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m6_05_annotations_demo
{
    public static class AnnotationsDemo
    {
        /// <summary>
        /// Add a Trigger (Annotation) every time a Property Changed
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** Annotations ***");

            string courseJson = @"{
                                'name': 'Solr',
                                'secondsWatched': 0
                                }";
            //create
            JObject course = JObject.Parse(courseJson);

            course.AddAnnotation(new Dictionary<DateTime, int>());
            course.PropertyChanged += (sender, arguments) =>
            {
                DateTime annotationDate = DateTime.Now;
                Console.WriteLine("Adding new annotation at: " + annotationDate);

                course.Annotation<Dictionary<DateTime, int>>().Add(annotationDate,
                    ((JObject)sender)["secondsWatched"].Value<int>());
            };

            //Make a change
            course["secondsWatched"] = 10;

            //I showed you the event being raised, I'll make another change
            course["secondsWatched"] = 150;

            //And another one
            course["secondsWatched"] = 250;

            Dictionary<DateTime, int> changesDone = course.Annotation<Dictionary<DateTime, int>>();
            foreach (KeyValuePair<DateTime, int> change in changesDone)
            {
                Console.WriteLine("Changed on: " + change.Key + ": " + change.Value);
            }

            Console.WriteLine();
        }
    }
}
