using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project_To_Do_List.Models
{
    [Table("TableToDo")]
    public class ItemToDo
    {
        [Key]
        public int STT { get; set; }
        public string textToDo { get; set; }
        public int status { get; set; }

 
    }
}

