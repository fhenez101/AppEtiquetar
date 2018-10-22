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
    public class TipografiaController : BaseController
    {
        NotificationMessage nm = new NotificationMessage();
        RequestSettings rs = null;

        public TipografiaController()
        {
            rs = new RequestSettings(this);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View((new TipografiaB() as ITipografiaB).ReadList());
        }

        [HttpGet]
        [RouteData]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tipografia = (new TipografiaB() as ITipografiaB).Read(id.Value);

            if (tipografia == null)
            {
                return HttpNotFound();
            }

            return View(tipografia);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "")] Tipografia tipografia)
        {
            if (ModelState.IsValid)
            {
                tipografia.Path = string.Empty;
                (new TipografiaB() as ITipografiaB).Create(tipografia, nm);

                if (nm.IsNotified)
                {
                    ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                    return View(tipografia);
                }

                return RedirectToAction("Index");
            }

            return View(tipografia);
        }

        [HttpGet]
        [RouteData]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tipografia = (new TipografiaB() as ITipografiaB).Read(id.Value);

            SessionSettings<Tipografia>.OldModel = tipografia;

            if (tipografia == null)
            {
                return HttpNotFound();
            }

            return View(tipografia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "")] Tipografia tipografia)
        {
            if (ModelState.IsValid)
            {
                tipografia.ModifiedProperties = new CompareObjects<Tipografia>()
                    .Compare(SessionSettings<Tipografia>.OldModel, tipografia, "RowVersion", "State");
                SessionSettings<Tipografia>.Remove(nameof(SessionSettings<Tipografia>.OldModel));

                tipografia.Path = string.Empty;
                (new TipografiaB() as ITipografiaB).Edit(tipografia, nm);

                if (nm.IsNotified)
                {
                    ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                    return View(tipografia);
                }
                return RedirectToAction("Index");
            }
            return View(tipografia);
        }
    }
}