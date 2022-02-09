using System;
using System.Linq;
using Beehive.Api.Core.Interfaces.DataAccess;
using Beehive.Api.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
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
            UseInMemoryDatabase();
            InitializeScopeFactory();
        }

        private IServiceScopeFactory ScopeFactory { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private void InitializeScopeFactory()
        {
            ScopeFactory = _services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }

        public T GetService<T>() where T : class
        {
            var scope = ScopeFactory.CreateScope();
            var result = scope.ServiceProvider.GetRequiredService<T>();
            return result;
        }

        public void Replace<T>(T replacement) where T : class
        {
            var descriptor = _services.SingleOrDefault(d => d.ServiceType == typeof(T));
            if (descriptor != null)
            {
                _services.Remove(descriptor);
            }

            _services.AddScoped(_ => replacement);
            InitializeScopeFactory();
        }

        private void UseInMemoryDatabase()
        {
            var descriptor = _services.SingleOrDefault(d => d.ServiceType == typeof(DrumDbContext));
            if (descriptor != null)
            {
                _services.Remove(descriptor);
            }

            _services.AddDbContext<DrumDbContext>(options => options.UseInMemoryDatabase("drumSampleDb"));

            descriptor = _services.SingleOrDefault(d => d.ServiceType == typeof(DbContext));
            if (descriptor != null)
            {
                _services.Remove(descriptor);
            }

            _services.AddScoped<DbContext>(provider => provider.GetService<DrumDbContext>());
        }

        public async void ClearDatabase()
        {
            var unitOfWork = GetService<IUnitOfWork>();
            var existingEntities = unitOfWork.DrumRepository.GetAll();
            foreach (var entity in existingEntities)
            {
                unitOfWork.DrumRepository.Delete(entity);
            }

            await unitOfWork.SaveChangesAsync();
        }
    }

    [CollectionDefinition("Behavioural Tests")]
    public class BehaviouralTestCollection : ICollectionFixture<Fixture>
    {
    }
}