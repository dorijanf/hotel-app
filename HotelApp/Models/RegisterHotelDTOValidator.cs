using FluentValidation;
using HotelApp.API.DbContexts.Repositories;

namespace HotelApp.API.Models
{
    public class RegisterHotelDTOValidator
        :AbstractValidator<RegisterHotelDTO>
    {
        private readonly IHotelRepository _hotelRepository;
        public RegisterHotelDTOValidator(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;

            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Hotel name is required.");
            RuleFor(x => x.Name).MinimumLength(2)
                .WithMessage("Username must have at least 2 characters.");
            RuleFor(x => x.Name).Must(DuplicateHotelName)
                .WithMessage("A hotel with that name already exists");

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

        private bool DuplicateHotelName(string name)
        {
            var hotelExists = _hotelRepository.GetHotelByName(name);
            if (hotelExists != null)
            {
                return false;
            }
            return true;
        }
    }
}
