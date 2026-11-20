using MediatR;
using System.Security.Claims;
using TripWise.Application.Common.Models;
using TripWise.Application.Features.Identity.Commands.LoginUser;
using TripWise.Application.Features.Identity.Commands.RegisterUser;

namespace TripWise.WebAPI.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth")
            .WithTags("Authentication");

        // Register
        group.MapPost("/register", async (
            RegisterUserCommand command,
            ISender sender,
            CancellationToken cancellationToken) =>
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

            return Results.Json(
                new { Id = result.Data },
                statusCode: StatusCodes.Status201Created);
        })
        .AllowAnonymous()
        .WithName("Register")
        .WithSummary("Yeni kullanıcı kaydı oluşturur.")
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .ProducesValidationProblem();

        // Login
        group.MapPost("/login", async (
            LoginUserCommand command,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return Results.Json(
                    new
                    {
                        result.ErrorCode,
                        result.Errors
                    },
                    statusCode: StatusCodes.Status401Unauthorized);
            }

            return Results.Ok(result.Data);
        })
        .AllowAnonymous()
        .WithName("Login")
        .WithSummary("Kullanıcı girişi yapar ve JWT Access Token üretir.")
        .Produces<AccessTokenResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .ProducesValidationProblem();
        // Get Current User
        group.MapGet("/me", (ClaimsPrincipal user) =>
        {
            var userId = user.FindFirst("sub")?.Value;

            return Results.Ok(new
            {
                UserId = userId
            });
        })
        .RequireAuthorization()
        .WithName("GetCurrentUser")
        .WithSummary("Giriş yapan kullanıcının bilgilerini getirir.")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized);
        return app;
    }
}