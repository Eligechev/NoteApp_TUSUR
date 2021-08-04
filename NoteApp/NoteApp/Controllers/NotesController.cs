using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
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
            project = notesManager.GetNotesProject();
        }

        /// <summary>
        /// Получение всех заметок
        /// </summary>
        /// <returns><see cref="ActionResult"/>.</returns>
        [HttpGet]
        public ActionResult<Project> Notes(NotesFilter filter)
        {
            project = notesManager.GetNotesProject(filter);
            return project;
        }

        /// <summary>
        /// Добавление заметки
        /// </summary>
        /// <param name="model">Модель добавляемой заметки.</param>
        [HttpPost]
        public ActionResult<Project> AddNote(NoteModel model)
        {
            this.notesManager.AddNote(project, model);
            return project;
        }

        /// <summary>
        /// Редактирование заметки
        /// </summary>
        /// <param name="model"></param>
        [HttpPut]
        public ActionResult<Project> EditNote(NoteModel model)
        {
            notesManager.EditNotes(project, model);
            return project;
        }

        /// <summary>
        /// Удаление заметки
        /// </summary>
        /// <param name="model">Модель удаляемой заметки.</param>
        public ActionResult<Project> DeleteNote(int id)
        {
            this.notesManager.DeleteNote(project, id);
            return project;
        }
    }
}
