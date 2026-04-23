using Inter_Trade_Test_Task.BL.Models;
using Inter_Trade_Test_Task.DAL.DTO;

namespace Inter_Trade_Test_Task.BL.Service
{
    public interface IService<TModel, TApiDto> where TModel : IModel<IDtoEntity, TApiDto>
    {
        public  void Insert(TApiDto dto);
        public void Update(TApiDto dto);
        public void Delete(long id);
        public Task<TApiDto> GetById(long id);
        public Task<List<TApiDto>> Get();

    }
}
