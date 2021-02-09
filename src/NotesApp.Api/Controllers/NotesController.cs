using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Application.Services.Interfaces;

namespace NotesApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;
        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var notes = await _noteService.GetAllAsync();
            return Ok(notes);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var noteId = Guid.NewGuid();

            await _noteService.CreateAsync(noteId, "Note title", "Note content");
            return Created("", new object());
        }
    }
}