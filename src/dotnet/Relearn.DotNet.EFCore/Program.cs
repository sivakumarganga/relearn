
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Relearn.DotNet.EFCore;

IHostBuilder builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<RelearnDbContext>(options =>
        {
            options.UseSqlServer(context.Configuration.GetConnectionString("RelearnDb"));
        });
        services.AddHostedService<AppServiceWorker>();
    });
var host = builder.Build();
await host.ApplyMigrationsAsync();
await host.SeedDefaultDataAsync();
await host.RunAsync();



