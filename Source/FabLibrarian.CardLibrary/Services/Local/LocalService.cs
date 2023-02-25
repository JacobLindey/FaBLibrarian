using System.Collections.Concurrent;
using FabLibrarian.CardLibrary.Model;
using FabLibrarian.CardLibrary.Services.Local.Abstractions;
using FabLibrarian.CardLibrary.Services.Local.Model;

namespace FabLibrarian.CardLibrary.Services.Local;

public class LocalService : ILocalService
{
    private readonly ILocalClient _localClient;

    public LocalService(ILocalClient localClient)
    {
        _localClient = localClient;
    }
    
    public async Task<IReadOnlyCollection<ICardData?>> NamedSearchAsync(params LocalNamedSearchOptions[] options)
    {
        var semaphore = new SemaphoreSlim(10, 10);
        var responses = new ConcurrentBag<ICardData?>();

        var tasks = options.Select(async option =>
        {
            await semaphore.WaitAsync();

            try
            {
                Console.WriteLine($"[LocalService#NamedSearchAsync {{ fuzzy : {option.Fuzzy} }}] starting search...");
                var result = await _localClient.NamedSearchAsync(option);
                var found = result?.ToString() ?? "none";
                Console.WriteLine($"[LocalService#NamedSearchAsync {{ fuzzy : {option.Fuzzy} }}] found: {found}");
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