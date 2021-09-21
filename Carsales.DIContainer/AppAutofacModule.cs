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
        }
    }
}
