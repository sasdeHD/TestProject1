using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SpaServices;
using TestProject1.Data.Interfaces;
using TestProject1.Data.Reprository;
using TestProject1.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddTransient<IClaimRepository, ClaimReprository>();
        builder.Services.AddControllers();
        builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("AppDbContext"));
        builder.Services.AddSpaStaticFiles(configuration =>
        {
            configuration.RootPath = "ClientApp/dist";
        });
        
        var app = builder.Build();

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseSpaStaticFiles();
        app.UseSpa(spa =>
        {
            spa.Options.SourcePath = "ClientApp";
            if (builder.Environment.IsDevelopment())
            {
                spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
            }
        });

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            SeedData.Initialize(services);
        }
        app.Run();
    }
}