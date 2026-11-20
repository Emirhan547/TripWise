using FluentValidation;

namespace TripWise.Application.Features.TripDestinations.Commands.AddDestinationToTrip;

public sealed class AddDestinationToTripCommandValidator
    : AbstractValidator<AddDestinationToTripCommand>
{
    public AddDestinationToTripCommandValidator()
    {
        RuleFor(x => x.TripId)
            .NotEmpty();

        RuleFor(x => x.DestinationId)
            .NotEmpty();

        RuleFor(x => x.ArrivalDate)
            .NotEqual(default(DateOnly));

        RuleFor(x => x.DepartureDate)
            .NotEqual(default(DateOnly))
            .GreaterThanOrEqualTo(x => x.ArrivalDate)
            .WithMessage("Ayrılış tarihi varış tarihinden önce olamaz.");
    }
}