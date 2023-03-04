using SnailHerd.CardForge.Core.Common;

namespace SnailHerd.CardForge.Core.Cards.Mana;

/// <summary>
/// Represents aspects of mana symbols as a bitmask.
/// </summary>
public class ManaAtom : FlagEnumeration<ManaAtom>
{
    public static readonly ManaAtom None = new(0, nameof(None));
    public static readonly ManaAtom White = new(MagicColor.White);
    public static readonly ManaAtom Blue = new(MagicColor.Blue);
    public static readonly ManaAtom Black = new(MagicColor.Black);
    public static readonly ManaAtom Red = new(MagicColor.Red);
    public static readonly ManaAtom Green = new(MagicColor.Green);
    public static readonly ManaAtom Colorless = new(1 << 5, nameof(Colorless));
    
    public static readonly ManaAtom Generic = new(1 << 6, nameof(Generic));
    
    public static readonly ManaAtom IsVariable = new(1 << 8, nameof(IsVariable));
    public static readonly ManaAtom OrTwoGeneric = new(1 << 9, nameof(OrTwoGeneric));
    public static readonly ManaAtom OrTwoLife = new(1 << 10, nameof(OrTwoLife));
    public static readonly ManaAtom IsSnow = new(1 << 11, nameof(IsSnow));

    public static readonly ManaAtom[] ManaColors = { White, Blue, Black, Red, Green };
    public static readonly ManaAtom[] ManaTypes = {White, Blue, Black, Red, Green, Colorless};

    public static readonly ManaAtom AllManaColors = White | Blue | Black | Red | Green;
    public static readonly ManaAtom AllManaTypes = AllManaColors | Colorless;
    
    private ManaAtom(MagicColor color) : this(color.Id, color.Name) {}
    
    private ManaAtom(int id, string name)
        : base(id, name)
    { }

    public static ManaAtom operator |(ManaAtom left, ManaAtom right)
    {
        return new ManaAtom(left.Id | right.Id, $"{left.Name}|{right.Name}");
    }
}