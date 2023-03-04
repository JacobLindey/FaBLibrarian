using System.Collections;

namespace SnailHerd.CardForge.Core.Cards.Mana;

/// <inheritdoc cref="IManaCostParser"/>
public class ManaCostParser : IManaCostParser
{
    private readonly string[] _cost;
    private int _genericCost;
    private IEnumerable<ManaCostShard?>? _parsedShards;

    public ManaCostParser(string cost)
    {
        _cost = cost.Split(" ");
    }

    public IEnumerator<ManaCostShard?> GetEnumerator()
    {
        _parsedShards ??= DoParse();
        return _parsedShards.GetEnumerator();
    }

    private IEnumerable<ManaCostShard?> DoParse()
    {
        foreach (var unparsed in _cost)
        {
            if (int.TryParse(unparsed, out var value))
            {
                _genericCost += value;
                yield return null;
            }
            else
            {
                yield return ManaCostShard.ParseNonGeneric(unparsed);
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int TotalGenericCost
    {
        get
        {
            _parsedShards ??= DoParse();
            return _genericCost;
        }
    }
}