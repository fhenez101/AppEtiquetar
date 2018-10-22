using App.Etiketarte.UI.Settings;
using App.Etiketarte.Utilities.Images;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.Etiketarte.UI.Helpers
{
    public static class Images
    {
        public static MvcHtmlString ShowImage<T>(this HtmlHelper helper, string imagePath, T[] obj)
        {
            return new ImageHelper<dynamic>().ShowImage(imagePath, Configuration.ImageParts, obj);
        }

        public static MvcHtmlString CreateImage(this HtmlHelper helper)
        {
            var containerDiv = new TagBuilder("div");
            var formTheme = new TagBuilder("div");
            var formGroup = new TagBuilder("div");
            var inputGroup = new TagBuilder("div");
            var label = new TagBuilder("label");
            var groupBtn = new TagBuilder("span");
            var btnFile = new TagBuilder("span");
            var inputFile = new TagBuilder("input");
            var inputText = new TagBuilder("input");
            var img = new TagBuilder("img");

            inputFile.MergeAttributes(new RouteValueDictionary(
                new Dictionary<string, object>()
                {
                    {"type", "file" },
                    { "id", "imgInp"},
                    {"name", "file" }
                }));

            btnFile.MergeAttribute("class", "btn btn-default btn-file");
            btnFile.InnerHtml = "Browse… " + inputFile.ToString();

            groupBtn.MergeAttribute("class", "input-group-btn");
            groupBtn.InnerHtml = btnFile.ToString();

            inputText.MergeAttributes(new RouteValueDictionary(
                new Dictionary<string, object>()
                {
                    {"type", "text" },
                    { "class", "form-control"},
                    {"readonly", "readonly" }
                }));

            inputGroup.MergeAttribute("class", "input-group");
            inputGroup.InnerHtml = groupBtn.ToString() + inputText.ToString();

            label.InnerHtml = "Upload Image";

            img.MergeAttribute("id", "img-upload");

            formGroup.MergeAttribute("class", "form-group");
            formGroup.InnerHtml = label.ToString() + inputGroup.ToString() + img.ToString();

            formTheme.MergeAttribute("class", "form-group form-theme");
            formTheme.InnerHtml = formGroup.ToString();

            containerDiv.MergeAttribute("class", "containerImage");
            containerDiv.InnerHtml = formTheme.ToString();

            return MvcHtmlString.Create(containerDiv.ToString(TagRenderMode.Normal));
        }
    }
}