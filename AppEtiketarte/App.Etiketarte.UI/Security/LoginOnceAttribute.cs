using App.Etiketarte.UI.Models.Identity;
using App.Etiketarte.UI.Settings;
using System;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.Etiketarte.UI.Security
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LoginOnceAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!(SessionSettings<FacebookModel>.FacebookModel?.IsInSession ?? false))
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        area = string.Empty,
                        controller = "Error",
                        action = "AccessDenied"
                    })
                );
            }
        }
    }
}