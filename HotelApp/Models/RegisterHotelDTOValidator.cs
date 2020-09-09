   using FluentValidation;
using HotelApp.API.DbContexts.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HotelApp.API.Models
{
    public class RegisterHotelDTOValidator
        : AbstractValidator<RegisterHotelDTO>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IHotelStatusRepository _hotelStatusRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RegisterHotelDTOValidator(IHotelRepository hotelRepository,
                                 IHotelStatusRepository hotelStatusRepository,
                                 IHttpContextAccessor httpContextAccessor)
        {
            _hotelRepository = hotelRepository;
            _hotelStatusRepository = hotelStatusRepository;
            _httpContextAccessor = httpContextAccessor;

            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Hotel name is required.");

            RuleFor(x => x.Name).MinimumLength(2)
                .WithMessage("Username must have at least 2 characters.");

            RuleFor(x => x.Name).Must(DuplicateHotelName)
                .WithMessage("A hotel with that name already exists.");

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

            RuleFor(x => x.StatusId).Must(StatusIdValidation)
                .WithMessage("Hotel status does not exist.");
        }

        private bool StatusIdValidation(int? id)
        {
            if (id.HasValue)
            {
                var statusIdExists = _hotelStatusRepository.GetHotelStatusById((int)id);
                if(statusIdExists != null)
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private bool DuplicateHotelName(string name)
        {
            var isMethodPost = HttpMethods.IsPost(_httpContextAccessor.HttpContext.Request.Method);
            int id = -1;
            if (!isMethodPost) 
            {
                id = Int32.Parse(_httpContextAccessor.HttpContext.GetRouteValue("id").ToString());
            }
            if (_hotelRepository.GetAllHotelsForUser().Any(x => x.Name != name &&
               (isMethodPost == true || x.Id != id)))
            {
                return false;
            }
            return true;
        }
    }
}
