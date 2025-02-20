using Microsoft.AspNetCore.Mvc;
using NotesApp.Auth;
using NotesApp.Auth.Dto;

namespace NotesApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        AuthService authService,
        ILogger<AuthController> logger) : ControllerBase
    {
        [HttpPost("sign-up")]
        public async Task<ActionResult<Guid>> SignUp(SignUpDto signUpDto)
        {
            var userId = await authService.RegisterAsync(signUpDto);
            return Ok(userId);
        }
    }
}
