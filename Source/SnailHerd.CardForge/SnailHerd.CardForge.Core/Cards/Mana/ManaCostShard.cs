using SnailHerd.CardForge.Core.Common;

namespace SnailHerd.CardForge.Core.Cards.Mana;

/// <summary>
/// Represents a mana symbol
/// </summary>
public partial class ManaCostShard : Enumeration<ManaCostShard>
{
    private static readonly KeyedCache<ManaCostShard, int> _shardCache = new(shard => shard.Id);
    public static IReadOnlyCollection<ManaCostShard> Values => _shardCache.Values.ToList();
    
    private static ManaCostShard Register(ManaAtom atomMask, string name, string? imageKey = null)
    {
        var shard = new ManaCostShard(atomMask, name, imageKey);
        _shardCache.AddOrUpdate(shard);
        return shard;
    }
    
    private ManaCostShard(ManaAtom atom, string name, string? imageKey = null)
        : this(atom.Id, name, imageKey) 
    { }
    
    private ManaCostShard(int id, string name, string? imageKey = null)
        : base(id, name)
    {
        ImageKey = imageKey ?? name;
    }

    /// <summary>
    /// Gets the shard's mana value.
    /// </summary>
    public int ManaValue => GetManaValue();

    /// <summary>
    /// Gets the shard's color mask.
    /// </summary>
    public ManaCostShard ColorMask => this & ColorsSuperposition;
    
    /// <summary>
    /// Gets the mana value, adjusted slightly to make color mana more significant.
    /// Should only be used for comparison purposes; using this method allows sorting.
    /// </summary>
    public float CompareManaValue => GetCompareManaValue();
    
    public string ImageKey { get; }

    public static ManaCostShard operator |(ManaCostShard left, ManaCostShard right)
    {
        return new ManaCostShard(left.Id | right.Id, $"{left.Name}|{right.Name}");
    }

    public static ManaCostShard operator &(ManaCostShard left, ManaCostShard right)
    {
        return new ManaCostShard(left.Id & right.Id, $"{left.Name}&{right.Name}");
    }

    public static ManaCostShard? ParseNonGeneric(string unparsed)
    {
        var atoms = ManaAtom.None;
        foreach (var c in unparsed)
        {
            atoms |= c switch
            {
                'W' => ManaAtom.White,
                'U' => ManaAtom.Blue,
                'B' => ManaAtom.Black,
                'R' => ManaAtom.Red,
                'G' => ManaAtom.Green,
                'P' => ManaAtom.OrTwoLife,
                'S' => ManaAtom.IsSnow,
                'X' => ManaAtom.IsVariable,
                'C' => ManaAtom.Colorless,
                '0' => ManaAtom.Generic,
                '1' => ManaAtom.Generic,
                '2' => ManaAtom.OrTwoGeneric,
                '3' => ManaAtom.Generic,
                '4' => ManaAtom.Generic,
                '5' => ManaAtom.Generic,
                '6' => ManaAtom.Generic,
                '7' => ManaAtom.Generic,
                '8' => ManaAtom.Generic,
                '9' => ManaAtom.Generic,
                _   => throw new ArgumentOutOfRangeException($"Unsupported mana atom '{c}'")
            };
        }

        // ManaAtom.OrTwoGeneric requires another atom for context; otherwise its just generic
        if (atoms == ManaAtom.OrTwoGeneric || atoms == (ManaAtom.OrTwoGeneric | ManaAtom.Generic))
        {
            atoms = ManaAtom.Generic;
        }

        return ValueOf(atoms);
    }

    public static ManaCostShard? ValueOf(ManaAtom atom)
    {
        return atom.Id == 0 
            ? Generic
            : Values.FirstOrDefault(shard => shard.Id == atom.Id);
    }

    private int GetManaValue()
    {
        if (0 != (Id & ManaAtom.IsVariable.Id))
        {
            return 0;
        }

        if (0 != (Id & ManaAtom.OrTwoGeneric.Id))
        {
            return 2;
        }

        return 1;
    }
    
    private float GetCompareManaValue()
    {
        const float VariableCost = 0.0001f;
        const float WhiteCost = 0.0005f;
        const float BlueCost = 0.0020f;
        const float BlackCost = 0.0080f;
        const float RedCost = 0.0320f;
        const float GreenCost = 0.1280f;
        const float PhyrexianCost = 0.00003f;
        
        if (0 != (Id & ManaAtom.IsVariable.Id))
        {
            return VariableCost;
        }

        float cost = 0 != (Id & ManaAtom.OrTwoGeneric.Id) ? 2 : 1;

        if (0 != (Id & ManaAtom.White.Id))
        {
            cost += WhiteCost;
        }
        
        if (0 != (Id & ManaAtom.Blue.Id))
        {
            cost += BlueCost;
        }
        
        if (0 != (Id & ManaAtom.Black.Id))
        {
            cost += BlackCost;
        }
        
        if (0 != (Id & ManaAtom.Red.Id))
        {
            cost += RedCost;
        }
        
        if (0 != (Id & ManaAtom.Green.Id))
        {
            cost += GreenCost;
        }

        if (0 != (Id & ManaAtom.OrTwoLife.Id))
        {
            cost += PhyrexianCost;
        }
        
        return cost;
    }
}