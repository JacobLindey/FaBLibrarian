using System.Text.RegularExpressions;
using Discord;
using Discord.WebSocket;
using FabLibrarian.CardLibrary.Model;

namespace FabLibrarian.Discord;

public class MessageService
{
    private readonly ICardMatcher _matcher = new CardMatcher(new ScryfallApi());
    private static readonly Regex CardKeyRegex = new(@"\[\[([\w\s-_:()]+)\]\]");
    
    public MessageService(BaseSocketClient client)
    {
        client.MessageReceived += OnMessageReceieved;
    }

    private async Task OnMessageReceieved(SocketMessage message)
    {
        if (message.Author.IsBot)
        {
            Console.WriteLine("message from a bot");
            return;
        }

        var regexMatches = CardKeyRegex.Matches(message.Content);
        if (!regexMatches.Any())
        {
            return;
        }
        
        var searches = regexMatches.Select(x => x.Groups[1].Value).ToList();
        var matchedCards = await _matcher.Match(searches);

        await new CardResponseMessage(searches, matchedCards).SendMessageAsync((IUserMessage) message);
    }
}