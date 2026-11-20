using MediatR;
using TripWise.Application.Common.Results;
using TripWise.Application.Contracts.Persistence;

namespace TripWise.Application.Features.Destinations.Commands.UpdateDestination;

public sealed class UpdateDestinationCommandHandler(IDestinationRepository destinationRepository): IRequestHandler<UpdateDestinationCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateDestinationCommand request,CancellationToken cancellationToken)
    {
        var destination = await destinationRepository.GetByIdAsync( request.Id, cancellationToken,tracking: true);

        if (destination is null)
        {
            return Result<Guid>.NotFound("Destinasyon bulunamadı.");
        }

        destination.CityName = request.CityName.Trim();
        destination.CountryName = request.CountryName.Trim();
        destination.CountryCode = request.CountryCode.Trim().ToUpperInvariant();

        await destinationRepository.UpdateAsync(destination,cancellationToken);

        return Result<Guid>.Success(destination.Id);
    }
}