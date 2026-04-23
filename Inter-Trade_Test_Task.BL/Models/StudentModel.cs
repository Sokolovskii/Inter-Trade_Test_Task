using Inter_Trade_Test_Task.BL.ApiDTO;
using Inter_Trade_Test_Task.DAL.DTO;

namespace Inter_Trade_Test_Task.BL.Models
{
    public class StudentModel : IModel<StudentDTO, StudentApiDTO>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName {  get; set; }
        public DateTime BirthDate { get; set; }
        public long ClassId { get; set; }
        public StudentModel(){ }

        public bool IsValid()
        {
            return Id > 0 &&
                FirstName != null && FirstName != string.Empty &&
                LastName != null && LastName != string.Empty &&
                BirthDate != default &&
                ClassId > 0;
        }

        public StudentDTO ModelToDTO()
        {
            return new StudentDTO()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                BirthDate = BirthDate,
                ClassId = ClassId
            };
        }

        public StudentApiDTO ModelToAPI()
        {
            return new StudentApiDTO()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                BirthDate = BirthDate,
                ClassId = ClassId
            };
        }

        public void FillModelFromDTO(StudentDTO dto)
        {
            Id = dto.Id;
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            BirthDate = dto.BirthDate;
            ClassId = dto.ClassId;
        }

        public void FillModelFromDTO(StudentApiDTO dto)
        {
            Id = dto.Id;
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            BirthDate = dto.BirthDate;
            ClassId = dto.ClassId;
        }
    }
}
