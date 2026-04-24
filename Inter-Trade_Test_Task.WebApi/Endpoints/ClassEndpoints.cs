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
                return await service.Get();
            }).WithTags("classes");

            app.MapGet("/api/classes/" + "{ Id }", async (long id) =>
            {
                var service = serviceProvider.GetRequiredService<IService<ClassModel, ClassApiDTO>>();
                return await service.GetById(id);
            }).WithTags("classes");

            app.MapPost("/api/classes", async (ClassApiDTO dto) =>
            {
                var service = serviceProvider.GetRequiredService<IService<ClassModel, ClassApiDTO>>();
                await service.Insert(dto);
            }).WithTags("classes");

            app.MapPut("/api/classes/" + "{ Id }", async (ClassApiDTO dto) =>
            {
                var service = serviceProvider.GetRequiredService<IService<ClassModel, ClassApiDTO>>();
                await service.Update(dto);
            }).WithTags("classes");

            app.MapDelete("/api/classes/" + "{ Id }",async (long id) =>
            {
                var service = serviceProvider.GetRequiredService<IService<ClassModel, ClassApiDTO>>();
                await service.Delete(id);
            }).WithTags("classes");
        }
    }
}
