using ECommerce.Application.Commen;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Entitys
{
    public  class IdentityService : IIdentityService
    {
        public UserManager<ApplicationUser> UserManager { get; }
        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        

        public async Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default)
        {
            var user= await UserManager.FindByEmailAsync(email);
            if(user ==null )
                return Result<bool>.Fail(Error.NotFound("UserNotFound", "User not found"));
            else
                return Result<bool>.Ok(await UserManager.CheckPasswordAsync(user, password));

        }

        public Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken ct = default)
        {
            var user = UserManager.FindByEmailAsync(email).Result;
            if (user == null)
                return Task.FromResult(Result<IdentityUserResult>.Fail(Error.NotFound("UserNotFound", "User not found")));
            else
            {
                var result = new IdentityUserResult(user.Id, user.DisplayName, user.Email, user.UserName);
                return Task.FromResult(Result<IdentityUserResult>.Ok(result));
            }
        }

        public async Task<Result<IdentityUserResult>> CraeteUserAsync(ResgisterDto resgisterDto, CancellationToken ct = default)
        { 
            var user= new ApplicationUser
            {
                Email = resgisterDto.Email,
                UserName = resgisterDto.UserName,
                DisplayName = resgisterDto.DisplayName,
                PhoneNumber = resgisterDto.PhoneNumber.ToString()
            };
            var result = await  UserManager.CreateAsync(user, resgisterDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => Error.Failure(e.Code, e.Description)).ToList();
                return Result<IdentityUserResult>.Fail(errors);
            }
            return Result<IdentityUserResult>.Ok(new IdentityUserResult(user.Id, user.DisplayName, user.Email, user.UserName));
        }

        public async Task<Result<IReadOnlyList<string>>> GetUserRoles(string email, CancellationToken ct = default)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user == null)
                return Result<IReadOnlyList<string>>.Fail(Error.NotFound("user not found "));
            var roles = await UserManager.GetRolesAsync(user);
            return Result<IReadOnlyList<string>>.Ok(roles.ToList());
        }
    }
}
