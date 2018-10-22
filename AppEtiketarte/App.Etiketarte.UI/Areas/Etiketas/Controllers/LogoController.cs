using App.Etiketarte.Business.Etiketas.Clases;
using App.Etiketarte.Business.Etiketas.Interfaces;
using App.Etiketarte.Model;
using App.Etiketarte.Notification;
using App.Etiketarte.UI.Security;
using App.Etiketarte.UI.Settings;
using App.Etiketarte.Utilities.Images;
using App.Etiketarte.Utilities.Models;
using App.Etiketarte.Utilities.Refleccion;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Areas.Etiketas.Controllers
{
    [Secure]
    public class LogoController : BaseController
    {
        NotificationMessage nm = new NotificationMessage();
        RequestSettings rs = null;

        public LogoController()
        {
            rs = new RequestSettings(this);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View((new LogoB() as ILogoB).ReadList());
        }

        [HttpGet]
        [RouteData]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var logo = (new LogoB() as ILogoB).Read(id.Value);

            if (logo == null)
            {
                return HttpNotFound();
            }

            return View(logo);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "")] Logo logo, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file != null)
            {
                var logoSplit = new ImageHelper<LogoSplit>().SplitImageWaterMark(new SafeImage()
                {
                    HttpPostedFileBaseImg = file,
                    FullPath = Configuration.LogosPath,
                    WatermarkImg = Path.Combine(Configuration.WaterMarksPath, Configuration.WaterMaerkArtesImg),
                    ImageParts = Configuration.ImageParts,
                    Angle = Configuration.Angle
                });

                logo.Path = Configuration.LogosPath;
                logo.LogoSplits = logoSplit;

                (new LogoB() as ILogoB).Create(logo, nm);

                if (nm.IsNotified)
                {
                    ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                    return View(logo);
                }

                return RedirectToAction("Index");
            }

            return View(logo);
        }

        [HttpGet]
        [RouteData]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var logo = (new LogoB() as ILogoB).Read(id.Value);

            SessionSettings<Logo>.OldModel = logo;

            if (logo == null)
            {
                return HttpNotFound();
            }

            return View(logo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdForma,Codigo,Nombre,Estado,RowVersion")] Logo logo, HttpPostedFileBase file)
        {
            try
            {
                var logoSplit = new List<LogoSplit>();

                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        logoSplit = new ImageHelper<LogoSplit>().SplitImageWaterMark(new SafeImage()
                        {
                            HttpPostedFileBaseImg = file,
                            FullPath = Configuration.FormasPath,
                            WatermarkImg = Path.Combine(Configuration.WaterMarksPath, Configuration.WaterMaerkArtesImg),
                            ImageParts = Configuration.ImageParts,
                            Angle = Configuration.Angle
                        });

                        var fileList = SessionSettings<Logo>.OldModel.LogoSplits.Select(x => x.Nombre).ToList();
                        var logoSplitList = logoSplit.Select(x => x.Nombre).ToArray();

                        (new LogoB() as ILogoB)
                            .EditLogoSplit(SessionSettings<Logo>.OldModel.LogoSplits, logoSplitList, nm);

                        if (nm.IsNotified)
                        {
                            ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                            return View(logo);
                        }

                        new ImageHelper<dynamic>().DeleteFile(fileList, Configuration.LogosPath);
                    }

                    logo.Path = Configuration.LogosPath;
                    logo.ModifiedProperties = new CompareObjects<Logo>()
                       .Compare(SessionSettings<Logo>.OldModel, logo, "RowVersion", "State", "Path");

                    SessionSettings<Logo>.Remove(nameof(SessionSettings<Logo>.OldModel));

                    (new LogoB() as ILogoB).Edit(logo, nm);

                    if (nm.IsNotified)
                    {
                        ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                        return View(logo);
                    }

                    return RedirectToAction("Index");
                }

                return View(logo);
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
            var logo = (new LogoB() as ILogoB).Read(id.Value);

            if (logo != null)
            {
                var fileList = logo.LogoSplits.Select(x => x.Nombre).ToList();

                (new LogoB() as ILogoB).DeleteLogoSplit(logo.LogoSplits, nm);
                new ImageHelper<dynamic>().DeleteFile(fileList, Configuration.LogosPath);

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

            (new LogoB() as ILogoB).Delete(logo, nm);

            if (nm.IsNotified)
            {
                rs.Message = nm.Messages.FirstOrDefault()?.Description;
            }

            return RedirectToAction("Index");
        }
    }
}