using FluentValidation;
using FluentValidation.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.Models
{
    public class RegisterHotelDTOValidator
        :AbstractValidator<RegisterHotelDTO>
    {
        public RegisterHotelDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Hotel name is required.");

            RuleFor(x => x.ContactNumber).NotEmpty()
                .WithMessage("Contact number is required.");

            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("Email is required.");
            RuleFor(x => x.Email).EmailAddress()
                .WithMessage("Email must be in a correct format.");

            RuleFor(x => x.Address).NotEmpty()
                .WithMessage("Hotel address is required.");

            RuleFor(x => x.City).NotEmpty()
                .WithMessage("Specifying the city where the hotel is located is required.");
        }
    }
}
