using System.Text.Json.Serialization;

namespace FabLibrarian.CardLibrary.Services.FabOS.Model;

public class CardDao
{
    /// <summary>
    /// A UUID representing the card within this data set.
    /// </summary>
    [JsonPropertyName("unique_id")]
    public string UniqueId { get; set; } = string.Empty;
    
    /// <summary>
    /// The name of the card.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The pitch value of the card. Can be a number or blank.
    /// </summary>
    [JsonPropertyName("pitch")]
    public string Pitch { get; set; } = string.Empty;
    
    /// <summary>
    /// The cost of the card. Can be a number, X, XX, X and a number, or blank.
    /// </summary>
    [JsonPropertyName("cost")]
    public string Cost { get; set; } = string.Empty;

    /// <summary>
    /// The power of the card. Can be a number, *, X, or blank.
    /// </summary>
    [JsonPropertyName("power")]
    public string Power { get; set; } = string.Empty;

    /// <summary>
    /// The defense of the card. Can be a number, *, or blank.
    /// </summary>
    [JsonPropertyName("defense")]
    public string Defense { get; set; } = string.Empty;

    /// <summary>
    /// The health of the card. Can be a number or blank.
    /// </summary>
    [JsonPropertyName("health")]
    public string Health { get; set; } = string.Empty;

    /// <summary>
    /// The intelligence of the card. Can be a number or blank.
    /// </summary>
    [JsonPropertyName("intelligence")]
    public string Intelligence { get; set; } = string.Empty;

    /// <summary>
    /// The functional text of the card, formatted in Markdown.
    /// </summary>
    [JsonPropertyName("functional_text")]
    public string FunctionalText { get; set; } = string.Empty;

    /// <summary>
    /// The flavor text of the card, formatted in Markdown.
    /// </summary>
    [JsonPropertyName("flavor_text")]
    public string FlavorText { get; set; } = string.Empty;

    /// <summary>
    /// The full type text box of the card.
    /// </summary>
    [JsonPropertyName("type_text")]
    public string TypeText { get; set; } = string.Empty;
    
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
        /// Gets or sets the <see cref="CardDao.UniqueId"/> of the other face to this double-sided card.
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