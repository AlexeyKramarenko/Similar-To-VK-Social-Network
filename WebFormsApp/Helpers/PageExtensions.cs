using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFormsApp
{
    public abstract class ActionResult
    {
        public abstract void Execute();
    }

    public class RedirectResult : ActionResult
    {
        private string url;

        public RedirectResult(string url)
        {
            this.url = url;
        }

        public override void Execute()
        {
            HttpContext.Current.Response.Redirect(this.url);
        }
    }

    public static class ActionResultInvoker
    {
        public static ActionResult LastExecutedResult { get; set; }

        public static void Execute(ActionResult result)
        {
            if (HttpContext.Current != null)
            {
                result.Execute();
            }
            else
            {
                LastExecutedResult = result;
            }
        }
    }

    public static class PageExtensions
    {
        public static void Redirect(this System.Web.UI.Page page, string url)
        {
            ActionResultInvoker.Execute(new RedirectResult(url));
        }
        public static void Redirect(this System.Web.UI.MasterPage page, string url)
        {
            ActionResultInvoker.Execute(new RedirectResult(url));
        }
    }
}