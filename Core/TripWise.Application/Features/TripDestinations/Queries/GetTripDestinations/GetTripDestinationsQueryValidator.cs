using FluentValidation;

namespace TripWise.Application.Features.TripDestinations.Queries.GetTripDestinations;

public sealed class GetTripDestinationsQueryValidator
    : AbstractValidator<GetTripDestinationsQuery>
{
    public GetTripDestinationsQueryValidator()
    {
        RuleFor(x => x.TripId)
            .NotEmpty()
            .WithMessage("Seyahat ID bilgisi boş olamaz.");
    }
}