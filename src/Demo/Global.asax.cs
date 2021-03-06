﻿using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Demo
{
    public class EPiServerApplication : EPiServer.Global
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //Tip: Want to call the EPiServer API on startup? Add an initialization module instead (Add -> New Item.. -> EPiServer -> Initialization Module)
        }

        protected override void RegisterRoutes(RouteCollection routes)
        {
            base.RegisterRoutes(routes);
            routes.MapRoute("GoogleAuth", "GoogleAuth/{action}", new { controller = "GoogleAuth", action = "Index" });
            routes.MapRoute("GoogleAuthCallback", "AuthCallback/{action}", new { controller = "AuthCallback", action = "Index" });
        }
    }
}