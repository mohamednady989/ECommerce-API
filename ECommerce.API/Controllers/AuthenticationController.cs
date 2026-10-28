using ECommerce.Application.Contracts;
using ECommerce.Application.DTO.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : APIBaseController
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {
            var result = await authenticationService.LoginAsync(loginDto, ct);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);



        }
        [HttpOptions("Register")]
        public async Task<ActionResult<UserDto>> Register(ResgisterDto resgisterDto)
        {
            return ToActionResult(await authenticationService.RegisterAsync(resgisterDto));
        }

    }
}
