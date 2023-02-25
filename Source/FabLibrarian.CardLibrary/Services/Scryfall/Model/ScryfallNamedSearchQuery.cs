using System.Web;

namespace FabLibrarian.CardLibrary.Services.Scryfall.Model;

public class ScryfallNamedSearchQuery
{
    private readonly ScryfallNamedSearchOptions _options;
    private const string BaseUri = "https://api.scryfall.com/cards/named";

    public ScryfallNamedSearchQuery(ScryfallNamedSearchOptions options)
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