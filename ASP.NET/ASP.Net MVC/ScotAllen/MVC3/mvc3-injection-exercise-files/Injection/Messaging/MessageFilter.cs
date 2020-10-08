using System;
using System.Web.Mvc;

namespace Injection.Messaging
{
    public class MessageFilter : IActionFilter
    {
        private readonly IMessenger _messenger;

        public MessageFilter(IMessenger messenger)
        {
            _messenger = messenger;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _messenger.SendMessage(
                    filterContext.RouteData.Values["controller"] as string
                );
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }        
    }
}