namespace SnailHerd.CardForge.Core.Cards;

public interface ICardFace : ICardCharacteristics, ICardRawAbilities, IComparable<ICardFace>
{
    string AltName { get; }
}