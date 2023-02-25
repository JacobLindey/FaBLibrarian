using Discord;
using FabLibrarian.CardLibrary.Model;

namespace FabLibrarian.Discord;

public class CardResponseMessage
{
    public CardResponseMessage(IReadOnlyCollection<(string, ICardData?)> results)
    {
        Results = results;
    }

    public IReadOnlyCollection<(string, ICardData?)> Results { get; }

    public async Task SendMessageAsync(IUserMessage message)
    {
        var embeds = new List<Embed>();
        
        foreach (var (search, card) in Results)
        {
            if (card is null)
            {
                var embed = new EmbedBuilder()
                           .WithTitle($"Search: {search}")
                           .WithDescription("A unique card could not be found for the given search criteria.")
                           .Build();
                embeds.Add(embed);
            }
            else
            {
                foreach (var image in card.ImageUris)
                {
                    var embed = new EmbedBuilder()
                               .WithTitle(card.CardName)
                               .WithUrl(card.DatabaseUrl)
                               .WithImageUrl(image)
                               .Build();
                    embeds.Add(embed);
                }
            }
        }
        
        await message.ReplyAsync(embeds: embeds.ToArray());
    }
}