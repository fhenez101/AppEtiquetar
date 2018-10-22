using App.Etiketarte.UI.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Helpers
{
    public static class Email
    {
        public static MvcHtmlString ReCaptcha(this HtmlHelper helper)
        {
            var sb = new StringBuilder();
            var js = new TagBuilder("script");
            var div = new TagBuilder("div");

            div.MergeAttribute("class", "g-recaptcha");
            div.MergeAttribute("data-sitekey", Configuration.RecaptchaPublicKey);

            js.MergeAttribute("src", Configuration.RecaptchaApiUrl);

            sb.AppendLine(div.ToString(TagRenderMode.Normal));
            sb.AppendLine(js.ToString(TagRenderMode.Normal));

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}