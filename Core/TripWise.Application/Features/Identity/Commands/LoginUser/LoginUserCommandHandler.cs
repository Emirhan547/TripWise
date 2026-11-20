using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TripWise.Application.Abstractions.Identity;
using TripWise.Application.Common.Models;
using TripWise.Application.Common.Results;

namespace TripWise.Application.Features.Identity.Commands.LoginUser
{
    public sealed class LoginUserCommandHandler(
      IIdentityService identityService,
      IJwtTokenService jwtTokenService)
      : IRequestHandler<LoginUserCommand, Result<AccessTokenResponse>>
    {
        public async Task<Result<AccessTokenResponse>> Handle(
            LoginUserCommand request,
            CancellationToken cancellationToken)
        {
            var authResult = await identityService.AuthenticateAsync(
                request.Email,
                request.Password);

            if (authResult.IsFailure)
            {
                return Result<AccessTokenResponse>.Fail(
                    authResult.ErrorCode!,
                    authResult.Errors);
            }

            var token = jwtTokenService.GenerateAccessToken(
                authResult.Data);

            return Result<AccessTokenResponse>.Success(token);
        }
    }
}