using System.Threading.Tasks;
using FabLibrarian.CardLibrary.Model;
using FabLibrarian.CardLibrary.Services.Scryfall;
using FabLibrarian.CardLibrary.Services.Scryfall.Abstractions;
using FabLibrarian.CardLibrary.Services.Scryfall.Model;
using FluentAssertions;
using Moq;
using Xunit;

namespace FabLibrarian.CardLibrary.Tests;

public class ScryfallServiceTests
{
    [Fact]
    public async Task NamedSearchAsync_ExistingCard_Return_Card()
    {
        var options = new ScryfallNamedSearchOptions("aust com");
        var austereCommandCard = new CardData(
            "Austere Command",
            "https://scryfall.com/card/clb/687/austere-command",
            new[] { "https://cards.scryfall.io/large/front/3/d/3d24036e-075f-4991-a740-a9d943722ad2.jpg" }
        );

        var scryfallClientMock = new Mock<IScryfallClient>();
        scryfallClientMock.Setup(client => client.NamedSearchAsync(options))
                          .ReturnsAsync(austereCommandCard);
        
        var api = new ScryfallService(scryfallClientMock.Object);
        var result = await api.NamedSearchAsync(options);
        result.Should().HaveCount(1).And.Contain(austereCommandCard);
    }
}