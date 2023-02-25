using FabLibrarian.CardLibrary.Model;
using FabLibrarian.CardLibrary.Services.Local.Model;

namespace FabLibrarian.CardLibrary.Services.Local.Abstractions;

public interface ILocalClient
{
    Task<ICardData?> NamedSearchAsync(LocalNamedSearchOptions options);
}