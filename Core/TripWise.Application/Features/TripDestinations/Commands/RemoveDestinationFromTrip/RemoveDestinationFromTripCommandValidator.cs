using FluentValidation;

namespace TripWise.Application.Features.TripDestinations.Commands.RemoveDestinationFromTrip;

public sealed class RemoveDestinationFromTripCommandValidator
    : AbstractValidator<RemoveDestinationFromTripCommand>
{
    public RemoveDestinationFromTripCommandValidator()
    {
        RuleFor(x => x.TripId)
            .NotEmpty();

        RuleFor(x => x.DestinationId)
            .NotEmpty();
    }
}