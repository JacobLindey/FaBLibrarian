using System.Collections.ObjectModel;

namespace FabLibrarian.CardLibrary.Model;

public class CardId : IEquatable<CardId>
{
    public static readonly CardId Empty = new(string.Empty);
    
    public CardId(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public bool Equals(CardId? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CardId) obj);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}

public interface ICardData
{
    CardId Id { get; }
    string Name { get; }
    Uri? ReferenceUri { get; }
}

public class CardData : ICardData
{
    public CardId Id { get; set; } = CardId.Empty;
    public string Name { get; set; } = string.Empty;
    public Uri? ReferenceUri { get; set; }
}

public class CardRepository
{
    private readonly Dictionary<CardId, CardData> _cardData = new();

    public CardRepository()
    {
        AddCard(new CardId("razor-reflex-red"), "Razor Reflex (R)", new Uri("https://fabdb.net/cards/razor-reflex-red"));   
    }

    private void AddCard(CardId id, string name, Uri? imageUri = null)
    {
        _cardData.Add(
            id,
            new CardData
            {
                Id = id,
                Name = name,
                ReferenceUri = imageUri
            }
        );
    }

    public bool TryGetCard(CardId id, out ICardData? cardData)
    {
        if (_cardData.ContainsKey(id))
        {
            cardData = _cardData[id];
            return true;
        }

        cardData = null;
        return false;
    }

    public ReadOnlyCollection<ICardData> FuzzyMatch(string filter)
    {
        return _cardData.Values
                        .Where(x => string.Equals(x.Name, filter, StringComparison.InvariantCultureIgnoreCase))
                        .Cast<ICardData>().ToList().AsReadOnly();
    }
}
