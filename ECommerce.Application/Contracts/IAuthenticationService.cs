using ECommerce.Application.Commen;
using ECommerce.Application.DTO.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Contracts
{
    public  interface IAuthenticationService
    {
        Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default);
        Task<Result<UserDto>> RegisterAsync(ResgisterDto registerDto, CancellationToken ct = default);

    }
}
