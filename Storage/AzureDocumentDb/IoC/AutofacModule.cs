using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AzureDocumentDb.Manager;
using Core.Interfaces;
using Core.Models.Config;

namespace AzureDocumentDb.IoC
{
    public class AutofacModule : Module
    {
        private const string ProductCollectionName = "ProductCollection";
        private const string CartCollectionName = "CartCollection";

        protected override void Load(ContainerBuilder builder)
        {
            //Instantiate the Client
            builder.RegisterType<AzureDocClient>().As<IAzureDocClient>().SingleInstance();

            //Instantiate the DB
            builder.Register<AzureDocDatabase>(c =>
            {
                var docClient = c.Resolve<IAzureDocClient>();
                docClient.InitializeClient(); //Initialize the client before IAzureDocDatabase is available

                return new AzureDocDatabase(docClient, c.Resolve<DocumentDbConfig>());
            }).As<IAzureDocDatabase>().SingleInstance();

            //Register a Task<IAzureDocDatabase> that will run it's InitializeDatabaseAsync method
            builder.Register(c => c.Resolve<IAzureDocDatabase>().InitializeDatabaseAsync());

            //Register a Task<ProductCollection> that will resolve Task<IAzureDocDatabase> and Initialize the collection
            builder.Register(async c =>
            {
                var collection = new ProductCollection(await c.Resolve<Task<IAzureDocDatabase>>(), ProductCollectionName);
                await collection.InitializeAsync();

                return collection;
            });

            //Register a Task<CartCollection> that will resolve Task<IAzureDocDatabase> and Initialize the collection
            builder.Register(async c =>
                {
                    var collection = new CartCollection(await c.Resolve<Task<IAzureDocDatabase>>(), CartCollectionName);
                    await collection.InitializeAsync();

                    return collection;
                });
            
            builder.Register(c => c.Resolve<Task<ProductCollection>>().Result).As<IProductCollection>().SingleInstance();
            builder.Register(c => c.Resolve<Task<CartCollection>>().Result).As<ICartCollection>().SingleInstance();

            builder.RegisterType<ProductStorageManager>().As<IProductStorage>().SingleInstance();
            builder.RegisterType<CartStorageManager>().As<ICartStorage>().SingleInstance();
        }
    }
}
