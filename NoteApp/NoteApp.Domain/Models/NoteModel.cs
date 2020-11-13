using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Converters;
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

        /// <summary>
        /// При инициализации объекта указывается дата создания заметки.
        /// </summary>
        public NoteModel()
        {
            this.CreationTime = DateTime.Now;
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
                if (String.IsNullOrEmpty(_noteName))
                {
                    throw new ArgumentException("Имя заметки не должно быть пустым");
                }

                if (_noteName.Length > 50)
                {
                    throw new ArgumentException("Название не должно содержать более 50 символов");
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
                if (String.IsNullOrEmpty(_noteMessage))
                    throw new ArgumentException("Текст заметки не должен быть пустым");

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
        public DateTime CreationTime { get; }

        /// <summary>
        /// Время редактирования
        /// </summary>
        public DateTime ChangeDate { get; set; }

        private void ChangeEditTime()
        {
            this.ChangeDate = DateTime.Now;
        }
    }
}
