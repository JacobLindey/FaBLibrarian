using FabLibrarian.CardLibrary.Model;
using FabLibrarian.CardLibrary.Services.Scryfall.Model;

namespace FabLibrarian.CardLibrary.Services.Scryfall.Abstractions;

public interface IScryfallClient
{
    Task<ICardData?> NamedSearchAsync(ScryfallNamedSearchOptions options);
}