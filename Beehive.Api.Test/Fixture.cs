using System;
using System.Linq;
using Beehive.Api.Infrastructure.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Beehive.Api.Test
{
    public class Fixture : IDisposable
    {
        public Fixture()
        {
            var builder = new ConfigurationBuilder().AddInMemoryCollection();
            var configuration = builder.Build();

            // Example of how to replace config:
            //configuration["ApiUrl"] = "http://someurl/api";

            var startup = new Startup(configuration);
            var services = new ServiceCollection();
            startup.ConfigureServices(services);
            ReplaceDrumClient(services);
            ScopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }

        private IServiceScopeFactory ScopeFactory { get; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public T GetItemUnderTest<T>() where T : class
        {
            var scope = ScopeFactory.CreateScope();
            var result = scope.ServiceProvider.GetRequiredService<T>();
            return result;
        }

        private static void ReplaceDrumClient(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IDrumClient));

            if (descriptor != null) services.Remove(descriptor);

            services.AddScoped<IDrumClient, DrumClientStub>();
        }
    }


    [CollectionDefinition("Behavioural Tests")]
    public class BehaviouralTestCollection : ICollectionFixture<Fixture>
    {
    }
}