using System;
using System.Collections.Generic;
using System.Text;
using ToDo.BLL.Entity;

namespace ToDo.BLL.Interfaces
{
    public interface ITodoEntryDataProvider
    {
        public TODOEntry Create(TODOEntry item);
        public int Remove(int id);
        public TODOEntry Update(TODOEntry item);
        public List<TODOEntry> GetAll();
        public TODOEntry Get(int id);
        public int SetStatus(int id, bool status);
    }
}
