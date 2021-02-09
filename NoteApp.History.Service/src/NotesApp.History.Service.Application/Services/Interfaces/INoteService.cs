using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotesApp.History.Service.Application.DTOs;

namespace NotesApp.History.Service.Application.Services.Interfaces
{
    public interface INoteService
    {
        Task<NoteDto> GetAsync(Guid id);
    }
}