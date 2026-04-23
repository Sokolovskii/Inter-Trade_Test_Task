using System.Data.SQLite;

namespace Inter_Trade_Test_Task.DAL.DBL
{
    public static class DBConfig
    {
        public const string DBName = "Inter-Trade_Test_Task.db";
        public const string ConnectionString = $"Data Source={DBName}, Mode=ReadWrite";

        public static SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            if (!File.Exists(DBName)) CreateDbWithTables(connection);
            return connection;
        }

        private static void CreateDbWithTables(SQLiteConnection connection)
        {
            SQLiteConnection.CreateFile(DBName);
            var command = new SQLiteCommand(SQLScripts.CreateTables, connection);
            command.ExecuteNonQuery();
        }

        public static void DropDB()
        {
            var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            var command = new SQLiteCommand("DROP TABLE if EXISTS Students; DROP TABLE if EXISTS Classes; DROP TABLE if EXISTS Schools", connection);
            command.ExecuteNonQuery();
            File.Delete(@"C:\Users\Александр\source\repos\ConsoleApp2\ConsoleApp2\bin\Debug\net10.0\Inter-Trade_Test_Task.db");
        }
    }
}
