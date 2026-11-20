

using TripWise.Application.Common.Models;

namespace TripWise.Application.Abstractions.Identity;

public interface IJwtTokenService
{
    AccessTokenResponse GenerateAccessToken(Guid userId);
}
