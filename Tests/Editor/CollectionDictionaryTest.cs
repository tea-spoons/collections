
namespace TeaSpoons.Collections.Editor.Tests
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using UnityEngine.TestTools;

    public abstract class CollectionDictionaryTest
    {
        protected abstract ICollectionDictionary<string, int> baseDictionary { get; }

#line hidden
        protected void AssertKeyAndValueCount(int keyCount, int valueCount)
        {
            Assert.AreEqual(keyCount, baseDictionary.KeyCount, "KeyCount");
            Assert.AreEqual(valueCount, baseDictionary.ValueCount, "ValueCount");
        }

        protected void AssertFirstAndSecondKey(bool first, bool second)
        {
            Assert.AreEqual(first, baseDictionary.ContainsKey("first"), "Key \"first\"");
            Assert.AreEqual(second, baseDictionary.ContainsKey("second"), "Key \"second\"");
        }
#line default
    }
}
