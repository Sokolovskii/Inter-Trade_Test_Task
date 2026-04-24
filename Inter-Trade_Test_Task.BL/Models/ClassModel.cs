using Inter_Trade_Test_Task.BL.ApiDTO;
using Inter_Trade_Test_Task.DAL.DTO;

namespace Inter_Trade_Test_Task.BL.Models
{
    public class ClassModel : IModel<ClassDTO, ClassApiDTO>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long SchoolId { get; set; }

        public ClassModel() 
        {}

        public bool IsValid(bool IsInsertion)
        {
            return (IsInsertion || Id>0) &&
                Name != null && Name != default &&
                SchoolId > 0;
        }

        public void FillModelFromDTO(ClassApiDTO dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            SchoolId = dto.SchoolId;
        }

        public void FillModelFromDTO(ClassDTO dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            SchoolId = dto.SchoolId;
        }

        public ClassApiDTO ModelToAPI() 
        { 
            return new ClassApiDTO() 
            { 
                Id = Id,
                Name = Name,
                SchoolId = SchoolId,
            };
        }

        public ClassDTO ModelToDTO()
        {
            return new ClassDTO()
            {
                Id = Id,
                Name = Name,
                SchoolId = SchoolId
            };
        }
    }
}
