using Inter_Trade_Test_Task.BL.Service;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Inter_Trade_Test_Task.WebApi.Utils
{
    public static class EndpointsRegistrator
    {
        public static void RegisterEndpoints<TEntity>(WebApplication app)
        {
            var columnName = typeof(TEntity).GetCustomAttribute<TableAttribute>().Name;
            app.MapGet($"/api/{columnName.ToLower()}", async (CancellationToken ct, IService<TEntity> service) =>
            {
                return await service.Get();
            }).WithTags($"{columnName.ToLower()}");

            app.MapGet($"/api/{columnName.ToLower()}/" + "{ Id }", async (long id, CancellationToken ct, IService<TEntity> service) =>
            {
                return await service.GetById(id);
            }).WithTags($"{columnName.ToLower()}");

            app.MapPost($"/api/{columnName.ToLower()}", async (TEntity dto, CancellationToken ct, IService<TEntity> service) =>
            {
                await service.Insert(dto);
            }).WithTags($"{columnName.ToLower()}");

            app.MapPut($"/api/{columnName.ToLower()}/" + "{ Id }", async (TEntity dto, CancellationToken ct, IService<TEntity> service) =>
            {
                await service.Update(dto);
            }).WithTags($"{columnName.ToLower()}");

            app.MapDelete($"/api/{columnName.ToLower()}/" + "{ Id }", async (long id, CancellationToken ct, IService<TEntity> service) =>
            {
                await service.Delete(id);
            }).WithTags($"{columnName.ToLower()}");
        }
    }
}
