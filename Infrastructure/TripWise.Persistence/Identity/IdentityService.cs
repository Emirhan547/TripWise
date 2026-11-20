using Microsoft.AspNetCore.Identity;
using TripWise.Application.Abstractions.Identity;
using TripWise.Application.Common.Results;

namespace TripWise.Persistence.Identity;

public sealed class IdentityService(UserManager<ApplicationUser> userManager) : IIdentityService
{
    public async Task<Result<Guid>> RegisterAsync(string email,string password)
    {
        var user = new ApplicationUser
        {
            UserName = email.Trim(),
            Email = email.Trim()
        };

        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description).ToArray();

            return Result<Guid>.Fail("Identity.RegisterFailed",errors);
        }

        return Result<Guid>.Success(user.Id);
    }
    public async Task<Result<Guid>> AuthenticateAsync(
    string email,
    string password)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return Result<Guid>.Fail(
                "Identity.InvalidCredentials",
                new[] { "E-posta veya parola hatalı." });
        }

        var isPasswordValid = await userManager.CheckPasswordAsync(
            user,
            password);

        if (!isPasswordValid)
        {
            return Result<Guid>.Fail(
                "Identity.InvalidCredentials",
                new[] { "E-posta veya parola hatalı." });
        }

        return Result<Guid>.Success(user.Id);
    }
}
