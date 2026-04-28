using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inter_Trade_Test_Task.DAL.Models
{
    [Table("Classes")]
    public class Class : IModel
    {
        [Column("Id", TypeName = "INTEGER PRIMARY KEY"), Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Range(0, long.MaxValue, ErrorMessage = "Идентификатор должен быть неотрицательным")]
        public long Id { get; set; }

        [Column("Name", TypeName = "TEXT"), Required]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Имя должно иметь значение")]
        public string Name { get; set; }

        [Column("SchoolId", TypeName = "INTEGER"),ForeignKey("Schools"), Required]
        [Range(1, long.MaxValue, ErrorMessage = "Идентификатор должен быть неотрицательным")]
        public long SchoolId { get; set; }
    }
}
