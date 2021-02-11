using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.History.Service.Api;
using NotesApp.History.Service.Infrastructure.EntityFramework;

namespace NotesApp.History.Service.IntegrationTests.Tests
{
    public class WebAppFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(".");
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(services => 
            {
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var dbCtx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    dbCtx.SeedNotes();
                }
            });
        }
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>();
        }
    }
}