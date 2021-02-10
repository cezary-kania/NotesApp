using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.Infrastructure.EntityFramework;

namespace NotesApp.IntegrationTests.Tests
{
    public class CustomWebAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => 
            {
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var dbCtx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    dbCtx.SeedNotes();
                }
            });
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings-tests.json")
                .Build();
            builder.UseConfiguration(config);
        }
    }
}