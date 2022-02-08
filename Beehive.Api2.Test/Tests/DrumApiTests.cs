using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Beehive.Api2.Models.DTOs;
using Beehive.Api2.Test.Utilities;
using FluentAssertions;
using Xunit;

namespace Beehive.Api2.Test.Tests;

public class DrumApiTests
{
    [Fact]
    public async Task GetMethodShouldReturnData()
    {
        var factory = new CustomWebApplicationFactory();
        var client = factory.CreateClient();
        var response = await client.GetFromJsonAsync<IEnumerable<DrumDto>>("/api/Drum");
        var drumDtos = (response ?? throw new InvalidOperationException()).ToList();
        drumDtos.Should().NotBeNull();
        drumDtos.Should().NotContainNulls();
        var first = drumDtos[0];
        first.Id.Should().Be(1);
        first.Label.Should().Be("ANDSDG123");
        first.WarehouseNumber.Should().Be(4);
        var second = drumDtos[1];
        second.Id.Should().Be(2);
        second.Label.Should().Be("fkewre578");
        second.WarehouseNumber.Should().Be(4);
    }
}