using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace WebFormsApp
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");

            routes.MapPageRoute(null, routeUrl: "EmailVerification/{username}", physicalFile: "~/EmailVerification.aspx");

            routes.MapPageRoute(null, routeUrl: "people/UserID={UserID}/Online={Online}", physicalFile: "~/People.aspx"); 
        }
    }
}
