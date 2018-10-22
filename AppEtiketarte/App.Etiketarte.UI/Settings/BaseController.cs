using App.Etiketarte.Business.Root.Clases;
using App.Etiketarte.Business.Root.Interfaces;
using App.Etiketarte.UI.Models.Identity;
using App.Etiketarte.Utilities.ModelSerialization;
using Facebook;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Settings
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var facebookId = filterContext.HttpContext.User.Identity.GetUserId() ?? string.Empty;
            bool isInSession = SessionSettings<FacebookModel>.FacebookModel?.IsInSession ?? false;

            if (SessionSettings<FacebookModel>.FacebookModel == null)
            {
                SessionSettings<FacebookModel>.FacebookModel = new FacebookModel();
            }

            if (!string.IsNullOrEmpty(facebookId) && !isInSession)
            {
                string accessToken = Base64String.Base64Decode(facebookId);
                var fb = new FacebookClient(accessToken);
                var myInfo = fb.Get($"/me?fields={XmlSerialization.XmlToList(Configuration.FaceBookFieldsPath)}");
                var faceObj = Serialization.Deserialize<FacebookModel>(myInfo.ToString());
                var existUser = (new UsuarioB() as IUsuarioB).Read(faceObj.FacebookId);

                if (existUser != null)
                {
                    faceObj.Id = accessToken;
                    faceObj.UserName = faceObj.FirstName;
                    faceObj.IsInSession = true;
                    faceObj.IsAdmin = (existUser.IdPerfil == 1) ? true : false;

                    SessionSettings<FacebookModel>.FacebookModel = faceObj;
                }
                else
                {
                    Request.Cookies.AllKeys.ToList().ForEach(x =>
                    {
                        Response.Cookies[x].Expires = DateTime.Now.AddDays(-1);
                    });
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}