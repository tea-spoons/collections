
namespace System.Collections.Generic
{
    /// <summary>
    /// A dictionary that can store multiple values for each key.
    /// </summary>
    /// <typeparam name="TKey">The dictionary key type.</typeparam>
    /// <typeparam name="TCollection">The collection type holding the values for each key.</typeparam>
    /// <typeparam name="TValue">The type of value being stored.</typeparam>
    public interface ICollectionDictionary<TKey, TValue>
    {
        /// <summary>
        /// The amount of keys that have one or more value in this dictionary.
        /// </summary>
        int KeyCount { get; }
        /// <summary>
        /// The total amount of values stored for any key.
        /// </summary>
        int ValueCount { get; }

        /// <summary>
        /// An <see cref="IEnumerable{TValue}"/> providing the values mapped to <paramref name="key"/>.
        /// Will always return a valid reference, even when there are no elements.
        /// </summary>
        IEnumerable<TValue> this[TKey key] { get; }

        /// <summary>
        /// Attempts to add <paramref name="value"/> to <paramref name="key"/>.
        /// </summary>
        /// <returns><c>true</c> if the value could be added.</returns>
        bool Add(TKey key, TValue value);

        /// <summary>
        /// Removes all values from <paramref name="key"/>, effectively removing the <paramref name="key"/>.
        /// </summary>
        /// <returns><c>true</c> if one or more values were removed, <c>false</c> if the key did not have any values.</returns>
        bool RemoveKey(TKey key);

        /// <summary>
        /// Returns whether <paramref name="value"/> is currently mapped to <paramref name="key"/> at least once.
        /// </summary>
        bool Contains(TKey key, TValue value);

        /// <summary>
        /// Returns whether <paramref name="key"/> is currently present in the dictionary with one or more values.
        /// </summary>
        bool ContainsKey(TKey key);

        /// <summary>
        /// Returns an <see cref="IEnumerable{TValue}"/> with all <paramref name="values"/> that are mapped to <paramref name="key"/>.
        /// </summary>
        /// <param name="values">An <see cref="IEnumerable{TValue}"/> containing all values mapped to <paramref name="key"/>. <c>null</c> if there are none.</param>
        /// <returns><c>true</c> if there are one or more values mapped to <paramref name="key"/>.</returns>
        bool TryGetValues(TKey key, out IEnumerable<TValue> values);

        /// <summary>
        /// Returns the number of values stored for the given <paramref name="key"/>.
        /// </summary>
        int GetValueCount(TKey key);

        /// <summary>
        /// Removes all keys and values.
        /// </summary>
        /// <param name="keepCollections">If <c>true</c>, all contained collections will stay referenced to avoid garbage.</param>
        void Clear(bool keepCollections = true);
    }
}
