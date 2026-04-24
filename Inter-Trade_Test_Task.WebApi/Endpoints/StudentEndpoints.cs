using Inter_Trade_Test_Task.BL.ApiDTO;
using Inter_Trade_Test_Task.BL.Models;
using Inter_Trade_Test_Task.BL.Service;
using System.Text.Json;

namespace Inter_Trade_Test_Task.WebApi.Endpoints
{
    public static class StudentEndpoints
    {
        public static void AddEndpoints(WebApplication app, IServiceProvider serviceProvider)
        {
            app.MapGet("/api/students", async () =>
            {
                var service = serviceProvider.GetRequiredService<IService<StudentModel, StudentApiDTO>>();
                var resultList = await service.Get();
                var result = resultList.Select(e => JsonSerializer.Serialize(e));
                return result;
            }).WithTags("students");

            app.MapGet("/api/students/" + "{ Id }", async (long id) =>
            {
                var service = serviceProvider.GetRequiredService<IService<StudentModel, StudentApiDTO>>();
                var a = await service.GetById(id);
                var result = JsonSerializer.Serialize(a);
                return result;
            }).WithTags("students");

            app.MapPost("/api/students", (string json) =>
            {
                var dto = JsonSerializer.Deserialize<StudentApiDTO>(json);
                var service = serviceProvider.GetRequiredService<IService<StudentModel, StudentApiDTO>>();
                service.Insert(dto);
            }).WithTags("students");

            app.MapPut("/api/students/" + "{ Id }", (string json) =>
            {
                var dto = JsonSerializer.Deserialize<StudentApiDTO>(json);
                var service = serviceProvider.GetRequiredService<IService<StudentModel, StudentApiDTO>>();
                service.Update(dto);
            }).WithTags("students");

            app.MapDelete("/api/students/" + "{ Id }", (long id) =>
            {
                var service = serviceProvider.GetRequiredService<IService<StudentModel, StudentApiDTO>>();
                service.Delete(id);
            }).WithTags("students");
        }
    }
}
