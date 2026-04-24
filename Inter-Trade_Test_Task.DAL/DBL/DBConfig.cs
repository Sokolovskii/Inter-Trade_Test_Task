using System.Data.SQLite;

namespace Inter_Trade_Test_Task.DAL.DBL
{
    public static class DBConfig
    {
        public const string DBName = "Inter-Trade_Test_Task.db";
        public const string ConnectionString = $"Data Source={DBName}, Mode=ReadWrite, PRAGMA journal_mode=WAL";

        public static SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            if (!File.Exists(DBName)) CreateDbWithTables(connection);
            //while (IsDBLocked())
            //{
            //    await Task.Delay(100);
            //}
            return connection;
        }

        private static void CreateDbWithTables(SQLiteConnection connection)
        {
            SQLiteConnection.CreateFile(DBName);
            var command = new SQLiteCommand(SQLScripts.CreateTables, connection);
            command.ExecuteNonQuery();
        }

        //private static bool IsDBLocked()
        //{
        //    bool locked = true;
        //    SQLiteConnection connection = new SQLiteConnection(ConnectionString);
        //    connection.Open();

        //    try
        //    {
        //        SQLiteCommand beginCommand = connection.CreateCommand();
        //        beginCommand.CommandText = "BEGIN EXCLUSIVE"; // tries to acquire the lock
        //                                                      // CommandTimeout is set to 0 to get error immediately if DB is locked 
        //                                                      // otherwise it will wait for 30 sec by default
        //        beginCommand.CommandTimeout = 0;
        //        beginCommand.ExecuteNonQuery();

        //        SQLiteCommand commitCommand = connection.CreateCommand();
        //        commitCommand.CommandText = "COMMIT"; // releases the lock immediately
        //        commitCommand.ExecuteNonQuery();
        //        locked = false;
        //    }
        //    catch (SQLiteException)
        //    {
        //        // database is locked error
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }

        //    return locked;
        //}
    }
}
