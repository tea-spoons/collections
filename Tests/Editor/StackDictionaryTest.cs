
namespace TeaSpoons.Collections.Editor.Tests
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using UnityEngine.TestTools;

    public class StackDictionaryTest : CollectionDictionaryTest
    {
        private StackDictionary<string, int> dictionary;
        protected override ICollectionDictionary<string, int> baseDictionary => dictionary;

        [SetUp]
        public void SetUp()
        {
            dictionary = new StackDictionary<string, int>();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void Push_Pop_Peek_Contains_Count()
        {
            AssertKeyAndValueCount(0, 0);
            AssertFirstAndSecondKey(false, false);
            int popped;

            dictionary.Push("first", 1);
            AssertKeyAndValueCount(1, 1);
            AssertFirstAndSecondKey(true, false);
            Assert.AreEqual(1, dictionary.Peek("first"));

            dictionary.Push("first", 2);
            AssertKeyAndValueCount(1, 2);
            AssertFirstAndSecondKey(true, false);
            Assert.AreEqual(2, dictionary.Peek("first"));

            dictionary.Push("second", 1);
            AssertKeyAndValueCount(2, 3);
            AssertFirstAndSecondKey(true, true);
            Assert.AreEqual(2, dictionary.Peek("first"));
            Assert.AreEqual(1, dictionary.Peek("second"));

            dictionary.Push("first", 3);
            AssertKeyAndValueCount(2, 4);
            AssertFirstAndSecondKey(true, true);
            Assert.AreEqual(3, dictionary.Peek("first"));
            Assert.AreEqual(1, dictionary.Peek("second"));

            popped = dictionary.Pop("first");
            Assert.AreEqual(3, popped);
            AssertKeyAndValueCount(2, 3);
            AssertFirstAndSecondKey(true, true);
            Assert.AreEqual(2, dictionary.Peek("first"));
            Assert.AreEqual(1, dictionary.Peek("second"));

            popped = dictionary.Pop("second");
            Assert.AreEqual(1, popped);
            AssertKeyAndValueCount(1, 2);
            AssertFirstAndSecondKey(true, false);
            Assert.AreEqual(2, dictionary.Peek("first"));
        }

        [Test]
        public void TryPop_TryPeek()
        {
            dictionary.Add("first", 1);
            dictionary.Add("first", 2);
            dictionary.Add("second", 1);
            AssertKeyAndValueCount(2, 3);
            AssertFirstAndSecondKey(true, true);

            int peekedOrPopped;
            bool success;

            success = dictionary.TryPeek("first", out peekedOrPopped);
            Assert.IsTrue(success);
            Assert.AreEqual(2, peekedOrPopped);
            AssertKeyAndValueCount(2, 3);
            AssertFirstAndSecondKey(true, true);

            success = dictionary.TryPeek("second", out peekedOrPopped);
            Assert.IsTrue(success);
            Assert.AreEqual(1, peekedOrPopped);
            AssertKeyAndValueCount(2, 3);
            AssertFirstAndSecondKey(true, true);

            success = dictionary.TryPop("first", out peekedOrPopped);
            Assert.IsTrue(success);
            Assert.AreEqual(2, peekedOrPopped);
            AssertKeyAndValueCount(2, 2);
            AssertFirstAndSecondKey(true, true);

            success = dictionary.TryPop("first", out peekedOrPopped);
            Assert.IsTrue(success);
            Assert.AreEqual(1, peekedOrPopped);
            AssertKeyAndValueCount(1, 1);
            AssertFirstAndSecondKey(false, true);

            success = dictionary.TryPop("first", out peekedOrPopped);
            Assert.IsFalse(success);
            AssertKeyAndValueCount(1, 1);
            AssertFirstAndSecondKey(false, true);

            success = dictionary.TryPop("second", out peekedOrPopped);
            Assert.IsTrue(success);
            Assert.AreEqual(1, peekedOrPopped);
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
