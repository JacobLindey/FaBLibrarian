using FabLibrarian.CardLibrary.Model;

namespace FabLibrarian.Discord;

public interface ICardMatcher
{
    Task<IReadOnlyCollection<(string, ICardData?)>> Match(IEnumerable<string> requestedSearches);
}