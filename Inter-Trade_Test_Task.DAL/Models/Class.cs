using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inter_Trade_Test_Task.DAL.Models
{
    [Table("Classes")]
    public class Class : IModel
    {
        [Column("Id", TypeName = "INTEGER"), Key]
        public long Id { get; set; }

        [Column("Name", TypeName = "TEXT")]
        public string Name { get; set; }

        [Column("SchoolId", TypeName = "INTEGER")]
        public long SchoolId { get; set; }

        [ForeignKey("SchoolId")]
        public virtual School School { get; set; }

        public bool IsValid()
        {
            return Id>0 &&
                Name != null && Name != default &&
                SchoolId > 0;
        }
    }
}
