using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NoteApp.Domain.Enums;

namespace NoteApp.ViewModels
{
    public class NoteViewModel
    {
        [JsonProperty("Id")]
        public int? Id { get; set; }

        [JsonProperty("NoteName")]
        public string NoteName { get; set; }

        [JsonProperty("NoteMessage")]
        public string NoteMessage { get; set; }


        /// <summary>
        /// Категория заметки.
        /// </summary>
        public NotesCategories? NotesCategory { get; set; }

        /// <summary>
        /// Время создания.
        /// </summary>
        public DateTime? CreationTime { get; }

        /// <summary>
        /// Время редактирования
        /// </summary>
        public DateTime? ChangeDate { get; set; }
    }
}
