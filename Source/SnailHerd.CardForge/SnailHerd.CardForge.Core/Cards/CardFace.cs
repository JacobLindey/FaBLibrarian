using SnailHerd.CardForge.Core.Cards.Mana;

namespace SnailHerd.CardForge.Core.Cards;

public class CardFace : ICardFace
{
    public string Name { get; }
    public string AltName { get; set; }
    public CardType Type { get; set; }
    public ManaCost ManaCost { get; set; }
    public ColorSet Color { get; set; }
    public string OracleText { get; set; }
    public string InitialLoyalty { get; set; }
    
    public int IntPower { get; }
    public int IntToughness { get; }
    public string Power { get; }
    public string Toughness { get; }

    public IEnumerable<string> Keywords { get; }
    public IEnumerable<string> Replacements { get; }
    public IEnumerable<string> Triggers { get; }
    public IEnumerable<string> StaticAbilities { get; }
    public IEnumerable<string> Abilities { get; }
    public string NonAbilityText { get; }
    public IEnumerable<KeyValuePair<string, string>> Variables { get; }

    public CardFace(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Card name is empty");
        
        Name = name;
    }
    
    public int CompareTo(ICardFace? other)
    {
        return string.Compare(Name, other?.Name, StringComparison.InvariantCultureIgnoreCase);
    }



    public override string ToString()
    {
        return Name;
    }
    
    
}