﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace APCrmAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // config.EnableCors(new EnableCorsAttribute("http://localhost:4200", "*", "*"));
            config.EnableCors(new EnableCorsAttribute("https://ztanvir-1.github.io", "*", "*"));

            // var cors = new EnableCorsAttribute("https://ztanvir-1.github.io", "*", "*");
            // config.EnableCors(cors);

            //config.MessageHandlers.Add(new CorsMessageHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
