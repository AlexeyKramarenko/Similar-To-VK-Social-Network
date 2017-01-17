using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using WebFormsApp.App_Start;

namespace WebFormsApp
{
    public class Global : HttpApplication
    {
        protected void Application_BeginRequest()
        {

        }
        protected void Application_EndRequest()
        {

        }
        protected void Application_Start(object sender, EventArgs e)
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfig.RegisterMappings();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        #region session support in Web API
        public override void Init()
        {
            this.PostAuthenticateRequest += Application_PostAuthenticateRequest;
            base.Init();
        }

        void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }
        #endregion
    }
}