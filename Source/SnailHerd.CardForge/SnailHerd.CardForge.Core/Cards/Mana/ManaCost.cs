using System.Collections;

namespace SnailHerd.CardForge.Core.Cards.Mana;

/// <summary>
/// Represents a mana cost.
/// </summary>
public sealed class ManaCost : IComparable<ManaCost>, IEnumerable<ManaCostShard>
{
    public static readonly ManaCost NoCost = new(-1);
    public static readonly ManaCost Zero = new(0);
    public static readonly ManaCost One = new(1);
    public static readonly ManaCost Two = new(2);
    public static readonly ManaCost Three = new(3);
    public static readonly ManaCost Four = new(4);

    public static ManaCost FromValue(int manaValue)
    {
        return manaValue switch
        {
            0 => Zero,
            1 => One,
            2 => Two,
            3 => Three,
            4 => Four,
            _ => manaValue > 0 ? new ManaCost(manaValue) : NoCost
        };
    }

    private readonly IReadOnlyCollection<ManaCostShard> _shards;

    private ManaCost(int manaValue)
    {
        HasNoCost = manaValue < 0;
        GenericCost = manaValue < 0 ? 0 : manaValue;
        _shards = new List<ManaCostShard>();
    }

    private ManaCost(int manaValue, IEnumerable<ManaCostShard> shards)
        : this(manaValue)
    {
        _shards = shards.ToList();
    }

    public ManaCost(IManaCostParser parser)
    {
        HasNoCost = false;
        GenericCost = parser.TotalGenericCost;
        
        var shards = new List<ManaCostShard>();
        foreach (var shard in parser)
        {
            if (shard != null && shard != ManaCostShard.Generic)
            {
                shards.Add(shard);
            }
        }
        _shards = shards;
    }
    
    public int GenericCost { get; }
    public bool HasNoCost { get; }

    public float CompareWeight
    {
        get
        {
            var weight = GenericCost + _shards.Sum(shard => shard.CompareManaValue);
            if (HasNoCost)
            {
                weight -= 1; // for those cards without any listed mana cost
            }

            return weight;
        }
    }
    
    public int CompareTo(ManaCost? other)
    {
        return CompareWeight.CompareTo(other?.CompareWeight);
    }

    public IEnumerator<ManaCostShard> GetEnumerator()
    {
        return _shards.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}