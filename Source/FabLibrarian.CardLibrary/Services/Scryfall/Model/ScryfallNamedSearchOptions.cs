namespace FabLibrarian.CardLibrary.Services.Scryfall.Model;

public class ScryfallNamedSearchOptions
{
    public ScryfallNamedSearchOptions(string fuzzyQuery, string? set = null)
    {
        Fuzzy = fuzzyQuery;
        Set = set;  
    }
    
    public string Fuzzy { get; }
    public string? Set { get; }

    public string Format { get; } = "json";
}