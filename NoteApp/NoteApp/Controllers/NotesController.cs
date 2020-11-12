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
            
            // Костыль конечно, но сделано для разора, чтобы при загрузке страницы отображался список.
            project = notesManager.GetNotesProject();
        }

        [HttpPost]
        public void AddNote(NoteModel model)
        {
            project.NoteModels.Add(model);
        //    notesManager.EditNotes(project);
        }

        [HttpGet]
        public ActionResult<Project> GetNotes()
        {
            project = notesManager.GetNotesProject(null);
            return View("../Views/Notes.cshtml", project);
        }

        [HttpPut]
        public void EditNote(NoteModel model)
        {
        }

        [HttpDelete]
        public void DeleteNote(NoteModel model)
        {
            project.NoteModels.Remove(model);
         //   notesManager.EditNotes(project);
        }
    }
}
