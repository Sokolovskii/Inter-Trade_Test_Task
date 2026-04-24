using Inter_Trade_Test_Task.DAL.DBL;
using Inter_Trade_Test_Task.DAL.DTO;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Inter_Trade_Test_Task.DAL.Repository
{
    public class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : IDtoEntity, new()
    {
        public Task InsertAsync(TEntity dto)
        {
            return Task.Run(() =>
            {
                using (var connection = DBConfig.GetConnection())
                {
                    var command = QueryConstructor.GetCommand(dto, RequestTypes.Insert);
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return;
            });
        }

        public Task<List<TEntity>> Get()
        {
            return Task.Run(() =>
            {
                using (var connection = DBConfig.GetConnection())
                {
                    var result = new List<TEntity>();
                    var command = QueryConstructor.GetCommand(new TEntity() { Id = 0 }, RequestTypes.GetAll);
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command.CommandText, connection);
                    var data = new DataTable();
                    adapter.Fill(data);
                    foreach (var item in data.Select())
                    {
                        result.Add(GetDtoFromRow(item));
                    }
                    return result;
                }
            });
        }

        public Task<TEntity> GetById(long id)
        {
            return Task.Run(() =>
            {
                using (var connection = DBConfig.GetConnection())
                {
                    try
                    {
                        var command = QueryConstructor.GetCommand(new TEntity() { Id = id }, RequestTypes.Get);
                        var data = new DataTable();
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(command.CommandText, connection);
                        adapter.Fill(data);
                        var result = GetDtoFromRow(data.Select().FirstOrDefault());
                        connection.Close();
                        if (result == null) throw new FileNotFoundException($"Запись с идентификатором {id} не найдена");
                        return result;
                    }
                    catch (Exception ex) 
                    {
                        throw new InvalidOperationException(ex.Message);
                    }
                    
                }
            });
        }

        public Task RemoveAsync(long id)
        {
            return Task.Run(() =>
            {
                using (var connection = DBConfig.GetConnection())
                {
                    var command = QueryConstructor.GetCommand(new TEntity() { Id = id }, RequestTypes.Delete);
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                    connection.Close();
                    return;
                }
            });
        }

        public async Task UpdateAsync(TEntity dto)
        {
            await Task.Run(async () =>
            {
                using (var connection = DBConfig.GetConnection())
                {
                    var other = await GetById(dto.Id);
                    if (other == null) return;
                    var entityProps = dto.GetType().GetProperties().Where(e => e.Name != "Id");
                    var entityForUpdate = new TEntity() { Id = dto.Id };
                    foreach (var entityProp in entityProps)
                    {
                        if (!entityProp.GetValue(dto).Equals(entityProp.GetValue(other)))
                        {
                            entityProp.SetValue(entityForUpdate, entityProp.GetValue(dto));
                        }
                    }
                    var command = QueryConstructor.GetCommand(entityForUpdate, RequestTypes.Update);
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                    connection.Close();
                    return;
                }
            });
        }

        private TEntity GetDtoFromRow(DataRow row)
        {
            if (row == null) return default;
            var dto = new TEntity();
            var props = typeof(TEntity).GetProperties();
            foreach (var prop in props)
            {
                var propType = prop.PropertyType;
                var propertyValue = (() =>
                {
                    if (propType == typeof(string)) return (object)row.Field<string>(prop.Name);
                    if (propType == typeof(long)) return (object)row.Field<long>(prop.Name);
                    if (propType == typeof(DateTime)) return (object)row.Field<DateTime>(prop.Name);
                    return null;
                });
                var a = propertyValue.Invoke();
                prop.SetValue(dto, propertyValue.Invoke());
            }
            return dto;
        }
    }
}
