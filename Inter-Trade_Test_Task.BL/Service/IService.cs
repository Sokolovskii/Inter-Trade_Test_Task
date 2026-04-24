namespace Inter_Trade_Test_Task.BL.Service
{
    public interface IService<TModel, TApiDto>
    {
        public Task Insert(TApiDto dto);
        public Task Update(TApiDto dto);
        public Task Delete(long id);
        public Task<TApiDto> GetById(long id);
        public Task<List<TApiDto>> Get();

    }
}
