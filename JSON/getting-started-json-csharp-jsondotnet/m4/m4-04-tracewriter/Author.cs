using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m4_04_tracewriter
{
    public class Author
    {
        public string name { get; set; }
        public string[] courses { get; set; }
        public DateTime since { get; set; }
        public bool happy { get; set; }
        public object issues { get; set; }
        public Car car { get; set; }
    }

    public class Car
    {
        public string model { get; set; }
        public int year { get; set; }
    }

}
