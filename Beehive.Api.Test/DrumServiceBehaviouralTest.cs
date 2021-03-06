using System.Collections.Generic;
using System.Linq;
using Beehive.Api.Core.Interfaces.Clients;
using Beehive.Api.Core.Interfaces.DataAccess;
using Beehive.Api.Core.Models;
using Beehive.Api.Core.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Beehive.Api.Test
{
    [Collection("Behavioural Tests")]
    public class DrumServiceBehaviouralTest
    {
        private readonly Fixture _fixture;

        public DrumServiceBehaviouralTest(Fixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ShouldBeAbleToRetrieveTheItemUnderTest()
        {
            var itemUnderTest = _fixture.GetService<IDrumService>();
            itemUnderTest.Should().NotBeNull();
        }

        [Fact]
        public async void ShouldSwapOutDrumClientCorrectlyWithStubbedImplementation()
        {
            _fixture.Replace<IDrumClient>(new DrumClientStub());
            var itemUnderTest = _fixture.GetService<IDrumService>();

            var result = await itemUnderTest.GetAsync(new GetDrumsQueryDto { WarehouseNumber = 4 });

            result.Drums.FirstOrDefault()?.Label.Should().Be("Second test drum");
        }

        [Fact]
        public async void ShouldSwapOutDrumClientCorrectlyWithMockedObject()
        {
            var mockedClient = new Mock<IDrumClient>();
            mockedClient
                .Setup(x => x.GetDrumsForWarehouse(It.IsAny<int>()))
                .Returns(new List<Drum> { new() { Label = "Mocked Drum" } });
            _fixture.Replace(mockedClient.Object);
            var itemUnderTest = _fixture.GetService<IDrumService>();

            var result = await itemUnderTest.GetAsync(new GetDrumsQueryDto { WarehouseNumber = 5 });

            result.Drums.FirstOrDefault()?.Label.Should().Be("Mocked Drum");
        }

        [Fact]
        public async void ShouldWriteDrumToRepository()
        {
            _fixture.ClearDatabase();
            _fixture.Replace<IDrumClient>(new DrumClientStub());
            var itemUnderTest = _fixture.GetService<IDrumService>();
            var repository = _fixture.GetService<IRepository<Drum>>();
            var itemsInRepoBeforeMethodCall = repository.GetAll();
            itemsInRepoBeforeMethodCall.Count().Should().Be(0);

            await itemUnderTest.GetAsync(new GetDrumsQueryDto { WarehouseNumber = 4 });

            var itemsInRepoAfterMethodCall = repository.GetAll();
            itemsInRepoAfterMethodCall.Count().Should().Be(1);
            itemsInRepoAfterMethodCall.FirstOrDefault()?.Label.Should().Be("Second test drum");
        }

        [Fact]
        public async void ShouldUseSpyToRecordParametersSentToApi()
        {
            var spy = new DrumClientSpy();
            _fixture.Replace<IDrumClient>(spy);
            var itemUnderTest = _fixture.GetService<IDrumService>();

            await itemUnderTest.GetAsync(new GetDrumsQueryDto { WarehouseNumber = 4 });

            spy.RecordedWarehouseNumbers.Count.Should().Be(1);
            spy.RecordedWarehouseNumbers.FirstOrDefault().Should().Be(4);
        }
    }

    public class DrumClientStub : IDrumClient
    {
        public IEnumerable<Drum> GetDrumsForWarehouse(int warehouseNumber)
        {
            var result = new List<Drum>
            {
                new()
                {
                    Size = Size.Small,
                    WarehouseNumber = 4,
                    Label = "Second test drum"
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