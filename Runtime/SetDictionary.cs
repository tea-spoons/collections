
namespace System.Collections.Generic
{
    /// <summary>
    /// A dictionary of sets containing <typeparamref name="TValue"/>s.
    /// </summary>
    /// <inheritdoc/>
    public sealed class SetDictionary<TKey, TValue> : CollectionDictionaryWithICollections<TKey, HashSet<TValue>, TValue>
    {
        protected override bool Add(HashSet<TValue> collection, TValue value)
        {
            return collection.Add(value);
        }
    }
}
