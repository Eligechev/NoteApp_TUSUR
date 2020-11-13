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
        }

        /// <summary>
        /// Добавление заметки
        /// </summary>
        /// <param name="model">Модель добавляемой заметки.</param>
        [HttpPost]
        public void AddNote(NoteModel model)
        {
            project.NoteModels.Add(model);
            notesManager.EditNotes(project);
        }
        
        /// <summary>
        /// Редактирование заметки.
        /// </summary>
        /// <param name="model">Модель редактируемой заметки.</param>
        [HttpPut]
        public void EditNote(NoteModel model)
        {
            var editableItemIndex = project.NoteModels.FindIndex(n => n.Id == model.Id);
            project.NoteModels[editableItemIndex] = model;
            notesManager.EditNotes(project);
        }

        /// <summary>
        /// Удаление заметки
        /// </summary>
        /// <param name="model">Модель удаляемой заметки.</param>
        [HttpDelete]
        public void DeleteNote(NoteModel model)
        {
            project.NoteModels.Remove(model);
            notesManager.EditNotes(project);
        }
    }
}
