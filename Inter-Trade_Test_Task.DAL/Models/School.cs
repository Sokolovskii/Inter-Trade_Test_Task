using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.WebSockets;

namespace Inter_Trade_Test_Task.DAL.Models
{
    [Table("Schools")]
    public class School : IModel
    {
        [Key]
        [Column("Id", TypeName = "INTEGER PRIMARY KEY"), Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Range(0, long.MaxValue, ErrorMessage = "Идентификатор должен быть неотрицательным")]
        public long Id { get; set; }

        [Column("Name", TypeName = "TEXT"), Required]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Имя должно иметь значение")]
        public string Name { get; set; }

        [Column("Address", TypeName = "TEXT"), Required]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Адрес должен иметь значение")]
        public string Address { get; set; }
    }
}
