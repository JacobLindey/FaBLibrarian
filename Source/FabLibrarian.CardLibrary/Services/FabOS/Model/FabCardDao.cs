using System.Text.Json.Serialization;

namespace FabLibrarian.CardLibrary.Services.FabOS.Model;

public class FabCardDao
{
    [JsonPropertyName("unique_id")]
    public string UniqueId { get; set; } = string.Empty;
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("printings")]
    public List<FabPrinting> Printings { get; set; } = new();

    public class FabPrinting
    {
        [JsonPropertyName("unique_id")]
        public string UniqueId { get; set; } = string.Empty;

        /// <summary>
        /// Get or set the id of the card of the form {SET}{NUM},
        /// e.g. UPR001
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        
        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; } = string.Empty;
        
        [JsonPropertyName("double_sided_card_info")]
        public DoubleSidedCardInfoDao[]? DoubleSidedCardInfo { get; set; }
    }

    public class DoubleSidedCardInfoDao
    {
        /// <summary>
        /// Gets or sets the <see cref="FabCardDao.UniqueId"/> of the other face to this double-sided card.
        /// </summary>
        [JsonPropertyName("other_face_unique_id")]
        public string OtherFaceUniqueId { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets whether this printing is the front or back of the double-sided card
        /// </summary>
        [JsonPropertyName("is_front")]
        public bool IsFront { get; set; }
        
        /// <summary>
        /// Gets or sets whether this double-sided card is a "Double Face Card"
        /// i.e. that both sides are one game piece (see Invoke Azvolai // Azvolai)
        /// i.e. *not* a double-sided token
        /// </summary>
        [JsonPropertyName("is_DFC")]
        public bool IsDFC { get; set; }
    }
}