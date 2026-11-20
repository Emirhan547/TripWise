using System;
using System.Collections.Generic;
using System.Text;

namespace TripWise.Application.Abstractions.Identity
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
    }
}
