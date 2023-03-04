using FabLibrarian.CardLibrary.Model;
using FabLibrarian.CardLibrary.Services.Local.Model;

namespace FabLibrarian.CardLibrary.Services.Local.Abstractions;

public interface ILocalClient
{
    Task<ISearchResult> NamedSearchAsync(LocalNamedSearchOptions options);
}

public class SuccessLocalNamedSearchResult : SuccessSearchResult
{
    public double Similiarity { get; }

    public SuccessLocalNamedSearchResult(string search, ICardData card, double similiarity)
        : base(search, card)
    {
        Similiarity = similiarity;
    }
    
    public override string ToString()
    {
        return $"{Card.CardName} ({Similiarity:P0})";
    }
}

public class NoMatchSearchResult : FailureSearchResult
{
    public NoMatchSearchResult(string search, SuccessLocalNamedSearchResult closestMatch) 
        : base(search, GetReason(closestMatch))
    {
    }

    private static string GetReason(SuccessLocalNamedSearchResult closestMatch)
    {
        return $"No matches found. Closest {closestMatch.Card.CardName} with {closestMatch.Similiarity:P0} similarity";
    }
    
    public override string ToString()
    {
        return $"{Reason}";
    }
}

public class AmbiguousSearchResult : FailureSearchResult
{
    public AmbiguousSearchResult(string search, IEnumerable<SuccessLocalNamedSearchResult> matches) 
        : base(search, GetReason(matches))
    {
    }
    
    private static string GetReason(IEnumerable<SuccessLocalNamedSearchResult> matches)
    {
        var ambiguous = string.Join(", ",
            matches.Select(x => $"{x.Card.CardName} ({x.Similiarity:P0})")
        );
        return $"Ambiguous matches found: {ambiguous}";
    }
    
    public override string ToString()
    {
        return $"{Reason}";
    }
}