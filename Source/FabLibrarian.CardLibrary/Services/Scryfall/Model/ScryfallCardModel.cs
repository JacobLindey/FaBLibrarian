using System.Text.Json.Serialization;

namespace FabLibrarian.CardLibrary.Services.Scryfall.Model;

public class ScryfallCardModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("scryfall_uri")]
    public string ScryfallUri { get; set; } = string.Empty;

    [JsonPropertyName("card_faces")]
    public List<CardFaceModel> CardFaces { get; set; } = new();
    
    [JsonPropertyName("image_uris")]
    public ImageUrisModel? ImageUris { get; set; } = new();
    
    public class ImageUrisModel
    {
        [JsonPropertyName("normal")]
        public string Normal { get; set; } = string.Empty;
    }

    public class CardFaceModel
    {
        [JsonPropertyName("image_uris")]
        public ImageUrisModel ImageUris { get; set; } = new();
    }
}