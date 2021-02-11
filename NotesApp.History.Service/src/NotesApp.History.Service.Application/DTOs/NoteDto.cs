using System;
using System.Collections.Generic;

namespace NotesApp.History.Service.Application.DTOs
{
    public class NoteDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<NoteVersionDto> Versions { get; set; }
    }
}