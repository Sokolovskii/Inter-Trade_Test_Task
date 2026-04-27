using Inter_Trade_Test_Task.DAL.DBL;
using Inter_Trade_Test_Task.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.Marshalling;

namespace Inter_Trade_Test_Task.DAL.Repository
{
    public class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class, IModel, new()
    {
        private readonly IDbConnnectionFactory _dbConnnectionFactory;
        public AsyncRepository(IDbConnnectionFactory dbConnnectionFactory)
        {
            _dbConnnectionFactory = dbConnnectionFactory;
            CreateDatabaseIfNotExist();
        }
        public async Task InsertAsync(TEntity dto, CancellationToken ct = default)
        {
            var type = typeof(TEntity);
            var tableAttr = type.GetCustomAttribute<TableAttribute>();
            var columnDefs = new List<string>();

            await using (var connection = await _dbConnnectionFactory.GetConnection())
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"INSERT INTO {tableAttr.Name} (";
                foreach (var prop in type.GetProperties()) 
                {
                    var columnAttr = prop.GetCustomAttribute<ColumnAttribute>();
                    var propValue = prop.GetValue(dto);

                    columnDefs.Add("@" + columnAttr.Name);
                    command.Parameters.AddWithValue("@" + columnAttr.Name, propValue);
                }

                command.CommandText += string.Join(',', columnDefs);
                command.CommandText += ")";

                await command.ExecuteNonQueryAsync(ct);
                await connection.CloseAsync();
            }
                
        }

        public async Task<List<TEntity>> Get(CancellationToken ct = default)
        {
            var type = typeof(TEntity);
            var tableAttr = type.GetCustomAttribute<TableAttribute>();
            var columns2props = new Dictionary<string, PropertyInfo>();
            foreach (var prop in type.GetProperties())
            {
                columns2props.Add(prop.GetCustomAttribute<ColumnAttribute>()?.Name ?? prop.Name, prop);
            }

            var result = new List<TEntity>();

            await using (var connection = await _dbConnnectionFactory.GetConnection())
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"SELECT * FROM {tableAttr.Name}";

                await using(var reader = await command.ExecuteReaderAsync(ct))
                {
                    if(await reader.ReadAsync())
                    {
                        var entity = new TEntity();

                        for(var i=0; i<reader.FieldCount; i++)
                        {
                            var field = reader.GetName(i);
                            var prop = columns2props[field];
                            object? value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            prop.SetValue(entity, value);
                        }

                        result.Add(entity);
                    }

                    return result;
                    
                }
            }
        }

        public async Task<TEntity> GetById(long id, CancellationToken ct = default)
        {
            var type = typeof(TEntity);
            var tableAttr = type.GetCustomAttribute<TableAttribute>();
            var props = type.GetProperties();
            string keyColumnName = props.FirstOrDefault(e => e.GetCustomAttribute<KeyAttribute>() != null)?.GetCustomAttribute<ColumnAttribute>()?.Name ?? "Id";

            await using (var connection = await _dbConnnectionFactory.GetConnection())
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"SELECT * FROM {tableAttr.Name} WHERE {keyColumnName} = @key";
                command.Parameters.AddWithValue("@key", id);

                await using (var reader = await command.ExecuteReaderAsync(ct))
                {
                    if (await reader.ReadAsync(ct))
                    {
                        var result = new TEntity();
                        foreach (var prop in props)
                        {
                            string columnName = prop.GetCustomAttribute<ColumnAttribute>()?.Name ?? prop.Name;
                            int ordinal = reader.GetOrdinal(columnName);
                            if (!reader.IsDBNull(ordinal))
                            {
                                object value = reader.GetValue(ordinal);
                                if (value.GetType() != prop.PropertyType)
                                    value = Convert.ChangeType(value, prop.PropertyType);
                                prop.SetValue(result, value);
                            }
                        }
                        return result;
                    }
                    return null;
                }
            }
        }

        public async Task RemoveAsync(long id, CancellationToken ct = default)
        {
            var type = typeof(TEntity);
            var tableAttr = type.GetCustomAttribute<TableAttribute>();
            var props = type.GetProperties();
            string keyColumnName = props.FirstOrDefault(e => e.GetCustomAttribute<KeyAttribute>() != null)?.GetCustomAttribute<ColumnAttribute>()?.Name ?? "Id";

            await using (var connection = await _dbConnnectionFactory.GetConnection())
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = $"DELETE FROM {tableAttr.Name} WHERE {keyColumnName} = @key";
                command.Parameters.AddWithValue("@key", id);
                await command.ExecuteNonQueryAsync(ct);
            }
        }

        public async Task UpdateAsync(TEntity dto, CancellationToken ct = default)
        {
            var type = typeof(TEntity);
            var tableAttr = type.GetCustomAttribute<TableAttribute>();
            var props = type.GetProperties();
            var keyProp = props.FirstOrDefault(e => e.GetCustomAttribute<KeyAttribute>() != null);
            string keyColumnName = keyProp?.GetCustomAttribute<ColumnAttribute>()?.Name ?? "Id";
            object keyValue = keyProp.GetValue(dto);

            if (keyValue == null) throw new ArgumentException("Укажите идентификатор записи для обновления");

            var setList = new List<string>();

            await using (var connection = await _dbConnnectionFactory.GetConnection())
            using (var command = new SQLiteCommand(connection))
            {

                foreach (var prop in props)
                {
                    if (prop != keyProp)
                    {
                        var columnName = prop.GetCustomAttribute<ColumnAttribute>()?.Name ?? prop.Name;
                        object value = prop.GetValue(dto);

                        if (value == null) continue;
                        setList.Add($"{columnName} = @{columnName}");
                        command.Parameters.AddWithValue($"@{columnName}", value);
                    }
                }
                command.CommandText = $"UPDATE {tableAttr.Name} SET {string.Join(',', setList)} WHERE {keyColumnName} = {keyValue}";
                await command.ExecuteNonQueryAsync(ct);
            }
        }

        private async void CreateDatabaseIfNotExist()
        {
            using (var connection = await _dbConnnectionFactory.GetConnection())
            using (var command = new SQLiteCommand(connection))
            {
                if (await IsTableExists()) return;
                var type = typeof(TEntity);
                var tableAttr = type.GetCustomAttribute<TableAttribute>();
                var props = type.GetProperties();
                command.CommandText = $"CREATE TABLE [{tableAttr.Name}] (\n";
                var columnDefs = new List<string>();

                foreach(var prop in props)
                {
                    var columnAttr = prop.GetCustomAttribute<ColumnAttribute>();
                    var typeName = columnAttr.TypeName;
                    var isNullable = !IsValueTypeOrNullable(prop.PropertyType);

                    var def = $"[{columnAttr.Name}] {typeName}";
                    if (!isNullable) def += " NOT NULL";
                    columnDefs.Add(def);
                }

                command.CommandText += string.Join(",\n", columnDefs);
                command.CommandText += "\n);";
                await command.ExecuteNonQueryAsync();
            }
        }

        private async Task<bool> IsTableExists()
        {
            using (var connection = await _dbConnnectionFactory.GetConnection())
            using (var command = new SQLiteCommand(connection))
            {
                var type = typeof(TEntity);
                var tableAttr = type.GetCustomAttribute<TableAttribute>();
                command.CommandText = @"SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name = @tableName";
                command.Parameters.AddWithValue("@tableName", tableAttr.Name);

                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result) > 0;
            }
        }

        private static bool IsValueTypeOrNullable(Type type)
        {
            return type.IsValueType || (type.IsGenericType &&
                   type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }
}
