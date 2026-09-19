
namespace TeaSpoons.Collections.Editor.Tests
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using UnityEngine.TestTools;

    public class QueueDictionaryTest : CollectionDictionaryTest
    {
        private QueueDictionary<string, int> dictionary;
        protected override ICollectionDictionary<string, int> baseDictionary => dictionary;

        [SetUp]
        public void SetUp()
        {
            dictionary = new QueueDictionary<string, int>();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void Enqueue_Dequeue_Peek_Contains_Count()
        {
            AssertKeyAndValueCount(0, 0);
            AssertFirstAndSecondKey(false, false);
            int dequeued;

            dictionary.Enqueue("first", 1);
            AssertKeyAndValueCount(1, 1);
            AssertFirstAndSecondKey(true, false);
            Assert.AreEqual(1, dictionary.Peek("first"));

            dictionary.Enqueue("first", 2);
            AssertKeyAndValueCount(1, 2);
            AssertFirstAndSecondKey(true, false);
            Assert.AreEqual(1, dictionary.Peek("first"));

            dictionary.Enqueue("second", 1);
            AssertKeyAndValueCount(2, 3);
            AssertFirstAndSecondKey(true, true);
            Assert.AreEqual(1, dictionary.Peek("first"));
            Assert.AreEqual(1, dictionary.Peek("second"));

            dictionary.Enqueue("first", 3);
            AssertKeyAndValueCount(2, 4);
            AssertFirstAndSecondKey(true, true);
            Assert.AreEqual(1, dictionary.Peek("first"));
            Assert.AreEqual(1, dictionary.Peek("second"));

            dequeued = dictionary.Dequeue("first");
            Assert.AreEqual(1, dequeued);
            AssertKeyAndValueCount(2, 3);
            AssertFirstAndSecondKey(true, true);
            Assert.AreEqual(2, dictionary.Peek("first"));
            Assert.AreEqual(1, dictionary.Peek("second"));

            dequeued = dictionary.Dequeue("second");
            Assert.AreEqual(1, dequeued);
            AssertKeyAndValueCount(1, 2);
            AssertFirstAndSecondKey(true, false);
            Assert.AreEqual(2, dictionary.Peek("first"));
        }

        [Test]
        public void TryDequeue_TryPeek()
        {
            dictionary.Add("first", 1);
            dictionary.Add("first", 2);
            dictionary.Add("second", 1);
            AssertKeyAndValueCount(2, 3);
            AssertFirstAndSecondKey(true, true);

            int peekedOrDequeued;
            bool success;

            success = dictionary.TryPeek("first", out peekedOrDequeued);
            Assert.IsTrue(success);
            Assert.AreEqual(1, peekedOrDequeued);
            AssertKeyAndValueCount(2, 3);
            AssertFirstAndSecondKey(true, true);

            success = dictionary.TryPeek("second", out peekedOrDequeued);
            Assert.IsTrue(success);
            Assert.AreEqual(1, peekedOrDequeued);
            AssertKeyAndValueCount(2, 3);
            AssertFirstAndSecondKey(true, true);

            success = dictionary.TryDequeue("first", out peekedOrDequeued);
            Assert.IsTrue(success);
            Assert.AreEqual(1, peekedOrDequeued);
            AssertKeyAndValueCount(2, 2);
            AssertFirstAndSecondKey(true, true);

            success = dictionary.TryDequeue("first", out peekedOrDequeued);
            Assert.IsTrue(success);
            Assert.AreEqual(2, peekedOrDequeued);
            AssertKeyAndValueCount(1, 1);
            AssertFirstAndSecondKey(false, true);

            success = dictionary.TryDequeue("first", out peekedOrDequeued);
            Assert.IsFalse(success);
            AssertKeyAndValueCount(1, 1);
            AssertFirstAndSecondKey(false, true);

            success = dictionary.TryDequeue("second", out peekedOrDequeued);
            Assert.IsTrue(success);
            Assert.AreEqual(1, peekedOrDequeued);
            AssertKeyAndValueCount(0, 0);
            AssertFirstAndSecondKey(false, false);
        }

        [Test]
        public void RemoveKey()
        {
            dictionary.Add("first", 1);
            dictionary.Add("first", 2);
            dictionary.Add("first", 3);
            dictionary.Add("first", 4);
            dictionary.Add("second", 1);
            AssertKeyAndValueCount(2, 5);
            AssertFirstAndSecondKey(true, true);
            bool success;

            success = dictionary.RemoveKey("first");
            Assert.IsTrue(success);
            AssertKeyAndValueCount(1, 1);
            AssertFirstAndSecondKey(false, true);

            success = dictionary.RemoveKey("first");
            Assert.IsFalse(success);
            AssertKeyAndValueCount(1, 1);
            AssertFirstAndSecondKey(false, true);
        }

        [Test]
        public void Clear()
        {
            dictionary.Add("first", 1);
            dictionary.Add("first", 2);
            dictionary.Add("second", 1);

            dictionary.Clear();

            AssertKeyAndValueCount(0, 0);
            AssertFirstAndSecondKey(false, false);
        }
    }
}
