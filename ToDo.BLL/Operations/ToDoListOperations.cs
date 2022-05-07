using System;
using System.Collections.Generic;
using System.Text;
using ToDo.BLL.Entity;
using ToDo.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace ToDo.BLL.Operations
{
    public class ToDoListOperations : ITodoListDataProvider
    {
        private ToDoListDbContext db { get; set; }
        public ToDoListOperations()
        {
            db = new ToDoListDbContext();
        }

        public ToDoListOperations(ToDoListDbContext dbContext)
        {
            db = dbContext;
        }

        public TODOList Create(string name)
        {

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "List name is null or empty");
            }

            TODOList newList = new TODOList()
            {
                name = name,
                isVisible = true
            };

            db.Lists.Add(newList);
            db.SaveChanges();

            return newList;
        }

        public TODOList Get(int id)
        {
            TODOList getList = db.Lists.FirstOrDefault(p => p.id == id);

            if (getList is null)
            {
                throw new ArgumentNullException(nameof(getList), "List with such id not exists ");
            }

            return getList;
        }

        public List<TODOList> GetAll()
        {
            List<TODOList> allLists = new List<TODOList>();
            var lists = db.Lists;
            foreach (var list in lists) // query executed and data obtained from database
            {
                allLists.Add(list);
            }

            return allLists;
        }

        public int Remove(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "out of range");
            }

            TODOList removeList = db.Lists.Find(id);
            if (removeList is null)
                throw new ArgumentNullException(nameof(removeList), "List with such ID not found");
            db.Lists.Remove(removeList);
            int returnValue = db.SaveChanges();

            return returnValue;
        }

        public TODOList Update(TODOList item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "List is null for update");
            }
            db.Lists.Update(item);

            return item;
        }
    }
}
