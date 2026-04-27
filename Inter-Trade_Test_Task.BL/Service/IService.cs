namespace Inter_Trade_Test_Task.BL.Service
{
    public interface IService<TEntity>
    {
        public Task Insert(TEntity model);
        public Task Update(TEntity model);
        public Task Delete(long id);
        public Task<TEntity> GetById(long id);
        public Task<List<TEntity>> Get();

    }
}
