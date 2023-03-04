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

    private ISearchResult DoNamedSearch(LocalNamedSearchOptions options)
    {
        const double MatchThreshold = 0.50;
        const double NearnessThreshold = 0.10;
        
        var matrix = _cards
                    .Select(x => FindSimilarity(x, options.Fuzzy))
                    .Zip(_cards, (Similarity, Card) => (Similarity, Card))
                    .ToList();
            
        var matches = matrix.Where(x => x.Item1 > MatchThreshold).ToList();

        var count = matches.Count;
        switch (count)
        {
            case 0:
            {
                var closest = matrix.MaxBy(x => x.Item1);
                return new NoMatchSearchResult(
                    options.Fuzzy,
                    ResultToSuccess(options.Fuzzy, closest)
                );
            }
            case 1:
            {
                var match = matches[0];
                return ResultToSuccess(options.Fuzzy, match);
            }
            default:
                var nearMatches = matches.OrderByDescending(x => x.Similarity).Take(2).ToList();

                if (nearMatches[0].Similarity - nearMatches[1].Similarity < NearnessThreshold)
                {
                    return new AmbiguousSearchResult(
                        options.Fuzzy,
                        nearMatches.Select(x => ResultToSuccess(options.Fuzzy, x))
                    );
                }

                return ResultToSuccess(options.Fuzzy, nearMatches[0]);
        }
    }

    private static SuccessLocalNamedSearchResult ResultToSuccess(string search, (double, LocalCardModel) result)
    {
        return new SuccessLocalNamedSearchResult(
            search,
            new CardData(result.Item2.CardName, result.Item2.DatabaseUrl, result.Item2.ImageUris),
            result.Item1
        );
    }
    
    public Task<ISearchResult> NamedSearchAsync(LocalNamedSearchOptions options)
    {
        return Task.Run(() => DoNamedSearch(options));
    }

    private static double FindSimilarity(LocalCardModel cardModel, string fuzzy)
    {
        var nameSplits = cardModel.CardName.Split("//");
        var fuzzySplits = fuzzy.Split("//");

        return nameSplits
           .Max(nameSplit =>
                fuzzySplits
                   .Select(fuzzySplit => fuzzySplit.LevenshteinTokenRatio(nameSplit))
                   .Max()
            );
    }
}