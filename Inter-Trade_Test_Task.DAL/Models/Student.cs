
using Inter_Trade_Test_Task.DAL.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inter_Trade_Test_Task.DAL.Models
{
    [Table("Students")]
    public class Student : IModel
    {
        [Column("Id", TypeName = "INTEGER PRIMARY KEY"), Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Range(0, long.MaxValue, ErrorMessage = "Идентификатор должен быть неотрицательным")]
        public long Id { get; set; }

        [Column("FirstName", TypeName = "TEXT"), Required]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Имя должно иметь значение")]
        public string FirstName { get; set; }

        [Column("LastName", TypeName = "TEXT"), Required]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Фамилия должна иметь значение")]
        public string LastName {  get; set; }

        [Column("BirthDate", TypeName = "DATETIME"), Required]
        [BirthDate(ErrorMessage = "Укажите корректную дату рождения")]
        public DateTime BirthDate { get; set; }

        [Column("ClassId", TypeName = "INTEGER"), ForeignKey("Classes"), Required]
        [Range(1, long.MaxValue, ErrorMessage = "Идентификатор должен быть положительным")]
        public long ClassId { get; set; }

    }
}
