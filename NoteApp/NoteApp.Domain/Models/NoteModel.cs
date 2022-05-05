using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Converters;
using NoteApp.Common;
using NoteApp.Domain.Enums;

namespace NoteApp.Domain.Models
{
    /// <summary>
    /// Сущность заметки.
    /// </summary>
    public class NoteModel
    {
        private string _noteMessage;
        private string _noteName;
        private List<string> _errors;

        /// <summary>
        /// При инициализации объекта указывается дата создания заметки.
        /// </summary>
        public NoteModel()
        {
            this.CreationTime = DateTime.Now;
            _errors = new List<string>();
        }
        
        /// <summary>
        /// Идентификатор заметки. 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя заметки.
        /// </summary>
        public string NoteName
        {
            get => _noteName;
            
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    _errors.Add("Имя заметки не должно быть пустым");
                }

                if (value.Length > 50)
                {
                    _errors.Add("Название не должно содержать более 50 символов");
                }

                _noteName = value;
                this.ChangeEditTime();
            }
        }

        /// <summary>
        /// Текст заметки.
        /// </summary>
        public string NoteMessage
        {
            get => _noteMessage;

            set
            {
                if (String.IsNullOrEmpty(value))
                    _errors.Add("Текст заметки не должен быть пустым");

                _noteMessage = value;
                this.ChangeEditTime();
            }
        }

        /// <summary>
        /// Категория заметки.
        /// </summary>
        public NotesCategories NotesCategory { get; set; }

        /// <summary>
        /// Время создания.
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Время редактирования
        /// </summary>
        public DateTime ChangeDate { get; set; }


        public void CheckModel()
        {
            if (this._errors.Count != 0)
            {
                throw new ValidationException(this._errors);
            }
        }

        private void ChangeEditTime()
        {
            this.ChangeDate = DateTime.Now;
        }
    }
}
