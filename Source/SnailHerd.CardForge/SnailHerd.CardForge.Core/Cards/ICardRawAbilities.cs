namespace SnailHerd.CardForge.Core.Cards;

public interface ICardRawAbilities
{
    IEnumerable<string> Keywords { get; }
    IEnumerable<string> Replacements { get; }
    IEnumerable<string> Triggers { get; }
    IEnumerable<string> StaticAbilities { get; }
    IEnumerable<string> Abilities { get; }
    string NonAbilityText { get; }
    IEnumerable<KeyValuePair<string, string>> Variables { get; }
}