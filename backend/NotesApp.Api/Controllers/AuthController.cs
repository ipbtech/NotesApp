using FluentValidation;
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
        public async Task<ActionResult<Guid>> SignUp(
            [FromBody] SignUpDto signUpDto)
        {
            var userId = await authService.RegisterAsync(signUpDto);
            logger.LogInformation(@"User @{userId} was sign succeeded up", userId);
            return Ok(userId);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokensDto>> Login(
            [FromBody] LoginDto loginDto)
        {
            var tokens = await authService.LoginAsync(loginDto);
            return Ok(tokens);
        }

        [HttpPost("logout")]
        public Task<ActionResult> Logout()
        {
            return Task.FromResult<ActionResult>(Ok());
        }

        [HttpPost("refresh-token")]
        public Task<ActionResult<RefreshTokenDto>> RefreshToken(
            [FromBody] RefreshTokenDto refreshTokenDto)
        {
            return Task.FromResult<ActionResult<RefreshTokenDto>>(Ok());
        }

        [HttpPost("revoke-token")]
        public Task<ActionResult> RevokeToken()
        {
            return Task.FromResult<ActionResult>(Ok());
        }
    }
}