using FluentValidation;

namespace TripWise.Application.Features.Trips.Queries.GetTrips
{
    public sealed class GetTripsQueryValidator : AbstractValidator<GetTripsQuery>
    {
        public GetTripsQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("Sayfa numarası 0'dan büyük olmalıdır.");

            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("Sayfa başına kayıt sayısı 0'dan büyük olmalıdır.").LessThanOrEqualTo(100).WithMessage("Sayfa başına en fazla 100 kayıt getirilebilir.");
        }
    }
}
