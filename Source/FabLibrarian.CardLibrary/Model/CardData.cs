namespace FabLibrarian.CardLibrary.Model;

public class CardData : ICardData
{
    public CardData(
        string cardName,
        string url,
        string[] imageUris)
    {
        CardName = cardName;
        DatabaseUrl = url;
        ImageUris = imageUris;
    }

    public string CardName { get; }
    public string DatabaseUrl { get; }
    public string[] ImageUris { get; }

    public override string ToString()
    {
        return $"{{ card: {CardName}, db: {DatabaseUrl}, images: {string.Join(", ", ImageUris)} }}";
    }
}