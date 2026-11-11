using TripWise.Domain.Abstractions;

namespace TripWise.Domain.Entities;

public sealed class TripDestination : Entity
{
    public Guid TripId { get; set; }
    public Guid DestinationId { get; set; }
    public int VisitOrder { get; set; }
    public DateOnly ArrivalDate { get; set; }
    public DateOnly DepartureDate { get; set; }
}
