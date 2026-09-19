# Collections
Handy multi-purpose collection classes.

# Usage
All collections are in the `System.Collections.Generic` namespace.

Collection dictionaries:
- `ListDictionary<TKey, TValue>`: A dictionary of lists, similar to `Dictionary<TKey, List<TValue>>`.
- `SetDictionary<TKey, TValue>`: A dictionary of sets, similar to `Dictionary<TKey, HashSet<TValue>>`.
- `StackDictionary<TKey, TValue>`: A dictionary of stacks, similar to `Dictionary<TKey, Stack<TValue>>`.
- `QueueDictionary<TKey, TValue>`: A dictionary of queues, similar to `Dictionary<TKey, Queue<TValue>>`.
- `DictionaryDictionary<TFirstKey, TSecondKey, TValue>`: A dictionary of dictionaries, similar to `Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>>`.

Other collections:
- `UniqueDrawer<T>`: A collection of `T`s that can be drawn (removed and returned) in a specified and/or random order until the drawer is empty.
- `BidirectionalDictionary<TKey, TValue>`: Mostly a `Dictionary<TKey, TValue>` that is extended to provide performant methods with `TValue` inputs.
- `SerializableDictionary<TKey, TValue>`: A dictionary that can be serialized, similar to `Dictionary<TKey, TValue>`.

## Installation

In Unity: **Window > Package Manager > + > Add package from git URL**, then enter:

```
https://github.com/tea-spoons/collections.git
```

Pin a release by appending a tag, for example `#v0.10.0`.

## License

Copyright (c) 2026 Bigpoint. Authored by Muhammad Tarek Abdou.

Available for research, education and other noncommercial use under the [PolyForm Noncommercial 1.0.0](LICENSE.md)
license. Commercial use is not permitted.
