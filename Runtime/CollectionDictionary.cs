
namespace System.Collections.Generic
{
    using System.Linq;

    /// <inheritdoc/>
    public abstract class CollectionDictionary<TKey, TCollection, TValue> : ICollectionDictionary<TKey, TValue>
        where TCollection : IEnumerable<TValue>, new()
    {
        private readonly Dictionary<TKey, TCollection> dictionary = new Dictionary<TKey, TCollection>();

        public Dictionary<TKey, TCollection>.KeyCollection Keys => dictionary.Keys;

        public IEnumerable<TValue> this[TKey key]
        {
            get
            {
                return dictionary.TryGetValue(key, out var collection) ? collection : Enumerable.Empty<TValue>();
            }
        }

        public int KeyCount { get; protected set; }
        public int ValueCount { get; protected set; }


        public bool Add(TKey key, TValue value)
        {
            var collection = GetCollection(key, true);
            var collectionWasEmpty = Count(collection) == 0;

            var result = Add(collection, value);
            if (result)
            {
                ValueCount++;

                if (collectionWasEmpty)
                {
                    KeyCount++;
                }
            }
            return result;
        }

        protected abstract bool Add(TCollection collection, TValue value);

        public bool RemoveKey(TKey key)
        {
            if (dictionary.TryGetValue(key, out var values) && Count(values) > 0)
            {
                ValueCount -= Count(values);
                ClearValues(values);
                KeyCount--;

                return true;
            }

            return false;
        }

        public bool Contains(TKey key, TValue value)
        {
            var collection = GetCollection(key, false);
            if (collection != null)
            {
                return Contains(collection, value);
            }
            return false;
        }

        public bool ContainsKey(TKey key)
        {
            return GetValueCount(key) > 0;
        }

        public bool TryGetValues(TKey key, out IEnumerable<TValue> values)
        {
            if (dictionary.TryGetValue(key, out var collection) && Count(collection) > 0)
            {
                values = collection;
                return true;
            }

            values = default;
            return false;
        }

        public int GetValueCount(TKey key)
        {
            var collection = GetCollection(key, false);

            if (collection == null)
            {
                return 0;
            }

            return Count(collection);
        }

        public void Clear(bool keepCollections = true)
        {
            if (keepCollections)
            {
                foreach (var collection in dictionary.Values)
                {
                    ClearValues(collection);
                }
            }
            else
            {
                dictionary.Clear();
            }

            KeyCount = 0;
            ValueCount = 0;
        }

        protected TCollection GetCollection(TKey key, bool createIfNotFound)
        {
            TCollection result;

            if (!dictionary.TryGetValue(key, out result) && createIfNotFound)
            {
                result = new TCollection();
                dictionary[key] = result;
            }

            return result;
        }

        protected abstract int Count(TCollection collection);

        protected abstract void ClearValues(TCollection collection);

        protected abstract bool Contains(TCollection collection, TValue value);

        //protected abstract IEnumerable<TValue> GetEnumerable(TCollection collection);
    }
}
