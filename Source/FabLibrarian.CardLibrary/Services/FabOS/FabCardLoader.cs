using System.Text.Json;
using FabLibrarian.CardLibrary.Services.FabOS.Model;
using FabLibrarian.CardLibrary.Services.Local.Model;

namespace FabLibrarian.CardLibrary.Services.FabOS;

public class FabCardLoader
{
    public async Task<IEnumerable<LocalCardModel>> LoadCardModels(string filePath)
    {
        var content = await File.ReadAllTextAsync(filePath);
        var fabDaos = JsonSerializer.Deserialize<CardDao[]>(content);

        return fabDaos is null
            ? new List<LocalCardModel>()
            : new FabCardConverterFactory(fabDaos).ToLocalCardModels();
    }
}