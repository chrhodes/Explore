using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcIntroduction.Models
{
    public class Movie
    {
        [Required]
        public string Title { get; set; }
        [Range(1,4)]
        public int Rating { get; set; }
    }
}