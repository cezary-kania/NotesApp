using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Application.DTOs;
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
        public async Task<IActionResult> GetAll()
        {
            var notes = await _noteService.GetAllAsync();
            return Ok(notes);
        }

        [HttpGet("{noteId}")]
        public async Task<IActionResult> Get(Guid noteId)
        {
            var note = await _noteService.GetAsync(noteId);
            if(note == null) return NotFound();
            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateNoteDto createNoteDto)
        {
            var noteId = Guid.NewGuid();
            await _noteService.CreateAsync(noteId, createNoteDto.Title, createNoteDto.Content);
            return Created($"Notes/{noteId}", null);
        }

        [HttpPut("{noteId}")]
        public async Task<IActionResult> Put([FromBody]UpdateNoteDto updateNoteDto, Guid noteId)
        {
            var note = await _noteService.GetAsync(noteId);
            if(note == null) return NotFound(); 
            await _noteService.UpdateAsync(noteId, updateNoteDto.Title, updateNoteDto.Content);
            return NoContent();
        }

        [HttpDelete("{noteId}")]
        public async Task<IActionResult> Delete(Guid noteId)
        {
            var note = await _noteService.GetAsync(noteId);
            if(note == null) return NotFound(); 
            await _noteService.DeleteAsync(noteId);
            return NoContent();
        }
    }
}