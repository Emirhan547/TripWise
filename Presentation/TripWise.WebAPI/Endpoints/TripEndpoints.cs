using MediatR;
using TripWise.Application.Features.Trips.Commands.CreateTrip;
using TripWise.Application.Features.Trips.Commands.DeleteTrip;
using TripWise.Application.Features.Trips.Commands.UpdateTrip;
using TripWise.Application.Features.Trips.Queries.GetTripById;
using TripWise.Application.Features.Trips.Queries.GetTrips;

namespace TripWise.WebAPI.Endpoints;

public static class TripEndpoints
{
    public static IEndpointRouteBuilder MapTripEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/trips").WithTags("Trips").RequireAuthorization();
        group.MapPost("/", async (CreateTripCommand command,ISender sender,CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return Results.BadRequest(new
                {
                    result.ErrorCode,
                    result.Errors
                });
            }

            return Results.Created($"/api/trips/{result.Data}",new { Id = result.Data });
        })
        .WithName("CreateTrip")
        .WithSummary("Yeni seyahat oluşturur.")
        .Produces(StatusCodes.Status201Created)
        .ProducesValidationProblem();

        group.MapGet("/{id:guid}", async (Guid id,ISender sender,CancellationToken cancellationToken) =>
        {
           
            var result = await sender.Send(new GetTripByIdQuery(id), cancellationToken);
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
        .WithName("GetTripById")
        .WithSummary("ID üzerinden seyahat detaylarını getirir.")
        .Produces<TripDetailsDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);


       


        group.MapPut("/{id:guid}", async (Guid id,UpdateTripCommand command,ISender sender,CancellationToken cancellationToken) =>
        {
            var updateCommand = command with { Id = id };
            var result = await sender.Send(updateCommand, cancellationToken);

            if (result.IsFailure)
            {
                return Results.BadRequest(new
                {
                    result.ErrorCode,
                    result.Errors
                });
            }
            return Results.NoContent();
        })
        .WithName("UpdateTrip")
        .WithSummary("Mevcut seyahati günceller.")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest)
        .ProducesValidationProblem();

        group.MapDelete("/{id:guid}", async (Guid id,ISender sender,CancellationToken cancellationToken) =>
        {
       
            var result = await sender.Send(new DeleteTripCommand(id), cancellationToken);

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
        .WithName("DeleteTrip")
        .WithSummary("Seyahati siler.")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/", async (
            [AsParameters] GetTripsQuery query,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return Results.BadRequest(new
                {
                    result.ErrorCode,
                    result.Errors
                });
            }

            return Results.Ok(result.Data);
        })
        .WithName("GetTrips")
        .WithSummary("Seyahatleri sayfalı olarak listeler.")
        .Produces(StatusCodes.Status200OK)
        .ProducesValidationProblem();
        return app;

    }

}