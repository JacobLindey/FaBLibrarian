namespace FabLibrarian.CardLibrary.Services.Local.Model;

public class LocalCardModel
{
    public const double MatchThreshold = 0.30;
    
    public LocalCardModel(string cardName, string databaseUrl, params string[] imageUris)
    {
        CardName = cardName;
        DatabaseUrl = databaseUrl;
        ImageUris = imageUris;
    }
    
    public string CardName { get; }
    public string DatabaseUrl { get; }
    
    public string[] ImageUris { get; }

    public bool IsMatch(string fuzzy)
    {
        var nameSplits = CardName.Split("//");
        var fuzzySplits = fuzzy.Split("//");

        return nameSplits
           .Any(nameSplit =>
                fuzzySplits
                   .Select(fuzzySplit =>
                    {
                        var r =  fuzzySplit.LevenshteinTokenRatio(nameSplit);
                        Console.WriteLine($"[LocalCardData#IsMatch {{ CardName: '{nameSplit}', fuzzy:'{fuzzySplit}' }}] {r:P0} similarity");
                        return r;
                    })
                   .Any(r => r > MatchThreshold)
            );
    }
}