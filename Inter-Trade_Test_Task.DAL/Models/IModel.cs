using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inter_Trade_Test_Task.DAL.Models
{
    public interface IModel
    {
        long Id { get; set; }
        public bool IsValid();
    }
}
