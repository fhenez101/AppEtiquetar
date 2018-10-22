using App.Etiketarte.UI.Areas.Etiketas.Models.MailModels;
using App.Etiketarte.UI.Security;
using App.Etiketarte.UI.Settings;
using App.Etiketarte.Utilities.Email;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Areas.Etiketas.Controllers
{
    [AllowAnonymous]
    public class ContactoController : BaseController
    {
        EmailSettings ms = null;

        public ContactoController()
        {
            ms = new EmailSettings(this);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new Contact());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ReCaptcha]
        public ActionResult Send(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                if (ModelState.ContainsKey(nameof(contact.ReCaptcha)))
                {
                    ModelState[nameof(contact.ReCaptcha)].Errors.Select(x => x.ErrorMessage).ToList().ForEach(x =>
                    {
                        ModelState.AddModelError(nameof(contact.ReCaptcha), x);
                    });
                }

                return View("Index", contact);
            }
            else
            {
                var email = new EmailModel
                {
                    Body = ms.RenderViewToString(Configuration.EtiketasContact, contact),
                    Subject = contact.Nombre,
                    To = new List<MailAddress>() { new MailAddress(Configuration.InfoMail) }
                };

                new Thread(() => new EmailSender().Send(email)).Start();
            }

            return View(contact);
        }
    }
}