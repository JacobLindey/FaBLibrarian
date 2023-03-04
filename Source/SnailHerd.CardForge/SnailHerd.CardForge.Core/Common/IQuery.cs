namespace SnailHerd.CardForge.Core.Common;

/// <summary>
/// Enables querying a cache.
/// </summary>
/// <typeparam name="TItem"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IQuery<TItem, TKey>
{
    /// <summary>
    /// Gets the entry count.
    /// </summary>
    int Count { get; }
    
    /// <summary>
    /// Gets the items.
    /// </summary>
    IEnumerable<TItem> Values { get; }
    
    /// <summary>
    /// Gets the keys.
    /// </summary>
    IEnumerable<TKey> Keys { get; }
    
    /// <summary>
    /// Gets the items together with their keys.
    /// </summary>
    IEnumerable<KeyValuePair<TKey, TItem>> KeyValues { get; }
    
    /// <summary>
    /// Lookup a single item using the specified key.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    TItem? Lookup(TKey key);
}