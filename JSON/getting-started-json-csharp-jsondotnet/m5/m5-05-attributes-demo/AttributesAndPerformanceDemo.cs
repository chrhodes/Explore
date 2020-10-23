using JsonSamples;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m5_05_attributes_demo
{
    public static class AttributesAndPerformanceDemo
    {
        /// <summary>
        /// JsonIgnoreAttribute
        /// </summary>
        public static void Show ()
        {
            Stopwatch watch = new Stopwatch();
            string coursesJson;

            Console.Clear();
            Console.WriteLine("*** Performance Attributes ***");


            // Without attributes  
            Console.WriteLine("- SerializeObject() without attributes");
            List<CourseView> courses = Generate.CourseViews(100000);

            List<CourseViewWithoutAttributes> coursesWithoutAttributes = new List<CourseViewWithoutAttributes>();

            List<CourseViewWithAttributes> coursesWithAttributes = new List<CourseViewWithAttributes>();

            foreach (var course in courses)
            {
                coursesWithoutAttributes.Add(new CourseViewWithoutAttributes (course));
                coursesWithAttributes.Add(new CourseViewWithAttributes (course));
            }
            
            watch.Start();
            coursesJson = JsonConvert.SerializeObject(coursesWithoutAttributes);
            watch.Stop();
            Console.WriteLine("Time without attributes: " + watch.ElapsedMilliseconds);
            watch.Reset();

            Console.WriteLine("- DeserializeObject() without attributes");
            watch.Start();
            coursesWithoutAttributes = JsonConvert.DeserializeObject<List<CourseViewWithoutAttributes>>(coursesJson);
            watch.Stop();
            Console.WriteLine("Time without attributes: " + watch.ElapsedMilliseconds);
            watch.Reset();

            // With attributes  
            Console.WriteLine("- SerializeObject() with JsonIgnoreAttribute");
            watch.Start();
            coursesJson = JsonConvert.SerializeObject(coursesWithAttributes);
            watch.Stop();
            Console.WriteLine("Time with attributes: " + watch.ElapsedMilliseconds);
            watch.Reset();

            Console.WriteLine("- DeserializeObject() with JsonIgnoreAttribute");
            watch.Start();
            coursesWithAttributes = JsonConvert.DeserializeObject<List<CourseViewWithAttributes>>(coursesJson);
            watch.Stop();
            Console.WriteLine("Time with attributes: " + watch.ElapsedMilliseconds);
        }
    }
}
