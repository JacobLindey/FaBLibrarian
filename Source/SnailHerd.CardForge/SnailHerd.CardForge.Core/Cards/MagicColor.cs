using SnailHerd.CardForge.Core.Common;

namespace SnailHerd.CardForge.Core.Cards;

/// <summary>
/// A bitmask that represents the MTG colors.
/// </summary>
public class MagicColor : FlagEnumeration<MagicColor>
{
    public static readonly MagicColor Colorless = new(0,      nameof(Colorless), "C");
    
    public static readonly MagicColor White     = new(1 << 0, nameof(White),     "W");
    public static readonly MagicColor Blue      = new(1 << 1, nameof(Blue),      "U");
    public static readonly MagicColor Black     = new(1 << 2, nameof(Black),     "B");
    public static readonly MagicColor Red       = new(1 << 3, nameof(Red),       "R");
    public static readonly MagicColor Green     = new(1 << 4, nameof(Green),     "G");
    
    public static readonly MagicColor AllColors = White | Blue | Black | Red | Green;

    public static readonly MagicColor[] WUBRG =
    {
        White, 
        Blue, 
        Black, 
        Red, 
        Green
    };

    public static readonly MagicColor[] ColorPair =
    {
        White | Blue,
        Blue  | Black,
        Black | Red,
        Red   | Green,
        Green | White,
        White | Black,
        Blue  | Red,
        Black | Green,
        Red   | White,
        Green | Blue
    };

    private MagicColor(int id, string name, string shortName)
        : base(id, name)
    {
        ShortName = shortName;
    }
    
    public string ShortName { get; }
    
    public static MagicColor operator |(MagicColor left, MagicColor right)
    {
        return new MagicColor(
            left.Id | right.Id,
            left.Name + "|" + right.Name,
            left.ShortName + "&" + right.ShortName
        );
    }

    public static MagicColor operator &(MagicColor left, MagicColor right)
    {
        return new MagicColor(
            left.Id & right.Id,
            left.Name + "&" + right.Name,
            left.ShortName + "&" + right.ShortName
        );
    }
}