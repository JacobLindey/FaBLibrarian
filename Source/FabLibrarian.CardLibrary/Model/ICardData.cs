namespace FabLibrarian.CardLibrary.Model;

public interface ICardData
{
    string CardName { get; }
    string DatabaseUrl { get; }
    string[] ImageUris { get; }
}