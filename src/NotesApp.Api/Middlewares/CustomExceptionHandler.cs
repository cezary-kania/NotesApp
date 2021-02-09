using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NotesApp.Application.Services.Exceptions;

namespace NotesApp.Api.Middlewares
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;
        public CustomExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.BadRequest;
            var message = "Unknown error occured.";
            var exceptionType = exception.GetType();
            switch(exception)
            {
                case ServiceException e when exceptionType == typeof(ServiceException):
                    statusCode = HttpStatusCode.BadRequest;
                    message = e.Message;
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }
            var content = JsonConvert.SerializeObject(new { Error = message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) statusCode;
            return context.Response.WriteAsync(content);
        }
    }
}