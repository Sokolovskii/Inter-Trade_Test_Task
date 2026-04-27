using System.Data.SQLite;

namespace Inter_Trade_Test_Task.DAL.DBL
{
    public class SQLiteConnectionFactory : IDbConnnectionFactory
    {
        private readonly IConfiguration _configuration;
        public SQLiteConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<SQLiteConnection> GetConnection()
        {
            var connection = new SQLiteConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();
            return connection;
        }
    }
}
