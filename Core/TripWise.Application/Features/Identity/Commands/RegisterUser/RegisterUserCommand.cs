using MediatR;
using TripWise.Application.Common.Results;

namespace TripWise.Application.Features.Identity.Commands.RegisterUser;

public sealed record RegisterUserCommand(string Email,string Password) : IRequest<Result<Guid>>;



