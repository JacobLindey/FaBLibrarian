namespace FabLibrarian.CardLibrary.Model;



public interface ISearchResult
{
    public string Search { get; }
}

public class SuccessSearchResult : ISearchResult
{
    public SuccessSearchResult(string search, ICardData card)
    {
        Search = search;
        Card = card;
    }
    
    public ICardData Card { get; }
    public string Search { get; }
}

public class FailureSearchResult : ISearchResult
{
    public FailureSearchResult(string search, string reason)
    {
        Search = search;
        Reason = reason;
    }

    public string Search { get; }
    public string Reason { get; }
}