using System.Data.SQLite;

namespace Inter_Trade_Test_Task.DAL.DBL
{
    public interface IDbConnnectionFactory
    {
        public Task<SQLiteConnection> GetConnection();
    }
}
