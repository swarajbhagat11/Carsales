using Carsales.Core.Mappers;
using Carsales.Models;
using Carsales.Models.Validators;
using Carsales.ViewModels;
using FluentValidation.Results;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Carsales.Tests
{
    public class CarDTOValidatorTest
    {
        public static readonly object[][] yearGreaterThatCurrentYear =
        {
            new object[] { "set", "year", DateTime.Now.Year + 1, "Car manufacturing year should be smaller or equal to current year." }
        };

        [Theory]
        [InlineData("delete", "year", "", "Car manufacturing year should be greater than 1950.")]
        [InlineData("set", "year", 1949, "Car manufacturing year should be greater than 1950.")]
        [MemberData(nameof(yearGreaterThatCurrentYear))]
        [InlineData("delete", "make", "", "Please provide name of car manufacturer.")]
        [InlineData("set", "make", "", "Make must be between 1 to 50 character.")]
        [InlineData("delete", "model", "", "Please provide model of car.")]
        [InlineData("set", "model", "", "Car model must be between 1 to 50 character.")]
        [InlineData("delete", "comments", "", "Please provide comments for car.")]
        [InlineData("set", "comments", "", "Comments for car must be between 10 to 500 character.")]
        [InlineData("set", "advertisedPriceType", "", "Advertised Price Type should be one of: DAP,EGC.")]
        [InlineData("set", "advertisedPriceType", "abc", "Advertised Price Type should be one of: DAP,EGC.")]
        [InlineData("delete", "email", "", "Please provide email.")]
        [InlineData("set", "email", "", "Please provide valid email for contact.")]
        [InlineData("set", "email", "test@", "Please provide valid email for contact.")]
        public void CarDTO_model_validations(string mode, string prop, object value, string error)
        {
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

            Assert.True(validateCarDto(carJObj, mode, prop, value).Contains(error));
        }

        [Theory]
        [InlineData("DAP", "set", "price", 0, "Please provide drive away price for car.")]
        [InlineData("EGC", "set", "price", 0, "Please provide excluding government charges for car.")]
        public void CarDTO_model_validations_of_price_based_on_advertisedPriceType(string advertisedPriceType, string mode, string prop, object value, string error)
        {
            JObject carJObj = new JObject
            {
                ["year"] = 2020,
                ["make"] = "Honda",
                ["model"] = "City VX",
                ["comments"] = "good car for drive",
                ["advertisedPriceType"] = advertisedPriceType,
                ["price"] = 1299120,
                ["isDealer"] = false,
                ["name"] = "Test",
                ["phone"] = "8748399849",
                ["email"] = "test@gmail.com"
            };

            Assert.True(validateCarDto(carJObj, mode, prop, value).Contains(error));
        }

        [Theory]
        [InlineData(false, "delete", "name", "", "Please provide name for contact.")]
        [InlineData(false, "set", "name", "", "Name must be between 1 to 50 character.")]
        [InlineData(true, "set", "name", "Test", "Name should be empty when car is a dealer vehicle.")]
        [InlineData(false, "delete", "phone", "", "Please provide phone number for contact.")]
        [InlineData(false, "set", "phone", "", "Please provide a valid phone number for contact.")]
        [InlineData(false, "set", "phone", "897384789", "Please provide a valid phone number for contact.")]
        [InlineData(true, "set", "phone", "8973847898", "Phone number should be empty when car is a dealer vehicle.")]
        [InlineData(true, "delete", "dealerABN", "", "Please provide dealer ABN.")]
        [InlineData(true, "set", "dealerABN", "", "Dealer ABN must be between 1 to 50 character.")]
        [InlineData(false, "set", "dealerABN", "ABC8989", "Dealer ABN should be empty when car is private.")]
        public void CarDTO_model_validations_based_on_isDealer(bool isDealer, string mode, string prop, object value, string error)
        {
            JObject carJObj = new JObject
            {
                ["year"] = 2020,
                ["make"] = "Honda",
                ["model"] = "City VX",
                ["comments"] = "good car for drive",
                ["advertisedPriceType"] = "DAP",
                ["price"] = 1299120,
                ["isDealer"] = isDealer,
                ["name"] = "Test",
                ["phone"] = "8748399849",
                ["email"] = "test@gmail.com"
            };

            Assert.True(validateCarDto(carJObj, mode, prop, value).Contains(error));
        }

        private List<string> validateCarDto(JObject carJObj, string mode, string prop, object value)
        {
            switch (mode)
            {
                case "delete":
                    carJObj.Remove(prop);
                    break;
                default:
                    carJObj[prop] = JToken.FromObject(value);
                    break;
            }

            CarDTO carObj = ModelMapper.mapObjects<CarDTO>(carJObj.ToObject<Car>());
            var validator = new CarValidator();
            ValidationResult results = validator.Validate(carObj);

            return results.Errors.Select(x => x.ErrorMessage).ToList();
        }
    }
}
