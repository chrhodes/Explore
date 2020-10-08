using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MvcControllers2.Tests.TestDoubles
{
    class FakeResponse : HttpResponseBase
    {
        public override void Write(string s)
        {
            // ...
        }
    }
}
