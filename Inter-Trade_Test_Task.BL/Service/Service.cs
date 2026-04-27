using Inter_Trade_Test_Task.DAL.Models;
using Inter_Trade_Test_Task.DAL.Repository;

namespace Inter_Trade_Test_Task.BL.Service
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class, IModel, new()
    {
        private readonly IAsyncRepository<TEntity> _repo;
        public Service(IAsyncRepository<TEntity> repo)
        {
            _repo = repo;
        }
        public async Task Delete(long id)
        {
            if (id > 0) await _repo.RemoveAsync(id);
            else throw new ArgumentException("Идентификатор записи должен быть больше 0");
        }

        public async Task<List<TEntity>> Get()
        {
            return await _repo.Get();
        }

        public async Task<TEntity> GetById(long id)
        {
            if (id <= 0) throw new ArgumentException("Идентификатор записи должен быть больше 0");
            return await _repo.GetById(id);
        }

        public async Task Insert(TEntity dto)
        {
            if (!dto.IsValid()) throw new ArgumentException("Запись содержит отсутствующие поля, либо ее идентификатор меньше или равен 0");
            await _repo.InsertAsync(dto);
        }

        public async Task Update(TEntity dto)
        {
            if (!dto.IsValid()) throw new ArgumentException("Запись содержит отсутствующие поля, либо ее идентификатор меньше или равен 0");
            await _repo.UpdateAsync(dto);
        }
    }
}
