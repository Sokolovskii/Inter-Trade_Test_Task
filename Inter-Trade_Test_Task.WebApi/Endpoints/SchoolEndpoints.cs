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
                var resultList = await service.Get();
                var result = resultList.Select(e => JsonSerializer.Serialize(e));
                return resultList;
            }).WithTags("schools");

            app.MapGet("/api/schools/" + "{ Id }", async (long id) =>
            {
                var service = serviceProvider.GetRequiredService<IService<SchoolModel, SchoolApiDTO>>();
                var a = await service.GetById(id);
                var result = JsonSerializer.Serialize(a);
                return result;
            }).WithTags("schools");

            app.MapPost("/api/schools/", (SchoolApiDTO dto) =>
            {
                //var dto = JsonSerializer.Deserialize<SchoolApiDTO>(json);
                var service = serviceProvider.GetRequiredService<IService<SchoolModel, SchoolApiDTO>>();
                service.Insert(dto);
            }).WithTags("schools");

            app.MapPut("/api/schools/" + "{ Id }", (SchoolApiDTO dto) =>
            {
                //var dto = JsonSerializer.Deserialize<SchoolApiDTO>(json);
                var service = serviceProvider.GetRequiredService<IService<SchoolModel, SchoolApiDTO>>();
                service.Update(dto);
            }).WithTags("schools");

            app.MapDelete("/api/schools/" + "{ Id }", (long id) =>
            {
                var service = serviceProvider.GetRequiredService<IService<SchoolModel, SchoolApiDTO>>();
                service.Delete(id);
            }).WithTags("schools");
        }
    }
}
