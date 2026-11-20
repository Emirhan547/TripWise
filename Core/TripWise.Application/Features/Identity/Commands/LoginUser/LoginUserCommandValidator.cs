using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripWise.Application.Features.Identity.Commands.LoginUser
{
    public sealed class LoginUserCommandValidator
     : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotEmpty().EmailAddress();

            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
