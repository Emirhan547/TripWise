using MediatR;
using TripWise.Application.Common.Results;
using TripWise.Application.Contracts.Persistence;

namespace TripWise.Application.Features.Destinations.Queries.GetDestinationById;

public sealed class GetDestinationByIdQueryHandler(IDestinationRepository destinationRepository): IRequestHandler<GetDestinationByIdQuery, Result<DestinationDetailsDto>>
{
    public async Task<Result<DestinationDetailsDto>> Handle(GetDestinationByIdQuery request,CancellationToken cancellationToken)
    {
        var destination = await destinationRepository.GetByIdAsync(request.Id,cancellationToken);

        if (destination is null)
        {
            return Result<DestinationDetailsDto>.NotFound("Destinasyon bulunamadı.");
        }

        var destinationDto = new DestinationDetailsDto(destination.Id,destination.CityName,destination.CountryName,destination.CountryCode,destination.CreatedAt);

        return Result<DestinationDetailsDto>.Success(destinationDto);
    }
}