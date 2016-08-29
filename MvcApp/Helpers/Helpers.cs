using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcApp.Helpers
{
    public static class Helpers
    {
        public static MvcHtmlString Ahthorization(this HtmlHelper helper)
        {
            var link = new TagBuilder("a");

            string page = null;
            string text = null;

            string actionName = helper.ViewContext.RouteData.Values["action"].ToString().ToLower();

            switch (actionName)
            {
                case "registration":

                    page = "/account/login";
                    text = "Login";

                    break;

                case "login":

                    page = "/account/registration";
                    text = "Registration";

                    break;

                case "waitingforconfirmation":

                    page = "/account/registration";
                    text = "Registration";

                    break;

                default:

                    page = "/account/login";
                    text = "Logout";

                    break;

            }

            link.SetInnerText(text);

            link.MergeAttribute("href",   page);

            return MvcHtmlString.Create(link.ToString());
        }

        public static MvcHtmlString SpanFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var valueGetter = expression.Compile();
            var value = valueGetter(helper.ViewData.Model);

            var span = new TagBuilder("span");
            span.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            if (value != null)
            {
                span.SetInnerText(value.ToString());
            }

            return MvcHtmlString.Create(span.ToString());
        }



    }
}
