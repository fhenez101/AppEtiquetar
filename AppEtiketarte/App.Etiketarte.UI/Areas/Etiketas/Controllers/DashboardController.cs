using App.Etiketarte.Business.Root.Clases;
using App.Etiketarte.Business.Root.Interfaces;
using App.Etiketarte.Model;
using App.Etiketarte.Notification;
using App.Etiketarte.UI.Models.Identity;
using App.Etiketarte.UI.Security;
using App.Etiketarte.UI.Settings;
using App.Etiketarte.Utilities.Refleccion;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Areas.Etiketas.Controllers
{
    [Secure]
    public class DashboardController : BaseController
    {
        NotificationMessage nm = new NotificationMessage();
        RequestSettings rs = null;

        public DashboardController()
        {
            rs = new RequestSettings(this);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Perfil()
        {
            var facebookId = SessionSettings<FacebookModel>.FacebookModel.FacebookId;

            if (facebookId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = (new UsuarioB() as IUsuarioB).Read(facebookId);
            string tmpFacebookId = usuario?.FacebookId ?? string.Empty;

            if (usuario == null && tmpFacebookId.Equals(facebookId))
            {
                return HttpNotFound();
            }
            else
            {
                return View(usuario);
            }
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "")] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var facebookId = SessionSettings<FacebookModel>.FacebookModel.FacebookId;
                    var oldModel = (new UsuarioB() as IUsuarioB).Read(facebookId);

                    usuario.IdUsuario = oldModel.IdUsuario;
                    usuario.IdPerfil = oldModel.IdPerfil;
                    usuario.FacebookId = oldModel.FacebookId;
                    usuario.FacebookEmail = oldModel.FacebookEmail;
                    usuario.ModifiedProperties = new CompareObjects<Usuario>()
                        .Compare(oldModel, usuario, "RowVersion", "State", "Estado");

                    (new UsuarioB() as IUsuarioB).Edit(usuario, nm);

                    if (nm.IsNotified)
                    {
                        ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                    }
                }

                return RedirectToAction("Perfil");
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Orden()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Rastrear()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CancelarOrden()
        {
            return View();
        }
    }
}