using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;

namespace MvcApp.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();


            //config.Routes.MapHttpRoute("RestApiRoute", "webapi/{controller}/{id}", new { id = RouteParameter.Optional }, new { id = @"\d+" }); //this replaces your current api route
            //config.Routes.MapHttpRoute("ApiWithActionRoute", "webapi/{controller}/{action}/{id}", new { id = RouteParameter.Optional });
            //config.Routes.MapHttpRoute("DefaultApiGetRoute", "webapi/{controller}", new { action = "Get" }, new { httpMethod = new HttpMethodConstraint(new string[] { "GET" }) });
            //config.Routes.MapHttpRoute("DefaultApiPostRoute", "webapi/{controller}", new { action = "Post" }, new { httpMethod = new HttpMethodConstraint(new string[] { "POST" }) });
            //config.Routes.MapHttpRoute("DefaultApiPutRoute", "webapi/{controller}", new { action = "Put" }, new { httpMethod = new HttpMethodConstraint(new string[] { "PUT" }) });
            //config.Routes.MapHttpRoute("DefaultApiDeleteRoute", "webapi/{controller}", new { action = "Delete" }, new { httpMethod = new HttpMethodConstraint(new string[] { "DELETE" }) });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "webapi/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "GetUsersList1",
               routeTemplate: "webapi/{controller}/{action}/{from}/{to}/{country}/{town}/{UserID}/{gender}/{online}",
               defaults: new
               {
                   from = RouteParameter.Optional,
                   to = RouteParameter.Optional,
                   country = RouteParameter.Optional,
                   town = RouteParameter.Optional,
                   gender = RouteParameter.Optional,
                   UserID = RouteParameter.Optional,
                   online = RouteParameter.Optional
               });

            config.Routes.MapHttpRoute(
                name: "GetUsersList2",
                routeTemplate: "webapi/{controller}/{action}/{from}/{to}/{country}/{town}/{UserID}/{gender}/{name}/{online}",
                defaults: new
                {
                    from = RouteParameter.Optional,
                    to = RouteParameter.Optional,
                    country = RouteParameter.Optional,
                    town = RouteParameter.Optional,
                    gender = RouteParameter.Optional,
                    UserID = RouteParameter.Optional,
                    name = RouteParameter.Optional,
                    online = RouteParameter.Optional
                });
        }
    }
}
