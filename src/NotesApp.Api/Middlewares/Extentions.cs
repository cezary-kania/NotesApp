using Microsoft.AspNetCore.Builder;

namespace NotesApp.Api.Middlewares
{
    public static class Extentions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware(typeof(CustomExceptionHandler));
    }
}