using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NotesApp.Domain.Entities;

namespace NotesApp.Infrastructure.EntityFramework
{
    public static class DbSeedExt
    {
        public static AppDbContext SeedNotes(this AppDbContext dbContext) 
        {
            var notes = new List<Note>();
            for(int i = 0; i < 10; ++i) 
            {
                var note = new Note(Guid.NewGuid());
                for(int j = 0; j < 10; ++j)
                {
                    var title = $"Title {i*10 + j}";
                    var content = $"Content {i*10 + j}";
                    note.AddVersion(title, content);
                }
                notes.Add(note);
            }
            dbContext.AddRange(notes);
            dbContext.SaveChanges();
            return dbContext;
        }
    }
}