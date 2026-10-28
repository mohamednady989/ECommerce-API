using ECommerce.Application.Commen;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public IIdentityService IdentityService { get; }
        public ITokenService TokenService { get; }
        public AuthenticationService(IIdentityService identityService, ITokenService tokenService)
        {
            IdentityService = identityService;
            TokenService = tokenService;
        }

    

        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {
            var UserResult=await IdentityService.FindUserByEmailAsync(loginDto.Email);
            if(!UserResult.IsSuccess)
                return Result<UserDto>.Fail(UserResult.Errors);
            var PasswordResult = await IdentityService.CheckPasswordAsync(loginDto.Email, loginDto.Password);
            if(!PasswordResult.IsSuccess)
                return Result<UserDto>.Fail(UserResult.Errors);
            if(!PasswordResult.data)
                return Result<UserDto>.Fail(new Error("InvalidPassword", "The password is invalid."));


            var user = UserResult.data;
            var roleResult = await IdentityService.GetUserRoles(user.Email);
            var roles = roleResult.data;
            var token=TokenService.CreateToken(user.Id.ToString(), user.Email, user.UserName, roles);

            return Result<UserDto>.Ok(new UserDto()
            {
                Email = loginDto.Email,
                DisplayName = UserResult.data.DisplayName,
                Token = token,

            });

        }

        public async Task<Result<UserDto>> RegisterAsync(ResgisterDto registerDto, CancellationToken ct = default)
        {
            var userResult = await  IdentityService.CraeteUserAsync(registerDto,ct );
            if(!userResult.IsSuccess)
                return Result<UserDto>.Fail(userResult.Errors);


            var user = userResult.data;
            var roleResult = await IdentityService.GetUserRoles(user.Email);
            var roles = roleResult.data;
            var token = TokenService.CreateToken(user.Id.ToString(), user.Email, user.UserName, roles);



            return Result<UserDto>.Ok(new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = token
            });


        }
    }
}
