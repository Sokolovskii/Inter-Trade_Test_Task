
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inter_Trade_Test_Task.DAL.Models
{
    [Table("Students")]
    public class Student : IModel
    {
        [Column("Id", TypeName = "INTEGER PRIMARY KEY"), Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("FirstName", TypeName = "TEXT"), Required]
        public string FirstName { get; set; }

        [Column("LastName", TypeName = "TEXT"), Required]
        public string LastName {  get; set; }

        [Column("BirthDate", TypeName = "DATETIME"), Required]
        public DateTime BirthDate { get; set; }

        [Column("ClassId", TypeName = "INTEGER"), ForeignKey("Classes"), Required]
        public long ClassId { get; set; }

        public bool IsValid()
        {
            return Id >= 0 &&
                FirstName != null && FirstName != string.Empty &&
                LastName != null && LastName != string.Empty &&
                BirthDate != default &&
                ClassId > 0;
        }
    }
}
