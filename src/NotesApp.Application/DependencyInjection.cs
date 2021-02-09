using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using NotesApp.Application.Services.Interfaces;
using NotesApp.Application.Services;
using AutoMapper;
using NotesApp.Application.Mappings;

namespace NotesApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            services.AddSingleton<IMapper>(AutoMapperConfig.Init());
            services.AddScoped<INoteService, NoteService>();
            return services;
        }
    }
}