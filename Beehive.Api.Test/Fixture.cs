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
        private readonly ServiceCollection _services;

        public Fixture()
        {
            var builder = new ConfigurationBuilder().AddInMemoryCollection();
            var configuration = builder.Build();

            // Example of how to replace config:
            //configuration["ApiUrl"] = "http://someurl/api";

            var startup = new Startup(configuration);
            _services = new ServiceCollection();
            startup.ConfigureServices(_services);
            InitializeScopeFactory();
        }

        private IServiceScopeFactory ScopeFactory { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private void InitializeScopeFactory()
        {
            ReplaceDrumClient();
            ScopeFactory = _services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }

        public T GetItemUnderTest<T>() where T : class
        {
            var scope = ScopeFactory.CreateScope();
            var result = scope.ServiceProvider.GetRequiredService<T>();
            return result;
        }

        private void ReplaceDrumClient()
        {
            var descriptor = _services.SingleOrDefault(d => d.ServiceType == typeof(IDrumClient));

            if (descriptor != null) _services.Remove(descriptor);

            _services.AddScoped<IDrumClient, DrumClientStub>();
        }

        // public void Replace<T>(T replacement) where T : class
        // {
        //     var descriptor = _services.SingleOrDefault(d => d.ServiceType == typeof(IDrumClient));
        //     
        // }
    }


    [CollectionDefinition("Behavioural Tests")]
    public class BehaviouralTestCollection : ICollectionFixture<Fixture>
    {
    }
}