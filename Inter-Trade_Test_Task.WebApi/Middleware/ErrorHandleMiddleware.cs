using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Inter_Trade_Test_Task.WebApi.Middleware
{
    public class ErrorHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionMessageAsync(context, exception);
            }
        }

        private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode = 500;

            switch (exception)
            {
                case SQLiteException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case SqliteException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case ArgumentException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case FileNotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;
            }
            ;
            var result = JsonSerializer.Serialize(new
            {
                StatusCode = statusCode,
                ErrorMessage = exception.Message
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
