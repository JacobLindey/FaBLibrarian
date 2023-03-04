using FabLibrarian.CardLibrary.Model;
using FabLibrarian.CardLibrary.Services.Local.Model;

namespace FabLibrarian.CardLibrary.Services.Local.Abstractions;

public interface ILocalService
{
    Task<ISearchResult> NamedSearchAsync(LocalNamedSearchOptions options);
}