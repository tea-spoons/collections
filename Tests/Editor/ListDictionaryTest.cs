
namespace TeaSpoons.Collections.Editor.Tests
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using UnityEngine.TestTools;

    public class ListDictionaryTest : CollectionDictionaryTest
    {
        private ListDictionary<string, int> dictionary;
        protected override ICollectionDictionary<string, int> baseDictionary => dictionary;

        [SetUp]
        public void SetUp()
        {
            dictionary = new ListDictionary<string, int>();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void Add_Remove_Contains_Count()
        {
            AssertKeyAndValueCount(0, 0);
            AssertFirstAndSecondKey(false, false);
            bool success;

            success = dictionary.Add("first", 1);
            Assert.IsTrue(success);
            AssertKeyAndValueCount(1, 1);
            AssertFirstAndSecondKey(true, false);

            success = dictionary.Add("first", 2);
            Assert.IsTrue(success);
            AssertKeyAndValueCount(1, 2);
            AssertFirstAndSecondKey(true, false);

            success = dictionary.Add("second", 1);
            Assert.IsTrue(success);
            AssertKeyAndValueCount(2, 3);
            AssertFirstAndSecondKey(true, true);

            success = dictionary.Add("first", 1);
            Assert.IsTrue(success);
            AssertKeyAndValueCount(2, 4);
            AssertFirstAndSecondKey(true, true);

            success = dictionary.Remove("first", 1);
            Assert.IsTrue(success);
            AssertKeyAndValueCount(2, 3);
            AssertFirstAndSecondKey(true, true);

            success = dictionary.Remove("second", 1);
            Assert.IsTrue(success);
            AssertKeyAndValueCount(1, 2);
            AssertFirstAndSecondKey(true, false);

            success = dictionary.Remove("first", -1);
            Assert.IsFalse(success);
            AssertKeyAndValueCount(1, 2);
            AssertFirstAndSecondKey(true, false);
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
        public void IndexerGetter()
        {
            dictionary.Add("first", 1);
            dictionary.Add("first", 2);
            dictionary.Add("second", 1);

            Assert.AreEqual(1, dictionary["first", 0]);
            Assert.AreEqual(2, dictionary["first", 1]);
            Assert.AreEqual(1, dictionary["second", 0]);
        }

        [Test]
        public void IndexerSetter()
        {
            dictionary.Add("first", 1);
            dictionary.Add("first", 2);
            dictionary.Add("second", 1);

            dictionary["first", 1] = 100;

            Assert.AreEqual(1, dictionary["first", 0]);
            Assert.AreEqual(100, dictionary["first", 1]);
            Assert.AreEqual(1, dictionary["second", 0]);
        }

        [Test]
        public void TryGetValue()
        {
            dictionary.Add("first", 1);
            dictionary.Add("first", 2);
            dictionary.Add("second", 1);

            int result;

            Assert.IsTrue(dictionary.TryGetValue("first", 0, out result));
            Assert.AreEqual(1, result);
            Assert.IsTrue(dictionary.TryGetValue("first", 1, out result));
            Assert.AreEqual(2, result);
            Assert.IsTrue(dictionary.TryGetValue("second", 0, out result));
            Assert.AreEqual(1, result);

            Assert.IsFalse(dictionary.TryGetValue("third", 0, out result));
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
