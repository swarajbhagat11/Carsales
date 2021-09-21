using Carsales.Core;
using Carsales.Core.HelperObjects;
using Carsales.Core.Mappers;
using Carsales.Models;
using Carsales.Models.Validators;
using Carsales.Repositories.Interfaces;
using Carsales.Services.Interfaces;
using Carsales.ViewModels;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Carsales.Services
{
    public class CarService : IVehicleService
    {
        private readonly ILogger<CarService> _logger;
        private readonly IGenericRepository<CarDTO> _carRepo;

        public CarService(ILogger<CarService> logger, IGenericRepository<CarDTO> carRepo)
        {
            _logger = logger;
            _carRepo = carRepo;
        }

        public ServiceResponse AddVehicle(JObject car)
        {
            CarDTO carObj = ModelMapper.mapObjects<CarDTO>(car.ToObject<Car>());
            _logger.LogInformation("AddVehicle mapping completed to CarDTO.");
            var validator = new CarValidator();
            ValidationResult results = validator.Validate(carObj);
            _logger.LogInformation("AddVehicle CarDTO model validated.", results);

            if (!results.IsValid)
            {
                _logger.LogInformation("AddVehicle CarDTO model is not valid.");
                return new ServiceResponse { hasSuccess = false, errors = results.Errors.Select(x => x.ErrorMessage).ToList() };
            }

            IEnumerable<CarDTO> existingCars = _carRepo.Find(x => x.email == carObj.email && x.year == carObj.year && x.make == carObj.make && x.model == carObj.model);
            if (existingCars.Count() > 0)
            {
                _logger.LogInformation("Car already exists with combination of Email, Year, Make and Model.");
                return new ServiceResponse { hasSuccess = false, errors = new List<string> { "Car already exists with combination of Email, Year, Make and Model." } };
            }

            _carRepo.Insert(carObj);
            _carRepo.Save();
            _logger.LogInformation("AddVehicle CarDTO model insertion succesful.");
            return new ServiceResponse { hasSuccess = true };
        }

        public ServiceResponse EditVehicle(Guid id, JObject car)
        {
            CarDTO carObj = ModelMapper.mapObjects<CarDTO>(car.ToObject<Car>());
            _logger.LogInformation("EditVehicle mapping completed to CarDTO.");
            var validator = new CarValidator();
            ValidationResult results = validator.Validate(carObj);
            _logger.LogInformation("EditVehicle CarDTO model validated.", results);

            if (!results.IsValid)
            {
                _logger.LogInformation("EditVehicle CarDTO model is not valid.");
                return new ServiceResponse { hasSuccess = false, errors = results.Errors.Select(x => x.ErrorMessage).ToList() };
            }

            CarDTO presentCarObj = _carRepo.GetById(id);
            if (presentCarObj == null)
            {
                _logger.LogInformation("Car record not found for provided Id.");
                return new ServiceResponse { hasSuccess = false, errors = new List<string> { "Car record not found for provided Id." } };
            }

            Utility.copyObject(carObj, presentCarObj);
            _logger.LogInformation("Source CarDTO object copied to destination CarDTO object.");

            _carRepo.Update(presentCarObj);
            _carRepo.Save();
            _logger.LogInformation("EditVehicle CarDTO model updation succesful.");
            return new ServiceResponse { hasSuccess = true };
        }

        public ServiceResponse DeleteVehicle(Guid id)
        {
            CarDTO presentCarObj = _carRepo.GetById(id);
            if (presentCarObj == null)
            {
                _logger.LogInformation("Car record not found for provided Id.");
                return new ServiceResponse { hasSuccess = false, errors = new List<string> { "Car record not found for provided Id." } };
            }

            _carRepo.Delete(id);
            _carRepo.Save();
            _logger.LogInformation("DeleteVehicle CarDTO model deletion succesful.");
            return new ServiceResponse { hasSuccess = true };
        }

        public List<JObject> GetSearchedVehicle(string email)
        {
            IEnumerable<CarDTO> res = _carRepo.Find(x => x.email.Contains(email));
            _logger.LogInformation("Car records fetched based on email.");
            List<JObject> searchedVeh = new List<JObject>();
            foreach (CarDTO carDto in res)
            {
                Car car = ModelMapper.mapObjects<Car>(carDto);
                searchedVeh.Add(JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(car)));
            }
            _logger.LogInformation("Car records converted to List<JObject>.");
            return searchedVeh;
        }
    }
}
