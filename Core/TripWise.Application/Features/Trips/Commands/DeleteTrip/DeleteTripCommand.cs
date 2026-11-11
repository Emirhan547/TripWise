using MediatR;

using TripWise.Application.Common.Results;

      namespace TripWise.Application.Features.Trips.Commands.DeleteTrip;

    public sealed record DeleteTripCommand(Guid Id): IRequest<Result<Guid>>;
