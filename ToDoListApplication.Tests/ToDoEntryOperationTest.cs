using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ToDo.BLL.Entity;
using ToDo.BLL.Operations;

namespace ToDoListApplication.Tests
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "<Ожидание>")]
    [TestFixture]
    public class ToDoEntryOperationTest
    {
        private readonly ToDoEntryOperations entryOperation = new ToDoEntryOperations();

        [TestCase("Clean Room", "wash window", "10/10/2022", true, 3)]
        public void Create(string title, string description, DateTime dt, bool isDone, int id)
        {
            TODOEntry newentity = new TODOEntry()
            {
                title = title,
                description = description,
                dueDate = dt,
                isDone = isDone,
                listid = id,
            };
            var result = this.entryOperation.Create(newentity);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.title, title);
            Assert.IsTrue(result.id != 0);
        }

        [TestCase(null)]
        public void CreateListNameNull(TODOEntry item)
        {
            Assert.Throws<ArgumentNullException>(() => this.entryOperation.Create(item), "Should throw an ArgumentNullException.");
        }

        [TestCase("Clean Room", "wash floor", "10/10/2022", false, 3)]
        public void Update(string title, string description, DateTime dt, bool isDone, int id)
        {
            TODOEntry newentity = new TODOEntry()
            {
                title = title,
                description = description,
                dueDate = dt,
                isDone = isDone,
                listid = id,
            };

            var result = this.entryOperation.Update(newentity);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.title, title);
            Assert.IsTrue(result.id != 0);
        }

        [TestCase(-1)]
        public void RemoveOutOfRange(int listId)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => this.entryOperation.Remove(listId), "Remove should throw an ArgumentOutOfRangeException.");
        }
    }
}
