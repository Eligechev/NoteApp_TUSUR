using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using NoteApp.Domain.Enums;
using NoteApp.Domain.Models;

[assembly:InternalsVisibleTo("NoteApp.Tests")]

namespace NoteApp.Domain
{
    public interface INotesManager
    {
        // /// <summary>
        // /// Сохраняем json файл с объектами заметок.
        // /// </summary>
        // /// <param name="project">Экземпляр списка заметок.</param>
        // void EditNotes(Project project);

        /// <summary>
        /// Получение из десериализованного json файла объекта Project.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns><see cref="Project"/>.</returns>
        Project GetNotesProject(NotesFilter filter = null);

        /// <summary>
        /// Редактирование заметки.
        /// </summary>
        /// <param name="project">Экземпляр проекта.</param>
        /// <param name="model">Заменяемая модель.</param>
        void EditNotes(Project project, NoteModel model);

        /// <summary>
        /// Удаление заметки
        /// </summary>
        /// <param name="project">Экземпляр проекта.</param>
        /// <param name="modelId"> ID Удаляемой модели.</param>
        void DeleteNote(Project project, int modelId);

        /// <summary>
        /// Сохранение записи.
        /// </summary>
        /// <param name="project">Экземпляр проекта.</param>
        /// <param name="model">Вставляемая модель.</param>
        void AddNote(Project project, NoteModel model);
    }

    public class NotesManager : INotesManager
    {
        private static string _path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string _projectFolderName = "\\NotesProjectData";
        private static string _projectFileName = "\\NotesProject.json";

        private static string _folderFullPath = _path + _projectFolderName;
        private static string _fileFullPath = _path + _projectFolderName + _projectFileName;

        private JsonSerializer serializer;
        private StreamWriter sw;

        /// <summary>
        /// Базовый публичный конструктор для взаимодействия из контроллера.
        /// </summary>
        public NotesManager() : this(_fileFullPath)
        {
        }

        /// <summary>
        /// Конструктор для работы с тестовым окружением. 
        /// </summary>
        /// <param name="filePath"> Полный путь к файлу.</param>
        internal NotesManager(string filePath)
        {
            _fileFullPath = filePath;

            this.CreateProjectFolderIfNotExists(_folderFullPath);

            this.serializer = new JsonSerializer();
            using (FileStream file = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                this.sw = new StreamWriter(file);
            }
        }

        public Project GetNotesProject(NotesFilter filter)
        {
            Project project = new Project();

            try
            {
                using (StreamReader file = File.OpenText(_fileFullPath))
                {
                    project = (Project)serializer.Deserialize(file, typeof(Project)) ?? new Project();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"An error while deserialization occured: { e.Message }");
            }

            if (filter != null)
            {
                var filteredNotes = project.NoteModels.Where(n => n.NotesCategory == filter.CategoryFilter && n.NoteName.Contains(filter.TextNameFilter)).ToList();
                project.NoteModels = filteredNotes;
            }
            
            return project;
        }

        public void EditNotes(Project project, NoteModel model)
        {
            var noteToEdit = project.NoteModels.FirstOrDefault(n => n.Id == model.Id);
            project.NoteModels.Remove(noteToEdit);
            project.NoteModels.Add(model);
            this.SaveChanges(project);
        }

        public void DeleteNote(Project project, int modelId)
        {
            var model = project.NoteModels.First(n => n.Id == modelId);
            project.NoteModels.Remove(model);
            this.SaveChanges(project);
        }

        public void AddNote(Project project, NoteModel model)
        {
            project.NoteModels.Add(model);
            this.SaveChanges(project);
        }


        internal void SaveChanges(Project project)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_fileFullPath))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, project);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"An error while serialization occured: { e.Message }");
            }
        }

        internal void CreateProjectFolderIfNotExists(string path)
        {
            if (Directory.Exists(path))
            {
                return;
            }

            Directory.CreateDirectory(path);
        }
    }
}
