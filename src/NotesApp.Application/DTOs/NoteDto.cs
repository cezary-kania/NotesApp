using System;
using System.Collections.Generic;

namespace NotesApp.Application.DTOs
{
    public class NoteDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public NoteVersionDto LastVersion { get; set; }
    }
}