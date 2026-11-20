using FluentValidation;

namespace TripWise.Application.Features.Destinations.Commands.UpdateDestination;

public sealed class UpdateDestinationCommandValidator: AbstractValidator<UpdateDestinationCommand>
{
    public UpdateDestinationCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.CityName).Cascade(CascadeMode.Stop).NotEmpty().MaximumLength(100);

        RuleFor(x => x.CountryName).Cascade(CascadeMode.Stop).NotEmpty().MaximumLength(100);

        RuleFor(x => x.CountryCode).Cascade(CascadeMode.Stop).NotEmpty().Length(2).Matches("^[A-Za-z]{2}$").WithMessage("Ülke kodu iki harften oluşmalıdır.");
    }
}