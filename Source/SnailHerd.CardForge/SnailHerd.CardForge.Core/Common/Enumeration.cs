namespace SnailHerd.CardForge.Core.Common;

public abstract class Enumeration<T> : IComparable<Enumeration<T>> where T : Enumeration<T>
{
    public string Name { get; }
    public int Id { get; }

    protected Enumeration(int id, string name)
    {
        (Id, Name) = (id, name);
        _idToValueMap.AddOrUpdate((T) this);
    }

    public override string ToString() => Name;

    public int CompareTo(Enumeration<T>? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return Id.CompareTo(other.Id);
    }

    private static readonly KeyedCache<T, int> _idToValueMap = new(value => value.Id);

    public static IEnumerable<T> Values => _idToValueMap.Values;
    
    public static T? Parse(string name)
    {
        return _idToValueMap.Values.FirstOrDefault(x => x.Name == name);
    }
}