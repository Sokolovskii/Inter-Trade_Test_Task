using Inter_Trade_Test_Task.BL.Models;
using Inter_Trade_Test_Task.DAL.DTO;
using Inter_Trade_Test_Task.DAL.Repository;
using System.Linq;

namespace Inter_Trade_Test_Task.BL.Service
{
    public class Service<TModel, TApiDto> : IService<TModel, TApiDto> where TModel : IModel<IDtoEntity, TApiDto>, new( )
    {
        private readonly IAsyncRepository<IDtoEntity> _repo;
        public Service(IAsyncRepository<IDtoEntity> repo)
        {
            _repo = repo;
        }
        public void Delete(long id)
        {
            if(id > 0) _repo.RemoveAsync(id);
            else throw new ArgumentException("Идентификатор записи должен быть больше 0");
        }

        public async Task<List<TApiDto>> Get()
        {
            var dtos = await _repo.Get();
            return [.. dtos.Select(e=> 
            {
                var model = new TModel();
                model.FillModelFromDTO(e);
                return model.ModelToAPI();
            })];
            
        }

        public async Task<TApiDto> GetById(long id)
        {
            if (id <= 0) throw new ArgumentException("Идентификатор записи должен быть больше 0");
            var dto = await _repo.GetById(id);
            var model = new TModel();
            model.FillModelFromDTO(dto);
            return model.ModelToAPI();
        }

        public void Insert(TApiDto dto)
        {
            var model = new TModel();
            model.FillModelFromDTO(dto);
            if(!model.IsValid()) throw new ArgumentException("Запись содержит отсутствующие поля, либо ее идентификатор меньше или равен 0");
            _repo.InsertAsync(model.ModelToDTO());
        }

        public void Update(TApiDto dto)
        {
            var model = new TModel();
            model.FillModelFromDTO(dto);
            if (!model.IsValid()) throw new ArgumentException("Запись содержит отсутствующие поля, либо ее идентификатор меньше или равен 0");
            _repo.UpdateAsync(model.ModelToDTO());
        }
    }
}
