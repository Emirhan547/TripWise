
using TripWise.Domain.Abstractions;

namespace TripWise.Domain.Entities;

public sealed class Destination : Entity
{
    public string CityName { get; set; } =default!;

    public string CountryName { get; set; } = default!;

    public string CountryCode { get; set; } = default!;
}
