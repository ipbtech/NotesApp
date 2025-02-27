using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Api.Extensions;
using NotesApp.Auth;
using NotesApp.Dto;

namespace NotesApp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class AuthController(
        AuthService authService,
        HttpContextProvider httpProvider,
        ILogger<AuthController> logger) : ControllerBase
    {


        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<ActionResult<Guid>> SignUp(
            [FromBody] SignUpRequestDto signUpRequestDto)
        {
            var userId = await authService.RegisterAsync(signUpRequestDto);
            logger.LogInformation(@"User {userId} was sign succeeded up", userId);
            return Ok(userId);
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseDto>> Login(
            [FromBody] LoginRequestDto loginRequestDto)
        {
            var loginResponse = await authService.LoginAsync(loginRequestDto);
            return Ok(loginResponse);
        }


        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult> Logout([FromBody] RefreshTokenRequestDto refreshTokenRequestDto)
        {
            await authService.LogoutAsync(refreshTokenRequestDto);
            return Ok();
        }


        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<ActionResult<RefreshTokenResponseDto>> RefreshToken(
            [FromBody] RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var newToken = await authService.RefreshTokenAsync(refreshTokenRequestDto);
            return Ok(newToken);
        }


        [HttpPost("revoke-token/current-user")]
        [Authorize]
        public async Task<ActionResult> RevokeTokensByCurrentUser()
        {
            var userId = httpProvider.GetCurrentUserId();
            await authService.RevokeRefreshTokenAsync(userId);
            logger.LogWarning(@"User {userId} revoked his refresh tokens", userId);
            return Ok();
        }


        [HttpPost("revoke-token/all-users")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> RevokeAllTokens()
        {
            await authService.RevokeRefreshTokenAsync();
            logger.LogWarning("All refresh tokens were succeeded revoked");
            return Ok();
        }


        [HttpPost("change-password")]
        [Authorize]
        public async Task<ActionResult> ChangePassword(
            [FromBody] ChangePasswordDto passwordDto)
        {
            var userId = httpProvider.GetCurrentUserId();
            await authService.ChangePasswordAsync(userId, passwordDto);
            logger.LogInformation(@"User {userId} changed his password", userId);
            return Ok();
        }

        //TODO forget passwords and confirmed email functionality
    }
}