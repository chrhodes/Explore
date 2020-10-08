using System.Web.Mvc;
using Injection.Messaging;

namespace Injection.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessenger _messenger;

        public HomeController(IMessenger messenger)
        {
            _messenger = messenger;
        }

        public ActionResult Index()
        {            
            _messenger.SendMessage("Hello!");
            return View();
        }
    }
}
