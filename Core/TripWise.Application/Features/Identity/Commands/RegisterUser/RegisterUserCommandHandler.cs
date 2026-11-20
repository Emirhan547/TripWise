using MediatR;
using TripWise.Application.Abstractions.Identity;
using TripWise.Application.Common.Results;

namespace TripWise.Application.Features.Identity.Commands.RegisterUser
{
    public sealed class RegisterUserCommandHandler(IIdentityService identityService) : IRequestHandler<RegisterUserCommand, Result<Guid>>
    {
        public Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return identityService.RegisterAsync(request.Email,request.Password);
        }
    }
}
