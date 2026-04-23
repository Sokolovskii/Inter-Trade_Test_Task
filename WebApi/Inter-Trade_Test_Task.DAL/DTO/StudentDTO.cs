namespace Inter_Trade_Test_Task.DAL.DTO
{
    public class StudentDTO : BaseDtoEntity
    {
        public long Classid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
