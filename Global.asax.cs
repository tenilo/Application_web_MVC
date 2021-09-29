using Appli.MVC.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Appli.MVC.Entity
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // le mapping
            AutoMapper.Mapper.CreateMap<DAL3.Personne, PersonneEditee>();
            // mapping inverse pour sauver les modifications dans la base
            AutoMapper.Mapper.CreateMap<PersonneEditee, DAL3.Personne>();

            

        }
    }
}
