using Microsoft.EntityFrameworkCore;

namespace ToDo.BLL.Entity
{
    public class ToDoListDbContext : DbContext
    {
        public virtual DbSet<TODOList> Lists { get; set; }
        public virtual DbSet<TODOEntry> Entries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=todolistdb;Trusted_Connection=True;");
        }
    }
}
