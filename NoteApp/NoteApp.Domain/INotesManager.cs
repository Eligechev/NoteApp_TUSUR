using System;

namespace NoteApp.Domain
{
    using NoteApp.Domain.Models;

    public interface INotesManager
    {
        void AddNote(NoteModel note);
    }

    public class NotesManager : INotesManager
    {
        public void AddNote(NoteModel note)
        {

        } 
    }
}
