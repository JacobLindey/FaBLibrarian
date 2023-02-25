using FabLibrarian.CardLibrary.Model;
using FabLibrarian.CardLibrary.Services.Local.Abstractions;
using FabLibrarian.CardLibrary.Services.Local.Model;
using FabLibrarian.CardLibrary.Services.Scryfall.Abstractions;
using FabLibrarian.CardLibrary.Services.Scryfall.Model;

namespace FabLibrarian.Discord;

public class CardMatcher : ICardMatcher
{
    private readonly IScryfallService _scryfallService;
    private readonly ILocalService _localService;

    public CardMatcher(ILocalService localService, IScryfallService scryfallService)
    {
        _localService = localService;
        _scryfallService = scryfallService;
    }
    
    public async Task<IReadOnlyCollection<(string, ICardData?)>> Match(IEnumerable<string> requestedSearches)
    {
        var requests = requestedSearches.ToArray();

        var localOpts = requests
                       .Select(search => new LocalNamedSearchOptions(search))
                       .ToArray();

        var scryOpts = requests
                      .Select(search => new ScryfallNamedSearchOptions(search))
                      .ToArray();
        
        var localTask = _localService.NamedSearchAsync(localOpts);
        var scryfallTask = _scryfallService.NamedSearchAsync(scryOpts);

        await Task.WhenAll(localTask, scryfallTask);

        var localMatches = await localTask;
        var scryMatches = await scryfallTask;

        var matches = localMatches
                     .Zip(scryMatches)
                     .Select(x => x.First /* ?? x.Second */);

        return requests.Zip(matches).ToArray();
    }
}