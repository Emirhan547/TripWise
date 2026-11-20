using MediatR;
using TripWise.Application.Common.Models;
using TripWise.Application.Common.Results;

namespace TripWise.Application.Features.Identity.Commands.LoginUser
{
    public sealed record LoginUserCommand(
     string Email,
     string Password
 ) : IRequest<Result<AccessTokenResponse>>;

}

