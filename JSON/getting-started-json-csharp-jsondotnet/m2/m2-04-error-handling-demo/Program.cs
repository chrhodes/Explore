using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m2_04_error_handling_demo
{
    class Program
    {
        static void Main(string[] args)
        {
            DeserializeErrorDemo.Show();
            ConversionErrorsDemo.Show();
            HandleConversionErrorsDemo.Show();
            OnErrorAttributeDemo.Show();
        }
    }
}
