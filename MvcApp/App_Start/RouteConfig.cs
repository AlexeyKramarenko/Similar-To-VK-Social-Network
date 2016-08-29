using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;

namespace MvcApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "People",
               url: "people/UserID={UserID}/Online={Online}",
               defaults: new { controller = "People", action = "PeoplePage", UserID = "", Online = "false" },
               namespaces: new[] { "MvcApp.Controllers" });

            routes.MapRoute(
                name: "PasswordInfo",
                url: "profile/passwordinfo/",
                defaults: new { controller = "Profile", action = "PasswordInfo" },
                namespaces: new[] { "MvcApp.Controllers" });

            routes.MapRoute(
                name: "EmailInfo",
                url: "profile/emailinfo/",
                defaults: new { controller = "Profile", action = "EmailInfo" },
                namespaces: new[] { "MvcApp.Controllers" });

            routes.MapRoute(
                name: "PhoneNumberInfo",
                url: "profile/phonenumberinfo/",
                defaults: new { controller = "Profile", action = "PhoneNumberInfo" },
                namespaces: new[] { "MvcApp.Controllers" });

            routes.MapRoute(
              name: "ContactInfo",
              url: "profile/contactinfo/",
              defaults: new { controller = "Profile", action = "ContactInfo" },
              namespaces: new[] { "MvcApp.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{UserID}",
                defaults: new { controller = "Account", action = "Login", UserID = UrlParameter.Optional });

        }
    }
}
