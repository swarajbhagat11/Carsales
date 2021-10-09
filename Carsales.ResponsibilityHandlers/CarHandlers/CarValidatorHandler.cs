using Carsales.Core.ChainOfResponsibility;
using Carsales.Core.HelperObjects;
using Carsales.Models;
using Carsales.Models.Validators;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carsales.ResponsibilityHandlers.CarHandlers
{
    public class CarValidatorHandler : ChainHandler
    {
        private readonly ILogger<CarValidatorHandler> _logger;

        public CarValidatorHandler(ILogger<CarValidatorHandler> logger)
        {
            _logger = logger;
        }

        public override object handle(object request)
        {
            var validator = new CarValidator();
            ValidationResult results = validator.Validate((CarDTO)request);
            _logger.LogInformation("CarDTO object validated and response is = " + results.IsValid);
            return new HandlerResponse
            {
                validation = results,
                dtoObj = request
            };
        }
    }
}
