
namespace TeaSpoons.Collections.Editor.Tests
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using UnityEngine.TestTools;

    public class DictionaryDictionaryTest
    {
        private DictionaryDictionary<string, string, int> dictionary;

        [SetUp]
        public void SetUp()
        {
            dictionary = new();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void Add_Remove_Contains_Count()
        {
            Assert.AreEqual(0, dictionary.Count);

            bool success;

            success = dictionary.TryAdd("first", "alpha", 1);
            Assert.IsTrue(success);
            Assert.AreEqual(1, dictionary.Count);
            Assert.AreEqual(1, dictionary.FirstKeyCount);
            Assert.AreEqual(1, dictionary["first", "alpha"]);

            success = dictionary.TryAdd("first", "alpha", 2);
            Assert.IsFalse(success);
            Assert.AreEqual(1, dictionary.Count);
            Assert.AreEqual(1, dictionary["first", "alpha"]);

            success = dictionary.TryAdd("second", "alpha", 10);
            Assert.IsTrue(success);
            Assert.AreEqual(2, dictionary.Count);
            Assert.AreEqual(2, dictionary.FirstKeyCount);
            Assert.AreEqual(10, dictionary["second", "alpha"]);

            success = dictionary.TryAdd("first", "bravo", 11);
            Assert.IsTrue(success);
            Assert.AreEqual(3, dictionary.Count);
            Assert.AreEqual(2, dictionary.FirstKeyCount);
            Assert.AreEqual(10, dictionary["second", "alpha"]);
            Assert.AreEqual(11, dictionary["first", "bravo"]);
        }

        [Test]
        public void RemoveKey()
        {
            dictionary.Add("first", "alpha", 1);
            dictionary.Add("first", "bravo", 2);
            dictionary.Add("first", "charlie", 3);
            dictionary.Add("first", "delta", 4);
            dictionary.Add("second", "alpha", 1);
            Assert.AreEqual(5, dictionary.Count);

            dictionary.RemoveKey("first");
            Assert.AreEqual(1, dictionary.Count);
        }

        [Test]
        public void Clear()
        {
            dictionary.Add("first", "alpha", 1);
            dictionary.Add("first", "bravo", 2);
            dictionary.Add("second", "alpha", 1);

            dictionary.Clear();

            Assert.AreEqual(0, dictionary.Count);
            Assert.AreEqual(0, dictionary.FirstKeyCount);
        }
    }
}
