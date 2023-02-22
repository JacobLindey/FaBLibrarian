using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Discord;
using Discord.WebSocket;
using FabLibrarian.CardLibrary.Model;

namespace FabLibrarian.Discord;

public static class CardMatcherTests
{
    public static void Test()
    {
        var matcher = new CardMatcher();
        var cards = matcher.Match("[[Razor Reflex (R)]]");
        foreach (var card in cards)
        {
            Console.WriteLine($"{card.Name}: {card.ReferenceUri}");
        }
    }
}

public class CardMatcher
{
    private static readonly Regex CardKeyRegex = new(@"\[\[([\w\s-_:()]+)\]\]");
    private readonly CardRepository _cardRepo = new();
    
    public ReadOnlyCollection<ICardData> Match(string content)
    {
        var cards = new List<ICardData>();
        var matches = CardKeyRegex.Matches(content);
        
        if (matches.Count > 0)
        {

            foreach (Match match in matches)
            {
                Console.WriteLine($"group 0: {match.Groups[1].Value}");
                var matchedCards = _cardRepo.FuzzyMatch(match.Groups[1].Value);
                if (matchedCards.Any())
                {
                    var card = matchedCards[0];
                    Console.WriteLine($"matched card: {card.Name}");
                    cards.Add(card);
                }
                else
                {
                    Console.WriteLine("no card found");
                }
            }
        }

        return cards.ToList().AsReadOnly();
    }

}

public class MessageService
{
    private readonly CardMatcher _matcher = new();
    
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

        if (message.Channel is not IMessageChannel messageChannel)
        {
            Console.WriteLine("not a message channel");
            return;
        }

        var cards = _matcher.Match(message.Content);
        var text = cards.Aggregate(string.Empty, (current, card) => current + $"[{card.Name}]({card.ReferenceUri})\n");

        if (!string.IsNullOrWhiteSpace(text))
        {
            await messageChannel.SendMessageAsync($"{text}");
        }
    }
}