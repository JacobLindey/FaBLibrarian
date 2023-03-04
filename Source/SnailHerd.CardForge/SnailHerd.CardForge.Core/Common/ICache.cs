namespace SnailHerd.CardForge.Core.Common;

/// <summary>
/// A cache that handles key/value mapping.
/// </summary>
/// <typeparam name="TItem">The type of the value object.</typeparam>
/// <typeparam name="TKey">The type of the key.</typeparam>
public interface ICache<TItem, TKey> : IQuery<TItem, TKey>
    where TKey : notnull
{
    /// <summary>
    /// Adds or updates the item.
    /// </summary>
    /// <param name="value"></param>
    void AddOrUpdate(TItem value);
    
    /// <summary>
    /// Clears all items.
    /// </summary>
    void Clear();
    
    /// <summary>
    /// Removes the item matching the specified key.
    /// </summary>
    /// <param name="key"></param>
    void Remove(TKey key);
    
    /// <summary>
    /// Removes all items matching the specified keys.
    /// </summary>
    /// <param name="keys"></param>
    void Remove(IEnumerable<TKey> keys);
}