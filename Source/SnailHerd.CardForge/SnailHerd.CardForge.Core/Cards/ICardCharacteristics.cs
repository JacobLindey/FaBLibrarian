using SnailHerd.CardForge.Core.Cards.Mana;

namespace SnailHerd.CardForge.Core.Cards;

public interface ICardCharacteristics
{
    string Name { get; }
    CardType Type { get; }
    ManaCost ManaCost { get; }
    ColorSet Color { get; }
    
    int IntPower { get; }
    int IntToughness { get; }
    
    string Power { get; }
    string Toughness { get; }
    
    string InitialLoyalty { get; }
    
    string OracleText { get; }
}