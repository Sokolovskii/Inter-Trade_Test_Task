using Inter_Trade_Test_Task.BL.ApiDTO;
using Inter_Trade_Test_Task.DAL.DTO;

namespace Inter_Trade_Test_Task.BL.Models
{
    public class SchoolModel : IModel<SchoolDTO, SchoolApiDTO>
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

        public void FillModelFromDTO(SchoolDTO dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            Address = dto.Address;
        }

        public void FillModelFromDTO(SchoolApiDTO dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            Address = dto.Address;
        }

        public SchoolApiDTO ModelToAPI()
        {
            return new SchoolApiDTO()
            {
                Id = Id,
                Name = Name,
                Address = Address
            };
        }

        public SchoolDTO ModelToDTO()
        {
            return new SchoolDTO()
            {
                Id = Id,
                Address = Address,
                Name = Name
            };
        }
    }
}
