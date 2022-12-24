using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project_To_Do_List.Models
{
    [Table("Content")]
    public class ItemToDo
    {
        [Key]
        public int ID { get; set; }
        public int IdUser { get; set; }
        public string Text { get; set; }
        public Boolean Status { get; set; }

 
    }
}

