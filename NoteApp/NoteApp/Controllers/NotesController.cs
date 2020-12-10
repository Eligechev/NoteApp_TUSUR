using System.Linq;
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
<<<<<<< HEAD
            
            // Костыль конечно, но сделано для разора, чтобы при загрузке страницы отображался список.
            project = notesManager.GetNotesProject();
=======

            // Костыль конечно, но сделано для разора, чтобы при загрузке страницы отображался список.
            project = notesManager.GetNotesProject();
        }

        /// <summary>
        /// Получение всех заметок
        /// </summary>
        /// <returns><see cref="ActionResult"/>.</returns>
        [HttpGet]
        public ActionResult<Project> GetNotes()
        {
            project = notesManager.GetNotesProject();
            return View("../Views/Notes.cshtml", project);
        }
        
        /// <summary>
        /// Получение всех заметок данного типа.
        /// </summary>
        /// <returns><see cref="ActionResult"/>.</returns>
        [HttpGet] 
        public ActionResult<Project> FilterNotesByCategory(NotesCategories categoriy)
        {
            project = notesManager.GetNotesProject(categoriy);
            return View("../Views/Notes.cshtml", project);
>>>>>>> Feature_AddDomainLayer
        }

        /// <summary>
        /// Добавление заметки
        /// </summary>
        /// <param name="model">Модель добавляемой заметки.</param>
        [HttpPost]
        public void AddNote(NoteModel model)
        {
            project.NoteModels.Add(model);
        //    notesManager.EditNotes(project);
        }

        public ActionResult<Project> Notes()
        {
            project = notesManager.GetNotesProject(null);
            return View("Notes",project);
        }

        public ActionResult<Project> FilteredNotes(NotesCategories noteCategory)
        {
            project.NoteModels = project.NoteModels.Where(n => n.NotesCategory == noteCategory).ToList();
            return  new ActionResult<Project>(project.NoteModels.FirstOrDefault().NotesCategory == noteCategory));
        }

        [HttpPut]
        public void EditNote(NoteModel model)
        {
            this.project.NoteModels.Where(n => n.)
        }

        /// <summary>
        /// Удаление заметки
        /// </summary>
        /// <param name="model">Модель удаляемой заметки.</param>
        [HttpDelete]
        public void DeleteNote(NoteModel model)
        {
            project.NoteModels.Remove(model);
         //   notesManager.EditNotes(project);
        }
    }
}
