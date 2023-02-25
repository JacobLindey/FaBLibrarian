namespace FabLibrarian.CardLibrary.Services.Local;

public static class FuzzyStringExtensions
{
    public static double LevenshteinTokenRatio(this string a, string b)
    {
        return LevenshteinDistanceRatio(Tokenize(a), Tokenize(b));
    }

    private static string Tokenize(string a)
    {
        var aSplit = a.ToLowerInvariant().Split(' ').ToList();
        aSplit.Sort();
        return aSplit.Aggregate((x, y) => x + y);
    }
    
    public static double LevenshteinDistanceRatio(this string a, string b)
    {
        return 1 - a.LevenshteinDistance(b) / (double) Math.Max(a.Length, b.Length);
    }

    public static int LevenshteinDistance(this string a, string b)
    {
        return LevenshteinDistanceInternal(a.ToLowerInvariant(), b.ToLowerInvariant());
    }
    
    private static int LevenshteinDistanceInternal(string a, string b)
    {
        var n = a.Length;
        var m = b.Length;

        var distance = new int[n+1, m+1];
        for (var i = 0; i <= n; i++)
        {
            distance[i, 0] = i;
        }

        for (var j = 0; j <= m; j++)
        {
            distance[0, j] = j;
        }
        
        for (var j = 1; j <= m; j++)
        {
            for (var i = 1; i <= n; i++)
            {
                var indicator = a[i-1] != b[j-1] ? 1 : 0;
                distance[i, j] = new[]
                {
                    distance[i - 1, j] + 1,
                    distance[i, j - 1] + 1,
                    distance[i - 1, j - 1] + indicator
                }.Min();
            }
        }

        return distance[n, m];
    }
}