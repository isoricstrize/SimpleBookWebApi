using Microsoft.AspNetCore.Mvc;
using SimpleBookWebApi.Dtos;
using SimpleBookWebApi.Entities;
using SimpleBookWebApi.Services;

namespace SimpleBookWebApi.Controllers
{
    // -----------------------------------------------------------------------------
    // AuthController
    //
    // Responsible for handling all authentication-related API endpoints.
    // Provides functionality for:
    //   • User registration
    //   • User login and JWT access token generation
    //   • Issuing and refreshing JWT tokens using refresh tokens
    //
    // This controller does NOT contain business logic. Instead, it delegates all
    // authentication operations to the IAuthService implementation.
    // -----------------------------------------------------------------------------

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<ApplicationUser>> Register(ApplicationUserDto request)
        {
            var user = await authService.RegisterAsync(request);
            if (user == null)
                return BadRequest("Username already exists.");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(ApplicationUserDto request)
        {
            var response = await authService.LoginAsync(request);
            if (response is null)
                return BadRequest("Invalid username or password.");

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await authService.RefreshTokensAsync(request);

            if (result is null || result.AccessToken is null || result.RefreshToken is null)
                return Unauthorized("Invalid refresh token.");

            return Ok(result);
        }


    }
}