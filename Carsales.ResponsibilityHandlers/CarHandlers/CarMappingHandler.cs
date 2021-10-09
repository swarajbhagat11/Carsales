using Carsales.Core.ChainOfResponsibility;
using Carsales.Core.Mappers;
using Carsales.Models;
using Carsales.ViewModels;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;

namespace Carsales.ResponsibilityHandlers.CarHandlers
{
    public class CarMappingHandler : ChainHandler
    {
        private readonly ILogger<CarMappingHandler> _logger;

        public CarMappingHandler(ILogger<CarMappingHandler> logger)
        {
            _logger = logger;
        }

        public override object handle(object request)
        {
            CarDTO carDtoObj = ModelMapper.mapObjects<CarDTO>(JObject.FromObject(request).ToObject<Car>());
            _logger.LogInformation("Car mapping completed to CarDTO.");
            return base.handle(carDtoObj);
        }
    }
}
