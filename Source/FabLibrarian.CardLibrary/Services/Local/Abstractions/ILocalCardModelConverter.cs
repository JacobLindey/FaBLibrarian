using FabLibrarian.CardLibrary.Services.Local.Model;

namespace FabLibrarian.CardLibrary.Services.Local.Abstractions;

public interface ILocalCardModelConverter<in T>
{
    IEnumerable<LocalCardModel> ToLocalCardModels();
}