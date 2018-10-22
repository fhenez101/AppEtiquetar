using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace App.Etiketarte.UI.Helpers
{
    public static class Input
    {
        public static HtmlString DetailFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object ViewData)
        {
            var viewData = HtmlHelper.AnonymousObjectToHtmlAttributes(ViewData);
            var htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(viewData["htmlAttributes"]);

            htmlAttributes.Add("readonly", "on");
            htmlAttributes.Add("disabled", "on");

            return htmlHelper.TextBoxFor(expression, htmlAttributes);
        }

        public static MvcHtmlString EditDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, int selectedOption, object htmlAttributes)
        {
            var propertyName = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).PropertyName;
            var dropdown = new TagBuilder("select");

            dropdown.Attributes.Add("name", propertyName);
            dropdown.Attributes.Add("id", propertyName);

            foreach (var item in selectList)
            {
                var option = new TagBuilder("option");
                option.MergeAttribute("value", item.Value);

                if (item.Value.Equals(selectedOption.ToString()))
                {
                    option.MergeAttribute("selected", "true");
                }

                option.InnerHtml = item.Text;
                dropdown.InnerHtml += option.ToString();
            }

            dropdown.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(dropdown.ToString(TagRenderMode.Normal));
        }
    }
}