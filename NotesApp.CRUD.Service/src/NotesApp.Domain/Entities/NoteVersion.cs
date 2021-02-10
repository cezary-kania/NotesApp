using System;

namespace NotesApp.Domain.Entities
{
    public class NoteVersion
    {
        public Guid NoteId { get; protected set; }
        public int VersionNo { get; protected set; }
        public string Title { get; protected set; }
        public string Content { get; protected set; }
        public DateTime VersionCreatedAt { get; protected set; }

        public NoteVersion(Guid noteId, int versionNo, string title, string content, DateTime versionCreatedAt)
        {
            NoteId = noteId;
            VersionNo = versionNo;
            VersionCreatedAt = versionCreatedAt;
            SetTitle(title);
            SetContent(content);
        }
        public void SetTitle(string title)
        {
            if(string.IsNullOrWhiteSpace(title)) throw new Exception("Title can not be empty.");
            Title = title;
        }

        public void SetContent(string content)
        {
            if(string.IsNullOrWhiteSpace(content)) throw new Exception("Content can not be empty.");
            Content = content;
        }
    }
}