using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.History.Service.Domain.Repositories;
using NotesApp.History.Service.Infrastructure.EntityFramework;
using NotesApp.History.Service.Infrastructure.Repositories;

namespace NotesApp.History.Service.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            var useMemoryDb = Convert.ToBoolean(configuration.GetSection("UseInMemoryDatabase").Value);
            if (useMemoryDb)
            {
                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("NotesDb"));
            }
            else
            {
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            }
            services.AddScoped<INoteRepository, NoteRepository>();
            return services;
        }
    }
}