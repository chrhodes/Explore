using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace MvcControllers2.Tests.TestDoubles
{
    class FakeRequestContext : RequestContext
    {
        public FakeRequestContext()
            :base(new FakeHttpContext(), new RouteData())
        {

        }
    }
}
