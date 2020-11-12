using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NoteApp.Domain.Enums;
using NoteApp.Domain.Models;

namespace NoteApp.Domain
{
    public interface INotesManager
    {
        /// <summary>
        /// Сохраняем json файл с объектами заметок.
        /// </summary>
        /// <param name="project">Экземпляр списка заметок.</param>
        void EditNotes(Project project);

        /// <summary>
        /// Получение из десериализованного json файла объекта Project.
        /// </summary>
        /// <returns></returns>
        Project GetNotesProject(NotesCategories? category = null);
    }

    public class NotesManager : INotesManager
    {
        private static string _path = Directory.GetCurrentDirectory();

        private JsonSerializer serializer = new JsonSerializer();
        private StreamWriter sw = new StreamWriter(_path);
        private StreamReader sr = new StreamReader(_path);

        public void EditNotes(Project project)
        {
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, project);
            }
        }

        public Project GetNotesProject(NotesCategories? category = null)
        {
            Project project = new Project();

            try
            {
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    project = (Project)serializer.Deserialize<Project>(reader);
                }
            }
            catch  
            {
                throw new Exception("An error while deserialization occured");
            }

            if (category != null)
            {
                var filteredNotes = project.NoteModels.Where(n => n.NotesCategory == category).ToList();
                project.NoteModels = filteredNotes;
            }
            
            return project;
        }
    }
}
