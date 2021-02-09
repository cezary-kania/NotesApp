using Microsoft.Extensions.DependencyInjection;
using NotesApp.History.Service.Application.Services.Interfaces;
using NotesApp.History.Service.Application.Services;
using AutoMapper;
using NotesApp.History.Service.Application.Mappings;

namespace NotesApp.History.Service.Application
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