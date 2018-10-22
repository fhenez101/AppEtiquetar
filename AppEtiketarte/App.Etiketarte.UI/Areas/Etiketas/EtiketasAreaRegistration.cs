using LowercaseRoutesMVC;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Areas.Etiketas
{
    public class EtiketasAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Etiketas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRouteLowercase(
                "Etiketas_default",
                "Etiketas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}