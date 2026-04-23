using StudentApiDto = Inter_Trade_Test_Task.WebApi.ApiDTO.StudentDTO;
using StudentEntityDto = Inter_Trade_Test_Task.DAL.DTO.StudentDTO;

namespace Inter_Trade_Test_Task.BL.Models
{
    public class StudentModel : IModel<StudentEntityDto, StudentApiDto>
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

        public StudentEntityDto ModelToDTO()
        {
            return new StudentEntityDto()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                BirthDate = BirthDate,
                ClassId = ClassId
            };
        }

        public StudentApiDto ModelToAPI()
        {
            return new StudentApiDto()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                BirthDate = BirthDate,
                ClassId = ClassId
            };
        }

        public void FillModelFromDTO(StudentEntityDto dto)
        {
            Id = dto.Id;
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            BirthDate = dto.BirthDate;
            ClassId = dto.ClassId;
        }

        public void FillModelFromDTO(StudentApiDto dto)
        {
            Id = dto.Id;
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            BirthDate = dto.BirthDate;
            ClassId = dto.ClassId;
        }
    }
}
