using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.Models
{
    public class LoginUserDTOValidator :
        AbstractValidator<LoginUserDTO>
    {
        public LoginUserDTOValidator()
        {
            RuleFor(x => x.UserName).NotEmpty()
                .WithMessage("Username is required");
            RuleFor(x => x.UserName).NotNull()
                .WithMessage("UserName is required");
            
            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("Password is required");
            RuleFor(x => x.Password).NotNull()
                .WithMessage("Password is required");
        }
    }
}
