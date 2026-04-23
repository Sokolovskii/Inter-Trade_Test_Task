using ClassApiDto = Inter_Trade_Test_Task.WebApi.ApiDTO.ClassDTO;
using ClassEntityDto = Inter_Trade_Test_Task.DAL.DTO.ClassDTO;

namespace Inter_Trade_Test_Task.BL.Models
{
    public class ClassModel : IModel<ClassEntityDto, ClassApiDto>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long SchoolId { get; set; }

        public ClassModel() 
        {}

        public bool IsValid()
        {
            return Id>0 &&
                Name != null && Name != default &&
                SchoolId > 0;
        }

        public void FillModelFromDTO(ClassApiDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            SchoolId = dto.SchoolId;
        }

        public void FillModelFromDTO(ClassEntityDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            SchoolId = dto.SchoolId;
        }

        public ClassApiDto ModelToAPI() 
        { 
            return new ClassApiDto() 
            { 
                Name = Name,
                SchoolId = SchoolId,
            };
        }

        public ClassEntityDto ModelToDTO()
        {
            return new ClassEntityDto()
            {
                Id = Id,
                Name = Name,
                SchoolId = SchoolId
            };
        }
    }
}
