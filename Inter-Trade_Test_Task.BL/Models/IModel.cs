using Inter_Trade_Test_Task.DAL.DTO;

namespace Inter_Trade_Test_Task.BL.Models
{
    public interface IModel<TDtoEntity, TApiDTO>  where TDtoEntity : IDtoEntity
    {
        long Id { get; set; }
        public TApiDTO ModelToAPI();
        public TDtoEntity ModelToDTO();
        public void FillModelFromDTO(TDtoEntity dto);
        public void FillModelFromDTO(TApiDTO dto);

        public bool IsValid(bool IsInsertion);
    }
}
