namespace Inter_Trade_Test_Task.BL.Service
{
    public interface IService<TEntity>
    {
        public Task Insert(TEntity model, CancellationToken ct);
        public Task Update(TEntity model, CancellationToken ct);
        public Task Delete(long id, CancellationToken ct);
        public Task<TEntity> GetById(long id, CancellationToken ct);
        public Task<List<TEntity>> Get(CancellationToken ct);

    }
}
