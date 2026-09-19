namespace TeaSpoons.Collections.Editor.Tests
{
    using UnityEngine;
    using UnityEditor;
    using UnityEngine.TestTools;
    using NUnit.Framework;
    using System.Collections.Generic;

    public class SerializableDictionaryTest
    {
        private SerializableDictionary<string, int> dictionary;

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

            dictionary.Add("a", 1);
            Assert.AreEqual(1, dictionary.Count);
            Assert.IsTrue(dictionary.ContainsKey("a"));

            dictionary.Add("b", 1);
            Assert.AreEqual(2, dictionary.Count);
            Assert.IsTrue(dictionary.ContainsKey("a"));
            Assert.IsTrue(dictionary.ContainsKey("b"));
        }

        [Test]
        public void Remove()
        {
            bool success;

            dictionary.Add("a", 1);
            dictionary.Add("b", 1);
            dictionary.Add("c", 2);

            success = dictionary.Remove("a");
            Assert.IsTrue(success);

            Assert.AreEqual(2, dictionary.Count);
            Assert.IsFalse(dictionary.ContainsKey("a"));
            Assert.IsTrue(dictionary.ContainsKey("b"));
            Assert.IsTrue(dictionary.ContainsKey("c"));

            success = dictionary.Remove("d");
            Assert.IsFalse(success);
            Assert.AreEqual(2, dictionary.Count);
        }

        [Test]
        public void RemoveKeyValuePair()
        {
            bool success;

            dictionary.Add("a", 1);
            dictionary.Add("b", 2);

            success = dictionary.Remove(new KeyValuePair<string, int>("a", 1));
            Assert.IsTrue(success);
            Assert.AreEqual(1, dictionary.Count);
            Assert.IsFalse(dictionary.ContainsKey("a"));

            success = dictionary.Remove(new KeyValuePair<string, int>("b", 1));
            Assert.IsFalse(success);
            Assert.AreEqual(1, dictionary.Count);
            Assert.IsTrue(dictionary.ContainsKey("b"));

            success = dictionary.Remove(new KeyValuePair<string, int>("c", 3));
            Assert.IsFalse(success);
            Assert.AreEqual(1, dictionary.Count);
        }

        [Test]
        public void IndexAccessor()
        {
            dictionary.Add("a", 1);
            dictionary.Add("b", 2);
            dictionary.Add("c", 1);

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
            Assert.AreEqual(4, dictionary.Count);
        }

        [Test]
        public void TryGetValue()
        {
            bool success;

            dictionary.Add("a", 1);
            dictionary.Add("b", 2);

            success = dictionary.TryGetValue("a", out var value);
            Assert.IsTrue(success);
            Assert.AreEqual(1, value);

            success = dictionary.TryGetValue("c", out _);
            Assert.IsFalse(success);
        }

        [Test]
        public void GetKeys()
        {
            dictionary.Add("a", 1);
            dictionary.Add("b", 1);
            dictionary.Add("c", 2);

            AssertEqualContents(dictionary.Keys, "a", "b", "c");
        }

        [Test]
        public void GetValues()
        {
            dictionary.Add("a", 1);
            dictionary.Add("b", 1);
            dictionary.Add("c", 2);

            var values = new List<int>(dictionary.Values);
            values.Sort();
            
            Assert.AreEqual(3, values.Count);
            Assert.AreEqual(1, values[0]);
            Assert.AreEqual(1, values[1]);
            Assert.AreEqual(2, values[2]);
        }

        [Test]
        public void Clear()
        {
            dictionary.Add("a", 1);
            dictionary.Add("b", 2);
            Assert.AreEqual(2, dictionary.Count);

            dictionary.Clear();
            Assert.AreEqual(0, dictionary.Count);
            Assert.IsFalse(dictionary.ContainsKey("a"));
            Assert.IsFalse(dictionary.ContainsKey("b"));
        }

        [Test]
        public void Contains()
        {
            dictionary.Add("a", 1);
            dictionary.Add("b", 2);

            Assert.IsTrue(dictionary.Contains(new KeyValuePair<string, int>("a", 1)));
            Assert.IsTrue(dictionary.Contains(new KeyValuePair<string, int>("b", 2)));
            Assert.IsFalse(dictionary.Contains(new KeyValuePair<string, int>("a", 2)));
            Assert.IsFalse(dictionary.Contains(new KeyValuePair<string, int>("c", 1)));
        }

        [Test]
        public void CopyTo()
        {
            dictionary.Add("a", 1);
            dictionary.Add("b", 2);

            var array = new KeyValuePair<string, int>[4];
            dictionary.CopyTo(array, 1);

            Assert.AreEqual(default(KeyValuePair<string, int>), array[0]);
            Assert.AreEqual(default(KeyValuePair<string, int>), array[3]);

            var copiedPairs = new HashSet<KeyValuePair<string, int>> { array[1], array[2] };
            Assert.IsTrue(copiedPairs.Contains(new KeyValuePair<string, int>("a", 1)));
            Assert.IsTrue(copiedPairs.Contains(new KeyValuePair<string, int>("b", 2)));
        }

        [Test]
        public void Enumeration()
        {
            dictionary.Add("a", 1);
            dictionary.Add("b", 2);

            var pairs = new List<KeyValuePair<string, int>>();
            foreach (var kvp in dictionary)
            {
                pairs.Add(kvp);
            }

            Assert.AreEqual(2, pairs.Count);
            AssertEqualContents(pairs, new KeyValuePair<string, int>("a", 1), new KeyValuePair<string, int>("b", 2));
        }

        [Test]
        public void IsReadOnly()
        {
            Assert.IsFalse(dictionary.IsReadOnly);
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
