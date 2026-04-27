
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inter_Trade_Test_Task.DAL.Models
{
    [Table("Student")]
    public class Student : IModel
    {
        [Column("Id", TypeName = "INTEGER"), Key]
        public long Id { get; set; }

        [Column("FirstName", TypeName = "TEXT")]
        public string FirstName { get; set; }

        [Column("LastName", TypeName = "TEXT")]
        public string LastName {  get; set; }

        [Column("BirthDate", TypeName = "DATETIME")]
        public DateTime BirthDate { get; set; }

        [Column("ClassId", TypeName = "INTEGER")]
        public long ClassId { get; set; }

        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }

        public bool IsValid()
        {
            return Id > 0 &&
                FirstName != null && FirstName != string.Empty &&
                LastName != null && LastName != string.Empty &&
                BirthDate != default &&
                ClassId > 0;
        }
    }
}
