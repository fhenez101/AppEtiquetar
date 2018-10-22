using App.Etiketarte.Notification;
using App.Etiketarte.UI.Security;
using App.Etiketarte.UI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Areas.Etiketas.Controllers
{
    [Secure]
    public class PromocionController : BaseController
    {
        NotificationMessage nm = new NotificationMessage();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}