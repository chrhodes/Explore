using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MvcControllers2.Tests.TestDoubles
{
    class FakeHttpContext : HttpContextBase
    {
        public override HttpResponseBase Response
        {
            get
            {
                return _fakeResponse;
            }
        }

        HttpResponseBase _fakeResponse = new FakeResponse();
    }
}
