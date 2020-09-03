using FluentValidation;
using System;

namespace HotelApp.API.Models
{
    public class ReservationDTOValidator 
        : AbstractValidator<ReservationDTO>
    {
        public ReservationDTOValidator()
        {
            RuleFor(x => x.DateFrom).NotEmpty()
                .WithMessage("The start date of the reservation is required.");

            RuleFor(x => x.DateTo).NotEmpty()
                .WithMessage("The end date of the reservation is required.");

            RuleFor(x => x.DateFrom).LessThan(x => x.DateTo)
                .WithMessage("The reservation start date has to be earlier than the end date.");

            RuleFor(x => x.DateTo).GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("The reservation end date must be greater than or equal to current time.");

            RuleFor(x => x.DateFrom).GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("The reservation start date must be greater than or equal to current time.");
        }
    }
}