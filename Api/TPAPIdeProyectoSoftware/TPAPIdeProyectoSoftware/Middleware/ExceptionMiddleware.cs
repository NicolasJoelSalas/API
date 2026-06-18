using System.Data;
using System.Net;
using System.Text.Json;
using Domain.Exceptions;

namespace TPAPIdeProyectoSoftware.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ReservationConflictException ex)
            {
                await WriteErrorResponse(
                    context,
                    HttpStatusCode.Conflict,
                    ex.Message
                );
            }
            catch (MissingDataException ex)
            {
                await WriteErrorResponse(
                    context,
                    HttpStatusCode.BadRequest,
                    ex.Message
                );
            }
            catch (Exception ex)
            {
                await WriteErrorResponse(
                    context,
                    HttpStatusCode.InternalServerError,
                    ex.Message
                );
            }
        }

        private static async Task WriteErrorResponse(
            HttpContext context,
            HttpStatusCode statusCode,
            string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                message
            };

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}
