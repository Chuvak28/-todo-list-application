using System;
using System.Collections.Generic;
using System.Text;
using ToDo.BLL.Entity;
using ToDo.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace ToDo.BLL.Operations
{
    public class ToDoEntryOperations : ITodoEntryDataProvider
    {
        public string Create(TODOEntry item)
        {
            throw new NotImplementedException();
        }

        public TODOEntry Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<TODOEntry> GetAll()
        {
            throw new NotImplementedException();
        }

        public string Remove(int id)
        {
            throw new NotImplementedException();
        }

        public string SetStatus(int id, string status)
        {
            throw new NotImplementedException();
        }

        public TODOEntry Update(TODOEntry item)
        {
            throw new NotImplementedException();
        }
    }
}
