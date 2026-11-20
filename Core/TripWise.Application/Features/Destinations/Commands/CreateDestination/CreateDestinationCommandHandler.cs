using MediatR;
using TripWise.Application.Common.Results;
using TripWise.Application.Contracts.Persistence;
using TripWise.Domain.Entities;

namespace TripWise.Application.Features.Destinations.Commands.CreateDestination;

public sealed class CreateDestinationCommandHandler( IDestinationRepository destinationRepository): IRequestHandler<CreateDestinationCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateDestinationCommand request,CancellationToken cancellationToken)
    {
        var destination = new Destination
        {
            CityName = request.CityName.Trim(),
            CountryName = request.CountryName.Trim(),
            CountryCode = request.CountryCode.Trim().ToUpperInvariant()
        };

        await destinationRepository.AddAsync(destination,cancellationToken);
        return Result<Guid>.Success(destination.Id);
    }
}