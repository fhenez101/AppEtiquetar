using App.Etiketarte.UI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Areas.Pymes.Controllers
{
    [AllowAnonymous]
    public class InicioController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}