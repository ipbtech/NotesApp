using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Auth;
using NotesApp.Auth.Dto;

namespace NotesApp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(
        AuthService authService,
        ILogger<AuthController> logger) : ControllerBase
    {
        
        
        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<ActionResult<Guid>> SignUp(
            [FromBody] SignUpDto signUpDto)
        {
            var userId = await authService.RegisterAsync(signUpDto);
            logger.LogInformation(@"User @{userId} was sign succeeded up", userId);
            return Ok(userId);
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<TokensDto>> Login(
            [FromBody] LoginDto loginDto)
        {
            var tokens = await authService.LoginAsync(loginDto);
            return Ok(tokens);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            return Ok();
        }


        [HttpPost("refresh-token")]
        [Authorize]
        public Task<ActionResult<RefreshTokenDto>> RefreshToken(
            [FromBody] RefreshTokenDto refreshTokenDto)
        {
            return Task.FromResult<ActionResult<RefreshTokenDto>>(Ok());
        }


        [HttpPost("revoke-token")]
        [Authorize(Roles = "Admin")]
        public Task<ActionResult> RevokeToken()
        {
            return Task.FromResult<ActionResult>(Ok());
        }
    }
}