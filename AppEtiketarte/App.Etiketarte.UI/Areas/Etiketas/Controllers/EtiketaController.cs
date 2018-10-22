using App.Etiketarte.Business.Etiketas.Clases;
using App.Etiketarte.Business.Etiketas.Interfaces;
using App.Etiketarte.Model;
using App.Etiketarte.Notification;
using App.Etiketarte.UI.Areas.Etiketas.Models.ViewModels;
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
    public class EtiketaController : BaseController
    {
        NotificationMessage nm = new NotificationMessage();
        RequestSettings rs = null;
        VmEtiketa gVmEtiketa = new VmEtiketa();

        public EtiketaController()
        {
            rs = new RequestSettings(this);
            gVmEtiketa.Producto = SetProduct();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(ReadList());
        }

        [HttpGet]
        [RouteData]
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var vmEtiketa = EtiketaToVmEtiketa(id.Value);

                if (vmEtiketa == null)
                {
                    return HttpNotFound();
                }

                return View(vmEtiketa);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(gVmEtiketa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "", Exclude = "Producto")] VmEtiketa vmEtiketa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    vmEtiketa.Nombre = vmEtiketa.Nombre.ToCapitalize();
                    vmEtiketa.Codigo = vmEtiketa.Codigo.ToUpper();
                    (new EtiketaB() as IEtiketaB).Create(VmEtiketaToEtiketa(vmEtiketa), nm);

                    if (nm.IsNotified)
                    {
                        ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                        return View(gVmEtiketa);
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(gVmEtiketa);
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [RouteData]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var vmEtiketa = EtiketaToVmEtiketa(id.Value);
                vmEtiketa.Producto = gVmEtiketa.Producto;

                SessionSettings<Etiketa>.OldModel = (new EtiketaB() as IEtiketaB).Read(id.Value);

                if (vmEtiketa == null)
                {
                    return HttpNotFound();
                }

                return View(vmEtiketa);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "", Exclude = "Producto")] VmEtiketa vmEtiketa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var etiketa = VmEtiketaToEtiketa(vmEtiketa);
                    etiketa.ModifiedProperties = new CompareObjects<Etiketa>()
                           .Compare(SessionSettings<Etiketa>.OldModel, etiketa, "RowVersion", "State");

                    SessionSettings<Etiketa>.Remove(nameof(SessionSettings<Etiketa>.OldModel));

                    vmEtiketa.Nombre = vmEtiketa.Nombre.ToCapitalize();
                    etiketa.Codigo = etiketa.Codigo.ToUpper();
                    (new EtiketaB() as IEtiketaB).Edit(etiketa, nm);

                    if (nm.IsNotified)
                    {
                        ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                        return View(gVmEtiketa);
                    }

                    return RedirectToAction("Index");
                }

                return View(gVmEtiketa);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [RouteData]
        public ActionResult Delete(int? id)
        {
            try
            {
                var etiketa = (new EtiketaB() as IEtiketaB).Read(id.Value);

                if (etiketa != null)
                {
                    (new EtiketaB() as IEtiketaB).Delete(etiketa, nm);

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
        [RouteData]
        public ActionResult Presentaciones(int id)
        {
            ViewBag.IdEtiketa = id;

            return View((new PresentacionB() as IPresentacionB).ReadList(id));
        }

        [HttpGet]
        public ActionResult CreatePresentacion(int id)
        {
            var presentacion = new Presentacion
            {
                IdEtiketa = id
            };

            return View(presentacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePresentacion(Presentacion presentacion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    (new PresentacionB() as IPresentacionB).Create(presentacion, nm);

                    if (nm.IsNotified)
                    {
                        ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                        return View(presentacion);
                    }

                    return RedirectToAction("Presentaciones", new { id = presentacion.IdEtiketa });
                }

                return View(presentacion);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult EditPresentacion(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var presentacion = (new PresentacionB() as IPresentacionB).Read(id.Value);

                SessionSettings<Presentacion>.OldModel = presentacion;

                if (presentacion == null)
                {
                    return HttpNotFound();
                }

                return View(presentacion);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPresentacion(Presentacion presentacion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    presentacion.ModifiedProperties = new CompareObjects<Presentacion>()
                       .Compare(SessionSettings<Presentacion>.OldModel, presentacion, "RowVersion", "State");

                    SessionSettings<Color>.Remove(nameof(SessionSettings<Color>.OldModel));

                    (new PresentacionB() as IPresentacionB).Edit(presentacion, nm);

                    if (nm.IsNotified)
                    {
                        ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                        return View(presentacion);
                    }

                    return RedirectToAction("Presentaciones", new { id = presentacion.IdEtiketa });
                }

                return View(presentacion);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult DetailsPresentacion(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var presentacion = (new PresentacionB() as IPresentacionB).Read(id.Value);

                if (presentacion == null)
                {
                    return HttpNotFound();
                }

                return View(presentacion);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult DeletePresentacion(int? id)
        {
            try
            {
                var presentacion = (new PresentacionB() as IPresentacionB).Read(id.Value);

                if (presentacion != null)
                {
                    if (nm.IsNotified)
                    {
                        ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                        return RedirectToAction("Presentaciones", new { id = presentacion.IdEtiketa });
                    }
                }
                else
                {
                    nm.Messages.Add(Messages.General.NO_EXISTS);
                }

               (new PresentacionB() as IPresentacionB).Delete(presentacion, nm);

                if (nm.IsNotified)
                {
                    rs.Message = nm.Messages.FirstOrDefault()?.Description;
                }

                return RedirectToAction("Presentaciones", new { id = presentacion.IdEtiketa });
            }
            catch
            {
                throw;
            }
        }

        #region PrivateFunctions

        private List<VmEtiketa> ReadList()
        {
            try
            {
                return (new EtiketaB() as IEtiketaB).ReadList()
                    .Select(x => new VmEtiketa
                    {
                        IdEtiketa = x.IdEtiketa,
                        NombreProducto = x.Producto.Nombre,
                        Codigo = x.Codigo,
                        Nombre = x.Nombre,
                        Estado = x.Estado,
                        RowVersion = x.RowVersion
                    }).ToList();
            }
            catch
            {
                throw;
            }
        }

        private Etiketa VmEtiketaToEtiketa(VmEtiketa vmEtiketa)
        {
            try
            {
                return new Etiketa()
                {
                    IdEtiketa = vmEtiketa.IdEtiketa,
                    IdProducto = vmEtiketa.IdProducto,
                    Codigo = vmEtiketa.Codigo,
                    Nombre = vmEtiketa.Nombre,
                    Estado = vmEtiketa.Estado,
                    RowVersion = vmEtiketa.RowVersion
                };
            }
            catch
            {
                throw;
            }
        }

        private VmEtiketa EtiketaToVmEtiketa(int idEtiketa)
        {
            try
            {
                var etiketa = (new EtiketaB() as IEtiketaB).Read(idEtiketa);

                return new VmEtiketa()
                {
                    IdEtiketa = etiketa.IdEtiketa,
                    IdProducto = etiketa.IdProducto,
                    NombreProducto = etiketa.Producto.Nombre,
                    Codigo = etiketa.Codigo,
                    Nombre = etiketa.Nombre,
                    Estado = etiketa.Estado,
                    RowVersion = etiketa.RowVersion
                };
            }
            catch
            {
                throw;
            }
        }

        private List<SelectListItem> SetProduct()
        {
            try
            {
                return (new ProductoB() as IProductoB).ReadList()
                    .Select(x => new SelectListItem
                    {
                        Value = x.IdProducto.ToString(),
                        Text = x.Nombre
                    }).ToList();
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}