using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ToDo.BLL.Entity;
using ToDo.BLL.Operations;

namespace ToDoListApplication.Tests
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "<Ожидание>")]
    [TestFixture]
    public class ToDoEntryOperationTest
    {
        private ToDoEntryOperations entryOperation;
        private Mock<DbSet<TODOEntry>> mockSet;
        private Mock<ToDoListDbContext> mockContext;

        [SetUp]
        public void SetUp()
        {
            this.mockSet = new Mock<DbSet<TODOEntry>>();

            var fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var lockedtodoEntries = fixture.Build<TODOEntry>().With(u => u.isDone, false).Create();

            var todoLists = new List<TODOEntry>
            {
                lockedtodoEntries,
                fixture.Build<TODOEntry>().With(u => u.isDone, true).Create(),
            }.AsQueryable();

            this.mockSet.As<IQueryable<TODOEntry>>().Setup(m => m.Provider).Returns(todoLists.Provider);
            this.mockSet.As<IQueryable<TODOEntry>>().Setup(m => m.Expression).Returns(todoLists.Expression);
            this.mockSet.As<IQueryable<TODOEntry>>().Setup(m => m.ElementType).Returns(todoLists.ElementType);
            this.mockSet.As<IQueryable<TODOEntry>>().Setup(m => m.GetEnumerator()).Returns(todoLists.GetEnumerator());

            this.mockContext = new Mock<ToDoListDbContext>();
            this.mockContext.Setup(m => m.Entries).Returns(this.mockSet.Object);
            this.entryOperation = new ToDoEntryOperations(this.mockContext.Object);
        }

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

            this.mockSet.Verify(x => x.Add(It.Is<TODOEntry>(u => u.title == title && u.isDone == isDone)), Times.Once);
            this.mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestCase(null)]
        public void CreateListNameNull(TODOEntry item)
        {
            Assert.Throws<ArgumentNullException>(() => this.entryOperation.Create(item), "Should throw an ArgumentNullException.");
        }

        [TestCase("Clean Room", "wash floor", "10/10/2022", false, 3)]
        public void Update(string title, string description, DateTime dt, bool isDone, int id)
        {
            var anyEntity = this.entryOperation.GetAll().FirstOrDefault();

            TODOEntry item = this.entryOperation.Get(anyEntity.id);

            // item.isVisible = false;
            // item.name += "_Updated";
            item.isDone = isDone;
            item.title += title;
            item.description = description;
            item.dueDate = dt;
            item.id = id;

            var result = this.entryOperation.Update(item);
            Assert.IsNotNull(result);
            Assert.IsTrue(string.Compare(result.title, item.title, StringComparison.InvariantCultureIgnoreCase) == 0); // Check the name updated

            this.mockSet.Verify(m => m.Update(It.IsAny<TODOEntry>()), Times.Once());
            this.mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestCase(-1)]
        public void RemoveOutOfRange(int listId)
        {
            Assert.Throws<ArgumentNullException>(() => this.entryOperation.Remove(listId), "Remove should throw an ArgumentOutOfRangeException.");
        }
    }
}
