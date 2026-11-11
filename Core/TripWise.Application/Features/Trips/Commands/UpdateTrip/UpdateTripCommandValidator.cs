using FluentValidation;

namespace TripWise.Application.Features.Trips.Commands.UpdateTrip;

public sealed class UpdateTripCommandValidator: AbstractValidator<UpdateTripCommand>
{
    public UpdateTripCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Seyahat ID zorunludur.");

        RuleFor(x => x.Name).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Seyahat adı zorunludur.").MinimumLength(3).WithMessage("Seyahat adı en az 3 karakter olmalıdır.").MaximumLength(150).WithMessage("Seyahat adı en fazla 150 karakter olabilir.");

        RuleFor(x => x.Description).MaximumLength(1000).WithMessage("Açıklama en fazla 1000 karakter olabilir.");

        RuleFor(x => x.StartDate).NotEmpty().WithMessage("Başlangıç tarihi zorunludur.");

        RuleFor(x => x.EndDate).NotEmpty().WithMessage("Bitiş tarihi zorunludur.");

        RuleFor(x => x) .Must(x => x.EndDate >= x.StartDate).WithMessage("Bitiş tarihi başlangıç tarihinden önce olamaz.").When(x => x.StartDate != default && x.EndDate != default);
    }
}
