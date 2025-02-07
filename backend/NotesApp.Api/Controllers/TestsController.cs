using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Application.Services;

namespace NotesApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController(NoteService noteService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            await noteService.GetNotesAsync(); 
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Create()
        {
            await noteService.Create();
            return Ok();
        }

        [HttpPost("test-user")]
        public async Task<ActionResult> CreateUser()
        {
            await noteService.CreateSeedUser();
            return Ok();
        }
    }
}
