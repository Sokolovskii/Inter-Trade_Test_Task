using System.Data.Common;

namespace Inter_Trade_Test_Task.DAL.DBL
{
    public interface IDbConnnectionFactory
    {
        public Task<DbConnection> GetConnection();
    }
}
