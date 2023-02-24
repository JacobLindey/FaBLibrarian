namespace FabLibrarian.CardLibrary.Model;

public interface ICardData
{
    string CardName { get; }
    string DatabaseUrl { get; }
    string ImageUri { get; }
}

public class CardData : ICardData
{
    public CardData(
        string cardName,
        string url,
        string imageUri)
    {
        CardName = cardName;
        DatabaseUrl = url;
        ImageUri = imageUri;
    }

    public string CardName { get; }
    public string DatabaseUrl { get; }
    public string ImageUri { get; }
}