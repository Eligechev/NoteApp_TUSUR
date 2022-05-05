using System;
using System.Collections.Generic;
using System.Text;
using NoteApp.Domain.Models;

namespace NoteApp.Domain
{
    /// <summary>
    /// Содержит в себе список заметок.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Инициализация списка записок.
        /// </summary>
        public Project()
        {
            this.NoteModels = new List<NoteModel>();    
        }
        
        /// <summary>
        /// Список заметок.
        /// </summary>
        public List<NoteModel> NoteModels { get; set; }
        
        /// <summary>
        /// Счетчик для отслеживания последнего добавленного идентификатора с целью избежнания коллизии данных.
        /// </summary>
        public int? IdentifierSequence { get; set; }
    }
}
