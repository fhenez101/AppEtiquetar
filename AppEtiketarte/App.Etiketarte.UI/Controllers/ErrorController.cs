using App.Etiketarte.UI.Settings;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Controllers
{
    [AllowAnonymous]
    public class ErrorController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BadRequest()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NotFound()
        {
            return View();
        }

        [HttpGet]
        public ActionResult InternalServerError()
        {
            return View();
        }
    }
}