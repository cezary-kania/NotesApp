using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Repositories;
using NotesApp.Infrastructure.EntityFramework;

namespace NotesApp.Infrastructure.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext _context;

        public NoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Note>> GetAllAsync()
            => await _context.Notes
                .Where(n => !n.IsDeleted)
                .Include(n => n.Versions)
                .ToListAsync();

        public async Task<Note> GetAsync(Guid id)
            => await _context.Notes
                .Where(n => !n.IsDeleted)
                .Include(n => n.Versions)
                .SingleOrDefaultAsync(note => note.Id == id);

        public async Task CreateAsync(Note note)
        {
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Note note)
        {
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
        }
    }
}