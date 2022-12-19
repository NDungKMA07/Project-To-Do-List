using Microsoft.EntityFrameworkCore;
namespace Project_To_Do_List.Models
{
    public class DbConnect:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string str = "Server = NGUYENDUNG\\SQLEXPRESS;Database = TodoList; UID = sa; Password = 123; Trust Server Certificate = true";
            optionsBuilder.UseSqlServer(str);   
        }

        public DbSet<ItemToDo> ToDoList { get; set; }   
    }
}
