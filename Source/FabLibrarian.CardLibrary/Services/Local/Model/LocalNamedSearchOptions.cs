namespace FabLibrarian.CardLibrary.Services.Local.Model;

public class LocalNamedSearchOptions
{
    public LocalNamedSearchOptions(string fuzzy)
    {
        Fuzzy = fuzzy;
    }
    
    public string Fuzzy { get; }
}