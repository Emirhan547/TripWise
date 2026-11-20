using FluentValidation;


namespace TripWise.Application.Features.Destinations.Queries.GetDestinations;
public sealed class GetDestinationsQueryValidator: AbstractValidator<GetDestinationsQuery>
{
    public GetDestinationsQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0).LessThanOrEqualTo(100);
    }
}
