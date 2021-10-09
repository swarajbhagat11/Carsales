using Autofac;
using Carsales.Models;
using Carsales.Repositories;
using Carsales.Repositories.Interfaces;
using Carsales.Services;
using Carsales.Services.Interfaces;

namespace Carsales.DIContainer
{
    public class AppAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CarService>().Named<IVehicleService>("car");
            builder.RegisterType<GenericRepository<CarDTO>>().As<IGenericRepository<CarDTO>>();

            // // Other Lifetime
            // // Transient
            // builder.RegisterType<MyService>().As<IService>()
            //     .InstancePerDependency();

            // // Scoped
            // builder.RegisterType<MyService>().As<IService>()
            //     .InstancePerLifetimeScope();

            // builder.RegisterType<MyService>().As<IService>()
            //     .InstancePerRequest();

            // // Singleton
            // builder.RegisterType<MyService>().As<IService>()
            //     .SingleInstance();

            // Scan an assembly for components
            //builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
            //       .Where(t => t.Name.EndsWith("Service"))
            //       .AsImplementedInterfaces();
        }
    }
}
