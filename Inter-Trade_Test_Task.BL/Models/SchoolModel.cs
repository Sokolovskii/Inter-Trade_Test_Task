using SchoolApiDto = Inter_Trade_Test_Task.WebApi.ApiDTO.SchoolDTO;
using SchoolEntityDto = Inter_Trade_Test_Task.DAL.DTO.SchoolDTO;

namespace Inter_Trade_Test_Task.BL.Models
{
    public class SchoolModel : IModel<SchoolEntityDto, SchoolApiDto>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public SchoolModel() 
        {}

        public bool IsValid()
        {
            return
                Id > 0 &&
                Name != null && Name != default &&
                Address != null && Address != default;
        }

        public void FillModelFromDTO(SchoolEntityDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            Address = dto.Address;
        }

        public void FillModelFromDTO(SchoolApiDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            Address = dto.Address;
        }

        public SchoolApiDto ModelToAPI()
        {
            return new SchoolApiDto()
            {
                Id = Id,
                Name = Name,
                Address = Address
            };
        }

        public SchoolEntityDto ModelToDTO()
        {
            return new SchoolEntityDto()
            {
                Id = Id,
                Address = Address,
                Name = Name
            };
        }
    }
}
