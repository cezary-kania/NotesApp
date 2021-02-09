using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotesApp.History.Service.Domain.Entities;
using NotesApp.History.Service.Domain.Repositories;
using NotesApp.History.Service.Infrastructure.EntityFramework;

namespace NotesApp.History.Service.Infrastructure.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext _context;

        public NoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Note> GetHistoryAsync(Guid id)
        {
            return await _context.Notes
                .Include(n => n.Versions)
                .SingleOrDefaultAsync(n => n.Id == id);
        }
    }
}