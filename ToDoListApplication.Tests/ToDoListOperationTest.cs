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
    public class ToDoListOperationTest
    {
        private ToDoListOperations listOperation;
        private Mock<DbSet<TODOList>> mockSet;
        private Mock<ToDoListDbContext> mockContext;

        //We will run setup before each test, to fit FIRST paradigm
        // https://medium.com/@tasdikrahman/f-i-r-s-t-principles-of-testing-1a497acda8d6
        [SetUp]
        public void SetUp() 
        {
            this.mockSet = new Mock<DbSet<TODOList>>();

            var fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            
            var lockedtodoLists = fixture.Build<TODOList>().With(u => u.isVisible, true).Create();

            var todoLists = new List<TODOList>
            {
                lockedtodoLists,
                fixture.Build<TODOList>().With(u => u.isVisible, false).Create(),
            }.AsQueryable();

            this.mockSet.As<IQueryable<TODOList>>().Setup(m => m.Provider).Returns(todoLists.Provider);
            this.mockSet.As<IQueryable<TODOList>>().Setup(m => m.Expression).Returns(todoLists.Expression);
            this.mockSet.As<IQueryable<TODOList>>().Setup(m => m.ElementType).Returns(todoLists.ElementType);
            this.mockSet.As<IQueryable<TODOList>>().Setup(m => m.GetEnumerator()).Returns(todoLists.GetEnumerator());

            this.mockContext = new Mock<ToDoListDbContext>();
            this.mockContext.Setup(m => m.Lists).Returns(this.mockSet.Object);
            this.listOperation = new ToDoListOperations(this.mockContext.Object);

        }

        [TestCase("Clean Room")]
        public void Create(string str)
        {
            var result = this.listOperation.Create(str);
            
            // We not going to deal with autoincrement, it possible, but will take much more time, let's check all over params
            
            this.mockSet.Verify(x => x.Add(It.Is<TODOList>(u => u.name == str && u.isVisible == true)), Times.Once);

            this.mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestCase(null)]
        public void CreateListNameNull(string str)
        {
            Assert.Throws<ArgumentNullException>(() => this.listOperation.Create(str), "Should throw an ArgumentNullException.");
        }

        // Don't use autoincremend identificator in your test, it should be a separate mechanism
        [TestCase("Work", false)]
        public void Update(string modifyName, bool isVisible)
        {
            // find any autofilled by Fixture entity
            var anyEntity = this.listOperation.GetAll().FirstOrDefault();

            // Next find specific
            TODOList item = this.listOperation.Get(anyEntity.id);

            // Change item after adding
            item.isVisible = false;
            item.name += "_Updated";

            var result = this.listOperation.Update(item);
            Assert.IsNotNull(result);
            Assert.IsTrue(string.Compare(result.name, item.name, StringComparison.InvariantCultureIgnoreCase) == 0); // Check the name updated

            this.mockSet.Verify(m => m.Update(It.IsAny<TODOList>()), Times.Once());
            this.mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestCase(null)]
        public void UpdateListNull(TODOList item)
        {
            Assert.Throws<ArgumentNullException>(() => this.listOperation.Update(item), "Should throw an ArgumentNullException.");
        }

        [TestCase(-1)]
        public void RemoveOutOfRange(int listId)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => this.listOperation.Remove(listId), "Remove should throw an ArgumentOutOfRangeException.");
        }
    }
}
