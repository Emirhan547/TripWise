using MediatR;
using TripWise.Application.Common.Models;
using TripWise.Application.Features.Destinations.Commands.CreateDestination;
using TripWise.Application.Features.Destinations.Commands.DeleteDestination;
using TripWise.Application.Features.Destinations.Commands.UpdateDestination;
using TripWise.Application.Features.Destinations.Queries.GetDestinationById;
using TripWise.Application.Features.Destinations.Queries.GetDestinations;

namespace TripWise.WebAPI.Endpoints;

public static class DestinationEndpoints
{
    public static IEndpointRouteBuilder MapDestinationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/destinations").WithTags("Destinations");

        group.MapPost("/", async (CreateDestinationCommand command,ISender sender,CancellationToken cancellationToken) =>
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

            return Results.Json(new { Id = result.Data },statusCode: StatusCodes.Status201Created);
        })
        .RequireAuthorization()
        .WithName("CreateDestination")
        .WithSummary("Yeni destinasyon oluşturur.")
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .ProducesValidationProblem();
        // Get Destination By Id
        group.MapGet("/{id:guid}", async (Guid id,ISender sender,CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetDestinationByIdQuery(id),cancellationToken);

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
        .WithName("GetDestinationById")
        .WithSummary("ID üzerinden destinasyon detaylarını getirir.")
        .Produces<DestinationDetailsDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem();
        // Get Destinations
        group.MapGet("/", async ([AsParameters] GetDestinationsQuery query,ISender sender, CancellationToken cancellationToken) =>
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
        .WithName("GetDestinations")
        .WithSummary("Destinasyonları sayfalı olarak listeler.")
        .Produces<PagedResult<DestinationListItemDto>>(StatusCodes.Status200OK)
        .ProducesValidationProblem();
        // Update Destination
        group.MapPut("/{id:guid}", async (Guid id, UpdateDestinationCommand command,ISender sender,CancellationToken cancellationToken) =>
        {
            var updateCommand = command with { Id = id };

            var result = await sender.Send(updateCommand,cancellationToken);

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

                return Results.BadRequest(new
                {
                    result.ErrorCode,
                    result.Errors
                });
            }

            return Results.NoContent();
        })
        .RequireAuthorization()
        .WithName("UpdateDestination")
        .WithSummary("Mevcut destinasyonu günceller.")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem();
        // Delete Destination
        group.MapDelete("/{id:guid}", async (Guid id,ISender sender,CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new DeleteDestinationCommand(id),cancellationToken);

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

                return Results.BadRequest(new
                {
                    result.ErrorCode,
                    result.Errors
                });
            }

            return Results.NoContent();
        })
        .RequireAuthorization()
        .WithName("DeleteDestination")
        .WithSummary("Destinasyonu siler.")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem();
        return app;
    }
}