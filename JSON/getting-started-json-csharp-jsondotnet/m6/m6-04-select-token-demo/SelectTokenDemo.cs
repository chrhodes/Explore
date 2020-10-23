using JsonSamples;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m6_04_select_token_demo
{
    public static class SelectTokenDemo
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** SelectToken ***");

            string xavierJson = Generate.SingleJson();

            string courseViews = Generate.CourseViewsString(100);

            JObject xavierJObject = JObject.Parse(xavierJson);

            //SelectToken on a property
            Console.WriteLine(xavierJObject.SelectToken("name"));

            //SelectToken on an array
            Console.WriteLine(xavierJObject.SelectToken("courses[0]"));

            //Get all values of an array using SelectToken
            foreach (string c in xavierJObject.SelectToken("courses").Select(c => (string)c).ToList())
            {
                Console.WriteLine(c);
            }

            //SelectToken with recursive descent and wildcard
            foreach (JToken w in xavierJObject.SelectTokens("$.courses..*"))
            {
                Console.WriteLine(w.ToString());
            }

            JObject courseViewsJArray = JObject.Parse(courseViews);

            //SelectToken on properties of members in an array
            Console.WriteLine(courseViewsJArray.SelectToken("views[0].watchedDate"));

            //And of course SelectToken with LINQ methods
            Console.WriteLine(courseViewsJArray.SelectToken("views").Sum(w => (int)w.SelectToken("secondsWatched")));

            //SelectToken with a more complex JSONPath, the last object
            Console.WriteLine(courseViewsJArray.SelectToken("$.views[-1:]"));

            //SelectToken with a condition
            IEnumerable<JToken> viewsJsonPathSelectTokens = courseViewsJArray.SelectTokens("$.views[?(@.course == 'Solr')]");
            Console.WriteLine(viewsJsonPathSelectTokens.Count());
            Console.WriteLine(viewsJsonPathSelectTokens.ElementAt(0).ToString());

            //SelectToken for the first three seconds watched
            foreach (JToken cv in courseViewsJArray.SelectTokens("$.views[:3].secondsWatched"))
            {
                Console.WriteLine(cv.ToString());
            }

            //Combine SelectToken with LINQ
            foreach (JToken cv in courseViewsJArray.SelectTokens("$.views[:5]").Where(v => v["course"].Value<string>() == "Solr"))
            {
                Console.WriteLine(cv.ToString());
            }
        }
    }
}
