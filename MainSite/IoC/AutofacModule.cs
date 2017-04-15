using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using MainSite.Interfaces;
using MainSite.Managers;

namespace MainSite.IoC
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductManager>().SingleInstance();
            builder.RegisterType<CartManager>().As<ICartManager>().SingleInstance();
        }
    }
}
