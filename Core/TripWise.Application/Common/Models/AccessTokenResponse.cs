
namespace TripWise.Application.Common.Models;

public sealed record AccessTokenResponse(
    string AccessToken,
    DateTimeOffset ExpiresAtUtc);


