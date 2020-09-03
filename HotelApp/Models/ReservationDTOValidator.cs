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

            //RuleFor(x => x.DateTo).LessThan(x => x.DateFrom)
            //    .WithMessage("The reservation end date has to be later than the start date.");

            //RuleFor(x => x.DateFrom).GreaterThan(x => x.DateTo)
            //    .WithMessage("The reservation start date has to be earlier than the end date.");
        }
    }
}