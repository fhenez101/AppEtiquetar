using App.Etiketarte.UI.Settings;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Gallery()
        {
            return View();
        }
    }
}