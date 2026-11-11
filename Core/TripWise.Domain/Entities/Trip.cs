using TripWise.Domain.Abstractions;
using TripWise.Domain.Enum;

namespace TripWise.Domain.Entities;

public sealed class Trip : Entity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public TripStatus Status { get; set; } = TripStatus.Planned;
}
