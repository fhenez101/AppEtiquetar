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
    public class ColorController : BaseController
    {
        NotificationMessage nm = new NotificationMessage();
        RequestSettings rs = null;

        public ColorController()
        {
            rs = new RequestSettings(this);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View((new ColorB() as IColorB).ReadList());
        }

        [HttpGet]
        [RouteData]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var color = (new ColorB() as IColorB).Read(id.Value);

            if (color == null)
            {
                return HttpNotFound();
            }

            return View(color);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "")] Color color)
        {
            if (ModelState.IsValid)
            {
                (new ColorB() as IColorB).Create(color, nm);

                if (nm.IsNotified)
                {
                    ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                    return View(color);
                }

                return RedirectToAction("Index");
            }

            return View(color);
        }

        [HttpGet]
        [RouteData]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var color = (new ColorB() as IColorB).Read(id.Value);

            SessionSettings<Color>.OldModel = color;

            if (color == null)
            {
                return HttpNotFound();
            }

            return View(color);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "")] Color color)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    color.ModifiedProperties = new CompareObjects<Color>()
                       .Compare(SessionSettings<Color>.OldModel, color, "RowVersion", "State");

                    SessionSettings<Color>.Remove(nameof(SessionSettings<Color>.OldModel));

                    (new ColorB() as IColorB).Edit(color, nm);

                    if (nm.IsNotified)
                    {
                        ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                        return View(color);
                    }

                    return RedirectToAction("Index");
                }

                return View(color);
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
            var color = (new ColorB() as IColorB).Read(id.Value);

            if (color != null)
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

            (new ColorB() as IColorB).Delete(color, nm);

            if (nm.IsNotified)
            {
                rs.Message = nm.Messages.FirstOrDefault()?.Description;
            }

            return RedirectToAction("Index");
        }
    }
}