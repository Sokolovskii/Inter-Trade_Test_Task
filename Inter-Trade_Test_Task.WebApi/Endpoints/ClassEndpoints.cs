using Inter_Trade_Test_Task.BL.ApiDTO;
using Inter_Trade_Test_Task.BL.Models;
using Inter_Trade_Test_Task.BL.Service;
using System.Text.Json;

namespace Inter_Trade_Test_Task.WebApi.Endpoints
{
    public static class ClassEndpoints
    {
        public static void AddEndpoints(WebApplication app, IServiceProvider serviceProvider)
        {
            app.MapGet("/api/classes", async () =>
            {
                var service = serviceProvider.GetRequiredService<IService<ClassModel, ClassApiDTO>>();
                var resultList = await service.Get();
                var result = resultList.Select(e => JsonSerializer.Serialize(e));
                return result;
            }).WithTags("classes");

            app.MapGet("/api/classes/" + "{ Id }", async (long id) =>
            {
                var service = serviceProvider.GetRequiredService<IService<ClassModel, ClassApiDTO>>();
                var a = await service.GetById(id);
                var result = JsonSerializer.Serialize(a);
                return result;
            }).WithTags("classes");

            app.MapPost("/api/classes", (string json) =>
            {
                var dto = JsonSerializer.Deserialize<ClassApiDTO>(json);
                var service = serviceProvider.GetRequiredService<IService<ClassModel, ClassApiDTO>>();
                service.Insert(dto);
            }).WithTags("classes");

            app.MapPut("/api/classes/" + "{ Id }", (string json) =>
            {
                var dto = JsonSerializer.Deserialize<ClassApiDTO>(json);
                var service = serviceProvider.GetRequiredService<IService<ClassModel, ClassApiDTO>>();
                service.Update(dto);
            }).WithTags("classes");

            app.MapDelete("/api/classes/" + "{ Id }", (long id) =>
            {
                var service = serviceProvider.GetRequiredService<IService<ClassModel, ClassApiDTO>>();
                service.Delete(id);
            }).WithTags("classes");
        }
    }
}
