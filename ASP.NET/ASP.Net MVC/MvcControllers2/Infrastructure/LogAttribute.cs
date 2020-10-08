using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Web.Routing;

namespace MvcControllers2.Infrastructure
{
    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("Action Executing", filterContext.RouteData); 
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("Action Executed", filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("Result Executing", filterContext.RouteData);           
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("Result Executed", filterContext.RouteData);           
        }

        void Log(string stageName, RouteData routeData)
        {
            Debug.WriteLine(
                String.Format("{0}::{1} - {2}", 
                    routeData.Values["controller"],
                    routeData.Values["action"],
                    stageName));
        }        
    }
}
