using System;
using System.Collections.Generic;
using ToDo.BLL.Entity;
using ToDo.BLL.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            if (name.Length > 30)
            {
                throw new ArgumentOutOfRangeException(nameof(name), "Name should not be more 30 characters");
            }

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
            TODOList getList = db.Lists.Find(id);

            if (getList is null)
            {
                throw new ArgumentNullException(nameof(getList), "List with such id not exists ");
            }

            return getList;
        }

        public List<TODOList> GetAll()
        {
            var list = db.Lists.ToList();

            return list;
        }

        public bool Remove(int id)
        {
            TODOList removeList = Get(id);
            
            db.Lists.Remove(removeList);
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (DbUpdateException) 
            { 
                return false; 
            }
        }

        public TODOList Update(TODOList item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "List is null for update");
            }
            db.Lists.Update(item);
            db.SaveChanges();
            return item;
        }
    }
}
