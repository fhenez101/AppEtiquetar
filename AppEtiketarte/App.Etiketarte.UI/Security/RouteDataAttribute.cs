using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.Etiketarte.UI.Security
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RouteDataAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            filterContext.RouteData.Values["id"] = GetIdValue(filterContext.RouteData.Values["id"]);
        }

        private object GetIdValue(object id)
        {
            if (id != null)
            {
                var match = new Regex(@"^(?<id>\d+).*$").Match(id.ToString());

                if (match.Success)
                {
                    return match.Groups["id"].Value;
                }
            }

            return id;
        }
    }
}