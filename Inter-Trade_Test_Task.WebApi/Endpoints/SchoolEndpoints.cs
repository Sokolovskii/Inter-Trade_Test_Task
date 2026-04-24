using Inter_Trade_Test_Task.BL.ApiDTO;
using Inter_Trade_Test_Task.BL.Models;
using Inter_Trade_Test_Task.BL.Service;
using System.Text.Json;

namespace Inter_Trade_Test_Task.WebApi.Endpoints
{
    public static class SchoolEndpoints
    {
        public static void AddEndpoints (WebApplication app, IServiceProvider serviceProvider)
        {
            app.MapGet("/api/schools", async () =>
            {
                var service = serviceProvider.GetRequiredService<IService<SchoolModel, SchoolApiDTO>>();
                return await service.Get();
            }).WithTags("schools");

            app.MapGet("/api/schools/" + "{ Id }", async (long id) =>
            {
                var service = serviceProvider.GetRequiredService<IService<SchoolModel, SchoolApiDTO>>();
                return await service.GetById(id);
            }).WithTags("schools");

            app.MapPost("/api/schools/", async (SchoolApiDTO dto) =>
            {
                //var dto = JsonSerializer.Deserialize<SchoolApiDTO>(json);
                var service = serviceProvider.GetRequiredService<IService<SchoolModel, SchoolApiDTO>>();
                await service.Insert(dto);
            }).WithTags("schools");

            app.MapPut("/api/schools/" + "{ Id }", async (SchoolApiDTO dto) =>
            {
                //var dto = JsonSerializer.Deserialize<SchoolApiDTO>(json);
                var service = serviceProvider.GetRequiredService<IService<SchoolModel, SchoolApiDTO>>();
                await service.Update(dto);
            }).WithTags("schools");

            app.MapDelete("/api/schools/" + "{ Id }", async (long id) =>
            {
                var service = serviceProvider.GetRequiredService<IService<SchoolModel, SchoolApiDTO>>();
                await service.Delete(id);
            }).WithTags("schools");
        }
    }
}
