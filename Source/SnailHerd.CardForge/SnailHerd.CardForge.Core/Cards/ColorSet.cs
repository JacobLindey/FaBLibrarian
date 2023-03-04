namespace SnailHerd.CardForge.Core.Cards;

public class ColorSet : IComparable<ColorSet>
{
    private static readonly ColorSet?[] _cache = new ColorSet?[32];

    public static readonly ColorSet AllColors = FromMask(MagicColor.AllColors);
    public static readonly ColorSet NoColors = FromMask(MagicColor.Colorless);

    public ColorSet(MagicColor colorMask)
    {
        ColorMask = colorMask;
    }
    
    public MagicColor ColorMask { get; }
    
    public static ColorSet FromMask(MagicColor mask)
    {
        var colorMask = mask & MagicColor.AllColors;
        return _cache[colorMask.Id] ??= new ColorSet(colorMask);
    }

    public int CompareTo(ColorSet? other)
    {
        return ColorMask.CompareTo(other?.ColorMask);
    }
}