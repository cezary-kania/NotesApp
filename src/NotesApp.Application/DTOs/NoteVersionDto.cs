using System;

namespace NotesApp.Application.DTOs
{
    public class NoteVersionDto
    {
        public Guid NoteId { get; set; }
        public int VersionNo { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}