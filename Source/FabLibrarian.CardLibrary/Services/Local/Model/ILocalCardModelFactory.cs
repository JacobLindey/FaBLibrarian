namespace FabLibrarian.CardLibrary.Services.Local.Model;

public record CardVersion(string Set, int CollectorNumber);

public interface ILocalCardModelFactory
{
    LocalCardModel CreateNew(
        string cardName,
        bool hasBack,
        CardVersion primaryVersion,
        params CardVersion[] additionalVersion);
}