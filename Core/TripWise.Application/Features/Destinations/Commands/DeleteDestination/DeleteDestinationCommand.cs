
using MediatR;
using TripWise.Application.Common.Results;

namespace TripWise.Application.Features.Destinations.Commands.DeleteDestination;

public sealed record DeleteDestinationCommand(Guid Id): IRequest<Result<Guid>>;
