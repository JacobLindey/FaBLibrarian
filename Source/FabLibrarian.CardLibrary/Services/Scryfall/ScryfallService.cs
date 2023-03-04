using System.Collections.Concurrent;
using FabLibrarian.CardLibrary.Model;
using FabLibrarian.CardLibrary.Services.Scryfall.Abstractions;
using FabLibrarian.CardLibrary.Services.Scryfall.Model;

namespace FabLibrarian.CardLibrary.Services.Scryfall;

public class ScryfallService : IScryfallService
{
    private readonly IScryfallClient _scryfallClient;
    
    public ScryfallService(IScryfallClient scryfallClient)
    {
        _scryfallClient = scryfallClient;
    }
    
    public async Task<IReadOnlyCollection<ICardData?>> NamedSearchAsync(params ScryfallNamedSearchOptions[] options)
    {
        var semaphore = new SemaphoreSlim(1);
        var responses = new ConcurrentBag<ICardData?>();

        var tasks = options.Select(async option =>
        {
            await semaphore.WaitAsync();

            try
            {
                Console.WriteLine($"[ScryfallService#NamedSearchAsync {{ fuzzy : {option.Fuzzy} }}] starting search...");
                var result = await _scryfallClient.NamedSearchAsync(option);
                var found = result is not null ? result.ToString() : "none";
                Console.WriteLine($"[ScryfallService#NamedSearchAsync {{ fuzzy : {option.Fuzzy} }}] found: {found}");
                responses.Add(result);
            }
            finally
            {
                semaphore.Release();
            }
        });

        await Task.WhenAll(tasks);
        return responses;
    }
}