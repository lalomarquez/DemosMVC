﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RelojChecador
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //validaciones en archivo App_GlobalResources/Resource1.resx
            ClientDataTypeModelValidatorProvider.ResourceClassKey = "Resource1";
            DefaultModelBinder.ResourceClassKey = "Resource1";
        }
    }
}
