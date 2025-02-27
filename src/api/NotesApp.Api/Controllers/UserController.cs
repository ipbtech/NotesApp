using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Api.Extensions;
using NotesApp.Domain.Interfaces.Services;
using NotesApp.Dto;

namespace NotesApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UserController(
        IUserService userService,
        HttpContextProvider httpProvider,
        ILogger<UserController> logger) : ControllerBase
    {

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserResponseDto>>> GetAllUserInfo()
        {
            var response = await userService.GetAllAsync();
            return Ok(response);
        }
        
        
        [HttpGet]
        public async Task<ActionResult<UserResponseDto>> GetCurrentUserInfo()
        {
            var userId = httpProvider.GetCurrentUserId();
            var response = await userService.GetAsync(userId);
            return Ok(response);
        }


        [HttpPut]
        public async Task<ActionResult<UserResponseDto>> UpdateCurrentUserInfo([FromBody] UserRequestDto userDto)
        {
            var userId = httpProvider.GetCurrentUserId();
            var response = await userService.UpdateAsync(userId, userDto);
            return Ok(response);
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteUser()
        {
            var userId = httpProvider.GetCurrentUserId();
            await userService.DeleteAsync(userId);
            logger.LogInformation("User @{userId} was deleted", userId);
            return Ok();
        }

        [HttpGet("avatar")]
        public async Task<ActionResult> GetAvatar()
        {
            return Ok();
        }

        [HttpPatch("avatar")]
        public async Task<ActionResult> UpdateAvatar()
        {
            return Ok();
        }

        [HttpDelete("avatar")]
        public async Task<ActionResult> DeleteAvatar()
        {
            return Ok();
        }
    }
}
