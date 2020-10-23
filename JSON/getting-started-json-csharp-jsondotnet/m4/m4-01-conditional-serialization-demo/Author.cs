using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m4_01_conditional_serialization_demo
{
    class Author
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string[] Courses { get; set; }

        public bool ShouldSerializeCourses()
        {
            //If Author IsActive then Courses will be serialized
            return IsActive;
        }

        public bool ShouldDeserializeCourses()
        {
            return false;
        }

        public DateTime since { get; set; }

        public bool happy { get; set; }

        public object issues { get; set; }

        public Car car { get; set; }
    }
}
