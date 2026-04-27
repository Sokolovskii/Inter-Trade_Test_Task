using System.Data.SQLite;

namespace Inter_Trade_Test_Task.DAL.DBL
{
    public class SQLiteConnectionFactory : IDbConnnectionFactory
    {
        private readonly string _connectionString;
        public SQLiteConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<SQLiteConnection> GetConnection()
        {
            var connection = new SQLiteConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
