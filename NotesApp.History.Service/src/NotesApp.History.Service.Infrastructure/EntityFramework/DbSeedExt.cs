using System;
using System.Collections.Generic;
using System.Linq;
using NotesApp.History.Service.Domain.Entities;
using NotesApp.History.Service.Infrastructure.EntityFramework;

namespace NotesApp.History.Service.Infrastructure.EntityFramework
{
    public static class DbSeedExt
    {
        public static AppDbContext SeedNotes(this AppDbContext dbContext) 
        {
            if(dbContext.Notes.Any()) return dbContext;
            var notes = new List<Note>();
            var generatedGuids = new Guid[] 
            {
                Guid.Parse("b1d3b50c-3542-4976-aae0-d85bec33b286"),
                Guid.Parse("8409c0e5-2734-48a2-a4c5-6afc49c9cf24"),
                Guid.Parse("87a65a3e-4eac-43e3-a1e5-26b20c069ade"),
                Guid.Parse("0fbbfa74-c69f-4a7e-9a97-af78876727a9"),
                Guid.Parse("59c72593-df76-4d12-be08-f96591c01db6"),
                Guid.Parse("12dc452a-bae9-4c5b-8805-87d05b33b789"),
                Guid.Parse("d4240b8c-75c3-4486-a861-fd04317611c8"),
                Guid.Parse("91799e99-0e14-4049-9dee-cdd371a4dc9d"),
                Guid.Parse("65dc07bb-60c6-490e-adff-7babc95712ea"),
                Guid.Parse("4b3dfac3-767b-495f-b8bd-477de95b1b4f"),
            };
            for(int i = 0; i < 10; ++i) 
            {
                var noteId = generatedGuids[i];
                var noteVersions = new HashSet<NoteVersion>();
                for(int j = 0; j < 10; ++j)
                {
                    var title = $"Title {i*10 + j}";
                    var content = $"Content {i*10 + j}";
                    noteVersions.Add(new NoteVersion(noteId,j+1, title, content));
                }
                var note = new Note(noteId, DateTime.UtcNow, DateTime.UtcNow, noteVersions);
                notes.Add(note);
            }
            dbContext.AddRange(notes);
            dbContext.SaveChanges();
            return dbContext;
        }
    }
}