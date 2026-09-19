
namespace System.Collections.Generic
{
    /// <summary>
    /// A dictionary of lists containing <typeparamref name="TValue"/>s.
    /// </summary>
    /// <inheritdoc/>
    public sealed class ListDictionary<TKey, TValue> : CollectionDictionaryWithICollections<TKey, List<TValue>, TValue>
    {
        public TValue this[TKey key, int index]
        {
            get
            {
                var collection = GetCollection(key, false);
                if (collection != null)
                {
                    return collection[index];
                }
                else
                {
                    throw new KeyNotFoundException(nameof(key));
                }
            }

            set
            {
                var collection = GetCollection(key, false);
                if (collection != null)
                {
                    collection[index] = value;
                }
                else
                {
                    throw new KeyNotFoundException(nameof(key));
                }
            }
        }

        public void Insert(TKey key, int index, TValue value)
        {
            var collection = GetCollection(key, index == 0);
            if (collection != null)
            {
                collection.Insert(index, value);
            }
            else
            {
                throw new KeyNotFoundException(nameof(key));
            }
        }

        public void RemoveAt(TKey key, int index)
        {
            var collection = GetCollection(key, false);
            if (collection != null)
            {
                collection.RemoveAt(index);
            }
            else
            {
                throw new KeyNotFoundException(nameof(key));
            }
        }

        public bool TryGetValue(TKey key, int index, out TValue value)
        {
            var collection = GetCollection(key, false);
            if (collection != null)
            {
                value = collection[index];
                return true;
            }

            value = default;
            return false;
        }

        protected override bool Add(List<TValue> collection, TValue value)
        {
            collection.Add(value);
            return true;
        }
    }
}
