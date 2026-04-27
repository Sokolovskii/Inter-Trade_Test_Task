using Inter_Trade_Test_Task.WebApi.Utils;
using Inter_Trade_Test_Task.WebApi.Middleware;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;
using Inter_Trade_Test_Task.DAL.DBL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var repoDirectory = Path.GetDirectoryName(typeof(SQLiteConnectionFactory).Assembly.Location);
var configPath = Path.Combine(repoDirectory, "appsettings.json");
builder.Configuration.AddJsonFile(configPath, optional: false, reloadOnChange: true);

builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddScoped<IDbConnnectionFactory, SQLiteConnectionFactory>();
APIRegistrator.RegisterEntitiesDI(builder, null, "Inter_Trade_Test_Task.DAL.Models");

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

APIRegistrator.RegisterEntitiesEndpoints(app, null, "Inter_Trade_Test_Task.DAL.Models");

var serviceProvider = app.Services.CreateScope().ServiceProvider;

app.Run();

