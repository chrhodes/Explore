using JsonSamples;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m5_05_attributes_demo
{
    public class CourseViewWithoutAttributes : CourseView
    {

        public CourseViewWithoutAttributes(CourseView cv)
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

        public int userId { get; set; }

        public string user { get; set; }
        public string course { get; set; }

        public DateTime watchedDate { get; set; }
        public int secondsWatched { get; set; }
    }
}
