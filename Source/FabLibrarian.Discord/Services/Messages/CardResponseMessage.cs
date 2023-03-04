using Discord;
using FabLibrarian.CardLibrary.Model;

namespace FabLibrarian.Discord.Services.Messages;

public class CardResponseMessage
{
    public CardResponseMessage(IReadOnlyCollection<ISearchResult> results)
    {
        Results = results;
    }

    public IReadOnlyCollection<ISearchResult> Results { get; }

    public async Task SendMessageAsync(IUserMessage message)
    {
        var embeds = new List<Embed>();
        
        foreach (var result in Results)
        {
            if (result is FailureSearchResult failure)
            {
                var embed = new EmbedBuilder()
                           .WithTitle($"Failed to find card for '{failure.Search}'")
                           .WithDescription(failure.Reason)
                           .Build();
                embeds.Add(embed);
            }
            else if (result is SuccessSearchResult success)
            {
                foreach (var image in success.Card.ImageUris)
                {
                    var embed = new EmbedBuilder()
                               .WithTitle(success.Card.CardName)
                               .WithUrl(success.Card.DatabaseUrl)
                               .WithImageUrl(image)
                               .Build();
                    embeds.Add(embed);
                }
            }
        }
        
        await message.ReplyAsync(embeds: embeds.ToArray());
    }
}