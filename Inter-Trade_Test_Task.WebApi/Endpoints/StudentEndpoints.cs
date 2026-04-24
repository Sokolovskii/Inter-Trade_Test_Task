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
                return await service.Get();
            }).WithTags("students");

            app.MapGet("/api/students/" + "{ Id }", async (long id) =>
            {
                var service = serviceProvider.GetRequiredService<IService<StudentModel, StudentApiDTO>>();
                return await service.GetById(id);
            }).WithTags("students");

            app.MapPost("/api/students", async (StudentApiDTO dto) =>
            {
                var service = serviceProvider.GetRequiredService<IService<StudentModel, StudentApiDTO>>();
                await service.Insert(dto);
            }).WithTags("students");

            app.MapPut("/api/students/" + "{ Id }", async (StudentApiDTO dto) =>
            {
                var service = serviceProvider.GetRequiredService<IService<StudentModel, StudentApiDTO>>();
                await service.Update(dto);
            }).WithTags("students");

            app.MapDelete("/api/students/" + "{ Id }", async (long id) =>
            {
                var service = serviceProvider.GetRequiredService<IService<StudentModel, StudentApiDTO>>();
                await service.Delete(id);
            }).WithTags("students");
        }
    }
}
