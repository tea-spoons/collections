
namespace TeaSpoons.Collections.Editor.Tests
{
    using UnityEngine;
    using UnityEditor;
    using UnityEngine.TestTools;
    using NUnit.Framework;
    using System.Collections.Generic;

    public class BidirectionalDictionaryTest
    {
        private BidirectionalDictionary<string, int> dictionary;

        [SetUp]
        public void SetUp()
        {
            dictionary = new();
        }

        [Test]
        public void Add_Contains_Count()
        {
            Assert.AreEqual(0, dictionary.Count);
            Assert.IsFalse(dictionary.ContainsKey("a"));
            Assert.IsFalse(dictionary.ContainsValue(1));
            bool success;

            success = dictionary.TryAdd("a", 1);
            Assert.IsTrue(success);
            Assert.AreEqual(1, dictionary.Count);
            Assert.IsTrue(dictionary.ContainsKey("a"));
            Assert.IsTrue(dictionary.ContainsValue(1));
            Assert.IsFalse(dictionary.ContainsValue(2));

            success = dictionary.TryAdd("a", 2);
            Assert.IsFalse(success);
            Assert.AreEqual(1, dictionary.Count);
            Assert.IsTrue(dictionary.ContainsKey("a"));
            Assert.IsTrue(dictionary.ContainsValue(1));
            Assert.IsFalse(dictionary.ContainsValue(2));

            success = dictionary.TryAdd("b", 1);
            Assert.IsTrue(success);
            Assert.AreEqual(2, dictionary.Count);
            Assert.IsTrue(dictionary.ContainsKey("a"));
            Assert.IsTrue(dictionary.ContainsKey("b"));
            Assert.IsTrue(dictionary.ContainsValue(1));
            Assert.IsFalse(dictionary.ContainsValue(2));
        }

        [Test]
        public void RemoveKey()
        {
            bool success;

            dictionary.TryAdd("a", 1);
            dictionary.TryAdd("b", 1);
            dictionary.TryAdd("c", 2);

            success = dictionary.RemoveKey("a");
            Assert.IsTrue(success);

            Assert.AreEqual(2, dictionary.Count);
            Assert.IsFalse(dictionary.ContainsKey("a"));
            Assert.IsTrue(dictionary.ContainsKey("b"));
            Assert.IsTrue(dictionary.ContainsKey("c"));
            Assert.IsTrue(dictionary.ContainsValue(1));
            Assert.IsTrue(dictionary.ContainsValue(2));
        }

        [Test]
        public void RemoveValue()
        {
            bool success;

            dictionary.TryAdd("a", 1);
            dictionary.TryAdd("b", 1);
            dictionary.TryAdd("c", 2);

            success = dictionary.RemoveValue(1);
            Assert.IsTrue(success);

            Assert.AreEqual(1, dictionary.Count);
            Assert.IsFalse(dictionary.ContainsKey("a"));
            Assert.IsFalse(dictionary.ContainsKey("b"));
            Assert.IsTrue(dictionary.ContainsKey("c"));
            Assert.IsFalse(dictionary.ContainsValue(1));
            Assert.IsTrue(dictionary.ContainsValue(2));
        }

        [Test]
        public void IndexAccessor()
        {
            dictionary.TryAdd("a", 1);
            dictionary.TryAdd("b", 2);
            dictionary.TryAdd("c", 1);

            Assert.AreEqual(1, dictionary["a"]);
            Assert.AreEqual(2, dictionary["b"]);
            Assert.AreEqual(1, dictionary["c"]);

            dictionary["c"] = 5;

            Assert.AreEqual(1, dictionary["a"]);
            Assert.AreEqual(2, dictionary["b"]);
            Assert.AreEqual(5, dictionary["c"]);

            dictionary["d"] = 6;

            Assert.AreEqual(1, dictionary["a"]);
            Assert.AreEqual(2, dictionary["b"]);
            Assert.AreEqual(5, dictionary["c"]);
            Assert.AreEqual(6, dictionary["d"]);
        }

        [Test]
        public void TryGetKeysFor()
        {
            bool success;

            dictionary.TryAdd("a", 1);
            dictionary.TryAdd("b", 1);
            dictionary.TryAdd("c", 2);

            success = dictionary.TryGetKeysFor(1, out var keys);
            Assert.IsTrue(success);
            AssertEqualContents(keys, "a", "b");

            success = dictionary.TryGetKeysFor(3, out _);
            Assert.IsFalse(success);
        }

        [Test]
        public void TryGetValue()
        {
            bool success;

            dictionary.TryAdd("a", 1);
            dictionary.TryAdd("b", 1);
            dictionary.TryAdd("c", 2);

            success = dictionary.TryGetValue("a", out var value);
            Assert.IsTrue(success);
            Assert.AreEqual(1, value);

            success = dictionary.TryGetValue("d", out _);
            Assert.IsFalse(success);
        }

        [Test]
        public void GetKeys()
        {
            dictionary.TryAdd("a", 1);
            dictionary.TryAdd("b", 1);
            dictionary.TryAdd("c", 2);

            AssertEqualContents(dictionary.Keys, "a", "b", "c");
        }

        [Test]
        public void GetValues()
        {
            dictionary.TryAdd("a", 1);
            dictionary.TryAdd("b", 1);
            dictionary.TryAdd("c", 2);

            AssertEqualContents( dictionary.Values, 1, 2);
        }

#line hidden
        private void AssertEqualContents<T>(IEnumerable<T> actual, params T[] expected)
        {
            AssertEqualContents(new HashSet<T>(actual), expected);
        }

        private void AssertEqualContents<T>(HashSet<T> actualSet, params T[] expected)
        {
            foreach (var item in expected)
            {
                Assert.IsTrue(actualSet.Remove(item));
            }

            Assert.IsEmpty(actualSet);
        }
#line default
    }
}
