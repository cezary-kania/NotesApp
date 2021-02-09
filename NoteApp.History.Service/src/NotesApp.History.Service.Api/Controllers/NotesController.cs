using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotesApp.History.Service.Application.Services.Interfaces;

namespace NotesApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteHistoryController : ControllerBase
    {
        private readonly INoteService _noteService;
        public NoteHistoryController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("{noteId}")]
        public async Task<IActionResult> Get(Guid noteId)
        {
            var note = await _noteService.GetAsync(noteId);
            if(note == null) return NotFound();
            return Ok(note);
        }
    }
}