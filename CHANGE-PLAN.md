# Change plan

> Draft. This file tracks what changed on the way to this repo and what I plan to change next. Edit freely.

## Origin

Originally developed at Bigpoint. Published here with Bigpoint's permission for research, education and other noncommercial use. Copyright (c) 2026 Bigpoint; see [LICENSE.md](LICENSE.md).

## Changes made before publishing

- Namespaces are now `TeaSpoons.*` and the package id is `com.tea-spoons.collections` (assemblies renamed to match).
- Internal build, registry and tracker references were removed; the repo uses GitHub Actions (`CI` and `Release`) built on `unity-ci-kit`.
- Added `LICENSE.md` (PolyForm Noncommercial 1.0.0), an install section in the README, and package metadata (author, license and documentation URLs).

## Planned changes

- [x] Tag and publish `v0.10.0` with the Release workflow.
- [ ] Run this package's tests in CI with `unity-ci-kit` (needs a small test-project helper in the kit).
<!-- review-items:start -->
- [ ] **P1** Make mutation cheap: mark the dictionary dirty and rebuild the entries list only when it is needed (or update it incrementally), while keeping inspector editing working. Benchmark 10,000 inserts before and after.
- [ ] **P1** Check that the drawer flags duplicate and `null` keys the way the reference project does, and add a test.
- [ ] **P1** Declares `unity: 2022.3`, but only Unity 6000.3.8f1 was tested. Add a Unity version matrix to CI once package tests run there (see the `unity-ci-kit` plan), or raise the minimum.
- [ ] **P2** Decide about the namespace: keep `System.Collections.Generic` for source compatibility, or move to `TeaSpoons.Collections` in a major version. Either way document the `ListDictionary` name clash.
- [ ] **P2** Document which of the collections Unity can serialize (dictionaries of lists cannot be, without a wrapper).
- [ ] **P2** Add a `CHANGELOG.md`. Unity's package layout lists one next to `README.md`, and the `unity-ci-kit` validator warns without it.
- [ ] **P2** The README is only 38 lines. Add a short example for each public type.
<!-- review-items:end -->

<!-- review:start -->
## Review (September 2026)

Reviewed as a senior Unity engineer would: I read the code and compared the package with similar open-source projects (September 2026). Those projects are listed for ideas only. Nothing was copied from them, and their licenses are noted in case code is ever reused. Priorities: **P0** correctness bug or broken metadata, **P1** should be done soon, **P2** nice to have.

### Compared with

| Project | License | Worth noting |
|---|---|---|
| [azixMcAze/Unity-SerializableDictionary](https://github.com/azixMcAze/Unity-SerializableDictionary) | MIT | Stores keys and values as arrays behind a property drawer. The inspector flags duplicate and `null` keys and warns about data loss. Lists or arrays as values need an extra `Storage<T>` wrapper class, and multi-object editing is documented as unsupported. |

### Findings from reading the code

- **[Perf]** `SerializableDictionary` calls `SerializeToList()` after every `Add`, indexer set and `Remove` (`SerializableDictionary.cs`, line 230). It clears and refills the whole entries list each time, so filling n items is O(n^2). `OnBeforeSerialize` is empty on purpose (comment at line 213: filling the list there would break inspector editing).
- **[Naming]** The types live in `System.Collections.Generic`. `ListDictionary` has the same name as `System.Collections.Specialized.ListDictionary` in the BCL, so code that imports both namespaces gets an ambiguous reference (CS0104).
- **[Good]** 8 test files, 1,077 lines: the best-tested package of the group.
<!-- review:end -->

## Notes and ideas

_Add your own here._
