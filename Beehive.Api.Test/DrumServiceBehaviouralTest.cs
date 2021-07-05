using System.Collections.Generic;
using System.Linq;
using Beehive.Api.Core.Models.Domain;
using Beehive.Api.Core.Services;
using Beehive.Api.Infrastructure.Clients;
using Beehive.Api.Infrastructure.Repositories;
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
        public void ShouldSwapOutDrumClientCorrectlyWithStubbedImplementation()
        {
            _fixture.Replace<IDrumClient>(new DrumClientStub2());
            var itemUnderTest = _fixture.GetService<IDrumService>();
            var result = itemUnderTest.Get(new GetDrumsQueryDto {WarehouseNumber = 4});
            result.Drums.FirstOrDefault()?.Label.Should().Be("Second test drum");
        }

        [Fact]
        public void ShouldSwapOutDrumClientCorrectlyWithMockedObject()
        {
            var mockedClient = new Mock<IDrumClient>();
            mockedClient
                .Setup(x => x.GetDrumsForWarehouse(It.IsAny<int>()))
                .Returns(new List<Drum> {new() {Label = "Mocked Drum"}});
            _fixture.Replace(mockedClient.Object);
            var itemUnderTest = _fixture.GetService<IDrumService>();
            var result = itemUnderTest.Get(new GetDrumsQueryDto {WarehouseNumber = 5});
            result.Drums.FirstOrDefault()?.Label.Should().Be("Mocked Drum");
        }

        [Fact]
        public void ShouldWriteDrumToRepository()
        {
            _fixture.Replace<IDrumClient>(new DrumClientStub2());
            var itemUnderTest = _fixture.GetService<IDrumService>();
            itemUnderTest.Get(new GetDrumsQueryDto {WarehouseNumber = 4});
            var repository = _fixture.GetService<IRepository<Drum>>();
            var itemsInRepo = repository.GetAll();
            itemsInRepo.Count().Should().Be(1);
        }
    }

    public class DrumClientStub2 : IDrumClient
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
}