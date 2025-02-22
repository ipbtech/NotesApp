using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NotesApp.Api.Extensions;
using NotesApp.Auth;
using NotesApp.Auth.Dto;
using NotesApp.Auth.Options;

namespace NotesApp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(
        AuthService authService,
        IOptions<JwtOptions> jwtOptions,
        HttpContextProvider httpProvider,
        ILogger<AuthController> logger) : ControllerBase
    {
        private const string DEVICE_SESSION_COOKIE_NAME = "DeviceSessionId";
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;


        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<ActionResult<Guid>> SignUp(
            [FromBody] SignUpRequestDto signUpRequestDto)
        {
            var userId = await authService.RegisterAsync(signUpRequestDto);
            logger.LogInformation(@"User @{userId} was sign succeeded up", userId);
            return Ok(userId);
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseDto>> Login(
            [FromBody] LoginRequestDto loginRequestDto)
        {
            var deviceSessionId = Guid.NewGuid();
            var loginResponse = await authService.LoginAsync(loginRequestDto, deviceSessionId);
            
            Response.Cookies.Append(
                DEVICE_SESSION_COOKIE_NAME, 
                deviceSessionId.ToString(), 
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationDays),
                    HttpOnly = true,
                    Secure = false, //https
                    SameSite = SameSiteMode.Lax
                });
            return Ok(loginResponse);
        }


        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            if (!TryGetDeviceSessionCookie(out Guid deviceSessionId))
                return BadRequest($"{DEVICE_SESSION_COOKIE_NAME} cookie is not exist or invalid");

            var userId = httpProvider.GetCurrentUserId();
            await authService.LogoutAsync(userId, deviceSessionId);

            Response.Cookies.Delete(DEVICE_SESSION_COOKIE_NAME);
            return Ok();
        }


        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<ActionResult<RefreshTokenResponseDto>> RefreshToken(
            [FromBody] RefreshTokenRequestDto refreshTokenRequestDto)
        {
            if (!TryGetDeviceSessionCookie(out Guid deviceSessionId))
                return BadRequest($"{DEVICE_SESSION_COOKIE_NAME} cookie is not exist or invalid");

            var newToken = await authService.RefreshTokenAsync(refreshTokenRequestDto, deviceSessionId);
            return Ok(newToken);
        }


        [HttpPost("revoke-token")]
        [Authorize(Roles = "Admin")]
        public Task<ActionResult> RevokeToken()
        {
            return Task.FromResult<ActionResult>(Ok());
        }

        private bool TryGetDeviceSessionCookie(out Guid deviceSessionId)
        {
            deviceSessionId = Guid.Empty;
            var deviceSessionCookie = Request.Cookies[DEVICE_SESSION_COOKIE_NAME];
            if (deviceSessionCookie is not null &&
                Guid.TryParse(deviceSessionCookie, out deviceSessionId))
            {
                return true;
            }
            return false;
        }
    }
}