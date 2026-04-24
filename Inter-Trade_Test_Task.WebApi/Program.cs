using Inter_Trade_Test_Task.BL.ApiDTO;
using Inter_Trade_Test_Task.BL.Models;
using Inter_Trade_Test_Task.BL.Service;
using Inter_Trade_Test_Task.DAL.DTO;
using Inter_Trade_Test_Task.DAL.Repository;
using Inter_Trade_Test_Task.WebApi.Endpoints;
using Inter_Trade_Test_Task.WebApi.Middleware;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAsyncRepository<ClassDTO>, AsyncRepository<ClassDTO>>();
builder.Services.AddScoped<IAsyncRepository<SchoolDTO>, AsyncRepository<SchoolDTO>>();
builder.Services.AddScoped<IAsyncRepository<StudentDTO>, AsyncRepository<StudentDTO>>();

builder.Services.AddScoped<IService<StudentModel, StudentApiDTO>, StudentService>();
builder.Services.AddScoped<IService<SchoolModel, SchoolApiDTO>, SchoolService>();
builder.Services.AddScoped<IService<ClassModel, ClassApiDTO>, ClassService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        //options.RoutePrefix = string.Empty; // Устанавливает Swagger UI в корневом URL
    });
}
app.UseMiddleware<ErrorHandleMiddleware>();
app.UseHttpsRedirection();

var serviceProvider = app.Services.CreateScope().ServiceProvider;

SchoolEndpoints.AddEndpoints(app, serviceProvider);
ClassEndpoints.AddEndpoints(app, serviceProvider);
StudentEndpoints.AddEndpoints(app, serviceProvider);

app.Run();

