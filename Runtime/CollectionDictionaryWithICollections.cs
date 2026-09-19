
namespace System.Collections.Generic
{
    /// <summary>
    /// Base class for a dictionary containing an <see cref="ICollection{TValue}"/> for each <typeparamref name="TKey"/>.
    /// </summary>
    /// <inheritdoc/>
    public abstract class CollectionDictionaryWithICollections<TKey, TCollection, TValue> : CollectionDictionary<TKey, TCollection, TValue>
        where TCollection : ICollection<TValue>, new()
    {
        /// <summary>
        /// Attempts to remove <paramref name="value"/> from <paramref name="key"/>.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the <paramref name="value"/> was removed,
        ///   <c>false</c> if it wasn't mapped to <paramref name="key"/>
        ///   or could not be removed for any other reason.
        /// </returns>
        public bool Remove(TKey key, TValue value)
        {
            var collection = GetCollection(key, false);

            if (collection != null && collection.Remove(value))
            {
                ValueCount--;

                if (Count(collection) == 0)
                {
                    KeyCount--;
                }

                return true;
            }

            return false;
        }

        protected override void ClearValues(TCollection collection)
        {
            collection.Clear();
        }

        protected override bool Contains(TCollection collection, TValue value)
        {
            return collection.Contains(value);
        }

        protected override int Count(TCollection collection)
        {
            return collection.Count;
        }
    }
}
