using FluentValidation;
using System;
using System.Collections.Generic;

namespace Carsales.Models.Validators
{
    public class CarValidator: AbstractValidator<CarDTO>
    {
        List<string> _advertisedPriceType = new List<string>() { "DAP", "EGC" };

        public CarValidator()
        {
            RuleFor(x => x.year).GreaterThan(1950).WithMessage("Car manufacturing year should be greater than 1950.").LessThanOrEqualTo(DateTime.Now.Year).WithMessage("Car manufacturing year should be smaller or equal to current year.");
            RuleFor(x => x.make).NotNull().WithMessage("Please provide name of car manufacturer.").Length(1, 50).WithMessage("Make must be between 1 to 50 character.");
            RuleFor(x => x.model).NotNull().WithMessage("Please provide model of car.").Length(1, 50).WithMessage("Car model must be between 1 to 50 character.");
            RuleFor(x => x.comments).NotNull().WithMessage("Please provide comments for car.").Length(10, 500).WithMessage("Comments for car must be between 10 to 500 character.");
            RuleFor(x => x.advertisedPriceType).Must(x => _advertisedPriceType.Contains(x)).WithMessage("Advertised Price Type should be one of: " + String.Join(",", _advertisedPriceType) + ".");
            RuleFor(x => x.driveAwayPrice).NotEmpty().When(x => x.advertisedPriceType == "DAP").WithMessage("Please provide drive away price for car.");
            RuleFor(x => x.excludingGovernmentCharges).NotEmpty().When(x => x.advertisedPriceType == "EGC").WithMessage("Please provide excluding government charges for car.");
            RuleFor(x => x.name).NotNull().WithMessage("Please provide name for contact.").Length(1, 50).When(x => !x.isDealer).WithMessage("Name must be between 1 to 50 character.");
            RuleFor(x => x.name).Empty().When(x => x.isDealer).WithMessage("Name should be empty when car is a dealer vehicle.");
            RuleFor(x => x.phone).NotNull().WithMessage("Please provide phone number for contact.").Matches(@"^((0|91)?[7-9][0-9]{9})$").When(x => !x.isDealer).WithMessage("Please provide a valid phone number for contact.");
            RuleFor(x => x.phone).Empty().When(x => x.isDealer).WithMessage("Phone number should be empty when car is a dealer vehicle.");
            RuleFor(x => x.email).NotNull().WithMessage("Please provide email.").EmailAddress().WithMessage("Please provide valid email for contact.");
            RuleFor(x => x.dealerABN).NotNull().WithMessage("Please provide dealer ABN.").Length(1, 50).When(x => x.isDealer).WithMessage("Dealer ABN must be between 1 to 50 character.");
            RuleFor(x => x.dealerABN).Empty().When(x => !x.isDealer).WithMessage("Dealer ABN should be empty when car is private.");
        }
    }
}
