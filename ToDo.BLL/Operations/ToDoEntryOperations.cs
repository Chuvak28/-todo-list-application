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
            // It's a good place to add validation for empty name, or innapropriate name (max lenght limitation)
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
            TODOEntry getList = db.Entries.FirstOrDefault(p => p.id == id); // Are you sure this will return NULL if no any entity with such ID??

            if (getList is null)
            {
                throw new ArgumentNullException(nameof(getList), "List with such id not exists ");
            }

            return getList;
        }

        public List<TODOEntry> GetAll()
        {
            // Rewrite to use LINQ instead of the loop
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

            TODOEntry removeList = db.Entries.Find(id); // In method Get you are using FirstOrDefault, choose one solution
            // It will be greate to reuse code, try to call here method GET to avoid code duplication

            if (removeList is null)
                throw new ArgumentNullException(nameof(removeList), "Entry with such ID not found");
            db.Entries.Remove(removeList);

            int returnValue = db.SaveChanges();

            return returnValue; // Are you sure about return value? Can you read carefully what db.SaveChanges made? 
        }

        // Are we still need SetStatus if we have Update method? 
        public int SetStatus(int id, bool status)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "out of range");
            }

            TODOEntry entry = db.Entries.FirstOrDefault(list => list.id == id); // Same here, if you need to find entity, you already have a GET method for this, reuse code

            // https://en.wikipedia.org/wiki/Don%27t_repeat_yourself

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
