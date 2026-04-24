using Inter_Trade_Test_Task.BL.ApiDTO;
using Inter_Trade_Test_Task.BL.Models;
using Inter_Trade_Test_Task.DAL.DTO;
using Inter_Trade_Test_Task.DAL.Repository;

namespace Inter_Trade_Test_Task.BL.Service
{
    public class StudentService : IService<StudentModel, StudentApiDTO>
    {
        private readonly IAsyncRepository<StudentDTO> _repo;
        public StudentService(IAsyncRepository<StudentDTO> repo)
        {
            _repo = repo;
        }
        public async Task Delete(long id)
        {
            if (id > 0) await _repo.RemoveAsync(id);
            else throw new ArgumentException("Идентификатор записи должен быть больше 0");
        }

        public async Task<List<StudentApiDTO>> Get()
        {
            var dtos = await _repo.Get();
            return [.. dtos.Select(e=>
                {
                    var model = new StudentModel();
                    model.FillModelFromDTO(e);
                    return model.ModelToAPI();
                })];
        }

        public async Task<StudentApiDTO> GetById(long id)
        {
            if (id <= 0) throw new ArgumentException("Идентификатор записи должен быть больше 0");
            var dto = await _repo.GetById(id);
            var model = new StudentModel();
            model.FillModelFromDTO(dto);
            return model.ModelToAPI();
        }

        public async Task Insert(StudentApiDTO dto)
        {
                var model = new StudentModel();
                model.FillModelFromDTO(dto);
                if (!model.IsValid(true)) throw new ArgumentException("Запись содержит отсутствующие поля, либо ее идентификатор меньше или равен 0");
                await _repo.InsertAsync(model.ModelToDTO());
        }

        public async Task Update(StudentApiDTO dto)
        {
            var model = new StudentModel();
            model.FillModelFromDTO(dto);
            if (!model.IsValid(false)) throw new ArgumentException("Запись содержит отсутствующие поля, либо ее идентификатор меньше или равен 0");
            await _repo.UpdateAsync(model.ModelToDTO());
        }
    }
}
