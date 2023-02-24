using FabLibrarian.CardLibrary.Model;

namespace FabLibrarian.Discord;

public interface ICardMatcher
{
    Task<IReadOnlyCollection<ICardData?>> Match(IEnumerable<string> requestedSearches);
}