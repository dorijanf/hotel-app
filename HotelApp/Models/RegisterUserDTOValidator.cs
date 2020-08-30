using FluentValidation;
using System.Linq;

namespace HotelApp.API.Models
{
    public class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserDTOValidator()
        {
            // Username
            RuleFor(x => x.UserName).NotNull()
                .WithMessage("Username is required.");
            RuleFor(x => x.UserName).MinimumLength(5)
                .WithMessage("Username must have at least 5 characters.");
            RuleFor(x => x.UserName).MaximumLength(50)
                .WithMessage("Username can have a maximum of 50 characters.");

            // Email
            RuleFor(x => x.Email).EmailAddress()
                .WithMessage("Email must be in a correct format.");
            RuleFor(x => x.Email).NotNull()
                .WithMessage("Email is required.");

            // Password
            RuleFor(x => x.Password).NotNull()
                .WithMessage("Password is required.");
            RuleFor(x => x.Password).MinimumLength(10)
                .WithMessage("Password must have a minimum of 10 characters.");
            RuleFor(x => x.Password).Must(ContainLowercase)
                .WithMessage("Password must contain at least one lowercase letter.");
            RuleFor(x => x.Password).Must(ContainUppercase)
                .WithMessage("Password must contain at least one uppercase letter.");
            RuleFor(x => x.Password).Must(ContainNumber)
                .WithMessage("Password must contain at least one number.");
            RuleFor(x => x.Password).Must(ContainSpecial)
                .WithMessage("Password must contain at least one special character.");

        }

        private bool ContainLowercase(string password)
        {
            return password.Any(ch => char.IsLower(ch));
        }

        private bool ContainUppercase(string password)
        {
            return password.Any(ch => char.IsUpper(ch));
        }

        private bool ContainNumber(string password)
        {
            return password.Any(ch => char.IsDigit(ch));
        }

        private bool ContainSpecial(string password)
        {
            return password.Any(ch => !char.IsLetterOrDigit(ch));
        }
    }
}
