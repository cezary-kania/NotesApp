using System;
using System.Collections.Generic;
using System.Linq;

namespace NotesApp.History.Service.Domain.Entities
{
    public class Note
    {
        private ISet<NoteVersion> _versions;
        public Guid Id { get; protected set; }
        public DateTime Created { get; protected set; }
        public DateTime Modified { get; protected set; }
        public bool IsDeleted { get; protected set; }
        
        public IEnumerable<NoteVersion> Versions
        {
            get => _versions;
            set => _versions = new HashSet<NoteVersion>(value);
        }
        protected Note()
        {
        }
        public Note(Guid noteId, DateTime created, DateTime modified, ISet<NoteVersion> versions) 
        {
            Id = noteId;
            Created = created;
            Modified = modified;
            _versions = versions;
        }
    }
}