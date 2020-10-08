using MvcControllers2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcControllers2.Controllers
{
    public class HelloController : IController
    {
        ILogger _logger = null;

        public HelloController(ILogger logger)
        {
            _logger = logger;
        }

        public void Execute(RequestContext requestContext)
        {
            requestContext.HttpContext.Response.Write("Hello from my Controller that derives from IController");
            _logger.Log("Log Message");
        }
    }
}