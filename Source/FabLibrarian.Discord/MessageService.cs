using System.Text.RegularExpressions;
using Discord;
using Discord.WebSocket;
using FabLibrarian.CardLibrary.Services.Local;
using FabLibrarian.CardLibrary.Services.Local.Model;
using FabLibrarian.CardLibrary.Services.Scryfall;
using FabLibrarian.Discord.DataLoader;

namespace FabLibrarian.Discord;

public class MessageService
{
    private readonly ICardMatcher _matcher;
    private static readonly Regex CardKeyRegex = new(@"\[\[([\w\s-_:(),\\/]+)\]\]");
    
    public MessageService(BaseSocketClient client)
    {
        var factory = new FabDbLocalCardModelFactory();
        var cards = CardDataLoader
                   .LoadAllFilesInDir("Data/FaB")
                   .Select(x => factory.CreateNew(
                            x.CardName,
                            x.HasBack,
                            new CardVersion(x.Primary.Set, x.Primary.CollectorNumber),
                            x.Alts.Select(y => new CardVersion(y.Set, y.CollectorNumber)).ToArray()
                        )
                    )
                   .ToArray();
        
        _matcher = new CardMatcher(
            new LocalService(new InMemoryLocalClient(cards)),
            new ScryfallService(new ScryfallHttpClient(new HttpClient()))
        );
        
        client.MessageReceived += OnMessageReceieved;
    }

    private Task OnMessageReceieved(SocketMessage message)
    {
        _ = Task.Run(async () =>
        {
            if (message.Author.IsBot)
            {
                return;
            }

            var regexMatches = CardKeyRegex.Matches(message.Content);
            if (!regexMatches.Any())
            {
                return;
            }

            var searches = regexMatches.Select(x => x.Groups[1].Value).ToList();
            var results = await _matcher.Match(searches);

            await new CardResponseMessage(results).SendMessageAsync((IUserMessage) message);
        });

        return Task.CompletedTask;
    }
}