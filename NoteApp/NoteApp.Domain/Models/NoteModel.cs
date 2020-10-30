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
        private int id;
        private string noteMessage;
        private string noteName;

        /// <summary>
        /// При инициализации объекта указывается дата создания заметки.
        /// </summary>
        public NoteModel()
        {
            this.CreationTime = DateTime.Now;
        }

        /// <summary>
        /// Имя заметки.
        /// </summary>
        public string NoteName
        {
            get => noteName;
            
            set
            {
                if (String.IsNullOrEmpty(noteName))
                {
                    throw new ArgumentException("Имя заметки не должно быть пустым");
                }

                if (noteName.Length > 50)
                {
                    throw new ArgumentException("Название не должно содержать более 50 символов");
                }

                noteName = value;
                this.ChangeEditTime();
            }
        }

        /// <summary>
        /// Текст заметки.
        /// </summary>
        public string NoteMessage
        {
            get => noteMessage;

            set
            {
                if (String.IsNullOrEmpty(noteMessage))
                    throw new ArgumentException("Текст заметки не должен быть пустым");

                noteMessage = value;
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
