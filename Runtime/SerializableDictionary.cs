namespace System.Collections.Generic
{
    using System;
    using System.Diagnostics;
    using UnityEngine;

    /// <summary>
    /// A dictionary that can be serialized by Unity's serialization system.
    /// </summary>
    [Serializable]
    public sealed class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver, IDictionary<TKey, TValue>
    {
        [Serializable]
        internal struct Entry
        {
            public TKey Key;
            public TValue Value;

            public Entry(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }

            public static implicit operator Entry(KeyValuePair<TKey, TValue> kvp)
            {
                return new Entry(kvp.Key, kvp.Value);
            }
        }

        [SerializeField]
        private List<Entry> entries = new();

        private readonly Dictionary<TKey, TValue> dictionary = new();

        public ICollection<TKey> Keys => dictionary.Keys;

        public ICollection<TValue> Values => dictionary.Values;

        public int Count => dictionary.Count;

        public bool IsReadOnly => false;

        public TValue this[TKey key]
        {
            get => dictionary[key];
            set
            {
                dictionary[key] = value;
                SerializeToList();
            }
        }

        /// <summary>
        /// Adds the specified key and value to the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <exception cref="ArgumentException">An element with the same key already exists in the dictionary.</exception>
        public void Add(TKey key, TValue value)
        {
            dictionary.Add(key, value);
            SerializeToList();
        }

        /// <summary>
        /// Adds the specified key-value pair to the dictionary.
        /// </summary>
        /// <param name="item">The key-value pair to add to the dictionary.</param>
        /// <exception cref="ArgumentException">An element with the same key already exists in the dictionary.</exception>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            dictionary.Add(item.Key, item.Value);
            SerializeToList();
        }

        /// <summary>
        /// Removes the element with the specified key from the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns><c>true</c> if the element is successfully removed; otherwise, <c>false</c>.</returns>
        public bool Remove(TKey key)
        {
            var didRemove = dictionary.Remove(key);

            if (didRemove)
            {
                SerializeToList();
            }

            return didRemove;
        }

        /// <summary>
        /// Removes the specified key-value pair from the dictionary.
        /// </summary>
        /// <param name="item">The key-value pair to remove from the dictionary.</param>
        /// <returns><c>true</c> if the key-value pair is successfully removed; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// The removal only succeeds if both the key exists and its value matches exactly.
        /// </remarks>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            var didRemove = dictionary.TryGetValue(item.Key, out var value) &&
                EqualityComparer<TValue>.Default.Equals(value, item.Value) &&
                dictionary.Remove(item.Key);

            if (didRemove)
            {
                SerializeToList();
            }

            return didRemove;
        }

        /// <summary>
        /// Determines whether the dictionary contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the dictionary.</param>
        /// <returns><c>true</c> if the dictionary contains an element with the key; otherwise, <c>false</c>.</returns>
        public bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Determines whether the dictionary contains the specified key-value pair.
        /// </summary>
        /// <param name="item">The key-value pair to locate in the dictionary.</param>
        /// <returns><c>true</c> if the dictionary contains the key-value pair; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// Returns <c>true</c> only if both the key exists and its value matches exactly.
        /// </remarks>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return dictionary.TryGetValue(item.Key, out var value) &&
                   EqualityComparer<TValue>.Default.Equals(value, item.Value);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value"/> parameter.</param>
        /// <returns><c>true</c> if the dictionary contains an element with the specified key; otherwise, <c>false</c>.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Gets the value associated with the specified key.<br/>
        /// However, if the key isn't present, <paramref name="defaultValue"/> is returned instead.
        /// </summary>
        public TValue GetValueOrDefault(TKey key, TValue defaultValue = default)
        {
            return dictionary.GetValueOrDefault(key, defaultValue);
        }

        /// <summary>
        /// Removes all entries from the dictionary.
        /// </summary>
        public void Clear()
        {
            dictionary.Clear();
            SerializeToList();
        }

        /// <summary>
        /// Copies the elements of the dictionary to an array, starting at a particular array index.
        /// </summary>
        /// <param name="array">The one-dimensional array that is the destination of the elements copied from the dictionary.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception>
        /// <exception cref="ArgumentException">The number of elements in the source dictionary is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.</exception>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < Count)
                throw new ArgumentException("Array is too small");

            int index = arrayIndex;
            foreach (var kvp in dictionary)
            {
                array[index++] = kvp;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the dictionary.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the dictionary.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the dictionary.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the dictionary.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            // Serializing the dictionary into the list here would break inspector editing, so this method is empty.
        }

        /// <remarks>
        /// Rebuilds the dictionary from the serialized list.
        /// </remarks>
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            dictionary.Clear();

            foreach (var entry in entries)
            {
                dictionary[entry.Key] = entry.Value;
            }
        }

        [Conditional("UNITY_EDITOR")]
        private void SerializeToList()
        {
            entries.Clear();

            foreach (var kvp in dictionary)
            {
                entries.Add(kvp);
            }
        }
    }
}
