using WebFormsApp.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.Routing;

namespace WebFormsApp
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_BeginRequest()
        {
          
        }
        protected void Application_EndRequest()
        {
           
        }
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfig.RegisterMappings();
            RouteConfig.RegisterRoutes(RouteTable.Routes);          
        }

        #region Поддержка сессии в Web API
        public override void Init()
        {
            this.PostAuthenticateRequest += Application_PostAuthenticateRequest;
            base.Init();
        }

        void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(
                SessionStateBehavior.Required);
        }
        #endregion
    }
}