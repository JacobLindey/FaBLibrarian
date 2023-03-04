namespace SnailHerd.CardForge.Core.Common;

public static class SetExtensions
{
    public static bool Extend<T>(this ISet<T> set, IEnumerable<T> other)
    {
        var changed = false;
        foreach (var x in other)
        {
            if (set.Add(x))
            {
                changed = true;
            }
        }
        return changed;
    }
}