using FluentValidation;

namespace TripWise.Application.Features.Destinations.Queries.GetDestinationById;

public sealed class GetDestinationByIdQueryValidator: AbstractValidator<GetDestinationByIdQuery>
{
    public GetDestinationByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Destinasyon ID bilgisi boş olamaz.");
    }
}