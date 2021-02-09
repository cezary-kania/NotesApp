using System;
using System.Threading.Tasks;
using NotesApp.History.Service.Domain.Entities;

namespace NotesApp.History.Service.Domain.Repositories
{
    public interface INoteRepository
    {
        Task<Note> GetHistoryAsync(Guid id);
    }
}