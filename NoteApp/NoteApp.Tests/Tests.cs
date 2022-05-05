using System;
using System.Linq;
using NUnit.Framework;
using Newtonsoft.Json;
using NoteApp.Domain;
using NoteApp.Domain.Enums;
using NoteApp.Domain.Models;

namespace NoteApp.Tests
{
    [TestFixture]
    public class Tests
    {
        private static string _path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NotesProjectData\\NotesProject.json";
        private readonly NotesManager notesManager = new NotesManager(_path);
        private Project testProject = new Project();

        [SetUp]
        public void TestInitialize()
        {
            var note1 = new NoteModel()
            {
                Id= 1,
                NoteMessage = "message1",
                NoteName = "name1",
                NotesCategory = NotesCategories.Home,
            };
            var note2 = new NoteModel()
            {
                Id= 2,
                NoteMessage = "message2",
                NoteName = "name2",
                NotesCategory = NotesCategories.Home,
            };
            var note3 = new NoteModel()
            {
                Id= 2,
                NoteMessage = "message2",
                NoteName = "name2",
                NotesCategory = NotesCategories.Finances,
            };

            testProject.NoteModels.Add(note1);
            testProject.NoteModels.Add(note2);
            testProject.NoteModels.Add(note3);

            notesManager.SaveChanges(testProject);
        }

        [Test]
        public void GetAllNotesTest()
        {
            // Act
            var notes = this.notesManager.GetNotesProject(null);

            // Assert
            var index = 0;
            foreach (var note in notes)
            {
                Assert.AreEqual(note.Id, testProject.NoteModels[index].Id);
                index++;
            }
        }

        [Test]
        [TestCase(NotesCategories.Home)]
        [TestCase(NotesCategories.Finances)]
        public void GetNotesByCategory(NotesFilter filter)
        {
            // Act 
            var notes = this.notesManager.GetNotesProject(filter);

            // Assert
            var index = 0;
            foreach(var testNote in testProject.NoteModels.Where(n => n.NotesCategory == filter.CategoryFilter))
            {
                Assert.AreEqual(testNote.Id, notes[index].Id);
                index++;
            }
        }
    }
}