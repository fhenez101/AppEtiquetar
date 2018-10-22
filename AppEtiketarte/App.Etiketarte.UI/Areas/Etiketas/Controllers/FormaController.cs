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
    public class FormaController : BaseController
    {
        NotificationMessage nm = new NotificationMessage();
        RequestSettings rs = null;

        public FormaController()
        {
            rs = new RequestSettings(this);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View((new FormaB() as IFormaB).ReadList());
        }

        [HttpGet]
        [RouteData]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var forma = (new FormaB() as IFormaB).Read(id.Value);

            if (forma == null)
            {
                return HttpNotFound();
            }

            return View(forma);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "")] Forma forma, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file != null)
            {
                var formaSplit = new ImageHelper<FormaSplit>().SplitImageWaterMark(new SafeImage()
                {
                    HttpPostedFileBaseImg = file,
                    FullPath = Configuration.FormasPath,
                    WatermarkImg = Path.Combine(Configuration.WaterMarksPath, Configuration.WaterMaerkArtesImg),
                    ImageParts = Configuration.ImageParts,
                    Angle = Configuration.Angle
                });

                forma.Codigo = forma.Codigo.ToUpper();
                forma.Path = Configuration.FormasPath;
                forma.FormaSplits = formaSplit;

                (new FormaB() as IFormaB).Create(forma, nm);

                if (nm.IsNotified)
                {
                    ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                    return View(forma);
                }

                return RedirectToAction("Index");
            }

            return View(forma);
        }

        [HttpGet]
        [RouteData]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var forma = (new FormaB() as IFormaB).Read(id.Value);

            SessionSettings<Forma>.OldModel = forma;

            if (forma == null)
            {
                return HttpNotFound();
            }

            return View(forma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdForma,Codigo,Nombre,Estado,RowVersion")] Forma forma, HttpPostedFileBase file)
        {
            try
            {
                var formaSplit = new List<FormaSplit>();

                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        formaSplit = new ImageHelper<FormaSplit>().SplitImageWaterMark(new SafeImage()
                        {
                            HttpPostedFileBaseImg = file,
                            FullPath = Configuration.FormasPath,
                            WatermarkImg = Path.Combine(Configuration.WaterMarksPath, Configuration.WaterMaerkArtesImg),
                            ImageParts = Configuration.ImageParts,
                            Angle = Configuration.Angle
                        });

                        var fileList = SessionSettings<Forma>.OldModel.FormaSplits.Select(x => x.Nombre).ToList();
                        var formaSplitList = formaSplit.Select(x => x.Nombre).ToArray();

                        (new FormaB() as IFormaB)
                            .EditFormaSplit(SessionSettings<Forma>.OldModel.FormaSplits, formaSplitList, nm);

                        if (nm.IsNotified)
                        {
                            ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                            return View(forma);
                        }

                        new ImageHelper<dynamic>().DeleteFile(fileList, Configuration.FormasPath);
                    }

                    forma.Codigo = forma.Codigo.ToUpper();
                    forma.Path = Configuration.FormasPath;
                    forma.ModifiedProperties = new CompareObjects<Forma>()
                       .Compare(SessionSettings<Forma>.OldModel, forma, "RowVersion", "State", "Path");

                    SessionSettings<Forma>.Remove(nameof(SessionSettings<Forma>.OldModel));

                    (new FormaB() as IFormaB).Edit(forma, nm);

                    if (nm.IsNotified)
                    {
                        ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                        return View(forma);
                    }

                    return RedirectToAction("Index");
                }

                return View(forma);
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
            var forma = (new FormaB() as IFormaB).Read(id.Value);

            if (forma != null)
            {
                var fileList = forma.FormaSplits.Select(x => x.Nombre).ToList();

                new ImageHelper<dynamic>().DeleteFile(fileList, Configuration.FormasPath);
            }
            else
            {
                nm.Messages.Add(Messages.General.NO_EXISTS);
            }

            (new FormaB() as IFormaB).Delete(forma, nm);

            if (nm.IsNotified)
            {
                rs.Message = nm.Messages.FirstOrDefault()?.Description;
            }

            return RedirectToAction("Index");
        }
    }
}