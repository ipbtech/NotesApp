using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
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
        ILogger<TagsController> logger) : ControllerBase
    {
        
        [HttpGet]
        //TODO rights authorize
        public async Task<ActionResult> GetAll()
        {
            return Ok();
        }

        [HttpGet("current-user")]
        public async Task<ActionResult> GetAllByCurrentUser()
        {

            return Ok();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetById([FromRoute, Required] Guid id)
        {
            return Ok();
        }

        [HttpPost("{tagName}")]
        public async Task<ActionResult> Create([FromRoute, Required] string tagName)
        {
            return Ok();
        }

        [HttpPut("{id:guid}/{newName}")]
        public async Task<ActionResult> Update([FromRoute, Required] Guid id, [FromRoute, Required] string newName)
        {
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete([FromRoute, Required] Guid id)
        {
            return Ok();
        }
    }
}
