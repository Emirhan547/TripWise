using MediatR;
using TripWise.Application.Common.Results;

namespace TripWise.Application.Features.Destinations.Commands.CreateDestination;

public sealed record CreateDestinationCommand(
 string CityName,
 string CountryName,
 string CountryCode
) : IRequest<Result<Guid>>;
