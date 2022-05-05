using NoteApp.Domain.Enums;

namespace NoteApp.Domain.Models
{
    public class NotesFilter
    {
        /// <summary>Фильтр по категории записки.</summary>
        public NotesCategories? CategoryFilter { get; set; }

        /// <summary>Фильтр по названию записки.</summary>
        public string TextNameFilter { get; set; }
    }
}
