
using FluentValidation;

namespace TripWise.Application.Features.Trips.Commands.CreateTrip;

public class CreateTripCommandValidator:AbstractValidator<CreateTripCommand>
{
    public CreateTripCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Seyahat adı zorunludur.").MinimumLength(3).WithMessage("Seyahat adı en az 3 karakter olmalıdır.").MaximumLength(150).WithMessage("Seyahat adı en fazla 150 karakter olabilir.");

        RuleFor(x => x.Description).MaximumLength(1000).WithMessage("Açıklama en fazla 1000 karakter olabilir.");

        RuleFor(x => x.StartDate).Must(date => date != default).WithMessage("Başlangıç tarihi zorunludur.");

        RuleFor(x => x.EndDate).Must(date => date != default).WithMessage("Bitiş tarihi zorunludur.");

        RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate).WithMessage( "Bitiş tarihi başlangıç tarihinden önce olamaz.");
    }
}
