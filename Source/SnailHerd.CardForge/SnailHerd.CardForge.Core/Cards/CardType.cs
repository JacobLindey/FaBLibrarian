using System.Text;
using SnailHerd.CardForge.Core.Common;

namespace SnailHerd.CardForge.Core.Cards;

public class CardType : IComparable<CardType>
{
    private readonly HashSet<CoreType> _coreTypes = new();
    private readonly HashSet<SuperType> _superTypes = new();
    private readonly HashSet<string> _subTypes = new();

    private bool _allCreatureTypes;
    private readonly HashSet<string> _excludedCreatureSubtypes = new();

    private readonly bool _incomplete;

    public CardType(bool incomplete)
    {
        _incomplete = incomplete;
    }

    public CardType(IEnumerable<string> from, bool incomplete)
    {
        _incomplete = incomplete;
        AddAll(from);
    }

    public CardType(CardType from)
    {
        AddAll(from);
        _allCreatureTypes = from._allCreatureTypes;
        _excludedCreatureSubtypes.Extend(from._excludedCreatureSubtypes);
    }

    public bool Add(string type)
    {
        bool changed;
        var ct = CoreType.Parse(type);
        if (ct is not null)
        {
            changed = _coreTypes.Add(ct);
        }
        else
        {
            var st = SuperType.Parse(type);
            changed = st is not null 
                ? _superTypes.Add(st) 
                : _subTypes.Add(type);
        }

        return changed;
    }

    public bool AddAll(CardType type)
    {
        var changed = false;
        changed |= _coreTypes.Extend(type._coreTypes);
        changed |= _superTypes.Extend(type._superTypes);
        changed |= _subTypes.Extend(type._subTypes);
        SanisfySubtypes();
        return changed;
    }
    
    public bool AddAll(IEnumerable<string> types)
    {
        var changed = false;
        foreach (var t in types)
        {
            if (Add(t))
            {
                changed = true;
            }
        }

        SanisfySubtypes();
        return changed;
    }


    
    private void SanisfySubtypes()
    {
        if (_incomplete)
        {
            return;
        }

        if (!IsCreature() && !IsTribal())
        {
            _allCreatureTypes = false;
        }

        if (!_subTypes.Any())
        {
            return;
        }

        if (!IsCreature() && !IsTribal())
        {
            _subTypes.RemoveWhere(IsCreatureType);
        }
    }

    public bool IsCreature()
    {
        return _coreTypes.Contains(CoreType.Creature);
    }

    public bool IsPlaneswalker()
    {
        return _coreTypes.Contains(CoreType.Planeswalker);
    }

    public bool IsTribal()
    {
        return _coreTypes.Contains(CoreType.Tribal);
    }
    
    public static bool IsCreatureType(string type)
    {
        return Constant.CreatureTypes.Contains(type);
    }

    public static class Constant
    {
        public static HashSet<string> BasicTypes { get; } = new();
        public static HashSet<string> LandTypes { get; } = new();
        public static HashSet<string> CreatureTypes { get; } = new();
        public static HashSet<string> SpellTypes { get; } = new();
        public static HashSet<string> EnchantmentTypes { get; } = new();
        public static HashSet<string> ArtifactTypes { get; } = new();
        public static HashSet<string> PlaneswalkerTypes { get; } = new();
        public static HashSet<string> DungeonTypes { get; } = new();

        public static Dictionary<string, string> SingularToPlural { get; } = new();

        public static Dictionary<string, string> PluralToSingular => SingularToPlural
           .ToDictionary(
                x => x.Value,
                x => x.Key
            );

        static Constant()
        {
            foreach (var c in CoreType.Values)
            {
                SingularToPlural.Add(c.Name, c.PluralName);
            }
        }
    }
    
    public class CoreType : Enumeration<CoreType>
    {
        public static readonly CoreType Artifact = new(nameof(Artifact), true, "artifacts");
        public static readonly CoreType Enchantment = new(nameof(Enchantment), true, "enchantments");
        public static readonly CoreType Creature = new(nameof(Creature), true, "creatures");
        public static readonly CoreType Instant = new(nameof(Instant), false, "instants");
        public static readonly CoreType Land = new(nameof(Land), true, "lands");
        public static readonly CoreType Planeswalker = new(nameof(Planeswalker), true, "planeswalkers");
        public static readonly CoreType Sorcery = new(nameof(Sorcery), false, "sorceries");
        public static readonly CoreType Tribal = new(nameof(Tribal), false, "tribals");
        
        public bool IsPermanent { get; }
        public string PluralName { get; }
        
        private CoreType(string name, bool isPermanent, string pluralName)
            : base(NextId(), name)
        {
            IsPermanent = isPermanent;
            PluralName = pluralName;
        }
        
        private static int currentId;
        private static int NextId()
        {
            var id = currentId;
            currentId++;
            return id;
        }
    }

    public class SuperType : Enumeration<SuperType>
    {
        public static readonly SuperType Basic = new(nameof(Basic));
        public static readonly SuperType Legendary = new(nameof(Legendary));
        
        public SuperType(string name) 
            : base(NextId(), name)
        { }

        private static int currentId;
        private static int NextId()
        {
            var id = currentId;
            currentId++;
            return id;
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder(string.Join(' ', GetTypesBeforeDash()));

        if (_subTypes.Any() || HasAllCreatureTypes())
        {
            sb.Append(" - ");
        }

        if (_subTypes.Any())
        {
            sb.Append(string.Join(' ', _subTypes));
        }

        if (HasAllCreatureTypes())
        {
            if (_subTypes.Any())
            {
                sb.Append(' ');
            }

            sb.Append("(All");
            if (_excludedCreatureSubtypes.Any())
            {
                sb.Append(" except ").Append(string.Join(' ', _excludedCreatureSubtypes));
            }
            sb.Append(')');
        }

        return sb.ToString();
    }

    public bool HasAllCreatureTypes()
    {
        if (!IsCreature() && !IsTribal())
        {
            return false;
        }

        return _allCreatureTypes;
    }
    
    private IEnumerable<string> GetTypesBeforeDash()
    {
        var types = new HashSet<string>();
        
        foreach (var st in _superTypes)
        {
            types.Add(st.Name);
        }

        foreach (var ct in _coreTypes)
        {
            types.Add(ct.Name);
        }

        return types;
    }

    public int CompareTo(CardType? other)
    {
        return string.Compare(ToString(), other?.ToString(), StringComparison.InvariantCultureIgnoreCase);
    }
}