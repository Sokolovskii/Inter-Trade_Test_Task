using Inter_Trade_Test_Task.WebApi.Utils;
using Inter_Trade_Test_Task.WebApi.Middleware;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;
using Inter_Trade_Test_Task.DAL.DBL;
using System.Collections;
using Inter_Trade_Test_Task.DAL.Repository;
using Inter_Trade_Test_Task.BL.Service;
using Inter_Trade_Test_Task.DAL.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddSingleton<IDbConnnectionFactory, SQLiteConnectionFactory>();

//APIRegistrator.RegisterEntitiesDI(builder, null, "Inter_Trade_Test_Task.DAL.Models");
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
builder.Services.AddScoped(typeof(IService<>),typeof(Service<>));

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

var service = (SQLiteConnectionFactory)app.Services.GetRequiredService<IDbConnnectionFactory>();
var tableBuilder = new TablesBuilder(service);
tableBuilder.CreateTableIfNotExcists<School>();
tableBuilder.CreateTableIfNotExcists<Class>();
tableBuilder.CreateTableIfNotExcists<Student>();

app.UseMiddleware<ErrorHandleMiddleware>();
app.UseHttpsRedirection();

APIRegistrator.RegisterEntitiesEndpoints(app, null, "Inter_Trade_Test_Task.DAL.Models");

var serviceProvider = app.Services.CreateScope().ServiceProvider;

app.Run();

