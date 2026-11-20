using TripWise.Application.Abstractions.Identity;

namespace TripWise.WebAPI.Services;

public sealed class CurrentUserService(
    IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public Guid UserId
    {
        get
        {
            var userId = httpContextAccessor.HttpContext?
                .User
                .FindFirst("sub")?
                .Value;

            if (!Guid.TryParse(userId, out var id))
            {
                throw new UnauthorizedAccessException(
                    "Giriş yapan kullanıcının ID bilgisi bulunamadı.");
            }

            return id;
        }
    }
}