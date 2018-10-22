using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Facebook;
using App.Etiketarte.UI.Settings;
using App.Etiketarte.UI.Models.Identity;
using App.Etiketarte.Utilities.ModelSerialization;
using App.Etiketarte.Notification;
using App.Etiketarte.Business.Root.Clases;
using App.Etiketarte.Business.Root.Interfaces;
using App.Etiketarte.Model;
using App.Etiketarte.UI.Security;

namespace App.Etiketarte.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private NotificationMessage nm = new NotificationMessage();
        private RequestSettings rs = null;
        private CustomUserManager CustomUserManager { get; set; }

        public AccountController()
            : this(new CustomUserManager())
        {
            rs = new RequestSettings(this);
        }

        public AccountController(CustomUserManager customUserManager)
        {
            CustomUserManager = customUserManager;
            rs = new RequestSettings(this);
        }

        [HttpGet]
        [LoginOnce]
        public ActionResult Login(string provider)
        {
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = Request?.UrlReferrer }));
        }

        [HttpGet]
        [LoginOnce]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();

            if (loginInfo == null)
            {
                return Redirect(returnUrl);
            }

            if (loginInfo.Login.LoginProvider.Equals("Facebook"))
            {
                var identity = AuthenticationManager.GetExternalIdentity(DefaultAuthenticationTypes.ExternalCookie);
                var access_token = identity.FindFirstValue("FacebookAccessToken");
                var fb = new FacebookClient(access_token);
                var myInfo = fb.Get($"/me?fields={XmlSerialization.XmlToList(Configuration.FaceBookFieldsPath)}");
                var faceObj = Serialization.Deserialize<FacebookModel>(myInfo.ToString());

                var user = await CustomUserManager.FindAsync(faceObj.Email, faceObj.FacebookId);

                if (user != null)
                {
                    await SignInAsync(faceObj, true, user.IsAdmin, access_token);
                    SessionSettings<FacebookModel>.FacebookModel = faceObj;

                    return Redirect(returnUrl);
                }
                else
                {
                    CreateUser(faceObj, nm);

                    if (nm.IsNotified)
                    {
                        ModelState.AddModelError(string.Empty, nm.Messages.First().Title);
                    }
                    else
                    {
                        bool isAdmin = (user != null) ? true : false;
                        await SignInAsync(faceObj, true, isAdmin, access_token);
                        SessionSettings<FacebookModel>.FacebookModel = faceObj;

                        return Redirect(returnUrl);
                    }
                }
            }
            else if (loginInfo.Login.LoginProvider.Equals("Google"))
            {
                var json = string.Empty;
                var accessToken = loginInfo.ExternalIdentity.Claims.Where(c => c.Type.Equals("urn:google:accesstoken")).Select(c => c.Value).FirstOrDefault();
                var apiRequestUri = new Uri($"https://www.googleapis.com/oauth2/v2/userinfo?access_token={accessToken}");
                using (var webClient = new System.Net.WebClient())
                {
                    json = webClient.DownloadString(apiRequestUri);
                }
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            SessionSettings<FacebookModel>.Remove(nameof(SessionSettings<FacebookModel>.FacebookModel));
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && CustomUserManager != null)
            {
                CustomUserManager.Dispose();
                CustomUserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers

        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            { }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };

                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }

                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        private void CreateUser(FacebookModel user, NotificationMessage nm)
        {
            var usuario = new Usuario
            {
                FacebookEmail = user.Email,
                FacebookId = user.FacebookId,
                Estado = true,
                IdPerfil = 3
            };

            (new UsuarioB() as IUsuarioB).Create(usuario, nm);
        }

        private async Task SignInAsync(FacebookModel user, bool isPersistent, bool isAdmin, string token)
        {
            user.Id = Base64String.Base64Encode(token);
            user.UserName = Base64String.Base64Encode(user.FacebookId);
            user.IsInSession = true;
            user.IsAdmin = isAdmin;

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            var identity = await CustomUserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                IsPersistent = isPersistent
            }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }

        #endregion
    }
}