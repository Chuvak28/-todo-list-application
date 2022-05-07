using System.Collections.Generic;
using ToDo.BLL.Entity;

namespace ToDo.BLL.Interfaces
{
    public interface ITodoListDataProvider
    {
        public TODOList Create(string listName);
        public int Remove(int id);
        public TODOList Update(TODOList item);
        public List<TODOList> GetAll();
        public TODOList Get(int id);
    }
}
