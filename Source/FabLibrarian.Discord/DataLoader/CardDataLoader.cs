using System.Text.Json;

namespace FabLibrarian.Discord.DataLoader;

public static class CardDataLoader
{
    public static IReadOnlyCollection<CardDao> LoadAllFilesInDir(string dirPath)
    {
        var allCards = new List<CardDao>();
        foreach (var dir in Directory.GetDirectories(dirPath))
        {
            var cards = LoadAllFilesInDir(dir);
            allCards.AddRange(cards);
        }
        
        foreach (var file in Directory.GetFiles(dirPath))
        {
            var cards = LoadFromFile(file);
            allCards.AddRange(cards);
        }
        return allCards;
    }

    public static IReadOnlyCollection<CardDao> LoadFromFile(string filePath)
    {
        var content = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<CardDao>>(content) ?? new List<CardDao>();
    }
}