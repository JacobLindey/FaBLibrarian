namespace SnailHerd.CardForge.Core.Cards.Mana;

/// <summary>
/// Parses a mana cost into a enumerated collection of <see cref="ManaCostShard"/>s.
/// </summary>
public interface IManaCostParser : IEnumerable<ManaCostShard?>
{
    int TotalGenericCost { get; }
}