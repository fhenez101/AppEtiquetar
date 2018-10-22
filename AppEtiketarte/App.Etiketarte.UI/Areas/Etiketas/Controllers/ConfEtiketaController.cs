using App.Etiketarte.Business.Etiketas.Clases;
using App.Etiketarte.Business.Etiketas.Interfaces;
using App.Etiketarte.Model;
using App.Etiketarte.Model.ViewModels;
using App.Etiketarte.Notification;
using App.Etiketarte.UI.Areas.Etiketas.Models.ViewModels;
using App.Etiketarte.UI.Security;
using App.Etiketarte.UI.Settings;
using App.Etiketarte.Utilities.Images;
using App.Etiketarte.Utilities.Models;
using App.Etiketarte.Utilities.Refleccion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Areas.Etiketas.Controllers
{
    [Secure]
    public class ConfEtiketaController : BaseController
    {
        NotificationMessage nm = new NotificationMessage();
        RequestSettings rs = null;
        VmConfEtiketa gVmConfEtiketa = new VmConfEtiketa();
        VmArteForma gVmArteForma = new VmArteForma();

        public ConfEtiketaController()
        {
            rs = new RequestSettings(this);
            gVmConfEtiketa.Etiketa = SetEtiketa();
            gVmArteForma.Forma = SetForma();
            gVmArteForma.Tipografias = SetTipografia();
            gVmArteForma.Colors = SetColor();
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

                var confEtiketa = ConfEtiketaToVmConfEtiketa(id.Value);

                if (confEtiketa == null)
                {
                    return HttpNotFound();
                }

                return View(confEtiketa);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(gVmConfEtiketa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "", Exclude = "Etiketa")] VmConfEtiketa vmConfEtiketa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    (new ConfEtiketaB() as IConfEtiketaB).Create(VmConfEtiketaToConfEtiketa(vmConfEtiketa), nm);

                    if (nm.IsNotified)
                    {
                        ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                        return View(gVmConfEtiketa);
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(gVmConfEtiketa);
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

                var vmConfEtiketa = ConfEtiketaToVmConfEtiketa(id.Value);
                vmConfEtiketa.Etiketa = gVmConfEtiketa.Etiketa;

                SessionSettings<ConfEtiketa>.OldModel = (new ConfEtiketaB() as IConfEtiketaB).Read(id.Value);

                if (vmConfEtiketa == null)
                {
                    return HttpNotFound();
                }

                return View(vmConfEtiketa);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "", Exclude = "Etiketa")] VmConfEtiketa vmConfEtiketa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var confEtiketa = VmConfEtiketaToConfEtiketa(vmConfEtiketa);
                    confEtiketa.ModifiedProperties = new CompareObjects<ConfEtiketa>()
                           .Compare(SessionSettings<ConfEtiketa>.OldModel, confEtiketa, "RowVersion", "State");

                    SessionSettings<ConfEtiketa>.Remove(nameof(SessionSettings<ConfEtiketa>.OldModel));

                    (new ConfEtiketaB() as IConfEtiketaB).Edit(confEtiketa, nm);

                    if (nm.IsNotified)
                    {
                        ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                        return View(gVmConfEtiketa);
                    }

                    return RedirectToAction("Index");
                }

                return View(gVmConfEtiketa);
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
                var confEtiketa = (new ConfEtiketaB() as IConfEtiketaB).Read(id.Value);

                if (confEtiketa != null)
                {
                    (new ConfEtiketaB() as IConfEtiketaB).Delete(confEtiketa, nm);

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

        #region ArteForma

        [HttpGet]
        [RouteData]
        public ActionResult ArteForma(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var arteForma = ReadListArteForma(id.Value);

            if (arteForma == null)
            {
                return HttpNotFound();
            }

            ViewBag.idConfEtiketa = id.Value.ToString();

            return View(arteForma);
        }

        [HttpGet]
        public ActionResult DetailsArteForma(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var confEtiketa = ReadArteForma(id.Value);

                if (confEtiketa == null)
                {
                    return HttpNotFound();
                }

                return View(confEtiketa);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult CreateArteForma(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            gVmArteForma.IdConfEtiketa = id.Value;
            return View(gVmArteForma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateArteForma([Bind(Include = "")] VmArteForma vmArteForma, string[][] matrix,  HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var arteSplit = new ImageHelper<ArteSplit>().SplitImageWaterMark(new SafeImage()
                {
                    HttpPostedFileBaseImg = file,
                    FullPath = Configuration.ArtesPath,
                    WatermarkImg = Path.Combine(Configuration.WaterMarksPath, Configuration.WaterMaerkArtesImg),
                    ImageParts = Configuration.ImageParts,
                    Angle = Configuration.Angle
                });

                var vmArteFormaM = new VmArteFormaMI
                {
                    IdConfEtiketa = vmArteForma.IdConfEtiketa,
                    IdForma = vmArteForma.IdForma,
                    IdTipografia = vmArteForma.IdTipografia,
                    IdColor = vmArteForma.IdColor,
                    TextTop = vmArteForma.TextTop,
                    TextLeft = vmArteForma.TextLeft,
                    TextAlign = vmArteForma.TextAlign,
                    FontMinSize = vmArteForma.FontMinSize,
                    FontMaxSize = vmArteForma.FontMaxSize,
                    NumeroLineas = vmArteForma.NumeroLineas,
                    CaracteresLinea = vmArteForma.CaracteresLinea,
                    ContainerWidth = vmArteForma.ContainerWidth,
                    Codigo = vmArteForma.Codigo.ToUpper(),
                    Nombre = vmArteForma.Nombre,
                    Path = Configuration.ArtesPath,
                    ArteSplit = arteSplit
                };

                (new ArteFormaB() as IArteFormaB).Create(vmArteFormaM, nm);

                if (nm.IsNotified)
                {
                    var fileList = arteSplit.Select(x => x.Nombre).ToList();
                    new ImageHelper<dynamic>().DeleteFile(fileList, Configuration.ArtesPath);
                    ModelState.AddModelError(string.Empty, nm.Messages.First().Title);

                    return RedirectToAction("CreateArteForma", gVmArteForma);
                }
            }

            return RedirectToAction("ArteForma", new { id = vmArteForma.IdConfEtiketa });
        }

        [HttpGet]
        public ActionResult EditArteForma(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var arteForma = ReadArteForma(id.Value);
                arteForma.Forma = gVmArteForma.Forma;
                arteForma.IdTipografiaEdit = arteForma.Tipografias.Select(x => x.IdTipoGrafia).ToArray();
                arteForma.IdColorEdit = arteForma.Colors.Select(x => x.IdColor).ToArray();
                arteForma.Tipografias = gVmArteForma.Tipografias;
                arteForma.Colors = gVmArteForma.Colors;

                SessionSettings<VmArteForma>.OldModel = arteForma;

                if (arteForma == null)
                {
                    return HttpNotFound();
                }

                return View(arteForma);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditArteForma([Bind(Include = "")] VmArteForma vmArteForma, HttpPostedFileBase file)
        {
            try
            {
                var arteSplit = new List<ArteSplit>();
                var fileList = new List<string>();

                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        arteSplit = new ImageHelper<ArteSplit>().SplitImageWaterMark(new SafeImage()
                        {
                            HttpPostedFileBaseImg = file,
                            FullPath = Configuration.ArtesPath,
                            WatermarkImg = Path.Combine(Configuration.WaterMarksPath, Configuration.WaterMaerkArtesImg),
                            ImageParts = Configuration.ImageParts,
                            Angle = Configuration.Angle
                        });

                        fileList = SessionSettings<VmArteForma>.OldModel.ArteSplit.Select(x => x.Nombre).ToList();
                    }

                    var vmArteFormaM = new VmArteFormaMU
                    {
                        IdArteForma = vmArteForma.IdArteForma,
                        IdForma = vmArteForma.IdForma,
                        IdTipografia = vmArteForma.IdTipografia,
                        IdColor = vmArteForma.IdColor,
                        TextTop = vmArteForma.TextTop,
                        TextLeft = vmArteForma.TextLeft,
                        TextAlign = vmArteForma.TextAlign,
                        FontMinSize = vmArteForma.FontMinSize,
                        FontMaxSize = vmArteForma.FontMaxSize,
                        NumeroLineas = vmArteForma.NumeroLineas,
                        CaracteresLinea = vmArteForma.CaracteresLinea,
                        ContainerWidth = vmArteForma.ContainerWidth,
                        IdArte = vmArteForma.IdArte,
                        Codigo = vmArteForma.Codigo.ToUpper(),
                        Nombre = vmArteForma.Nombre,
                        ArteSplit = arteSplit
                    };

                    (new ArteFormaB() as IArteFormaB).Edit(vmArteFormaM, nm);
                    SessionSettings<VmArteForma>.Remove(nameof(SessionSettings<VmArteForma>.OldModel));

                    if (nm.IsNotified)
                    {
                        fileList = arteSplit.Select(x => x.Nombre).ToList();
                        ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                    }

                    new ImageHelper<dynamic>().DeleteFile(fileList, Configuration.ArtesPath);

                    return RedirectToAction("EditArteForma", new { id = vmArteForma.IdArteForma });
                }

                return RedirectToAction("EditArteForma", gVmArteForma);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult DeleteArteForma(int? id)
        {
            var arteForma = (new ArteFormaB() as IArteFormaB).Read(id.Value);

            if (arteForma != null)
            {
                var fileList = arteForma.ArteSplit.Select(x => x.Nombre).ToList();

                (new ArteFormaB() as IArteFormaB).Delete(arteForma.IdArteForma, nm);


                if (nm.IsNotified)
                {
                    ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                    return RedirectToAction("ArteForma", new { id = arteForma.IdConfEtiketa });
                }

                new ImageHelper<dynamic>().DeleteFile(fileList, Configuration.ArtesPath);
            }
            else
            {
                nm.Messages.Add(Messages.General.NO_EXISTS);
            }

            return RedirectToAction("ArteForma", new { id = arteForma.IdConfEtiketa });
        }

        #endregion

        #region PrivateFunctions

        private List<VmConfEtiketa> ReadList()
        {
            try
            {
                return (new ConfEtiketaB() as IConfEtiketaB).ReadList()
                    .Select(x => new VmConfEtiketa
                    {
                        IdConfEtiketa = x.IdConfEtiketa,
                        IdEtiketa = x.IdEtiketa,
                        NombreEtiketa = x.Etiketa.Nombre,
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

        private ConfEtiketa VmConfEtiketaToConfEtiketa(VmConfEtiketa vmConfEtiketa)
        {
            try
            {
                return new ConfEtiketa
                {
                    IdConfEtiketa = vmConfEtiketa.IdConfEtiketa,
                    IdEtiketa = vmConfEtiketa.IdEtiketa,
                    Nombre = vmConfEtiketa.Nombre,
                    Estado = vmConfEtiketa.Estado,
                    RowVersion = vmConfEtiketa.RowVersion
                };
            }
            catch
            {
                throw;
            }
        }

        private VmConfEtiketa ConfEtiketaToVmConfEtiketa(int idConfEtiketa)
        {
            try
            {
                var confEtiketa = (new ConfEtiketaB() as IConfEtiketaB).Read(idConfEtiketa);

                return new VmConfEtiketa
                {
                    IdEtiketa = confEtiketa.IdEtiketa,
                    IdConfEtiketa = confEtiketa.IdConfEtiketa,
                    NombreEtiketa = confEtiketa.Etiketa.Nombre,
                    Nombre = confEtiketa.Nombre,
                    Estado = confEtiketa.Estado,
                    RowVersion = confEtiketa.RowVersion
                };
            }
            catch
            {
                throw;
            }
        }

        private List<SelectListItem> SetEtiketa()
        {
            try
            {
                return (new EtiketaB() as IEtiketaB).ReadList()
                    .Select(x => new SelectListItem
                    {
                        Value = x.IdEtiketa.ToString(),
                        Text = x.Nombre
                    }).ToList();
            }
            catch
            {
                throw;
            }
        }

        private List<Forma> SetForma()
        {
            return (new FormaB() as IFormaB).ReadList();
        }

        private List<Tipografia> SetTipografia()
        {
            return (new TipografiaB() as ITipografiaB).ReadList();
        }

        private List<Color> SetColor()
        {
            return (new ColorB() as IColorB).ReadList();
        }

        private VmArteForma ReadArteForma(int idArteForma)
        {
            try
            {
                var arteForma = (new ArteFormaB() as IArteFormaB).Read(idArteForma);
                var result = new VmArteForma
                {
                    IdArteForma = arteForma.IdArteForma,
                    IdConfEtiketa = arteForma.IdConfEtiketa,
                    NombreConfEtiketa = arteForma.NombreConfEtiketa,
                    NombreEtiketa = arteForma.NombreEtiketa,
                    TextTop = arteForma.TextTop,
                    TextLeft = arteForma.TextLeft,
                    TextAlign = arteForma.TextAlign,
                    FontMinSize = arteForma.FontMinSize,
                    FontMaxSize = arteForma.FontMaxSize,
                    NumeroLineas = arteForma.NumeroLineas,
                    CaracteresLinea = arteForma.CaracteresLinea,
                    ContainerWidth = arteForma.ContainerWidth,

                    //Arte
                    IdArte = arteForma.IdArte,
                    Codigo = arteForma.ArteCodigo,
                    Nombre = arteForma.ArteNombre,
                    ArtePath = arteForma.ArtePath,
                    ArteSplit = arteForma.ArteSplit,

                    //Forma
                    IdForma = arteForma.IdForma,
                    FormaCodigo = arteForma.FormaCodigo,
                    FormaNombre = arteForma.FormaNombre,
                    FormaPath = arteForma.FormaPath,
                    FormaSplit = arteForma.FormaSplit,

                    Tipografias = arteForma.Tipografias,
                    Colors = arteForma.Colores
                };

                return result;
            }
            catch
            {
                throw;
            }
        }

        private List<VmArteForma> ReadListArteForma(int idConfEtiketa)
        {
            try
            {
                var result = (new ArteFormaB() as IArteFormaB).ReadList(idConfEtiketa)
                    .Select(x => new VmArteForma
                    {
                        IdArteForma = x.IdArteForma,
                        NombreConfEtiketa = x.NombreConfEtiketa,
                        NombreEtiketa = x.NombreEtiketa,

                        //Arte
                        Codigo = x.ArteCodigo,

                        //Forma
                        FormaCodigo = x.FormaCodigo,

                        Tipografias = x.Tipografias,
                        Colors = x.Colores
                    }).ToList();

                return result;
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}