using Inter_Trade_Test_Task.DAL.DTO;

namespace Inter_Trade_Test_Task.DAL.Repository
{
    public interface IAsyncRepository<TEntity> where TEntity : IDtoEntity
    {
        Task InsertAsync(TEntity dto);
        Task UpdateAsync(TEntity dto);
        Task RemoveAsync(long id);
        Task<TEntity> GetById(long id);
        Task<IEnumerable<TEntity>> Get();
    }
}
