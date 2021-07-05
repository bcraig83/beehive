using System.Collections.Generic;
using System.Linq;
using Beehive.Api.Core.Models.Domain;
using Beehive.Api.Core.Services;
using Beehive.Api.Infrastructure.Clients;
using FluentAssertions;
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
            var itemUnderTest = _fixture.GetItemUnderTest<IDrumService>();
            itemUnderTest.Should().NotBeNull();
        }

        [Fact]
        public void ShouldSwapOutDrumClientCorrectly()
        {
            var itemUnderTest = _fixture.GetItemUnderTest<IDrumService>();
            var result = itemUnderTest.Get(new GetDrumsQueryDto {WarehouseNumber = 3});
            result.Drums.FirstOrDefault()?.Label.Should().Be("Test Drum");
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
                    Size = Size.Large,
                    WarehouseNumber = 3,
                    Label = "Test Drum"
                }
            };

            return result;
        }
    }
}