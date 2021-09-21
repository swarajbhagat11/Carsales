using Carsales.Core.HelperObjects;
using Carsales.Models;
using Carsales.Repositories.Interfaces;
using Carsales.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Carsales.Tests
{
    public class CarServiceTest
    {
        public Mock<ILogger<CarService>> mockLogger = new Mock<ILogger<CarService>>();
        public Mock<IGenericRepository<CarDTO>> mockCarRepo = new Mock<IGenericRepository<CarDTO>>();

        [Fact]
        public void Able_to_add_car()
        {
            CarService service = new CarService(mockLogger.Object, mockCarRepo.Object);
            JObject carObj = new JObject
            {
                ["year"] = 2021,
                ["make"] = "Honda",
                ["model"] = "City VX",
                ["comments"] = "good car for drive",
                ["advertisedPriceType"] = "DAP",
                ["price"] = 1299120,
                ["isDealer"] = false,
                ["name"] = "Test",
                ["phone"] = "8748399849",
                ["email"] = "test@gmail.com"
            };
            ServiceResponse res = service.AddVehicle(carObj);
            mockCarRepo.Verify(m => m.Insert(It.Is<CarDTO>(x => x.email == carObj.GetValue("email").ToString())), Times.Once);
            mockCarRepo.Verify(m => m.Save(), Times.Once);
            Assert.True(res.hasSuccess);
        }

        [Fact]
        public void Not_able_to_add_car_due_to_model_validation_error()
        {
            CarService service = new CarService(mockLogger.Object, mockCarRepo.Object);
            JObject carObj = new JObject
            {
                ["year"] = 1900,
                ["make"] = "Honda",
                ["model"] = "City VX",
                ["comments"] = "good car for drive",
                ["advertisedPriceType"] = "DAP",
                ["price"] = 1299120,
                ["isDealer"] = false,
                ["name"] = "Test",
                ["phone"] = "8748399849",
                ["email"] = "test@gmail.com"
            };
            ServiceResponse res = service.AddVehicle(carObj);
            Assert.False(res.hasSuccess);
        }

        [Fact]
        public void Not_able_to_add_car_due_to_car_record_existance()
        {
            CarService service = new CarService(mockLogger.Object, mockCarRepo.Object);
            JObject carJObj = new JObject
            {
                ["year"] = 2020,
                ["make"] = "Honda",
                ["model"] = "City VX",
                ["comments"] = "good car for drive",
                ["advertisedPriceType"] = "DAP",
                ["price"] = 1299120,
                ["isDealer"] = false,
                ["name"] = "Test",
                ["phone"] = "8748399849",
                ["email"] = "test@gmail.com"
            };
            CarDTO carObj = carJObj.ToObject<CarDTO>();
            mockCarRepo.Setup(e => e.Find(It.IsAny<Func<CarDTO, bool>>())).Returns(new List<CarDTO> { new CarDTO() });
            ServiceResponse res = service.AddVehicle(carJObj);
            Assert.False(res.hasSuccess);
        }

        [Fact]
        public void Able_to_update_car()
        {
            CarService service = new CarService(mockLogger.Object, mockCarRepo.Object);
            Guid id = Guid.NewGuid();
            JObject carObj = new JObject
            {
                ["year"] = 2021,
                ["make"] = "Honda",
                ["model"] = "City VX",
                ["comments"] = "good car for drive",
                ["advertisedPriceType"] = "DAP",
                ["price"] = 1299120,
                ["isDealer"] = false,
                ["name"] = "Test",
                ["phone"] = "8748399849",
                ["email"] = "test@gmail.com"
            };
            mockCarRepo.Setup(e => e.GetById(id)).Returns(new CarDTO());
            ServiceResponse res = service.EditVehicle(id, carObj);
            mockCarRepo.Verify(m => m.Update(It.Is<CarDTO>(x => x.email == carObj.GetValue("email").ToString())), Times.Once);
            mockCarRepo.Verify(m => m.Save(), Times.Once);
            Assert.True(res.hasSuccess);
        }

        [Fact]
        public void Not_able_to_update_car_due_to_car_record_not_exists()
        {
            CarService service = new CarService(mockLogger.Object, mockCarRepo.Object);
            JObject carObj = new JObject
            {
                ["year"] = 2021,
                ["make"] = "Honda",
                ["model"] = "City VX",
                ["comments"] = "good car for drive",
                ["advertisedPriceType"] = "DAP",
                ["price"] = 1299120,
                ["isDealer"] = false,
                ["name"] = "Test",
                ["phone"] = "8748399849",
                ["email"] = "test@gmail.com"
            };
            ServiceResponse res = service.EditVehicle(Guid.NewGuid(), carObj);
            Assert.False(res.hasSuccess);
        }

        [Fact]
        public void Not_able_to_update_car_due_to_model_validation_error()
        {
            CarService service = new CarService(mockLogger.Object, mockCarRepo.Object);
            JObject carObj = new JObject
            {
                ["id"] = Guid.NewGuid(),
                ["year"] = 2021,
                ["make"] = "",
                ["model"] = "City VX",
                ["comments"] = "good car for drive",
                ["advertisedPriceType"] = "DAP",
                ["price"] = 1299120,
                ["isDealer"] = false,
                ["name"] = "Test",
                ["phone"] = "8748399849",
                ["email"] = "test@gmail.com"
            };
            ServiceResponse res = service.EditVehicle(Guid.NewGuid(),carObj);
            Assert.False(res.hasSuccess);
        }

        [Fact]
        public void Able_to_delete_car()
        {
            CarService service = new CarService(mockLogger.Object, mockCarRepo.Object);
            Guid id = Guid.NewGuid();
            mockCarRepo.Setup(e => e.GetById(id)).Returns(new CarDTO());
            ServiceResponse res = service.DeleteVehicle(id);
            mockCarRepo.Verify(m => m.Delete(id), Times.Once);
            mockCarRepo.Verify(m => m.Save(), Times.Once);
            Assert.True(res.hasSuccess);
        }

        [Fact]
        public void Not_able_to_delete_due_to_record_not_exists()
        {
            CarService service = new CarService(mockLogger.Object, mockCarRepo.Object);
            ServiceResponse res = service.DeleteVehicle(Guid.NewGuid());
            Assert.False(res.hasSuccess);
        }

        [Fact]
        public void Get_searched_car_by_email()
        {
            CarService service = new CarService(mockLogger.Object, mockCarRepo.Object);
            string email = "test";
            mockCarRepo.Setup(e => e.Find(It.IsAny<Func<CarDTO, bool>>())).Returns(new List<CarDTO> { new CarDTO(), new CarDTO() });
            List<JObject> res = service.GetSearchedVehicle(email);
            Assert.Equal(2, res.Count);
        }
    }
}
