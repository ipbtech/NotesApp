using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Domain.Interfaces.Services;

namespace NotesApp.Api.Controllers
{
    [Route("api/[controller]")]
    //[Authorize] //TODO
    [ApiController]
    public class TagsController(
        ITagService tagService,
        ILogger<TagsController> logger) : ControllerBase
    {
        
        [HttpGet]
        public ActionResult GetAll([FromQuery] Guid? userId)
        {
            return Ok();
        }

        [HttpGet("{id:guid}")]
        public ActionResult GetById([FromRoute, Required] Guid id)
        {
            return Ok();
        }

        [HttpPost("{tagName}")]
        public ActionResult Create([FromRoute, Required] string tagName)
        {
            return Ok();
        }

        [HttpPut("{id:guid}")]
        public ActionResult Update([FromRoute, Required] Guid id, [FromBody] string newName)
        {
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Delete([FromRoute, Required] Guid id)
        {
            return Ok();
        }
    }
}
