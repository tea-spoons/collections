
namespace System.Collections.Generic
{
    /// <summary>
    /// A dictionary that is extended to provide performant methods with <c>TValue</c> inputs.
    /// </summary>
    /// <remarks>
    /// <c>TKey</c> is still required to be unique, but <c>TValue</c> isn't.
    /// </remarks>
    public sealed class BidirectionalDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> forward = new();
        private readonly SetDictionary<TValue, TKey> back = new();

        public ICollection<TKey> Keys => forward.Keys;

        public ICollection<TValue> Values => back.Keys;

        public int Count => forward.Count;

        public TValue this[TKey key]
        {
            get => forward[key];
            set => Override(key, value);
        }

        /// <summary>
        /// Attempts to map <paramref name="value"/> to <paramref name="key"/>.
        /// </summary>
        /// <returns><c>true</c> if there was no value for <paramref name="key"/> before, <c>false</c> otherwise.</returns>
        public bool TryAdd(TKey key, TValue value)
        {
            if (forward.TryAdd(key, value))
            {
                back.Add(value, key);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes the mapping for <paramref name="key"/>.
        /// </summary>
        /// <returns><c>true</c> is <paramref name="key"/> had a value mapped to it before, <c>false</c> otherwise.</returns>
        public bool RemoveKey(TKey key)
        {
            if (forward.TryGetValue(key, out var value))
            {
                forward.Remove(key);
                back.Remove(value, key);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes all keys that <paramref name="value"/> is mapped to.
        /// </summary>
        /// <returns><c>true</c> if one or more keys were removed, <c>false</c> otherwise.</returns>
        public bool RemoveValue(TValue value)
        {
            if (back.TryGetValues(value, out var keys))
            {
                foreach (var key in keys)
                {
                    forward.Remove(key);
                }

                back.RemoveKey(value);

                return true;
            }

            return false;
        }

        public bool ContainsKey(TKey key)
        {
            return forward.ContainsKey(key);
        }

        public bool ContainsValue(TValue value)
        {
            return back.ContainsKey(value);
        }

        /// <summary>
        /// Provides all keys that <paramref name="value"/> is mapped to.
        /// </summary>
        /// <param name="keys">An <see cref="IEnumerable{TKey}"/> containing all the relevant keys, or <c>null</c> when there are none.</param>
        /// <returns><c>true</c> if there is one or more values mapped to <paramref name="keys"/>, <c>false</c> otherwise.</returns>
        public bool TryGetKeysFor(TValue value, out IEnumerable<TKey> keys)
        {
            return back.TryGetValues(value, out keys);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return forward.TryGetValue(key, out value);
        }

        public void Clear()
        {
            forward.Clear();
            back.Clear();
        }

        /// <summary>
        /// Overrides the value for <paramref name="key"/> with <paramref name="value"/>.
        /// Used from the outside through setting <see cref="this[TKey]"/>.
        /// </summary>
        private void Override(TKey key, TValue value)
        {
            if (forward.TryGetValue(key, out var currentValue))
            {
                back.Remove(currentValue, key);
            }

            forward[key] = value;
            back.Add(value, key);
        }
    }
}
