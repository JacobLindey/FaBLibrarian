using Discord;
using FabLibrarian.CardLibrary.Model;

namespace FabLibrarian.Discord;

public class CardResponseMessage
{
    public CardResponseMessage(IEnumerable<string> search, IEnumerable<ICardData?> card)
    {
        Results = search.Zip(card);
    }

    public IEnumerable<(string, ICardData?)> Results { get; }

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
                var embed = new EmbedBuilder()
                           .WithTitle(card.CardName)
                           .WithUrl(card.DatabaseUrl)
                           .WithImageUrl(card.ImageUri)
                           .Build();
                embeds.Add(embed);
            }
        }
        
        await message.ReplyAsync(embeds: embeds.ToArray());
    }
}