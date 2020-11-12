using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NoteApp.Domain;
using NoteApp.Domain.Enums;
using NoteApp.Domain.Models;

namespace NoteApp.Controllers
{
    public class NotesController : Controller
    {
        private readonly ILogger<NotesController> _logger;
        private readonly INotesManager notesManager;
        private Project project;

        public NotesController(ILogger<NotesController> logger)
        {
            _logger = logger;
            notesManager = new NotesManager();
            project = new Project();
        }

        [HttpPost]
        public void AddNote(NoteModel model)
        {
            project.NoteModels.Add(model);
            notesManager.EditNotes(project);
        }

        [HttpGet]
        public void GetNotes(NotesCategories category)
        {
            project = notesManager.GetNotesProject(category);
        }

        [HttpPut]
        public void EditNote(NoteModel model)
        {
        }

        [HttpDelete]
        public void DeleteNote(NoteModel model)
        {
            project.NoteModels.Remove(model);
            notesManager.EditNotes(project);
        }
    }
}
