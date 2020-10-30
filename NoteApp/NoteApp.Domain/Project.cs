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
        /// Список заметок
        /// </summary>
        public List<NoteModel> NoteModels { get; set; } = new List<NoteModel>();
    }
}
