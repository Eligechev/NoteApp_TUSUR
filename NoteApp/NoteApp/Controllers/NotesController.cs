using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Domain;
using NoteApp.Domain.Models;
using NoteApp.ViewModels;

namespace NoteApp.Controllers
{
    /// <summary>
    /// Контроллер взаимодействия с информацией о записях.
    /// </summary>
    [ApiController]
    [Route("api/notes")]
    public class NotesController : Controller
    {
        private readonly INotesManager notesManager;

        public NotesController(INotesManager notesManager)
        {
            this.notesManager = notesManager;
        }

        /// <summary>
        /// Получение всех заметок  
        /// </summary>
        /// <returns><see cref="ActionResult"/>.</returns>
        [HttpGet("")]
        public ActionResult<List<NoteModel>> GetNotes([FromQuery] NotesFilter filter)
        {
            return notesManager.GetNotesProject(filter);
        }

        /// <summary>
        /// Получение всех заметок
        /// </summary>
        /// <returns><see cref="ActionResult"/>.</returns>
        [HttpGet("{id:int}")]
        public ActionResult<List<NoteModel>> GetNote(int id)
        {
            return notesManager.GetNotesProjectById(id);
        }

        /// <summary>
        /// Добавление заметки
        /// </summary>
        /// <param name="model">Модель добавляемой заметки.</param>
        [HttpPost("")]
        public ActionResult AddNote(NoteViewModel model)
        {
            this.notesManager.AddNote(this.ConvertIntoDomainModel(model));
            return Ok();
        }

        /// <summary>
        /// Редактирование заметки
        /// </summary>
        /// <param name="model"></param>
        [HttpPut("")]
        public ActionResult EditNote(NoteViewModel model)
        {
            this.notesManager.EditNotes(this.ConvertIntoDomainModel(model));
            
            return Ok();
        }

        /// <summary>
        /// Удаление заметки
        /// </summary>
        /// <param name="model">Модель удаляемой заметки.</param>
        [HttpDelete("{id:int}")]
        public ActionResult DeleteNote(int id)
        {
            this.notesManager.DeleteNote(id);
            return Ok();
        }

        [HttpDelete("RemoveAll")]
        public ActionResult RemoveAll()
        {
            this.notesManager.DeleteAll();
            return Ok();
        }
        
        private NoteModel ConvertIntoDomainModel(NoteViewModel viewModel)
        {
            var domainModel = new NoteModel();

            domainModel.Id = viewModel.Id.GetValueOrDefault();
            domainModel.NoteMessage = viewModel.NoteMessage;
            domainModel.NotesCategory = viewModel.NotesCategory.GetValueOrDefault();
            domainModel.NoteName = viewModel.NoteName;

            domainModel.CheckModel();
            return domainModel;
        }
    }
}
