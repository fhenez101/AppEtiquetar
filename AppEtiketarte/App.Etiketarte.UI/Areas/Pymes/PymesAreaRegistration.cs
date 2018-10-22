using LowercaseRoutesMVC;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Areas.Pymes
{
    public class PymesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Pymes";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRouteLowercase(
                "Pymes_default",
                "Pymes/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}