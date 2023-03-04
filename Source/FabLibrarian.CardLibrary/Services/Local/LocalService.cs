using System.Collections.Concurrent;
using FabLibrarian.CardLibrary.Model;
using FabLibrarian.CardLibrary.Services.Local.Abstractions;
using FabLibrarian.CardLibrary.Services.Local.Model;

namespace FabLibrarian.CardLibrary.Services.Local;

public class LocalService : ILocalService
{
    private readonly ILocalClient _localClient;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(10, 10);

    public LocalService(ILocalClient localClient)
    {
        _localClient = localClient;
    }
    
    public async Task<ISearchResult> NamedSearchAsync(LocalNamedSearchOptions options)
    {
     
        await _semaphore.WaitAsync();

        try
        {
            Console.WriteLine($"[LocalService#NamedSearchAsync {{ fuzzy : {options.Fuzzy} }}] starting search...");
            var result = await _localClient.NamedSearchAsync(options);
            Console.WriteLine($"[LocalService#NamedSearchAsync {{ fuzzy : {options.Fuzzy} }}] {result}");
            return result;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}