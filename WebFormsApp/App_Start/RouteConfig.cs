using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace WebFormsApp.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");

            routes.MapPageRoute(null, routeUrl: "EmailVerification/{username}", physicalFile: "~/EmailVerification.aspx");

            routes.MapPageRoute(null, routeUrl: "people/peoplepage/UserID={UserID}/Online={Online}", physicalFile: "~/People.aspx");
        }
    }
}
