using App.Etiketarte.Business.Etiketas.Clases;
using App.Etiketarte.Business.Etiketas.Interfaces;
using App.Etiketarte.Model;
using App.Etiketarte.Notification;
using App.Etiketarte.UI.Security;
using App.Etiketarte.UI.Settings;
using App.Etiketarte.Utilities.Refleccion;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Areas.Etiketas.Controllers
{
    [Secure]
    public class ProductoController : BaseController
    {
        NotificationMessage nm = new NotificationMessage();
        RequestSettings rs = null;

        public ProductoController()
        {
            rs = new RequestSettings(this);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View((new ProductoB() as IProductoB).ReadList());
        }

        [HttpGet]
        [RouteData]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var producto = (new ProductoB() as IProductoB).Read(id.Value);

            if (producto == null)
            {
                return HttpNotFound();
            }

            return View(producto);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                producto.Nombre = producto.Nombre.ToCapitalize();
                producto.Codigo = producto.Codigo.ToUpper();
                (new ProductoB() as IProductoB).Create(producto, nm);

                if (nm.IsNotified)
                {
                    ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                    return View(producto);
                }

                return RedirectToAction("Index");
            }

            return View(producto);
        }

        [HttpGet]
        [RouteData]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var producto = (new ProductoB() as IProductoB).Read(id.Value);

            SessionSettings<Producto>.OldModel = producto;

            if (producto == null)
            {
                return HttpNotFound();
            }

            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "")] Producto producto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    producto.Nombre = producto.Nombre.ToCapitalize();
                    producto.Codigo = producto.Codigo.ToUpper();
                    producto.ModifiedProperties = new CompareObjects<Producto>()
                       .Compare(SessionSettings<Producto>.OldModel, producto, "RowVersion", "State");

                    SessionSettings<Producto>.Remove(nameof(SessionSettings<Producto>.OldModel));

                    (new ProductoB() as ProductoB).Edit(producto, nm);

                    if (nm.IsNotified)
                    {
                        ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                        return View(producto);
                    }

                    return RedirectToAction("Index");
                }

                return View(producto);
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
            var producto = (new ProductoB() as IProductoB).Read(id.Value);

            if (producto != null)
            {
                if (nm.IsNotified)
                {
                    ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                nm.Messages.Add(Messages.General.NO_EXISTS);
            }

            (new ProductoB() as IProductoB).Delete(producto, nm);

            if (nm.IsNotified)
            {
                rs.Message = nm.Messages.FirstOrDefault()?.Description;
            }

            return RedirectToAction("Index");
        }
    }
}