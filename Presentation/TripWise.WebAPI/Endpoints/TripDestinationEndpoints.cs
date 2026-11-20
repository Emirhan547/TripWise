using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using TripWise.Application.Features.TripDestinations.Commands.AddDestinationToTrip;
using TripWise.Application.Features.TripDestinations.Commands.RemoveDestinationFromTrip;
using TripWise.Application.Features.TripDestinations.Queries.GetTripDestinations;

namespace TripWise.WebAPI.Endpoints;

public static class TripDestinationEndpoints
{
    public static IEndpointRouteBuilder MapTripDestinationEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/trips")
            .WithTags("Trip Destinations")
            .RequireAuthorization();

        group.MapPost("/{tripId:guid}/destinations", async (
            Guid tripId,
            AddDestinationToTripCommand command,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var addCommand = command with
            {
                TripId = tripId
            };

            var result = await sender.Send(
                addCommand,
                cancellationToken);

            if (result.IsFailure)
            {
                if (result.ErrorCode == "NotFound")
                {
                    return Results.NotFound(new
                    {
                        result.ErrorCode,
                        result.Errors
                    });
                }

                if (result.ErrorCode == "TripDestination.AlreadyExists")
                {
                    return Results.Conflict(new
                    {
                        result.ErrorCode,
                        result.Errors
                    });
                }

                return Results.BadRequest(new
                {
                    result.ErrorCode,
                    result.Errors
                });
            }

            return Results.Json(
                new { Id = result.Data },
                statusCode: StatusCodes.Status201Created);
        })
        .WithName("AddDestinationToTrip")
        .WithSummary("Bir seyahate destinasyon ekler.")
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status409Conflict)
        .ProducesValidationProblem();
        // Get Trip Destinations
        group.MapGet("/{tripId:guid}/destinations", async (
            Guid tripId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new GetTripDestinationsQuery(tripId),
                cancellationToken);

            if (result.IsFailure)
            {
                return Results.NotFound(new
                {
                    result.ErrorCode,
                    result.Errors
                });
            }

            return Results.Ok(result.Data);
        })
        .WithName("GetTripDestinations")
        .WithSummary("Bir seyahate ait destinasyonları ziyaret sırasına göre listeler.")
        .Produces<List<TripDestinationListItemDto>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem();
        // Remove Destination From Trip
        group.MapDelete("/{tripId:guid}/destinations/{destinationId:guid}", async (
            Guid tripId,
            Guid destinationId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new RemoveDestinationFromTripCommand(tripId, destinationId),
                cancellationToken);

            if (result.IsFailure)
            {
                return Results.NotFound(new
                {
                    result.ErrorCode,
                    result.Errors
                });
            }

            return Results.NoContent();
        })
        .WithName("RemoveDestinationFromTrip")
        .WithSummary("Seyahatten destinasyon kaldırır.")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem();
        return app;
    }
}