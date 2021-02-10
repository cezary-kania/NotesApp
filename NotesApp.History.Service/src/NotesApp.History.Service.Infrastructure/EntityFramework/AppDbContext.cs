using Microsoft.EntityFrameworkCore;
using NotesApp.History.Service.Domain.Entities;

namespace NotesApp.History.Service.Infrastructure.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteVersion> NoteVersions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var notesBuilder = modelBuilder.Entity<Note>();
            notesBuilder.HasKey(n => n.Id);            
            var notesVersionBuilder = modelBuilder.Entity<NoteVersion>();
            notesVersionBuilder.HasKey(nv => new {nv.NoteId, nv.VersionNo});
            
            //Db seeding
            if(Database.IsInMemory()) 
            {
                modelBuilder.Entity<Note>().HasData(
                    null // TODO: Replace with real data 
                );
            }
        }
    }
}