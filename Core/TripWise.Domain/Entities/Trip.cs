using TripWise.Domain.Abstractions;
using TripWise.Domain.Enum;

namespace TripWise.Domain.Entities;

public sealed class Trip : Entity
{
   
    public Guid? UserId { get; private set; }

    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public TripStatus Status { get; set; } = TripStatus.Planned;
    public ICollection<TripDestination> TripDestinations { get; private set; }= new List<TripDestination>();
    public Trip(Guid userId)
{
    if (userId == Guid.Empty)
        throw new ArgumentException("UserId boş olamaz.", nameof(userId));

    UserId = userId;
}
    private Trip()
    {
    }
}
