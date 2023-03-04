using System.Text.RegularExpressions;
using Discord;
using Discord.WebSocket;
using FabLibrarian.CardLibrary.Model;
using FabLibrarian.CardLibrary.Services;
using FabLibrarian.CardLibrary.Services.FabOS;
using FabLibrarian.CardLibrary.Services.Local;
using FabLibrarian.Discord.Configuration;

namespace FabLibrarian.Discord.Services.Messages;

public class MessageService
{
    private readonly ICardService _matcher;
    private static readonly Regex CardKeyRegex = new(@"\[\[([^\]]+)\]\]");
    
    public MessageService(BaseSocketClient client, ApplicationConfig applicationConfig)
    {
        var loader = new FabCardLoader();
        var cards = loader.LoadCardModels(applicationConfig.DataPath).Result.ToList();
        
        _matcher = new CardService(new LocalService(new InMemoryLocalClient(cards)));
        
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
            var results = new List<ISearchResult>();
            foreach (var search in searches)
            {
                var result = await _matcher.NamedSearchAsync(new NamedSearchRequest(search));
                results.Add(result);
            }

            await new CardResponseMessage(results).SendMessageAsync((IUserMessage) message);
        });

        return Task.CompletedTask;
    }
}