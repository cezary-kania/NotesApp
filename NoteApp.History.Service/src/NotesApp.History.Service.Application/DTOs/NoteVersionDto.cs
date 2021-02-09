using System;

namespace NotesApp.History.Service.Application.DTOs
{
    public class NoteVersionDto
    {
        public int VersionNo { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}