using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inter_Trade_Test_Task.DAL.Models
{
    public interface IModel
    {
        long Id { get; set; }
        public void Validate()
        {
            var result = new List<ValidationResult>();
            if (!Validator.TryValidateObject(this, new ValidationContext(this), result, true))
            {
                var errorMessages = result.Select(e => e.ErrorMessage);
                throw new ArgumentException(string.Join(", ", errorMessages));
            }
        }
    }
}
