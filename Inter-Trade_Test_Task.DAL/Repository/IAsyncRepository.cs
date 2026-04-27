using Inter_Trade_Test_Task.DAL.Models;

namespace Inter_Trade_Test_Task.DAL.Repository
{
    public interface IAsyncRepository<TEntity> where TEntity : class, IModel, new()
    {
        Task InsertAsync(TEntity model, CancellationToken ct = default);
        Task UpdateAsync(TEntity model, CancellationToken ct = default);
        Task RemoveAsync(long id, CancellationToken ct = default);
        Task<TEntity> GetById(long id, CancellationToken ct = default);
        Task<List<TEntity>> Get(CancellationToken ct = default);
    }
}
