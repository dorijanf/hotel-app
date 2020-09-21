using FluentValidation;

namespace HotelApp.API.Models
{
    public class AddRoomDTOValidator
        : AbstractValidator<AddRoomDTO>
    {
        public AddRoomDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Room name is required.");

            RuleFor(x => x.NumberOfBeds).GreaterThanOrEqualTo(1)
                .WithMessage("There must be at least one bed in the room.");

            RuleFor(x => x.Price).GreaterThanOrEqualTo(1)
                .WithMessage("The price of the room must be at least 1.");
        }
    }
}
