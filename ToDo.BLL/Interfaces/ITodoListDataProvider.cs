using System.Collections.Generic;
using ToDo.BLL.Entity;

namespace ToDo.BLL.Interfaces
{
    public interface ITodoListDataProvider
    {
        public TODOList Create(string name);
        public string Remove(int id);
        public TODOList Update(TODOList item);
        public List<TODOList> GetAll();
        public TODOList Get(int id);
    }
}
