using System.Collections.Generic;
using System.Linq;
using Beehive.Api2.Clients;
using Beehive.Api2.DataAccess;
using Beehive.Api2.Models.Entities;
using Beehive.Api2.Services;
using Beehive.Api2.Test.Utilities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Beehive.Api2.Test.Tests;

public class DrumServiceBehaviouralTest
{
    [Fact]
    public void ShouldBeAbleToRetrieveTheItemUnderTest()
    {
        // Arrange
        var factory = new CustomWebApplicationFactory();
        using var scope = factory.Services.CreateScope();

        // Act
        var itemUnderTest = (IDrumService)scope.ServiceProvider.GetRequiredService(typeof(IDrumService));

        // Assert
        itemUnderTest.Should().NotBeNull();
    }

    [Fact]
    public async void ShouldSwapOutDrumClientCorrectlyWithStubbedImplementation()
    {
        // Arrange
        var factory = new CustomWebApplicationFactory
        {
            Registrations = services => { services.SwapTransient<IDrumClient>(_ => new DrumClientStub()); }
        };
        using var scope = factory.Services.CreateScope();
        var itemUnderTest = (IDrumService)scope.ServiceProvider.GetRequiredService(typeof(IDrumService));

        // Act
        var result = await itemUnderTest.GetAllAsync();

        // Assert
        result.Single().Label.Should().Be("Came from stub!");
    }

    [Fact]
    public async void ShouldSwapOutDrumClientCorrectlyWithMockedObject()
    {
        // Arrange
        var mockedClient = new Mock<IDrumClient>();
        mockedClient
            .Setup(x => x.GetDrumsForWarehouse(It.IsAny<int>()))
            .Returns(new List<Drum> { new() { Label = "From moq object :)" } });

        var factory = new CustomWebApplicationFactory
        {
            Registrations = services => { services.SwapTransient(_ => mockedClient.Object); }
        };
        using var scope = factory.Services.CreateScope();
        var itemUnderTest = (IDrumService)scope.ServiceProvider.GetRequiredService(typeof(IDrumService));

        // Act
        var result = await itemUnderTest.GetAllAsync();

        // Assert
        result.Single().Label.Should().Be("From moq object :)");
    }

    [Fact]
    public async void ShouldWriteDrumToDatabase()
    {
        // Arrange
        var factory = new CustomWebApplicationFactory();
        using var scope = factory.Services.CreateScope();
        var context = (DrumDbContext)scope.ServiceProvider.GetRequiredService(typeof(DrumDbContext));
        var itemUnderTest = (IDrumService)scope.ServiceProvider.GetRequiredService(typeof(IDrumService));

        // Pre-assert
        context.Drums.Count().Should().Be(0);

        // Act
        var result = await itemUnderTest.GetAllAsync();

        // Assert
        context.Drums.Count().Should().Be(2);
        context.Drums.First().Label.Should().Be("ANDSDG123");
    }

    [Fact]
    public async void ShouldUseSpyToRecordParametersSentToApi()
    {
        // Arrange
        var spy = new DrumClientSpy();
        var factory = new CustomWebApplicationFactory
        {
            Registrations = services => { services.SwapTransient<IDrumClient>(_ => spy); }
        };
        using var scope = factory.Services.CreateScope();
        var itemUnderTest = (IDrumService)scope.ServiceProvider.GetRequiredService(typeof(IDrumService));

        // Act
        await itemUnderTest.GetAllAsync();

        // Assert
        spy.RecordedWarehouseNumbers.Count.Should().Be(1);
        spy.RecordedWarehouseNumbers.FirstOrDefault().Should().Be(4);
    }

    private class DrumClientStub : IDrumClient
    {
        public IEnumerable<Drum> GetDrumsForWarehouse(int warehouseNumber)
        {
            var result = new List<Drum>
            {
                new()
                {
                    Id = 21,
                    WarehouseNumber = 4,
                    Label = "Came from stub!"
                }
            };

            return result;
        }
    }

    public class DrumClientSpy : IDrumClient
    {
        public IList<int> RecordedWarehouseNumbers { get; } = new List<int>();

        public IEnumerable<Drum> GetDrumsForWarehouse(int warehouseNumber)
        {
            RecordedWarehouseNumbers.Add(warehouseNumber);
            return new List<Drum>();
        }
    }
}