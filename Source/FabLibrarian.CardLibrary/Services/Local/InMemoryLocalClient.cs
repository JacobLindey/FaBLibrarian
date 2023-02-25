using FabLibrarian.CardLibrary.Model;
using FabLibrarian.CardLibrary.Services.Local.Abstractions;
using FabLibrarian.CardLibrary.Services.Local.Model;

namespace FabLibrarian.CardLibrary.Services.Local;

public class InMemoryLocalClient : ILocalClient
{
    private readonly IReadOnlyCollection<LocalCardModel> _cards;

    public InMemoryLocalClient(IReadOnlyCollection<LocalCardModel> cards)
    {
        _cards = cards;
    }
    
    public Task<ICardData?> NamedSearchAsync(LocalNamedSearchOptions options)
    {
        const double MatchThreshold = 0.50;
        
        return Task.Run(() =>
        {
            LocalCardModel? matchedCard = null;
            foreach (var card in _cards)
            {
                var similarity = FindSimilarity(card, options.Fuzzy);
                if (similarity > MatchThreshold)
                {
                    if (matchedCard is not null)
                    {
                        return null;
                    }
                    
                    matchedCard = card;
                }
            }

            return matchedCard is not null
                ? (ICardData) new CardData(matchedCard.CardName, matchedCard.DatabaseUrl, matchedCard.ImageUris)
                : null;
        });
    }

    private static double FindSimilarity(LocalCardModel cardModel, string fuzzy)
    {
        var nameSplits = cardModel.CardName.Split("//");
        var fuzzySplits = fuzzy.Split("//");

        return nameSplits
           .Max(nameSplit =>
                fuzzySplits
                   .Select(fuzzySplit =>
                    {
                        var r = fuzzySplit.LevenshteinTokenRatio(nameSplit);
                        Console.WriteLine(
                            $"[LocalCardData#IsMatch {{ CardName: '{nameSplit}', fuzzy:'{fuzzySplit}' }}] {r:P0} similarity");
                        return r;
                    })
                   .Max()
            );
    }
}