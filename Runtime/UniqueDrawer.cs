
namespace System.Collections.Generic
{
    using Random = UnityEngine.Random;

    /// <summary>
    /// A collection of <typeparamref name="T"/>s that is drawn in a specifyable and/or random order, until the Drawer is empty.
    /// </summary>
    public sealed class UniqueDrawer<T>
    {
        public class EmptyDrawerException : Exception
        {
            public override string Message => "There is no item left to draw.";
        }

        /// <summary>
        /// A function that returns any <see cref="int"/> between 0 (inclusive) and <paramref name="maximum"/> (exclusive).
        /// Can be a random function, but does not have to be.
        /// </summary>
        public delegate int GetIndex(int maximum); 

        private readonly List<T> list;
        private readonly GetIndex getIndex;
        public int ItemCount => list.Count;

        /// <summary>
        /// Creates a new <see cref="UniqueDrawer{T}"/>.
        /// </summary>
        /// <param name="items">The items to draw.</param>
        /// <param name="getIndex">A <see cref="GetIndex"/> function that is used to draw items.</param>
        public UniqueDrawer(IEnumerable<T> items, GetIndex getIndex = null)
        {
            this.getIndex = getIndex ?? (max => Random.Range(0, max));

            list = new List<T>(items);
        }

        /// <summary>
        /// Empties the drawer, then fills it with <paramref name="items"/>.
        /// </summary>
        public void Fill(IEnumerable<T> items)
        {
            list.Clear();
            list.AddRange(items);
        }

        /// <summary>
        /// Draws one item, removing it from the Drawer in the process.
        /// </summary>
        /// <exception cref="EmptyDrawerException">Thrown when the drawer is empty.</exception>
        public T Draw()
        {
            if (ItemCount == 0)
            {
                throw new EmptyDrawerException();
            }

            var index = getIndex(list.Count);
            var item = list[index];
            list.RemoveAt(index);
            return item;
        }
    }
}
