using Inter_Trade_Test_Task.BL.Service;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Inter_Trade_Test_Task.WebApi.Utils
{
    /// <summary>
    /// Класс регистрации конечных точек
    /// </summary>
    public static class EndpointsRegistrator
    {
        /// <summary>
        /// Параметрический метод регистрации конечных точек
        /// </summary>
        /// <typeparam name="TEntity">Тип модели</typeparam>
        /// <param name="app">Инстанс приложения</param>
        public static void RegisterEndpoints<TEntity>(WebApplication app)
        {
            var columnName = typeof(TEntity).GetCustomAttribute<TableAttribute>().Name;
            app.MapGet($"/api/{columnName.ToLower()}", async (CancellationToken ct, IService<TEntity> service) =>
            {
                return await service.Get(ct);
            }).WithTags($"{columnName.ToLower()}");

            app.MapGet($"/api/{columnName.ToLower()}/" + "{ Id }", async (long id, CancellationToken ct, IService<TEntity> service) =>
            {
                return await service.GetById(id, ct);
            }).WithTags($"{columnName.ToLower()}");

            app.MapPost($"/api/{columnName.ToLower()}", async (TEntity dto, CancellationToken ct, IService<TEntity> service) =>
            {
                await service.Insert(dto, ct);
            }).WithTags($"{columnName.ToLower()}");

            app.MapPut($"/api/{columnName.ToLower()}/" + "{ Id }", async (TEntity dto, CancellationToken ct, IService<TEntity> service) =>
            {
                await service.Update(dto, ct);
            }).WithTags($"{columnName.ToLower()}");

            app.MapDelete($"/api/{columnName.ToLower()}/" + "{ Id }", async (long id, CancellationToken ct, IService<TEntity> service) =>
            {
                await service.Delete(id, ct);
            }).WithTags($"{columnName.ToLower()}");
        }
    }
}
