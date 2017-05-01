using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HSA_REST
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{username}",
                defaults: new { username = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute("DefaultApi1", "api/{controller}/{AccountNumber}");
        }
    }
}
