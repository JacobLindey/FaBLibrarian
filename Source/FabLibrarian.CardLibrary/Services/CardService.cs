using FabLibrarian.CardLibrary.Model;
using FabLibrarian.CardLibrary.Services.Local.Abstractions;
using FabLibrarian.CardLibrary.Services.Local.Model;

namespace FabLibrarian.CardLibrary.Services;

public interface ICardService
{
    Task<ISearchResult> NamedSearchAsync(NamedSearchRequest request);
}

public class CardService : ICardService
{
    private readonly ILocalService _localService;

    public CardService(ILocalService localService)
    {
        _localService = localService;
    }

    public async Task<ISearchResult> NamedSearchAsync(NamedSearchRequest request)
    {
        return await _localService.NamedSearchAsync(LocalNamedSearchOptions.From(request));
    }
}