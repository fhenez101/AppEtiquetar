using App.Etiketarte.Business.Root.Clases;
using App.Etiketarte.Business.Root.Interfaces;
using App.Etiketarte.UI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Areas.Etiketas.Controllers
{
    [AllowAnonymous]
    public class CarritoController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Shoping()
        {
            return View();
        }


        [HttpGet]
        public JsonResult GetProvincias()
        {
            return Json(
                (new ProvinciaCantonDistritoB() as IProvinciaCantonDistritoB).ProvinciaReadList(),
                JsonRequestBehavior.AllowGet
            );
        }

        [HttpGet]
        public JsonResult GetCantones(int codigo_provincia)
        {
            return Json(
                (new ProvinciaCantonDistritoB() as IProvinciaCantonDistritoB).CantonRead(codigo_provincia),
                JsonRequestBehavior.AllowGet
            );
        }

        [HttpGet]
        public JsonResult GetDistritos(int codigo_canton)
        {
            return Json(
                (new ProvinciaCantonDistritoB() as IProvinciaCantonDistritoB).DistritoRead(codigo_canton),
                JsonRequestBehavior.AllowGet
            );
        }
    }
}