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
    public class NoteController(
        INoteService noteService,
        HttpContextProvider httpProvider,
        ILogger<NoteController> logger) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteResponseDto>>> Get([FromBody] NotePaginationDto? paginationDto)
        {
            var userId = httpProvider.GetCurrentUserId();
            var notes = await noteService.GetAsync(paginationDto, userId);
            return Ok(notes);
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<NoteResponseDto>> Get([FromRoute] Guid id)
        {
            var userId = httpProvider.GetCurrentUserId();
            var note = await noteService.GetAsync(id, userId);
            return Ok(note);
        }


        [HttpPost]
        public async Task<ActionResult<NoteResponseDto>> Create([FromBody] NoteRequestDto noteDto)
        {
            var userId = httpProvider.GetCurrentUserId();
            var note = await noteService.CreateAsync(noteDto, userId);
            return Ok(note);
        }


        [HttpPut("{id:guid}")]
        public async Task<ActionResult<NoteResponseDto>> Update([FromRoute] Guid id, [FromBody] NoteRequestDto noteDto)
        {
            var userId = httpProvider.GetCurrentUserId();
            var note = await noteService.UpdateAsync(id, noteDto, userId);
            return Ok(note);
        }


        [HttpPut("new-tag")]
        public async Task<ActionResult<NoteResponseDto>> ChangeTag([FromBody] NoteTagUpdatingDto tagUpdatingDto)
        {
            var userId = httpProvider.GetCurrentUserId();
            var note = await noteService.UpdateTagAsync(tagUpdatingDto, userId);
            return Ok(note);
        }


        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete([FromBody] Guid id)
        {
            var userId = httpProvider.GetCurrentUserId();
            await noteService.DeleteAsync(id, userId);
            return Ok();
        }
    }
}
