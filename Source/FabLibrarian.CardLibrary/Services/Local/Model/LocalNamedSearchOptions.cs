using FabLibrarian.CardLibrary.Model;

namespace FabLibrarian.CardLibrary.Services.Local.Model;

public class LocalNamedSearchOptions
{
    public LocalNamedSearchOptions(string fuzzy)
    {
        Fuzzy = fuzzy;
    }
    
    public string Fuzzy { get; }

    public static LocalNamedSearchOptions From(NamedSearchRequest request)
    {
        return new LocalNamedSearchOptions(request.FuzzyName);
    }
}