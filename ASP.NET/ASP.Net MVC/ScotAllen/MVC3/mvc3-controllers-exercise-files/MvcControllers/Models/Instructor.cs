using System.Web.Mvc;

namespace MvcControllers.Models
{
    public class Instructor
    {
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
    }
}