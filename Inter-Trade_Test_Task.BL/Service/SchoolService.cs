using Inter_Trade_Test_Task.BL.ApiDTO;
using Inter_Trade_Test_Task.BL.Models;
using Inter_Trade_Test_Task.DAL.DTO;
using Inter_Trade_Test_Task.DAL.Repository;
using System.Data.SQLite;

namespace Inter_Trade_Test_Task.BL.Service
{
    public class SchoolService : IService<SchoolModel, SchoolApiDTO>
    {
        private readonly IAsyncRepository<SchoolDTO> _repo;
        public SchoolService(IAsyncRepository<SchoolDTO> repo)
        {
            _repo = repo;
        }
        public async Task Delete(long id)
        {
            try
            {
                if (id > 0) await _repo.RemoveAsync(id);
                else throw new ArgumentException("Идентификатор записи должен быть больше 0");
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<SchoolApiDTO>> Get()
        {
            var dtos = await _repo.Get();
            return [.. dtos.Select(e=>
                {
                    var model = new SchoolModel();
                    model.FillModelFromDTO(e);
                    return model.ModelToAPI();
                })];
        }

        public async Task<SchoolApiDTO> GetById(long id)
        {
            if (id <= 0) throw new ArgumentException("Идентификатор записи должен быть больше 0");
            var dto = await _repo.GetById(id);
            var model = new SchoolModel();
            model.FillModelFromDTO(dto);
            return model.ModelToAPI();
        }

        public async Task Insert(SchoolApiDTO dto)
        {
            var model = new SchoolModel();
            model.FillModelFromDTO(dto);
            if (!model.IsValid(true)) throw new ArgumentException("Запись содержит отсутствующие поля, либо ее идентификатор меньше или равен 0");
            await _repo.InsertAsync(model.ModelToDTO());
        }

        public async Task Update(SchoolApiDTO dto)
        {
            var model = new SchoolModel();
            model.FillModelFromDTO(dto);
            if (!model.IsValid(false)) throw new ArgumentException("Запись содержит отсутствующие поля, либо ее идентификатор меньше или равен 0");
            await _repo.UpdateAsync(model.ModelToDTO());
        }
    }
}
