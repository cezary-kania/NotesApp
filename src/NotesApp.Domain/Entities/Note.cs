using System;
using System.Collections.Generic;
using System.Linq;
using NotesApp.Domain.Enums;

namespace NotesApp.Domain.Entities
{
    public class Note
    {
        public ISet<NoteVersion> _versions = new HashSet<NoteVersion>();
        public Guid Id { get; protected set; }
        public DateTime Created { get; protected set; }
        public DateTime Modified { get; protected set; }
        public IEnumerable<NoteVersion> Versions
        {
            get => _versions;
            set => _versions = new HashSet<NoteVersion>(value);
        }
        protected Note()
        {
        }
        protected Note(Guid noteId) 
        {
            Id = noteId;
            Created = DateTime.UtcNow;
            Modified = DateTime.UtcNow;
        }

        public void UpdateModifyTime()
            => Modified = DateTime.UtcNow;
        
        public Note CreateNewNote(Guid noteId)
            => new Note(noteId);

        public void AddVersion(string title, string content) 
        {
            var versionNo = _versions.Count + 1;
            var newVersion = new NoteVersion(Id, versionNo, title, content);
            _versions.Add(newVersion);
        }
        
        public NoteVersion GetLastVersion()
            => _versions.OrderByDescending(version => version.VersionNo)
                        .First();
    }
}