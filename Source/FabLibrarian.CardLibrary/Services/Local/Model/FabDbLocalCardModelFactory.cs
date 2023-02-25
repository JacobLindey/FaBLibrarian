namespace FabLibrarian.CardLibrary.Services.Local.Model;

public class FabDbLocalCardModelFactory : ILocalCardModelFactory
{
    public LocalCardModel CreateNew(
        string cardName,
        bool hasBack,
        CardVersion primaryVersion,
        params CardVersion[] additionalVersion)
    {
        var images = new List<string>();

        var databaseUri = $"https://fabdb.net/cards/{primaryVersion.Set}{primaryVersion.CollectorNumber:000}";

        images.Add(GetImageUri(primaryVersion.Set, primaryVersion.CollectorNumber));
        if (hasBack)
        {
            images.Add(GetImageBackUri(primaryVersion.Set, primaryVersion.CollectorNumber));
        }
        
        foreach (var version in additionalVersion)
        {
            images.Add(GetImageUri(version.Set, version.CollectorNumber));
            if (hasBack)
            {
                images.Add(GetImageBackUri(version.Set, version.CollectorNumber));
            }
        }

        return new LocalCardModel(cardName, databaseUri, images.ToArray());
    }

    private static string GetImageUri(string set, int num)
    {
        return $"https://fabdb2.imgix.net/cards/printings/{set}{num:000}.png?w=450";
    }

    private static string GetImageBackUri(string set, int num)
    {
        return $"https://fabdb2.imgix.net/cards/printings/{set}{num:000}-BACK.png?w=450";
    }
}