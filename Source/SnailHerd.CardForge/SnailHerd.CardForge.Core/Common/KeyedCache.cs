namespace SnailHerd.CardForge.Core.Common;

public class KeyedCache<TValue, TKey> : ICache<TValue, TKey> where TKey : notnull
{
    private readonly Dictionary<TKey, TValue> _keyToValueMap = new();
    private readonly Func<TValue, TKey> _keyMapper;

    public KeyedCache(Func<TValue, TKey> keyMapper)
    {
        _keyMapper = keyMapper;
    }

    public void AddOrUpdate(TValue value)
    {
        var key = _keyMapper(value);
        if (_keyToValueMap.ContainsKey(key))
        {
            _keyToValueMap[key] = value;
        }
        else
        {
            _keyToValueMap.Add(key, value);
        }
    }

    public void Clear()
    {
        _keyToValueMap.Clear();
    }

    public void Remove(TKey key)
    {
        _keyToValueMap.Remove(key);
    }

    public void Remove(IEnumerable<TKey> keys)
    {
        foreach (var key in keys)
        {
            Remove(key);
        }
    }

    public int Count => _keyToValueMap.Count;

    public IEnumerable<TValue> Values => _keyToValueMap.Values;

    public IEnumerable<TKey> Keys => _keyToValueMap.Keys;
    
    public IEnumerable<KeyValuePair<TKey, TValue>> KeyValues => _keyToValueMap;
    
    public TValue? Lookup(TKey key)
    {
        return _keyToValueMap.ContainsKey(key) 
            ? _keyToValueMap[key]
            : default;
    }
}