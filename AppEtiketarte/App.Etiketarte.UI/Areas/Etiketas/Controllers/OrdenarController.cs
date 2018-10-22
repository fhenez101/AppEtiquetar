using App.Etiketarte.Business.Etiketas.Clases;
using App.Etiketarte.Business.Etiketas.Interfaces;
using App.Etiketarte.Notification;
using App.Etiketarte.UI.Areas.Etiketas.Models.ViewModels.Ordenes;
using App.Etiketarte.UI.Security;
using App.Etiketarte.UI.Settings;
using System;
using System.Linq;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Areas.Etiketas.Controllers
{
    [AllowAnonymous]
    public class OrdenarController : BaseController
    {
        NotificationMessage nm = new NotificationMessage();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Ropa()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cosas()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cuadernos()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Surtido()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Accesorios()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Llaveros()
        {
            return View();
        }

        #region Ajax

        [HttpGet]
        [AjaxOnly]
        public JsonResult GetFormas()
        {
            var idEtiketa = (new EtiketaB() as IEtiketaB).Read(TipoEtiketa.Ropa.ToString()).IdEtiketa;
            var formas = (new FormaB() as IFormaB).GetFormasOrden(idEtiketa);

            var vmEtiketas = new VmEtiketas
            {
                LFormas = formas.Select(x => new VmImagen
                {
                    Codigo = x.Codigo,
                    Nombre = x.Nombre,
                    ImageParts = Configuration.ImageParts,
                    Img = x.FormaSplits.Select(y => new VmUrlImage
                    {
                        Url = new Uri($"{Configuration.UrlForma}/{y.Nombre}").AbsoluteUri
                    }).ToList()
                }).ToList()
            };

            return Json(vmEtiketas, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AjaxOnly]
        public JsonResult GetArtes(string codigo)
        {
            var idEtiketa = (new EtiketaB() as IEtiketaB).Read(TipoEtiketa.Ropa.ToString()).IdEtiketa;
            var artes = (new ArteB() as IArteB).GetArtesOrden(idEtiketa, codigo);

            var vmEtiketas = new VmEtiketas
            {
                LArtes = artes.Select(x => new VmImagen
                {
                    Codigo = x.Codigo,
                    Nombre = x.Nombre,
                    ImageParts = Configuration.ImageParts,
                    Img = x.ArteSplits.Select(y => new VmUrlImage
                    {
                        Url = new Uri($"{Configuration.UrlArte}/{y.Nombre}").AbsoluteUri
                    }).ToList()
                }).ToList()
            };

            return Json(vmEtiketas, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AjaxOnly]
        public JsonResult GetTipografiasColorsConfiguration(string codigo)
        {
            var result = (new OrdenB() as IOrdenB).GetTipografiasColorsConfiguration(codigo, nm);
            var vmEtiketas = new VmEtiketas
            {
                LTipografias = result.Tipografias.Select(x => new VmTipografia
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                }).ToList(),
                LColores = result.Colores.Select(x => new VmColor
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Hexadecimal = x.Hexadecimal
                }).ToList(),
                LTextTop = result.TextTop,
                LTextLeft = result.TextLeft,
                LTextAlign = result.TextAlign,
                LNumeroLineas = result.NumeroLineas,
                LCaracteresLinea = result.CaracteresLinea,
                LFontMinSize = result.FontMinSize,
                LFontMaxSize = result.FontMaxSize,
                LContainerWidth = result.ContainerWidth
            };

            return Json(vmEtiketas, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}