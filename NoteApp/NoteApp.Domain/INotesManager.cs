using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Получение из десериализованного json файла объекта Project.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns><see cref="Project"/>.</returns>
        List<NoteModel> GetNotesProject(NotesFilter filter = null);

        /// <summary>
        /// Получение из десериализованного json файла объекта Project.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns><see cref="Project"/>.</returns>
        List<NoteModel> GetNotesProjectById(int id);

        /// <summary>
        /// Редактирование заметки.
        /// </summary>
        /// <param name="project">Экземпляр проекта.</param>
        /// <param name="model">Заменяемая модель.</param>
        void EditNotes(NoteModel model);

        /// <summary>
        /// Удаление заметки
        /// </summary>
        /// <param name="project">Экземпляр проекта.</param>
        /// <param name="modelId"> ID Удаляемой модели.</param>
        void DeleteNote(int modelId);

        /// <summary>
        /// Сохранение записи.
        /// </summary>
        /// <param name="project">Экземпляр проекта.</param>
        /// <param name="model">Вставляемая модель.</param>
        void AddNote(NoteModel model);

        /// <summary>
        /// Удаление всех заметок.
        /// </summary>
        void DeleteAll();
    }

    internal class NotesManager : INotesManager
    {
        private static string _path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string _projectFolderName = "\\NotesProjectData";
        private static string _projectFileName = "\\NotesProject.json";

        private static string _folderFullPath = _path + _projectFolderName;
        private static string _fileFullPath = _path + _projectFolderName + _projectFileName;

        private JsonSerializer serializer;
        private StreamWriter sw;
        private Project project;

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

            this.project = new Project();

            this.LoadFromFile();
        }

        public List<NoteModel> GetNotesProject(NotesFilter filter)
        {
            var query = project.NoteModels.AsEnumerable();

            if (filter?.CategoryFilter != null)
            {
                query = query.Where(n => n.NotesCategory == filter.CategoryFilter);
            }

            if (filter?.TextNameFilter != null)
            {
                query = query.Where(n => n.NoteMessage.StartsWith(filter.TextNameFilter));
            }

            return query.ToList();
        }

        public List<NoteModel> GetNotesProjectById(int id)
        {
            this.LoadFromFile();

            return this.project.NoteModels.Where(n => n.Id == id).ToList();
        }

        public void EditNotes(NoteModel model)
        {
            var noteToEdit = this.project.NoteModels.FirstOrDefault(n => n.Id == model.Id);
            this.project.NoteModels.Remove(noteToEdit);
            this.project.NoteModels.Add(model);
            this.SaveChanges(this.project);
        }

        public void DeleteNote(int modelId)
        {
            var model = this.project.NoteModels.First(n => n.Id == modelId);
            this.project.NoteModels.Remove(model);
            this.SaveChanges(this.project);
        }

        public void AddNote(NoteModel model)
        {
            this.project.NoteModels.Add(model);
            if (!this.project.IdentifierSequence.HasValue)
            {
                this.project.IdentifierSequence = 0;
            }
            
            model.Id = (int)this.project.IdentifierSequence;
            this.project.IdentifierSequence++;
            this.SaveChanges(this.project);
        }

        public void DeleteAll()
        {
            this.project.NoteModels.Clear();
            this.SaveChanges(this.project);
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

        private void LoadFromFile()
        {
            try
            {
                using (StreamReader file = File.OpenText(_fileFullPath))
                {
                    this.project = (Project)serializer.Deserialize(file, typeof(Project)) ?? new Project();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"An error while deserialization occured: { e.Message }");
            }
        }

        private void CreateProjectFolderIfNotExists(string path)
        {
            if (Directory.Exists(path))
            {
                return;
            }

            Directory.CreateDirectory(path);
        }
    }
}
