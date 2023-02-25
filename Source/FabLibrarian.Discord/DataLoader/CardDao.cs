using System.Text.Json.Serialization;

namespace FabLibrarian.Discord.DataLoader;

public class CardDao
{
    [JsonPropertyName("card_name")]
    public string CardName { get; set; } = string.Empty;

    [JsonPropertyName("has_back")]
    public bool HasBack { get; set; }

    [JsonPropertyName("primary")]
    public PrintId Primary { get; set; } = new();

    [JsonPropertyName("alt")]
    public List<PrintId> Alts { get; set; } = new();

    public class PrintId
    {
        [JsonPropertyName("set")]
        public string Set { get; set; } = string.Empty;
        
        [JsonPropertyName("num")]
        public int CollectorNumber { get; set; }
    }
}