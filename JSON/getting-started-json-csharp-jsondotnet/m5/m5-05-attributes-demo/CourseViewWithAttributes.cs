using JsonSamples;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m5_05_attributes_demo
{
    public class CourseViewWithAttributes : CourseView
    {
        public CourseViewWithAttributes (CourseView cv)
        {
            if (cv != null)
            {
                this.userId = cv.userId;
                this.user = cv.user;
                this.course = cv.course;
                this.watchedDate = cv.watchedDate;
                this.secondsWatched = cv.secondsWatched;
            }
        }

        [JsonIgnoreAttribute]
        public int userId { get; set; }
        [JsonIgnoreAttribute]
        public string user { get; set; }
        public string course { get; set; }
        [JsonIgnoreAttribute]
        public DateTime watchedDate { get; set; }
        public int secondsWatched { get; set; }
    }
}
