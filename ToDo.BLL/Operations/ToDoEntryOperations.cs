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
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            db.Entries.Add(item);
            db.SaveChanges();

            return item;
        }

        public TODOEntry Get(int id)
        {
            TODOEntry getList = db.Entries.FirstOrDefault(p => p.id == id);

            if (getList is null)
            {
                throw new ArgumentNullException(nameof(getList), "List with such id not exists ");
            }

            return getList;
        }

        public List<TODOEntry> GetAll()
        {
            List<TODOEntry> allLists = new List<TODOEntry>();
            var entries = db.Entries;
            foreach (var entry in entries) // query executed and data obtained from database
            {
                allLists.Add(entry);
            }

            return allLists;
        }

        public int Remove(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "out of range");
            }

            TODOEntry removeList = db.Entries.Find(id);
            if (removeList is null)
                throw new ArgumentNullException(nameof(removeList), "Entry with such ID not found");
            db.Entries.Remove(removeList);
            int returnValue = db.SaveChanges();

            return returnValue;
        }

        public int SetStatus(int id, bool status)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "out of range");
            }

            TODOEntry entry = db.Entries.FirstOrDefault(list => list.id == id);

            entry.isDone = status;

            db.Entries.Update(entry);
            int result = db.SaveChanges();

            return result;
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
