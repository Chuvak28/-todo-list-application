using Microsoft.EntityFrameworkCore;

namespace ToDo.BLL.Entity
{
    public class ToDoListDbContext : DbContext
    {
        internal DbSet<TODOList> Lists { get; set; }
        internal DbSet<TODOEntry> Entries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=todolistdb;Trusted_Connection=True;");
        }
    }
}
