using Microsoft.EntityFrameworkCore;
namespace Project_To_Do_List.Models
{
    public class DbConnect:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //tao doi tuong de doc thong tin cua file appsettings.json
            var builder = new ConfigurationBuilder();
            //set duong dan cua file appsettings.json
            builder.SetBasePath(Directory.GetCurrentDirectory());
            //add file appsettings.json vao doi tuong builder
            builder.AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            //doc chuoi ket noi o trong file appsettings.json
            string strDbConnectString = configuration.GetConnectionString("DbConnectString").ToString();
            //ket noi voi csdl thong qua chuoi ket noi
            optionsBuilder.UseSqlServer(strDbConnectString);
        }

        public DbSet<ItemToDo> ToDoList { get; set; }
        public DbSet<UserRecord> UserRecords { get; set; }
    }
}
