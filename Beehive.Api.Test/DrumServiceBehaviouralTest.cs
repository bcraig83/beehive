using Beehive.Api.Core.Services;
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
    }
}