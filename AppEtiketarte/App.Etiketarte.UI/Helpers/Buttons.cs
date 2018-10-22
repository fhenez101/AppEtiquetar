using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Helpers
{
    public static class Buttons
    {
        public static MvcHtmlString BtnAdd(this HtmlHelper helper, object attributes = null)
        {
            var input = new TagBuilder("input");

            input.MergeAttribute("type", "submit");
            input.MergeAttribute("class", "btn-guardar");
            input.MergeAttribute("value", "Agregar");

            if (attributes != null)
            {
                var htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(attributes);
                input.MergeAttributes(htmlAttributes);
            }

            return MvcHtmlString.Create(input.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString BtnModify(this HtmlHelper helper, object attributes = null)
        {
            var input = new TagBuilder("input");

            input.MergeAttribute("type", "submit");
            input.MergeAttribute("class", "btn-guardar");
            input.MergeAttribute("value", "Modificar");

            if (attributes != null)
            {
                var htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(attributes);
                input.MergeAttributes(htmlAttributes);
            }

            return MvcHtmlString.Create(input.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString AddAction(this HtmlHelper helper, string controller, object attributes = null)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var a = new TagBuilder("a");

            a.InnerHtml = "Agregar nuevo";

            if (attributes != null)
            {
                var htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(attributes);
                a.MergeAttribute("href", urlHelper.Action(controller, htmlAttributes));
            }
            else
            {
                a.MergeAttribute("href", urlHelper.Action(controller));
            }

            return MvcHtmlString.Create(a.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString BackAction(this HtmlHelper helper, string controller, object attributes = null)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var a = new TagBuilder("a");

            a.InnerHtml = "Volver a la lista";

            if (attributes != null)
            {
                var htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(attributes);
                a.MergeAttribute("href", urlHelper.Action(controller, htmlAttributes));
            }
            else
            {
                a.MergeAttribute("href", urlHelper.Action(controller));
            }

            return MvcHtmlString.Create(a.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString EditAction(this HtmlHelper helper, string action, string controller, object attributes = null)
        {
            var propertyButtons = new PropertyButtons
            {
                Action = action,
                Controller = controller,
                Iclass = "fa fa-pencil-square-o fa-lg",
                Aclass = "btn btn-primary btn-sm",
                Atitle = "Editar",
                Attributes = attributes
            };

            return CreateControl(helper, propertyButtons);
        }

        public static MvcHtmlString DetailAction(this HtmlHelper helper, string action, string controller, object attributes = null)
        {
            var propertyButtons = new PropertyButtons
            {
                Action = action,
                Controller = controller,
                Iclass = "fa fa-eye fa-lg",
                Aclass = "btn btn-info btn-sm",
                Atitle = "Detalles",
                Attributes = attributes
            };

            return CreateControl(helper, propertyButtons);
        }

        public static MvcHtmlString DeleteAction(this HtmlHelper helper, string action, string controller, object attributes = null)
        {
            var propertyButtons = new PropertyButtons
            {
                Action = action,
                Controller = controller,
                Iclass = "fa fa-trash-o fa-lg",
                Aclass = "btn btn-danger btn-sm",
                Atitle = "Eliminar",
                Attributes = attributes
            };

            return CreateControl(helper, propertyButtons);
        }

        public static MvcHtmlString ConfigurationAction(this HtmlHelper helper, string action, string controller, object attributes = null)
        {
            var propertyButtons = new PropertyButtons
            {
                Action = action,
                Controller = controller,
                Iclass = "fa fa-cogs fa-lg",
                Aclass = "btn btn-success btn-sm",
                Atitle = "Configurar",
                Attributes = attributes
            };

            return CreateControl(helper, propertyButtons);
        }

        private static MvcHtmlString CreateControl(HtmlHelper helper, PropertyButtons propertyButtons)
        {
            var i = new TagBuilder("i");
            var a = new TagBuilder("a");

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            i.MergeAttribute("class", propertyButtons.Iclass);

            if (propertyButtons.Attributes != null)
            {
                var htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(propertyButtons.Attributes);
                a.MergeAttribute("href", urlHelper.Action(propertyButtons.Action, propertyButtons.Controller, htmlAttributes));
            }
            else
            {
                a.MergeAttribute("href", urlHelper.Action(propertyButtons.Action, propertyButtons.Controller));
            }

            a.MergeAttribute("class", propertyButtons.Aclass);
            a.MergeAttribute("title", propertyButtons.Atitle);

            a.InnerHtml = i.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(a.ToString(TagRenderMode.Normal));
        }
    }

    internal class PropertyButtons
    {
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Iclass { get; set; }
        public string Aclass { get; set; }
        public string Atitle { get; set; }
        public object Attributes { get; set; }
    }
}