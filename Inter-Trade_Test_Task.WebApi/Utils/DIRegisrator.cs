using Inter_Trade_Test_Task.BL.Service;
using Inter_Trade_Test_Task.DAL.Repository;

namespace Inter_Trade_Test_Task.WebApi.Utils
{
    /// <summary>
    /// Класс регистратора DI-контейнеров для репозитория и сервиса
    /// </summary>
    public static class DIRegisrator
    {
        /// <summary>
        /// Параметрический метод регистрирации DI-контейнера репозитория 
        /// </summary>
        /// <typeparam name="TEntity">Тип модели</typeparam>
        /// <param name="builder">Билдер</param>
        /// <param name="lifetime">Время жизни контейнера</param>
        /// <exception cref="NotSupportedException"></exception>
        public static void SetupDIRepo<TEntity>(WebApplicationBuilder builder, LifeTime lifetime = LifeTime.Scoped)
        {
            var repoInterface = typeof(IAsyncRepository<>).MakeGenericType(typeof(TEntity));
            var repoImplementation = typeof(AsyncRepository<>).MakeGenericType(typeof(TEntity));
            switch (lifetime)
            {
                case LifeTime.Singleton:
                    builder.Services.AddSingleton(repoInterface, repoImplementation);
                    break;
                case LifeTime.Scoped:
                    builder.Services.AddScoped(repoInterface, repoImplementation);
                    break;
                case LifeTime.Transient:
                    builder.Services.AddTransient(repoInterface, repoImplementation);
                    break;
                default:
                    throw new NotSupportedException($"Не удалось зарегистрировать репозиторий сущности: {typeof(TEntity).Name}");
            }
        }

        /// <summary>
        /// Метод регистрирации DI-контейнера репозитория, принимающий тип модели в качестве аргумента
        /// </summary>
        /// <param name="builder">Билдер</param>
        /// <param name="entityType">Тип модели</param>
        /// <param name="lifetime">Время жизни контейнера</param>
        /// <exception cref="NotSupportedException"></exception>
        public static void SetupDIRepo(WebApplicationBuilder builder, Type entityType, LifeTime lifetime = LifeTime.Scoped)
        {
            var repoInterface = typeof(IAsyncRepository<>).MakeGenericType(entityType);
            var repoImplementation = typeof(AsyncRepository<>).MakeGenericType(entityType);
            switch (lifetime)
            {
                case LifeTime.Singleton:
                    builder.Services.AddSingleton(repoInterface, repoImplementation);
                    break;
                case LifeTime.Scoped:
                    builder.Services.AddScoped(repoInterface, repoImplementation);
                    break;
                case LifeTime.Transient:
                    builder.Services.AddTransient(repoInterface, repoImplementation);
                    break;
                default:
                    throw new NotSupportedException($"Не удалось зарегистрировать репозиторий сущности: {entityType.Name}");
            }
        }

        /// <summary>
        /// Параметрический метод регистрации DI-контейнера сервиса
        /// </summary>
        /// <typeparam name="TEntity">Тип модели</typeparam>
        /// <param name="builder">Билдер</param>
        /// <param name="lifetime">Время жизни контейнера</param>
        /// <exception cref="NotSupportedException"></exception>
        public static void SetupDIService<TEntity>(WebApplicationBuilder builder, LifeTime lifetime = LifeTime.Scoped)
        {
            var serviceInterface = typeof(IService<>).MakeGenericType(typeof(TEntity));
            var serviceImplementation = typeof(Service<>).MakeGenericType(typeof(TEntity));
            switch (lifetime)
            {
                case LifeTime.Singleton:
                    builder.Services.AddSingleton(serviceInterface, serviceImplementation);
                    break;
                case LifeTime.Scoped:
                    builder.Services.AddScoped(serviceInterface, serviceImplementation);
                    break;
                case LifeTime.Transient:
                    builder.Services.AddTransient(serviceInterface, serviceImplementation);
                    break;
                default:
                    throw new NotSupportedException($"Не удалось зарегистрировать сервис сущности: {typeof(TEntity).Name}");
            }
        }

        /// <summary>
        /// Метод регистрирации DI-контейнера репозитория, принимающий тип модели в качестве аргумента
        /// </summary>
        /// <param name="builder">Билдер</param>
        /// <param name="entityType">Тип модели</param>
        /// <param name="lifetime">Время жизни контейнера</param>
        /// <exception cref="NotSupportedException"></exception>
        public static void SetupDIService(WebApplicationBuilder builder, Type entityType, LifeTime lifetime = LifeTime.Scoped)
        {
            var serviceInterface = typeof(IService<>).MakeGenericType(entityType);
            var serviceImplementation = typeof(Service<>).MakeGenericType(entityType);
            switch (lifetime)
            {
                case LifeTime.Singleton:
                    builder.Services.AddSingleton(serviceInterface, serviceImplementation);
                    break;
                case LifeTime.Scoped:
                    builder.Services.AddScoped(serviceInterface, serviceImplementation);
                    break;
                case LifeTime.Transient:
                    builder.Services.AddTransient(serviceInterface, serviceImplementation);
                    break;
                default:
                    throw new NotSupportedException($"Не удалось зарегистрировать сервис сущности: {entityType.Name}");
            }
        }

        public enum LifeTime
        {
            Singleton,
            Scoped,
            Transient
        }
    }
}
