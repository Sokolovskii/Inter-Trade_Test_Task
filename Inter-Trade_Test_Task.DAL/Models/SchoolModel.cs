using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inter_Trade_Test_Task.DAL.Models
{
    [Table("Schools")]
    public class School : IModel
    {
        [Column("Id", TypeName = "INTEGER"), Key]
        public long Id { get; set; }

        [Column("Name", TypeName = "TEXT")]
        public string Name { get; set; }

        [Column("Address", TypeName = "TEXT")]
        public string Address { get; set; }

        public bool IsValid(bool IsInsertion)
        {
            return
                (IsInsertion || Id > 0) &&
                Name != null && Name != default &&
                Address != null && Address != default;
        }
    }
}
