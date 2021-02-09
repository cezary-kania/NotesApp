using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotesApp.Application.DTOs;

namespace NotesApp.Application.Services.Interfaces
{
    public interface INoteService
    {
        Task<NoteDetailsDto> GetAsync(Guid id);
        Task<IEnumerable<NoteDto>> GetAllAsync();
        Task CreateAsync(Guid noteId, string title, string content);
        Task UpdateAsync(Guid noteId, string title, string content);
        Task DeleteAsync(Guid noteId);
    }
}