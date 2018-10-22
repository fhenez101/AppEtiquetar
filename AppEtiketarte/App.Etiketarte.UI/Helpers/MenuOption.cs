using App.Etiketarte.UI.Settings;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml.XPath;

namespace App.Etiketarte.UI.Helpers
{
    public static class MenuOption
    {
        private static UrlHelper UrlHelper { get; set; }

        public static MvcHtmlString Menu(this HtmlHelper htmlHelper, object attributes = null)
        {
            UrlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var oDoc = new XPathDocument(Configuration.MenuPath).CreateNavigator();

            return new MvcHtmlString(GetMenuHtml(oDoc));
        }

        private static string GetMenuHtml(XPathNavigator nav, object attributes = null)
        {
            var item = new List<String>();
            var nodes = nav.Select("items/item");
            var ul = new TagBuilder("ul");

            if (attributes != null)
            {
                var htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(attributes);
                ul.MergeAttributes(htmlAttributes);
            }

            while (nodes.MoveNext())
            {
                var a = new TagBuilder("a");
                var li = new TagBuilder("li");

                string title = nodes.Current.GetAttribute("Title", string.Empty);
                string text = nodes.Current.GetAttribute("Text", string.Empty);
                string area = nodes.Current.GetAttribute("Area", string.Empty);
                string ctrl = nodes.Current.GetAttribute("Ctrl", string.Empty);
                string mtd = nodes.Current.GetAttribute("Mtd", string.Empty);

                a.MergeAttribute("title", title);
                a.InnerHtml = text;

                if (nodes.Current.HasChildren)
                {
                    li.InnerHtml = a.ToString() + GetMenuHtml(nodes.Current);
                }
                else
                {
                    a.MergeAttribute("href", UrlHelper.Action(mtd, ctrl, new { area = area }));
                    li.InnerHtml = a.ToString();
                }

                item.Add(li.ToString() + Environment.NewLine);
            }

            ul.InnerHtml = string.Join(string.Empty, item.ToArray());

            return ul.ToString(TagRenderMode.Normal);
        }
    }
}