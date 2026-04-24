namespace Inter_Trade_Test_Task.BL.Service
{
    public interface IService<TModel, TApiDto>
    {
        public  void Insert(TApiDto dto);
        public void Update(TApiDto dto);
        public void Delete(long id);
        public Task<TApiDto> GetById(long id);
        public Task<List<TApiDto>> Get();

    }
}
