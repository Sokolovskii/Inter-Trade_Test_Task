using System.ComponentModel.DataAnnotations;

namespace Inter_Trade_Test_Task.DAL.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BirthDateAttribute : ValidationAttribute
    {
        private readonly int _maxAge;

        public BirthDateAttribute(int maxAge = 100)
        {
            _maxAge = maxAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                var birthDate = (DateTime)value;

                if (birthDate > DateTime.Now.Date)
                {
                    return new ValidationResult(
                        ErrorMessage ?? "Дата рождения не может быть в будущем"
                    );
                }

                var minBirthDate = DateTime.Now.Date.AddYears(-_maxAge);
                if (birthDate < minBirthDate)
                {
                    return new ValidationResult(
                        ErrorMessage ?? $"Возраст не может превышать {_maxAge} лет"
                    );
                }
            }
            else
            {
                return new ValidationResult("Некорректный тип данных для даты рождения");
            }

            return ValidationResult.Success;
        }
    }
}
