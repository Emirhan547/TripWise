using MediatR;
using TripWise.Application.Common.Results;
using TripWise.Application.Contracts.Persistence;

namespace TripWise.Application.Features.Destinations.Commands.DeleteDestination;

public sealed class DeleteDestinationCommandHandler(IDestinationRepository destinationRepository): IRequestHandler<DeleteDestinationCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DeleteDestinationCommand request,CancellationToken cancellationToken)
    {
        var destination = await destinationRepository.GetByIdAsync(request.Id,cancellationToken,tracking: true);

        if (destination is null)
        {
            return Result<Guid>.NotFound("Destinasyon bulunamadı.");
        }

        destination.Delete();

        await destinationRepository.DeleteAsync(destination,cancellationToken);
        return Result<Guid>.Success(destination.Id);
    }
}