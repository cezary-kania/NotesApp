using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotesApp.Domain.Entities;

namespace NotesApp.Domain.Repositories
{
    public interface INoteRepository
    {
        Task<Note> GetAsync(Guid id);
        Task<Note> GetAsync(string title);
        Task<IEnumerable<Note>> GetAllAsync();
        Task CreateAsync(Note note);
        Task UpdateAsync(Note note);
        Task DeleteAsync(Note note);
    }
}