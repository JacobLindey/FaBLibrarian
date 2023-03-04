namespace FabLibrarian.CardLibrary.Model;

public class NamedSearchRequest
{
    public string FuzzyName { get; }

    public NamedSearchRequest(string fuzzyName)
    {
        FuzzyName = fuzzyName;
    }
}