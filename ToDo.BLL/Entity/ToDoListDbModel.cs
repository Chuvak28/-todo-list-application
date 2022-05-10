using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.BLL.Entity
{
    public class TODOList
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        public bool isVisible { get; set; }
        public List<TODOEntry> Entries { get; set; } = new List<TODOEntry>();
    }

    public class TODOEntry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime dueDate { get; set; }
        public bool isDone { get; set; }
        public int listid { get; set; }
        public TODOList TODOList { get; set; }
    }
}
