using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inter_Trade_Test_Task.DAL.Models
{
    [Table("Schools")]
    public class School : IModel
    {
        [Key]
        [Column("Id", TypeName = "INTEGER PRIMARY KEY"), Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("Name", TypeName = "TEXT"), Required]
        public string Name { get; set; }

        [Column("Address", TypeName = "TEXT"), Required]
        public string Address { get; set; }

        public bool IsValid()
        {
            return
                Id >= 0 &&
                Name != null && Name != string.Empty &&
                Address != null && Address != string.Empty;
        }
    }
}
