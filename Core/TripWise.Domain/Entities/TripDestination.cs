using TripWise.Domain.Abstractions;

namespace TripWise.Domain.Entities;

public sealed class TripDestination : Entity
{
    public Guid TripId { get; set; }

    public Trip Trip { get; set; } = default!;

    public Guid DestinationId { get; set; }

    public Destination Destination { get; set; } = default!;

    public int VisitOrder { get; set; }

    public DateOnly ArrivalDate { get; set; }

    public DateOnly DepartureDate { get; set; }
}