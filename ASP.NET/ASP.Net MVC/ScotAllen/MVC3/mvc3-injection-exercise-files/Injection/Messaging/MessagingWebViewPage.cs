using System.Web.Mvc;
using Ninject;

namespace Injection.Messaging
{
    public abstract class MessagingWebViewPage<T> 
                         : WebViewPage<T>
    {
        [Inject]
        public IMessenger Messenger { get; set; }
    }
}