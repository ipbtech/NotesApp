using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Api.Extensions;
using NotesApp.Domain.Interfaces.Services;
using NotesApp.Dto;

namespace NotesApp.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class TagController(
        ITagService tagService,
        HttpContextProvider httpProvider,
        ILogger<TagController> logger) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagResponseDto>>> GetAllByCurrentUser()
        {
            var userId = httpProvider.GetCurrentUserId();
            var tags = await tagService.GetAllAsync(userId);
            return Ok(tags);
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TagResponseDto>> GetById([FromRoute, Required] Guid id)
        {
            var userId = httpProvider.GetCurrentUserId();
            var tag = await tagService.GetByIdAsync(id, userId);
            return Ok(tag);
        }


        [HttpPost("{tagName}")]
        public async Task<ActionResult<TagResponseDto>> Create([FromRoute, Required] string tagName)
        {
            var userId = httpProvider.GetCurrentUserId();
            var tag = await tagService.CreateAsync(tagName, userId);

            logger.LogInformation("User {userId} created new tag {tagId}", userId, tag.Id);
            return Ok(tag);
        }


        [HttpPut("{id:guid}/{newName}")]
        public async Task<ActionResult<TagResponseDto>> Update([FromRoute, Required] Guid id, [FromRoute, Required] string newName)
        {
            var userId = httpProvider.GetCurrentUserId();
            var tag = await tagService.UpdateAsync(id, newName, userId);

            logger.LogInformation("User {userId} updated tag {tagId}", userId, tag.Id);
            return Ok(tag);
        }


        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete([FromRoute, Required] Guid id)
        {
            var userId = httpProvider.GetCurrentUserId();
            await tagService.DeleteAsync(id, userId);

            logger.LogInformation("User {userId} removed tag {tagId}", userId, id);
            return Ok();
        }
    }
}
