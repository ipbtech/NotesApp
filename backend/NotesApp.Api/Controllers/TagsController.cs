using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Application.Dtos;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces.Mapping;
using NotesApp.Domain.Interfaces.Services;

namespace NotesApp.Api.Controllers
{
    [ApiController]
    //[Authorize] //TODO
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class TagsController(
        ITagService tagService,
        IMapper<Tag, TagDto> mapper,
        ILogger<TagsController> logger) : ControllerBase
    {
        
        [HttpGet]
        //TODO rights authorize
        public async Task<ActionResult<TagDto>> GetAll()
        {
            var tags = await tagService.GetAllAsync();
            return Ok(mapper.MapToDto(tags));
        }

        [HttpGet("current-user")]
        public async Task<ActionResult<TagDto>> GetAllByCurrentUser()
        {
            //TODO
            var tags = await tagService.GetAllAsync();
            return Ok(mapper.MapToDto(tags));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TagDto>> GetById([FromRoute, Required] Guid id)
        {
            var tag = await tagService.GetByIdAsync(id);
            if (tag is null)
                return NotFound();

            return Ok(mapper.MapToDto(tag));
        }

        [HttpPost("{tagName}")]
        public async Task<ActionResult> Create([FromRoute, Required] string tagName)
        {
            if (string.IsNullOrEmpty(tagName) || string.IsNullOrWhiteSpace(tagName))
                return BadRequest();

            var tag = await tagService.CreateAsync(tagName);
            return Ok(mapper.MapToDto(tag));
        }

        [HttpPut("{id:guid}/{newName}")]
        public async Task<ActionResult<TagDto>> Update([FromRoute, Required] Guid id, [FromRoute, Required] string newName)
        {
            if (string.IsNullOrEmpty(newName) || string.IsNullOrWhiteSpace(newName))
                return BadRequest();

            var tag = await tagService.UpdateAsync(id, newName);
            return Ok(mapper.MapToDto(tag));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete([FromRoute, Required] Guid id)
        {
            await tagService.DeleteAsync(id);
            return Ok();
        }
    }
}
