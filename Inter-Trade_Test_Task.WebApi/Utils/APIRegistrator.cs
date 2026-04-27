using Inter_Trade_Test_Task.DAL.Models;
using System.Reflection;
using static Inter_Trade_Test_Task.WebApi.Utils.DIRegisrator;

namespace Inter_Trade_Test_Task.WebApi.Utils
{
    /// <summary>
    /// Регистрирует DI контейнеры и эндпоинты для всех моделей приложения
    /// </summary>
    public static class APIRegistrator
    {
        /// <summary>
        /// Регистрирует DI контейнеры репозитория и сервиса для всех моделей, которые найдет по указанному пространству имен
        /// </summary>
        /// <param name="builder">Билдер приложения</param>
        /// <param name="excluded">Типы, исключенные из общей регистрации</param>
        /// <param name="targetNamespace">Пространство имен моделей приложения</param>
        /// <param name="lifetime">Время жизни контейнера</param>
        public static void RegisterEntitiesDI(WebApplicationBuilder builder, IEnumerable<Type> excluded, string targetNamespace, LifeTime lifetime = LifeTime.Scoped)
        {
            var entityTypes = GetModelTypesFromNamespace(targetNamespace).ToList();
            if(excluded != null) entityTypes.Except(excluded).ToList();

            foreach (var entityType in entityTypes) 
            {
                DIRegisrator.SetupDIRepo(builder, entityType, lifetime);
                DIRegisrator.SetupDIService(builder, entityType, lifetime);
            }
        }

        /// <summary>
        /// Регистрирует конечные точки для всех моделей приложения
        /// </summary>
        /// <param name="app">Инстанс приложения</param>
        /// <param name="excluded">Типы, исключенные из общего построения </param>
        /// <param name="targetNamespace">Пространство имен моделей приложения</param>
        public static void RegisterEntitiesEndpoints(WebApplication app, IEnumerable<Type> excluded, string targetNamespace)
        {
            var entityTypes = GetModelTypesFromNamespace(targetNamespace).ToList();
            if (excluded != null) entityTypes.Except(excluded).ToList();

            foreach (var entityType in entityTypes)
            {
                var registerMethod = typeof(EndpointsRegistrator)
                    .GetMethod("RegisterEndpoints", BindingFlags.Public | BindingFlags.Static)?
                    .MakeGenericMethod(entityType);

                registerMethod?.Invoke(null, [app]);
            }
        }

        /// <summary>
        /// Выполняет поиск всех типов моделей в указанном пространстве имен
        /// </summary>
        /// <param name="targetNamespace">Пространство имен для поиска</param>
        /// <returns></returns>
        private static IEnumerable<Type> GetModelTypesFromNamespace(string targetNamespace)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = new List<Type>();

            foreach (var assembly in assemblies)
            {
                try
                {
                    var typesInAssembly = assembly.GetTypes()
                        .Where(t => t.Namespace != null && t.Namespace.StartsWith(targetNamespace) && !t.IsInterface)
                        .Where(t => t.GetInterface("IModel") != null)
                        .ToArray();

                    types.AddRange(typesInAssembly);
                }
                catch (ReflectionTypeLoadException)
                {
                    // Игнорируем проблемные сборки
                }
            }

            return types;
        }
    }
}
