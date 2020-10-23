using JsonSamples;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m5_01_performance_jsonwriter_demo
{
    public static class PerformanceDemo
    {
        /// <summary>
        /// Performance
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** Performance Demo ***");

            int numberOfViews = 1000000;
            string jsonWithViews1, jsonWithViews2;

            List<CourseView> courseViews = Generate.CourseViews(numberOfViews);

            Console.WriteLine("Performance Test: " + numberOfViews + " Objects");

            Console.WriteLine("- Create course viewership data and serialize");
            jsonWithViews1 = CreateCourseViewsAndSerialize(courseViews);

            Console.WriteLine("- Create JSON manually");
            jsonWithViews2 = CreateCourseViewsAndManuallyWriteJSON(courseViews);

            Console.WriteLine();

            Console.WriteLine("- Deserialize using reflection");
            ReadCourseViewsDeserialize(jsonWithViews1);

            Console.WriteLine("- Deserialize manually");
            ReadCourseViewsManually(jsonWithViews1);
        }

        #region Serialize

        /// <summary>
        /// Serialize using JsonConvert
        /// </summary>
        /// <param name="courseViews"></param>
        /// <returns></returns>
        private static string CreateCourseViewsAndSerialize(List<CourseView> courseViews)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            string jsonCourseViews = JsonConvert.SerializeObject(courseViews, Formatting.Indented);
            watch.Stop();
            Console.WriteLine("SerializeObject() took: " + watch.ElapsedMilliseconds + " ms");

            return jsonCourseViews;
        }

        /// <summary>
        /// Serialize using JsonTextWriter
        /// </summary>
        /// <param name="courseViews"></param>
        /// <returns></returns>
        private static string CreateCourseViewsAndManuallyWriteJSON(List<CourseView> courseViews)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            StringBuilder jsonCourseViews = new StringBuilder();
            StringWriter stringWriter = new StringWriter(jsonCourseViews);
            using (JsonWriter jsonWriter = new JsonTextWriter(stringWriter))
            {
                jsonWriter.Formatting = Formatting.Indented;
                jsonWriter.WriteStartArray();
                foreach (CourseView course in courseViews)
                {
                    jsonWriter.WriteStartObject();

                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.WriteValue(course.userId);

                    jsonWriter.WritePropertyName("user");
                    jsonWriter.WriteValue("user_" + course.userId);

                    jsonWriter.WritePropertyName("course");
                    jsonWriter.WriteValue(course.course);

                    jsonWriter.WritePropertyName("watchedDate");
                    jsonWriter.WriteValue(course.watchedDate);

                    jsonWriter.WritePropertyName("secondsWatched");
                    jsonWriter.WriteValue(course.secondsWatched);

                    jsonWriter.WriteEndObject();
                }
                jsonWriter.WriteEndArray();
            }

            watch.Stop();
            Console.WriteLine("Create JSON manually took: " + watch.ElapsedMilliseconds + " ms");
            return jsonCourseViews.ToString();
        }

        #endregion

        #region Deserialize

        /// <summary>
        /// Deserialize using JsonConverter
        /// </summary>
        /// <param name="jsonWithViews"></param>
        private static void ReadCourseViewsDeserialize(string jsonWithViews)
        {
            //Count number of minutes watched of Solr course
            Stopwatch watch = new Stopwatch();
            watch.Start();

            List<CourseView> courseViews = JsonConvert.DeserializeObject<List<CourseView>>(jsonWithViews);
            int secondsViewed = 0;

            foreach (CourseView courseView in courseViews)
            {
                if (courseView.course == "Solr")
                {
                    secondsViewed += courseView.secondsWatched;
                }
            }

            watch.Stop();
            Console.WriteLine("Calculation using Deserialize() took: " + watch.ElapsedMilliseconds + " ms. Total watched for Solr: " + secondsViewed);
        }

        /// <summary>
        /// Deserialize using JsonTextReader
        /// </summary>
        /// <param name="jsonWithViews"></param>
        private static void ReadCourseViewsManually(string jsonWithViews)
        {
            //Count number of minutes watched of Solr course
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int secondsViewed = 0;
            string currentCourse = string.Empty;

            JsonTextReader reader = new JsonTextReader(new StringReader(jsonWithViews));
            while (reader.Read())
            {
                if (reader.Value != null)
                {
                    if (reader.TokenType == JsonToken.String && reader.Value.ToString() == "Solr")
                    {
                        currentCourse = "Solr";
                    }
                    if (currentCourse == "Solr" && reader.Value.ToString() == "secondsWatched")
                    {
                        reader.Read();
                        secondsViewed += int.Parse(reader.Value.ToString());
                    }
                    //Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                }
                else
                {
                    currentCourse = string.Empty;
                    //Console.WriteLine("Token: {0}", reader.TokenType);
                }

            }

            watch.Stop();
            Console.WriteLine("Calculation using JsonTextReader took: " + watch.ElapsedMilliseconds + " ms. Total watched for Solr: " + secondsViewed);

        }
        #endregion
    }
}
