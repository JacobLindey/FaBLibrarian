namespace FabLibrarian.CardLibrary.Services.Local.Model;

public class LocalCardModel
{
    public LocalCardModel(string cardName, string databaseUrl, params string[] imageUris)
    {
        CardName = cardName;
        DatabaseUrl = databaseUrl;
        ImageUris = imageUris;
    }
    
    public string CardName { get; }
    public string DatabaseUrl { get; }
    
    public string[] ImageUris { get; }
}