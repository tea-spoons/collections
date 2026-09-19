
namespace System.Collections.Generic
{
    /// <summary>
    /// A dictionary containing a <see cref="Queue{TValue}"/> for each <typeparamref name="TKey"/>.
    /// </summary>
    public class QueueDictionary<TKey, TValue> : CollectionDictionary<TKey, Queue<TValue>, TValue>
    {
        protected override bool Add(Queue<TValue> collection, TValue value)
        {
            collection.Enqueue(value);
            return true;
        }

        /// <summary>
        /// Enqueues <paramref name="value"/> in <paramref name="key"/>'s queue.
        /// </summary>
        /// <remarks>
        /// Alias for <see cref="Add(Queue{TValue}, TValue)"/>.
        /// </remarks>
        public void Enqueue(TKey key, TValue value)
        {
            Add(key, value);
        }

        /// <summary>
        /// Attempts to dequeue a <paramref name="value"/> from <paramref name="key"/>'s queue.
        /// </summary>
        /// <returns><c>true</c> if a <paramref name="value"/> was dequeued from <paramref name="key"/>'s queue.</returns>
        public bool TryDequeue(TKey key, out TValue value)
        {
            value = default;

            var collection = GetCollection(key, false);
            if (collection != null)
            {
                var didDequeue = collection.TryDequeue(out value);
                if (didDequeue)
                {
                    ValueCount--;
                    if (collection.Count == 0)
                    {
                        KeyCount--;
                    }
                }
                return didDequeue;
            }
            return false;
        }

        /// <summary>
        /// Dequeues and returns the earliest value still mapped to <paramref name="key"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when there are no values mapped to <paramref name="key"/>.</exception>
        public TValue Dequeue(TKey key)
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
                return collection.Dequeue();
            }
            throw new InvalidOperationException($"There are no values mapped to {key}.");
        }

        /// <summary>
        /// Attempts to return the earliest <paramref name="value"/> in <paramref name="key"/>'s queue.
        /// </summary>
        /// <returns><c>true</c> if a <paramref name="value"/> exists in <paramref name="key"/>'s queue.</returns>
        public bool TryPeek(TKey key, out TValue value)
        {
            value = default;
            return GetCollection(key, false)?.TryPeek(out value) ?? false;
        }

        /// <summary>
        /// Returns the earliest value still mapped to <paramref name="key"/>.
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

        protected override void ClearValues(Queue<TValue> collection)
        {
            collection.Clear();
        }

        protected override bool Contains(Queue<TValue> collection, TValue value)
        {
            return collection.Contains(value);
        }

        protected override int Count(Queue<TValue> collection)
        {
            return collection.Count;
        }
    }
}
