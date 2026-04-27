using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inter_Trade_Test_Task.DAL.Models
{
    [Table("Classes")]
    public class Class : IModel
    {
        [Column("Id", TypeName = "INTEGER PRIMARY KEY"), Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("Name", TypeName = "TEXT"), Required]
        public string Name { get; set; }

        [Column("SchoolId", TypeName = "INTEGER"), ForeignKey("School"), Required]
        public long SchoolId { get; set; }

        public bool IsValid()
        {
            return Id>=0 &&
                Name != null && Name != string.Empty &&
                SchoolId > 0;
        }
    }
}
