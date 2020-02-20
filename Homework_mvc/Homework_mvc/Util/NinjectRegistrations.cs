using Homework_mvc.DAL;
using Homework_mvc.Infrastructure.Data;
using Homework_mvc.Services;
using Homework_mvc.Services.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework_mvc.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IMainService>().To<MainService>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IBlogContext>().To<BlogContext>();
        }
    }
}