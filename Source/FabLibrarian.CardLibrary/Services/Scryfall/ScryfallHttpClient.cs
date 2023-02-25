using System.Diagnostics;
using System.Text.Json;
using FabLibrarian.CardLibrary.Model;
using FabLibrarian.CardLibrary.Services.Scryfall.Abstractions;
using FabLibrarian.CardLibrary.Services.Scryfall.Model;

namespace FabLibrarian.CardLibrary.Services.Scryfall;

public class ScryfallHttpClient : IScryfallClient
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
    
    public async Task<ICardData?> NamedSearchAsync(ScryfallNamedSearchOptions options)
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
                    JsonSerializer.Deserialize<ScryfallCardModel>(content, DefaultJsonSerializerOptions);
                if (searchResult is not null)
                {
                    var images = new List<string>();

                    if (searchResult.ImageUris is not null)
                    {
                        images.Add(searchResult.ImageUris.Normal);
                    }
                    
                    foreach (var face in searchResult.CardFaces)
                    {
                        images.Add(face.ImageUris.Normal);
                    }
                    
                    return new CardData(
                        searchResult.Name,
                        searchResult.ScryfallUri,
                        images.ToArray()
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