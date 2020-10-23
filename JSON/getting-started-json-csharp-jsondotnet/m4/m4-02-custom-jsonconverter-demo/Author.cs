using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m4_02_custom_jsonconverter_demo
{
    public class Author
    {
        public string name { get; set; }
        public string[] courses { get; set; }
        public DateTime since { get; set; }
        public bool happy { get; set; }
        public object issues { get; set; }
    }
}
