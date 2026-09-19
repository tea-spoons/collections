
namespace System.Collections.Generic
{
    using System.Linq;

    /// <summary>
    /// A dictionary of dictionaries containing <typeparamref name="TValue"/>s.
    /// </summary>
    public sealed class DictionaryDictionary<TFirstKey, TSecondKey, TValue>
    {
        private readonly Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>> dictionary = new();

        /// <summary>
        /// Returns the total number of elements in the dictionary.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Returns the total number of first keys in the dictionary.
        /// </summary>
        public int FirstKeyCount => dictionary.Count;

        /// <summary>
        /// Gets or sets the value for <paramref name="firstKey"/> and <paramref name="secondKey"/>.
        /// </summary>
        public TValue this[TFirstKey firstKey, TSecondKey secondKey]
        {
            get => dictionary[firstKey][secondKey];
            set => GetOrCreateInnerDictionary(firstKey)[secondKey] = value;
        }

        /// <summary>
        /// Adds the given <paramref name="value"/> for <paramref name="firstKey"/> and <paramref name="secondKey"/>
        /// if there is none yet.
        /// </summary>
        /// <returns>
        ///   <c>true</c> is there was no value for that key pair yet,
        ///   <c>false</c> if there was one (which was kept).
        /// </returns>
        public bool TryAdd(TFirstKey firstKey, TSecondKey secondKey, TValue value)
        {
            if (GetOrCreateInnerDictionary(firstKey).TryAdd(secondKey, value))
            {
                Count++;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds the given <paramref name="value"/> for <paramref name="firstKey"/> and <paramref name="secondKey"/>
        /// if there is none yet.
        /// </summary>
        public void Add(TFirstKey firstKey, TSecondKey secondKey, TValue value)
        {
            GetOrCreateInnerDictionary(firstKey).Add(secondKey, value);
            Count++;
        }

        /// <summary>
        /// Removes the value for <paramref name="firstKey"/> and <paramref name="secondKey"/>.
        /// </summary>
        /// <returns><c>true</c> if there was a value that was now removed, <c>false</c> otherwise.</returns>
        public bool Remove(TFirstKey firstKey, TSecondKey secondKey)
        {
            if (dictionary.TryGetValue(firstKey, out var innerDictionary) &&
                innerDictionary.Remove(secondKey))
            {
                Count--;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes all values from <paramref name="firstKey"/>.
        /// </summary>
        public void RemoveKey(TFirstKey firstKey)
        {
            if (dictionary.TryGetValue(firstKey, out var innerDictionary))
            {
                Count -= innerDictionary.Count;
                innerDictionary.Clear();
            }
        }

        /// <summary>
        /// Returns the number of key-value pairs assigned to <paramref name="firstKey"/>.
        /// </summary>
        public int GetSecondKeyCount(TFirstKey firstKey)
        {
            if (dictionary.TryGetValue(firstKey, out var innerDictionary))
            {
                return innerDictionary.Count;
            }

            return 0;
        }

        /// <summary>
        /// Attempts to get the value for <paramref name="firstKey"/> and <paramref name="secondKey"/>.
        /// </summary>
        /// <param name="value">The assigned value, if there is one.</param>
        /// <returns>Whether there is a value assigned to the key pair.</returns>
        public bool TryGetValue(TFirstKey firstKey, TSecondKey secondKey, out TValue value)
        {
            if (dictionary.TryGetValue(firstKey, out var innerDictionary))
            {
                return innerDictionary.TryGetValue(secondKey, out value);
            }

            value = default;
            return false;
        }

        /// <summary>
        /// Removes all values from the dictionary.
        /// </summary>
        public void Clear()
        {
            dictionary.Clear();
            Count = 0;
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> containing all values assigned to  for <paramref name="firstKey"/>.
        /// </summary>
        public IEnumerable<TValue> GetAllValues(TFirstKey firstKey)
        {
            if (dictionary.TryGetValue(firstKey, out var innerDictionary))
            {
                return innerDictionary.Values;
            }

            return Enumerable.Empty<TValue>();
        }

        private Dictionary<TSecondKey, TValue> GetOrCreateInnerDictionary(TFirstKey firstKey)
        {
            if (!dictionary.TryGetValue(firstKey, out var innerDictionary))
            {
                innerDictionary = new();
                dictionary.Add(firstKey, innerDictionary);
            }

            return innerDictionary;
        }
    }
}
