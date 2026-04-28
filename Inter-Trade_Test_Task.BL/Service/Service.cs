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
        public async Task Delete(long id, CancellationToken ct)
        {
            if (id > 0) await _repo.RemoveAsync(id, ct);
            else throw new ArgumentException("Идентификатор записи должен быть больше 0");
        }

        public async Task<List<TEntity>> Get(CancellationToken ct)
        {
            return await _repo.Get(ct);
        }

        public async Task<TEntity> GetById(long id, CancellationToken ct)
        {
            if (id <= 0) throw new ArgumentException("Идентификатор записи должен быть больше 0");
            return await _repo.GetById(id, ct);
        }

        public async Task Insert(TEntity dto, CancellationToken ct)
        {
            dto.Validate();
            await _repo.InsertAsync(dto, ct);
        }

        public async Task Update(TEntity dto, CancellationToken ct)
        {
            dto.Validate();
            await _repo.UpdateAsync(dto, ct);
        }
    }
}
