using System.Threading.Tasks;
using FabLibrarian.CardLibrary.Services.Local;
using FabLibrarian.CardLibrary.Services.Local.Model;
using FluentAssertions;
using Xunit;

namespace FabLibrarian.CardLibrary.Tests;

public class LocalServiceTests
{
    [Fact]
    public async Task NamedSearchAsync_ExistingCard_Return_Card()
    {
        var factory = new FabDbLocalCardModelFactory();
        var card = factory.CreateNew(
            "Hyper Driver",
            false,
            new CardVersion("DYN", 110),
            new CardVersion("DYN", 111), new CardVersion("DYN", 112)
        );

        var options = new LocalNamedSearchOptions("hyp dri");
        
        var service = new LocalService(new InMemoryLocalClient(new []{ card }));
        var matchedCards = await service.NamedSearchAsync(options);

        matchedCards.Should().HaveCount(1);
    }

    [Theory]
    [InlineData("slippery bogle", "Hyper Driver", 10)]
    [InlineData("slippery boggle", "Hyper Driver", 11)]
    public void LevenschtienDistanceTests(string a, string b, int expected)
    {
        a.LevenshteinDistance(b).Should().Be(expected);
    }
}