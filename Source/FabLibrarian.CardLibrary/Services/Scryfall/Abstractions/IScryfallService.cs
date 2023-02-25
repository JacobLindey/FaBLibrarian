using FabLibrarian.CardLibrary.Model;
using FabLibrarian.CardLibrary.Services.Scryfall.Model;

namespace FabLibrarian.CardLibrary.Services.Scryfall.Abstractions;

public interface IScryfallService
{
    Task<IReadOnlyCollection<ICardData?>> NamedSearchAsync(params ScryfallNamedSearchOptions[] options);
}