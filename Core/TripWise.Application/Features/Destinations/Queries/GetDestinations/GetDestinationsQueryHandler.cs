using MediatR;
using TripWise.Application.Common.Models;
using TripWise.Application.Common.Results;
using TripWise.Application.Contracts.Persistence;

namespace TripWise.Application.Features.Destinations.Queries.GetDestinations;

public sealed class GetDestinationsQueryHandler(IDestinationRepository destinationRepository): IRequestHandler<GetDestinationsQuery,Result<PagedResult<DestinationListItemDto>>>
{
    public async Task<Result<PagedResult<DestinationListItemDto>>> Handle(GetDestinationsQuery request,CancellationToken cancellationToken)
    {
        var destinations = await destinationRepository.GetAllAsync(request.PageNumber,request.PageSize, cancellationToken);

        var totalCount = await destinationRepository.GetTotalCountAsync(cancellationToken);

        var items = destinations.Select(x => new DestinationListItemDto(x.Id,x.CityName,x.CountryName, x.CountryCode, x.CreatedAt)).ToList();

        var pagedResult = new PagedResult<DestinationListItemDto>(items,request.PageNumber,request.PageSize,totalCount);

        return Result<PagedResult<DestinationListItemDto>>.Success(pagedResult);
    }
}