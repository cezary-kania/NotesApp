using System;
using System.Collections.Generic;
using System.Linq;

namespace NotesApp.Domain.Entities
{
    public class Note
    {
        private ISet<NoteVersion> _versions = new HashSet<NoteVersion>();
        public Guid Id { get; protected set; }
        public DateTime Created { get; protected set; }
        public DateTime Modified { get; protected set; }
        public bool IsDeleted { get; protected set; } = false;
        
        public IEnumerable<NoteVersion> Versions
        {
            get => _versions;
            set => _versions = new HashSet<NoteVersion>(value);
        }
        protected Note()
        {
        }
        public Note(Guid noteId) 
        {
            Id = noteId;
            Created = DateTime.UtcNow;
            UpdateModifyTime();
        }

        public void UpdateModifyTime()
            => Modified = DateTime.UtcNow;

        public void AddVersion(string title, string content) 
        {
            var versionNo = _versions.Count + 1;
            var newVersion = new NoteVersion(Id, versionNo, title, content);
            _versions.Add(newVersion);
            UpdateModifyTime();
        }

        public NoteVersion GetLastVersion() 
            => _versions.OrderByDescending(version => version.VersionNo)
                        .FirstOrDefault();
                        
        public void MarkAsDeleted() {
            IsDeleted = true;
        }
    }
}