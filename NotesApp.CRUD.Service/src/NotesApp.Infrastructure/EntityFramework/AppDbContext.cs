using Microsoft.EntityFrameworkCore;
using NotesApp.Domain.Entities;

namespace NotesApp.Infrastructure.EntityFramework
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
            modelBuilder.Entity<Note>()
                .HasKey(n => n.Id);            
            modelBuilder.Entity<NoteVersion>()
                .HasKey(nv => new {nv.NoteId, nv.VersionNo});
        }
    }
}