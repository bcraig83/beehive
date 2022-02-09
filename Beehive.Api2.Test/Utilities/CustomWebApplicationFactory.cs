using System;
using System.Collections.Generic;
using System.Linq;
using Beehive.Api2.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Beehive.Api2.Test.Utilities;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public CustomWebApplicationFactory() : this(null, new Dictionary<string, string>())
    {
    }

    public CustomWebApplicationFactory(
        Action<IServiceCollection>? registrations, Dictionary<string, string> testConfiguration)
    {
        Registrations = registrations ?? (_ => { });
        TestConfiguration = testConfiguration;
    }

    public Action<IServiceCollection> Registrations { get; set; }
    public Dictionary<string, string> TestConfiguration { get; set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            SwapSqlWithInMemoryDatabase(services);
            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DrumDbContext>();
                db.Database.EnsureCreated();
            }

            Registrations(services);
        });


        builder.ConfigureAppConfiguration((_, configBuilder) =>
        {
            configBuilder.AddInMemoryCollection(TestConfiguration);
        });
    }

    private static void SwapSqlWithInMemoryDatabase(IServiceCollection services)
    {
        var dbName = Guid.NewGuid().ToString();
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DrumDbContext>));

        if (descriptor != null)
        {
            services.Remove(descriptor);
        }

        services.AddDbContext<DrumDbContext>(options => options.UseInMemoryDatabase(dbName));
    }
}