using App.Etiketarte.UI.Settings;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Security
{
    public class ReCaptchaAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string userIP = filterContext.RequestContext.HttpContext.Request.UserHostAddress;
            string privateKey = Configuration.RecaptchaPrivateKey;
            string response = filterContext.RequestContext.HttpContext.Request.Form["g-recaptcha-response"];
            string urlVerify = Configuration.RecaptchaVerify;

            string reply = new WebClient()
                .DownloadString($"{urlVerify}?secret={privateKey}&response={response}&remoteip={userIP}");

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            if (!captchaResponse.Success)
            {
                captchaResponse.ErrorCodes.ForEach(x =>
                {
                    switch (x.ToLower())
                    {
                        case ("missing-input-secret"):
                            ToModelState(filterContext, "The secret parameter is missing");
                            break;
                        case ("invalid-input-secret"):
                            ToModelState(filterContext, "The secret parameter is invalid or malformed");
                            break;
                        case ("missing-input-response"):
                            ToModelState(filterContext, "The response parameter is missing");
                            break;
                        case ("invalid-input-response"):
                            ToModelState(filterContext, "The response parameter is invalid or malformed");
                            break;
                        default:
                            ToModelState(filterContext, "Error occured. Please try again");
                            break;
                    }
                });
            }
        }

        private void ToModelState(ActionExecutingContext filterContext, string state)
        {
            ((Controller)filterContext.Controller).ModelState.AddModelError("ReCaptcha", state);
        }
    }

    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}