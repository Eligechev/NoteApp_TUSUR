using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NoteApp.Domain;
using NoteApp.Domain.Enums;
using NoteApp.Domain.Models;
using NoteApp.Models;

namespace NoteApp.Controllers
{
    public class NotesController : Controller
    {
        private readonly ILogger<NotesController> _logger;
        private readonly NotesManager notesManager;
        private Project project;

        public NotesController(ILogger<NotesController> logger)
        {
            _logger = logger;
            notesManager = new NotesManager();
            project = new Project();
        }

        [HttpPost]
        public void AddNote(string name, string text, NotesCategories notesCategoriy)
        {
            var note = new NoteModel();
            note.NoteMessage = text;
            note.NoteName = name;
            note.NotesCategory = notesCategoriy;
            
            project.NoteModels.Add(note);
            notesManager.AddNotes(project);
        }

        [HttpGet]
        public void GetNotes()
        {
            project = notesManager.GetNotesProject();
        }
    }
}
