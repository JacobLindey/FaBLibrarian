using System.Diagnostics;
using System.Text.Json;

namespace FabLibrarian.CardLibrary.Model;

public class ScryfallHttpClient
{
    private readonly HttpClient _httpClient;
    private const int RateLimitMilliseconds = 100;

    private static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public ScryfallHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<ICardData?> NamedSearchAsync(NamedSearchOptions options)
    {
        try
        {
            var watch = new Stopwatch();
            watch.Start();
                
            var query = new ScryfallNamedSearchQuery(options).ToString();
            var response = await _httpClient.GetAsync(query);
            
            watch.Stop();
            var remaining = RateLimitMilliseconds - watch.ElapsedMilliseconds;
            if (remaining > 0)
            {
                Console.WriteLine($"[ScryfallHttpClient#NamedSearchAsync] waiting {remaining} ms");
                await Task.Delay(TimeSpan.FromMilliseconds(remaining));
            }
            

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var searchResult =
                    JsonSerializer.Deserialize<NamedSearchReponse>(content, DefaultJsonSerializerOptions);
                if (searchResult is not null)
                {
                    return new CardData(
                        searchResult.Name,
                        searchResult.ScryfallUri,
                        searchResult.ImageUris.Normal
                    );
                }
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine($"[ScryfallHttpClient/NamedSearchAsync] : {e}");
            return null;
        }
    }
}