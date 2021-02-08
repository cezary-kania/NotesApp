using System;
using NotesApp.Domain.Enums;

namespace NotesApp.Domain.Entities
{
    public class Note
    {
        public Guid Id { get; protected set; }
        public string Title { get; protected set; }
        public string Content { get; protected set; }
        public DateTime Created { get; protected set; }
        public DateTime Modified { get; protected set; }
        public string Status { get; protected set; }
        public int VersionNo { get; protected set; }
        public Guid PrevVersionNoteId { get; protected set; }
        protected Note()
        {
        }

        protected Note(Guid noteId, string title, string content) 
        {
            Id = Guid.NewGuid(); // TODO: replace assignment with given noteId
            SetTitle(title);
            SetContent(content);
            SetStatus(NoteStatus.Created);
            VersionNo = 1;
            Created = DateTime.UtcNow;
            Modified = DateTime.UtcNow;
        }

        public void SetTitle(string title)
        {
            if(string.IsNullOrWhiteSpace(title)) throw new Exception("Title can not be empty.");
        }

        public void SetContent(string content)
        {
            if(string.IsNullOrWhiteSpace(content)) throw new Exception("Content can not be empty.");
        }
        public void MarkAsModified()
            => Modified = DateTime.UtcNow;

        public void SetStatus(string newStatus)
            => Status = newStatus;

        public Note CreateNewNote(Guid noteId, string title, string content)
            => new Note(noteId, title, content);

        public Note CreateNoteCopy(Guid noteId, Note note, string title, string content)
        {
            var noteCopy = new Note {
                Id = noteId,
                VersionNo = ++note.VersionNo,
                PrevVersionNoteId = note.Id,
                Created = note.Created,
                Modified = DateTime.UtcNow
            };
            noteCopy.SetStatus(NoteStatus.Created);
            noteCopy.SetTitle(title);
            noteCopy.SetContent(content);
            return noteCopy;
        } 

    }
}