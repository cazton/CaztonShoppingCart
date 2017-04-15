using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Core.Interfaces;

namespace CartServices.IoC
{
    public class AutofacModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            //Using AutoActivate to populate our products since IStartable order is arbitrary
            builder.RegisterType<CartPopulateService>().AsSelf().AutoActivate();
            builder.RegisterType<CartService>().As<ICartService>().SingleInstance();
        }
    }
}
