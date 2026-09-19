
namespace System.Collections.Generic
{
    /// <summary>
    /// A dictionary containing a <see cref="Stack{TValue}"/> for each <typeparamref name="TKey"/>.
    /// </summary>
    public class StackDictionary<TKey, TValue> : CollectionDictionary<TKey, Stack<TValue>, TValue>
    {
        protected override bool Add(Stack<TValue> collection, TValue value)
        {
            collection.Push(value);
            return true;
        }

        /// <summary>
        /// Pushes <paramref name="value"/> on <paramref name="key"/>'s stack.
        /// </summary>
        /// <remarks>
        /// Alias for <see cref="Add(Stack{TValue}, TValue)"/>.
        /// </remarks>
        public void Push(TKey key, TValue value)
        {
            Add(key, value);
        }

        /// <summary>
        /// Attempts to pop a <paramref name="value"/> from <paramref name="key"/>'s stack.
        /// </summary>
        /// <returns><c>true</c> if a <paramref name="value"/> was popped from <paramref name="key"/>'s stack.</returns>
        public bool TryPop(TKey key, out TValue value)
        {
            value = default;

            var collection = GetCollection(key, false);
            if (collection != null)
            {
                var didPop = collection.TryPop(out value);
                if (didPop)
                {
                    ValueCount--;
                    if (collection.Count == 0)
                    {
                        KeyCount--;
                    }
                }
                return didPop;
            }
            return false;
        }

        /// <summary>
        /// Pops and returns the latest value mapped to <paramref name="key"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when there are no values mapped to <paramref name="key"/>.</exception>
        public TValue Pop(TKey key)
        {
            var collection = GetCollection(key, false);
            var count = collection?.Count ?? 0;
            if (count > 0)
            {
                ValueCount--;
                if (count == 1)
                {
                    KeyCount--;
                }
                return collection.Pop();
            }
            throw new InvalidOperationException($"There are no values mapped to {key}.");
        }

        /// <summary>
        /// Attempts to return the latest <paramref name="value"/> added to <paramref name="key"/>'s stack.
        /// </summary>
        /// <returns><c>true</c> if a <paramref name="value"/> exists in <paramref name="key"/>'s stack.</returns>
        public bool TryPeek(TKey key, out TValue value)
        {
            value = default;
            return GetCollection(key, false)?.TryPeek(out value) ?? false;
        }

        /// <summary>
        /// Returns the latest value mapped to <paramref name="key"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when there are no values mapped to <paramref name="key"/>.</exception>
        public TValue Peek(TKey key)
        {
            var collection = GetCollection(key, false);
            if (collection?.Count > 0)
            {
                return collection.Peek();
            }
            throw new InvalidOperationException($"There are no values mapped to {key}.");
        }

        protected override void ClearValues(Stack<TValue> collection)
        {
            collection.Clear();
        }

        protected override bool Contains(Stack<TValue> collection, TValue value)
        {
            return collection.Contains(value);
        }

        protected override int Count(Stack<TValue> collection)
        {
            return collection.Count;
        }
    }
}
