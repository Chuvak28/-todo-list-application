using System;
using System.Collections.Generic;
using ToDo.BLL.Entity;
using ToDo.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace ToDo.BLL.Operations
{
    public class ToDoEntryOperations : ITodoEntryDataProvider
    {
        private ToDoListDbContext db { get; set; }
        public ToDoEntryOperations()
        {
            db = new ToDoListDbContext();
        }

        public ToDoEntryOperations(ToDoListDbContext dbContext)
        {
            db = dbContext;
        }
        public TODOEntry Create(TODOEntry item)
        {
            if (String.IsNullOrEmpty(item.title))
            {
                throw new ArgumentNullException(nameof(item), "Title is empty or null");
            }

            if (item.title.Length > 30)
            {
                throw new ArgumentOutOfRangeException(nameof(item.title), "Title should not be more 30 characters");
            }

            if (item is null)
            {
                throw new ArgumentNullException(nameof(item),"Entry is null");
            }

            db.Entries.Add(item);
            db.SaveChanges();

            return item;
        }

        public TODOEntry Get(int id)
        {
            TODOEntry getList = db.Entries.Find(id);

            if (getList is null)
            {
                throw new ArgumentNullException(nameof(getList), "Entry with such id not exists ");
            }

            return getList;
        }

        public List<TODOEntry> GetAll()
        {
            var list = db.Entries.ToList();

            return list;
        }

        public bool Remove(int id)
        {
            TODOEntry removeList = Get(id);
            db.Entries.Remove(removeList);
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

        public TODOEntry Update(TODOEntry item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "List is null for update");
            }
            db.Entries.Update(item);
            db.SaveChanges();
            return item;
        }
    }
}
