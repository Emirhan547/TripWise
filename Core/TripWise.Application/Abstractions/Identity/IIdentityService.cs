
using TripWise.Application.Common.Results;

namespace TripWise.Application.Abstractions.Identity;

public  interface IIdentityService
{
    Task<Result<Guid>> RegisterAsync(string email, string password);
    Task<Result<Guid>> AuthenticateAsync(string email,string password);
}
