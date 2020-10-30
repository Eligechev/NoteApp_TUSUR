using System;
using System.IO;
using Newtonsoft.Json;
using NoteApp.Domain.Models;

namespace NoteApp.Domain
{
    public interface INotesManager
    {
        /// <summary>
        /// Сохраняем json файл с объектами заметок.
        /// </summary>
        /// <param name="project">Экземпляр списка заметок.</param>
        void AddNotes(Project project);

        /// <summary>
        /// Получение из десериализованного json файла объекта Project.
        /// </summary>
        /// <returns></returns>
        Project GetNotesProject();
    }

    public class NotesManager : INotesManager
    {
        private static string _path = Directory.GetCurrentDirectory();
        private JsonSerializer serializer = new JsonSerializer();
        private StreamWriter sw = new StreamWriter(_path);
        private StreamReader sr = new StreamReader(_path);

        public void AddNotes(Project project)
        {
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, project);
            }
        }

        public Project GetNotesProject()
        {
            Project project = new Project();

            using (JsonReader reader = new JsonTextReader(sr))
            {
                project = (Project) serializer.Deserialize<Project>(reader);
            }

            return project;
        }
    }
}
