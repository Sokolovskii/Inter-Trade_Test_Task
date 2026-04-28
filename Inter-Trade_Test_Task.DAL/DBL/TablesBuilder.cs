using Microsoft.AspNetCore.Connections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SQLite;
using System.Reflection;

namespace Inter_Trade_Test_Task.DAL.DBL
{
    public class TablesBuilder
    {
        private readonly IDbConnnectionFactory _dbConnnectionFactory;
        public TablesBuilder(IDbConnnectionFactory dbConnnectionFactory) 
        { 
            _dbConnnectionFactory = dbConnnectionFactory;
        }
        public async void CreateTableIfNotExcists<TEntity>()
        {
            using (var connection = await _dbConnnectionFactory.GetConnection())
            using (var command = new SQLiteCommand((SQLiteConnection)connection))
            {
                if (await IsTableExists<TEntity>()) return;
                var type = typeof(TEntity);
                var tableAttr = type.GetCustomAttribute<TableAttribute>();
                var props = type.GetProperties();

                var columnDefs = new List<string>();
                var foreignKeyAttrs = new List<string>();
                foreach (var prop in props)
                {
                    var columnAttr = prop.GetCustomAttribute<ColumnAttribute>();
                    if (prop.GetCustomAttribute<ForeignKeyAttribute>() != null)
                        foreignKeyAttrs.Add($"FOREIGN KEY ({columnAttr.Name}) REFERENCES {prop.GetCustomAttribute<ForeignKeyAttribute>()?.Name}(Id)");
                    var typeName = columnAttr?.TypeName;

                    var isNullable = prop.GetCustomAttribute<RequiredAttribute>() == null;

                    var def = $"[{columnAttr.Name}] {typeName}";
                    if (!isNullable) def += " NOT NULL";
                    columnDefs.Add(def);
                }

                command.CommandText = $"CREATE TABLE [{tableAttr.Name}] (\n{string.Join(",\n", columnDefs)}\n";
                if (foreignKeyAttrs.Count > 0) command.CommandText += ",\n" + string.Join(",\n", foreignKeyAttrs);
                command.CommandText += "\n)";
                await command.ExecuteNonQueryAsync();
            }
        }

        private async Task<bool> IsTableExists<TEntity>()
        {
            using (var connection = await _dbConnnectionFactory.GetConnection())
            using (var command = new SQLiteCommand((SQLiteConnection)connection))
            {
                var type = typeof(TEntity);
                var tableAttr = type.GetCustomAttribute<TableAttribute>();
                command.CommandText = @"SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name = @tableName";
                command.Parameters.AddWithValue("@tableName", tableAttr.Name);

                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result) > 0;
            }
        }
    }
}
