using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.Domain.Repositories;
using NotesApp.Infrastructure.EntityFramework;
using NotesApp.Infrastructure.Repositories;

namespace NotesApp.Infrastructure
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