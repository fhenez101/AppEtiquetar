using App.Etiketarte.Business.Etiketas.Clases;
using App.Etiketarte.Business.Etiketas.Interfaces;
using App.Etiketarte.Model;
using App.Etiketarte.Model.ViewModels;
using App.Etiketarte.Notification;
using App.Etiketarte.UI.Areas.Etiketas.Models.ViewModels.Oferta;
using App.Etiketarte.UI.Security;
using App.Etiketarte.UI.Settings;
using App.Etiketarte.Utilities.Refleccion;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Areas.Etiketas.Controllers
{
    [Secure]
    public class OfertaController : BaseController
    {
        NotificationMessage nm = new NotificationMessage();
        RequestSettings rs = null;

        public OfertaController()
        {
            rs = new RequestSettings(this);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View((new OfertaB() as IOfertaB).ReadList());
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var oferta = (new OfertaB() as IOfertaB).Read(id.Value);

            if (oferta == null)
            {
                return HttpNotFound();
            }

            return View(oferta);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "")] Oferta oferta)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    oferta.Codigo = oferta.Codigo.ToUpper();
                    (new OfertaB() as IOfertaB).Create(oferta, nm);

                    if (nm.IsNotified)
                    {
                        ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                        return View(oferta);
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(oferta);
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var oferta = (new OfertaB() as IOfertaB).Read(id.Value);

                SessionSettings<Oferta>.OldModel = oferta;

                if (oferta == null)
                {
                    return HttpNotFound();
                }

                return View(oferta);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "")] Oferta oferta)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    oferta.Codigo = oferta.Codigo.ToUpper();
                    oferta.ModifiedProperties = new CompareObjects<Oferta>()
                       .Compare(SessionSettings<Oferta>.OldModel, oferta, "RowVersion", "State");

                    SessionSettings<Oferta>.Remove(nameof(SessionSettings<Oferta>.OldModel));

                    (new OfertaB() as IOfertaB).Edit(oferta, nm);

                    if (nm.IsNotified)
                    {
                        ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                        return View(oferta);
                    }

                    return RedirectToAction("Index");
                }

                return View(oferta);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                var oferta = (new OfertaB() as IOfertaB).Read(id.Value);

                if (oferta != null)
                {
                    (new OfertaB() as IOfertaB).Delete(oferta.IdOferta, nm);

                    if (nm.IsNotified)
                    {
                        ModelState.AddModelError(string.Empty, nm.Messages.First().Description);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    nm.Messages.Add(Messages.General.NO_EXISTS);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult ConfigOferta(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                ViewBag.IdOferta = id.Value;

                var vmOferta = (new OfertaB() as IOfertaB).GetDetalleOferta(id.Value)
                    .Select(x => new VmOferta
                    {
                        IdDetalleOferta = x.IdDetalleOferta,
                        IdOferta = x.IdOferta,
                        NombreEtiketa = x.NombreEtiketa,
                        NombreConfEtiketa = x.NombreConfEtiketa,
                        Cantidad = x.Cantidad,
                        Estado = x.Estado
                    });

                return View(vmOferta);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult DetailsDetalleOferta(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var detalleOferta = (new OfertaB() as IOfertaB).ReadDetalleOferta(id.Value);
                var vmOferta = new VmOferta
                {
                    IdOferta = detalleOferta.IdOferta,
                    UnidadPorPaquete = detalleOferta.UnidadPorPaquete,
                    NombreEtiketa = detalleOferta.NombreEtiketa,
                    NombreConfEtiketa = detalleOferta.NombreConfEtiketa,
                    Cantidad = detalleOferta.Cantidad,
                    Estado = detalleOferta.Estado
                };

                if (detalleOferta == null)
                {
                    return HttpNotFound();
                }

                return View(vmOferta);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult CreateDetalleOferta(int id)
        {
            var vmDetalleOferta = new VmDetalleOferta
            {
                IdOferta = id,
                CmbEtiketas = GetEtiketas()
            };

            return View(vmDetalleOferta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDetalleOferta(VmDetalleOferta vmDetalleOferta)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var detalleOferta = new DetalleOferta
                    {
                        IdOferta = vmDetalleOferta.IdOferta,
                        IdConfEtiketa = vmDetalleOferta.IdConfEtiketa,
                        IdPresentacion = vmDetalleOferta.IdPresentacion,
                        Cantidad = vmDetalleOferta.Cantidad,
                        Estado = vmDetalleOferta.Estado
                    };

                    (new OfertaB() as IOfertaB).CreateDetalleOferta(detalleOferta, nm);

                    if (nm.IsNotified)
                    {
                        ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                        return View(vmDetalleOferta);
                    }

                    return RedirectToAction("CreateDetalleOferta", new { id = vmDetalleOferta.IdOferta });
                }
                else
                {
                    return View(vmDetalleOferta);
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult EditDetalleOferta(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var detalleOferta = (new OfertaB() as IOfertaB).ReadDetalleOferta(id.Value);
                var vmOferta = new VmDetalleOferta
                {
                    IdDetalleOferta = detalleOferta.IdDetalleOferta,
                    IdOferta = detalleOferta.IdOferta,
                    IdEtiketa = detalleOferta.IdEtiketa,
                    Cantidad = detalleOferta.Cantidad,
                    Estado = detalleOferta.Estado,
                    CmbEtiketas = GetEtiketas()
                };

                if (detalleOferta == null)
                {
                    return HttpNotFound();
                }

                return View(vmOferta);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDetalleOferta(VmDetalleOferta vmDetalleOferta)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var detalleOferta = new DetalleOferta
                    {
                        IdDetalleOferta = vmDetalleOferta.IdDetalleOferta,
                        IdOferta = vmDetalleOferta.IdOferta,
                        IdConfEtiketa = vmDetalleOferta.IdConfEtiketa,
                        IdPresentacion = vmDetalleOferta.IdPresentacion,
                        Cantidad = vmDetalleOferta.Cantidad,
                        Estado = vmDetalleOferta.Estado
                    };

                    (new OfertaB() as IOfertaB).EditDetalleOferta(detalleOferta, nm);

                    if (nm.IsNotified)
                    {
                        ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                        return View(vmDetalleOferta);
                    }

                    return RedirectToAction("EditDetalleOferta", new { id = vmDetalleOferta.IdDetalleOferta });
                }
                else
                {
                    return View(vmDetalleOferta);
                }
            }
            catch
            {
                throw;
            }
        }

        public ActionResult DeleteDetalleOferta(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vmOfertaML = (new OfertaB() as IOfertaB).ReadDetalleOferta(id.Value);

            if (vmOfertaML != null)
            {
                if (nm.IsNotified)
                {
                    ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                    return RedirectToAction("ConfigOferta");
                }
            }
            else
            {
                nm.Messages.Add(Messages.General.NO_EXISTS);
            }

            (new OfertaB() as IOfertaB).DeleteDetalleOferta(vmOfertaML.IdDetalleOferta, nm);

            if (nm.IsNotified)
            {
                rs.Message = nm.Messages.FirstOrDefault()?.Description;
            }

            return RedirectToAction("ConfigOferta", new { id = vmOfertaML.IdOferta });
        }

        #region DetalleOferta

        private IEnumerable<SelectListItem> GetEtiketas()
        {
            var result = (new EtiketaB() as IEtiketaB).ReadList()
                .Select(x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.IdEtiketa.ToString()
                }).ToList();

            return result;
        }

        [AjaxOnly]
        public JsonResult GetConfEtiketaOferta(int idEtiketa)
        {
            var result = new VmDetalleOferta
            {
                CmbConfEtiketas = (new ConfEtiketaB() as IConfEtiketaB).GetConfEtiketaOferta(idEtiketa)
                .Select(x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.IdConfEtiketa.ToString()
                })
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public JsonResult GetPresentacionOferta(int idEtiketa)
        {
            var result = new VmDetalleOferta
            {
                CmbPresentaciones = (new PresentacionB() as IPresentacionB).GetPresentacionOferta(idEtiketa)
                .Select(x => new SelectListItem
                {
                    Text = x.UnidadPorPaquete.ToString(),
                    Value = x.IdPresentacion.ToString()
                })
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}