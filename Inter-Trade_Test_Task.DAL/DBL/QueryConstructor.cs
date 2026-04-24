using Inter_Trade_Test_Task.DAL.DTO;
using System.Data.SQLite;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Inter_Trade_Test_Task.DAL.DBL
{
    public static class QueryConstructor
    {
        private static Dictionary<Type, string> Dto2TableMap => new Dictionary<Type, string>()
        {
            {typeof(ClassDTO), "Classes"},
            {typeof(SchoolDTO), "Schools"},
            {typeof(StudentDTO), "Students"}
        };

        public static SQLiteCommand GetCommand(IDtoEntity entity, RequestTypes requestType)
        {
            SQLiteCommand command = new SQLiteCommand();
            Type type = entity.GetType();

            switch (requestType)
            {
                case RequestTypes.Insert:
                    var builder = new StringBuilder();
                    var typeProperties = type.GetProperties().Where(e => e.Name != "Id");

                    builder.Append($"INSERT INTO {Dto2TableMap[type]}(");
                    var argString = "VALUES (";

                    foreach (var property in typeProperties)
                    {
                        builder.Append($"{property.Name}");
                        if (property.PropertyType == typeof(DateTime))
                        {
                            argString += $"'{DateTime.Parse(property.GetValue(entity).ToString()).ToString("yyyy-MM-dd")}'";
                        }
                        else
                        {
                            argString += property.PropertyType == typeof(string) ? $"'{property.GetValue(entity)}'" : property.GetValue(entity);
                        }
                            
                        if (property != typeProperties.Last())
                        {
                            builder.Append(',');
                            argString += ',';
                        }
                    }
                    builder.Append(')').Append(argString + ')');

                    command.CommandText = builder.ToString();
                    break;
                case RequestTypes.Update:
                    builder = new StringBuilder();
                    typeProperties = type.GetProperties().Where(e => e.Name != "Id");

                    builder.Append($"UPDATE {Dto2TableMap[type]} SET ");
                    foreach (var property in typeProperties)
                    {
                        var propValue = property.GetValue(entity);
                        if (IsPropValid(property, propValue))
                        {
                            if(property.PropertyType == typeof(DateTime))
                            {
                                builder.Append($"{property.Name} = '{DateTime.Parse(propValue.ToString()).ToString("yyyy-MM-dd")}',");
                            }
                            else
                            {
                                builder.Append($"{property.Name} = {((property.PropertyType == typeof(string)) ? $"'{property.GetValue(entity)}'" : property.GetValue(entity))},");
                            }
                        }
                    }
                    builder.Remove(builder.Length - 1, 1);
                    builder.Append($" WHERE Id = {entity.Id}");
                    command.CommandText = builder.ToString();
                    break;

                case RequestTypes.Delete:
                    command.CommandText = $"DELETE FROM {Dto2TableMap[type]} WHERE Id = {entity.Id}";
                    break;
                case RequestTypes.Get:
                    command.CommandText = $"SELECT * FROM {Dto2TableMap[type]} WHERE Id = {entity.Id}";
                    break;
                case RequestTypes.GetAll:
                    command.CommandText = $"SELECT * FROM {Dto2TableMap[type]}";
                    break;
                default:
                    throw new NullReferenceException();
            }

            return command;
        }

        private static bool IsPropValid(PropertyInfo prop, object propValue)
        {
            if (prop.PropertyType == typeof(long)) return (long)propValue != 0;
            if (prop.PropertyType == typeof(string)) return (string)propValue != null && (string)propValue != string.Empty;
            if (prop.PropertyType == typeof(DateTime)) return (DateTime)propValue != null && (DateTime)propValue != DateTime.MinValue;
            return false;
        }
    }

    public enum RequestTypes
    {
        Insert,
        Update,
        Delete,
        Get,
        GetAll
    }

    
}
