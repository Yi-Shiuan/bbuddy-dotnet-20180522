using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using GOOS_Sample.Services;

namespace GOOS_Sample.App_Start
{
    public static class AutofacConfig
    {
        public static void Booststrap()
        {
            var builder = new ContainerBuilder();
//            builder.Register(x=> x.ResolveComponent())
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<IBudgetRepo>().As<BudgetRepo>();
            builder.RegisterType<IBudgetService>().As<BudgetService>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}