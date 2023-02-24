using FabLibrarian.CardLibrary.Model;

namespace FabLibrarian.Discord;

public class CardMatcher : ICardMatcher
{

    private readonly IScryfallApi _scryfallApi;

    public CardMatcher(IScryfallApi scryfallApi)
    {
        _scryfallApi = scryfallApi;
    }
    
    public async Task<IReadOnlyCollection<ICardData?>> Match(IEnumerable<string> requestedSearches)
    {
        var options = requestedSearches
                     .Select(x => new NamedSearchOptions(x))
                     .ToArray();
        return await _scryfallApi.NamedSearchAsync(options);
    }
}