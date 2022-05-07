﻿using System;
using System.Collections.Generic;
using System.Text;
using ToDo.BLL.Entity;

namespace ToDo.BLL.Interfaces
{
    public interface ITodoEntryDataProvider
    {
        public string Create(TODOEntry item);
        public string Remove(int id);
        public TODOEntry Update(TODOEntry item);
        public List<TODOEntry> GetAll();
        public TODOEntry Get(int id);
        public string SetStatus(int id, string status);
    }
}
