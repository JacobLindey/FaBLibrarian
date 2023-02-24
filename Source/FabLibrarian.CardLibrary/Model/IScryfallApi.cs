using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Web;

namespace FabLibrarian.CardLibrary.Model;

public interface IScryfallApi
{
    Task<IReadOnlyCollection<ICardData?>> NamedSearchAsync(params NamedSearchOptions[] options);
}

public class ScryfallNamedSearchQuery
{
    private readonly NamedSearchOptions _options;
    private const string BaseUri = "https://api.scryfall.com/cards/named";

    public ScryfallNamedSearchQuery(NamedSearchOptions options)
    {
        _options = options;
    }

    public override string ToString()
    {
        var builder = new UriBuilder(BaseUri)
        {
            Scheme = "https",
            Host = "api.scryfall.com",
            Path = "cards/named"
        };

        var queryParams = new List<string>
        {
            $"{HttpUtility.UrlEncode("fuzzy")}={HttpUtility.UrlEncode(_options.Fuzzy)}",
            $"{HttpUtility.UrlEncode("format")}={HttpUtility.UrlEncode(_options.Format)}"
        };
        
        if (_options.Set is not null)
        {
            queryParams.Add($"{HttpUtility.UrlEncode("set")}={HttpUtility.UrlEncode(_options.Set)}");
        }

        builder.Query =  string.Join("&", queryParams);

        return builder.ToString();
    }
}

public class ScryfallApi : IScryfallApi
{
    private readonly ScryfallHttpClient _httpClient;


    public ScryfallApi()
    {
        _httpClient = new ScryfallHttpClient(new HttpClient());
    }
    
    public async Task<IReadOnlyCollection<ICardData?>> NamedSearchAsync(params NamedSearchOptions[] options)
    {
        var semaphore = new SemaphoreSlim(1);

        var responses = new ConcurrentBag<ICardData?>();

        var tasks = options.Select(async option =>
        {
            await semaphore.WaitAsync();

            try
            {
                var result = await _httpClient.NamedSearchAsync(option);
                responses.Add(result);
            }
            finally
            {
                semaphore.Release();
            }
        });

        await Task.WhenAll(tasks);
        return responses;
    }
}

public class NamedSearchOptions
{
    public NamedSearchOptions(string fuzzyQuery, string? set = null)
    {
        Fuzzy = fuzzyQuery;
        Set = set;  
    }
    
    public string Fuzzy { get; }
    public string? Set { get; }

    public string Format { get; } = "json";
}

public class NamedSearchReponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("scryfall_uri")]
    public string ScryfallUri { get; set; } = string.Empty;
    
    [JsonPropertyName("image_uris")]
    public ImageUris ImageUris { get; set; } = new();
}

public class ImageUris
{
    [JsonPropertyName("normal")]
    public string Normal { get; set; } = string.Empty;
}