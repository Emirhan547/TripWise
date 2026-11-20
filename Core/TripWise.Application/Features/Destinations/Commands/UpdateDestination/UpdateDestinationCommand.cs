using MediatR;
using TripWise.Application.Common.Results;

namespace TripWise.Application.Features.Destinations.Commands.UpdateDestination;

public sealed record UpdateDestinationCommand(Guid Id,string CityName, string CountryName,string CountryCode) : IRequest<Result<Guid>>;