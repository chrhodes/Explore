using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcNuget.Models
{
    public class TimeCard
    {
        public Guid ID { get; set; }
        public int Hours { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}