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
    public class ToDoListOperationTest
    {
        private readonly ToDoListOperations listOperation = new ToDoListOperations();

        [TestCase("Clean Room")]
        public void Create(string str)
        {
            var result = this.listOperation.Create(str);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.name, str);
            Assert.IsTrue(result.id != 0);
        }

        [TestCase(null)]
        public void CreateListNameNull(string str)
        {
            Assert.Throws<ArgumentNullException>(() => this.listOperation.Create(str), "Should throw an ArgumentNullException.");
        }

        [TestCase(4, "Work", false)]
        public void Update(int listId, string modifyName, bool isVisible)
        {
            TODOList item = new TODOList()
            {
                id = listId,
                name = modifyName,
                isVisible = isVisible,
            };

            var result = this.listOperation.Update(item);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.name, modifyName);
            Assert.IsTrue(result.id != 0);
        }

        [TestCase(9, null, false)]
        public void ModifyListNull(int listId, string modifyName, bool isVisible)
        {
            TODOList item = new TODOList()
            {
                id = listId,
                name = modifyName,
                isVisible = isVisible,
            };
            Assert.Throws<ArgumentNullException>(() => this.listOperation.Update(item), "Should throw an ArgumentNullException.");
        }

        [TestCase(-1)]
        public void RemoveOutOfRange(int listId)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => this.listOperation.Remove(listId), "Remove should throw an ArgumentOutOfRangeException.");
        }

        [TestCase(10)]
        public void Remove(int id)
        {
            var result = this.listOperation.Remove(id);
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "List removed succesfully.");
        }
    }
}
