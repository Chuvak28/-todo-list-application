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

        /// <summary>
        /// Setы status of entry true if completed or false if uncompleted.
        /// A return value indicates whether the status changes succeeded or failed.
        /// </summary>
        /// <param name="id">An integer that represents ID of entry</param>
        /// <param name="status">A boolean that represents STATUS of entry and to be modified</param>
        /// <returns>greater than 0 if status changed successfully; otherwise less than 0
        /// if status failed to change</returns>
        public int SetStatus(int id, bool status);
    }
}
