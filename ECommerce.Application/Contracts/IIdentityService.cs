using ECommerce.Application.Commen;
using ECommerce.Application.DTO.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Contracts
{
    public  interface IIdentityService
    {
        Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken ct = default);
        Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default);
        Task<Result<IdentityUserResult>> CraeteUserAsync(ResgisterDto resgisterDto, CancellationToken ct = default);
        Task<Result<IReadOnlyList<string>>> GetUserRoles(string email, CancellationToken ct = default);


    }
}
