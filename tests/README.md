# Regression tests

Run from the repository root:

```sh
dotnet run --project tests/managed/SwiftlyS2.RegressionTests.csproj -c Release
python3 tests/native/run_tests.py
```

The managed runner requires .NET 10 and has no package dependencies. It compiles
the production container, hook-manager and entity-manager sources directly;
`NativeTestHost.cs` replaces only their native allocation, hook and schema
boundaries. It checks native layouts, borrowed/read-only buffers, index widths,
GC-safe storage and comparators, red-black invariants under randomized operations,
concurrent hook registration, reentrant callbacks and entity snapshots.

The native runner requires Python 3 and a C++20 compiler (`g++` by default; override
with `CXX`). It compiles the production mutex header and console-output header,
and extracts the production console, map-load, command-registry and event-drainer
method bodies into an isolated SDK-boundary fixture. Each executable has a
15-second timeout so reentrant deadlocks fail the run. These tests do not replace
a full native build or an in-game CS2 smoke test.

To compare with an archived managed source tree, pass
`-p:RegressionSourceRoot=/absolute/path/to/managed/src` to the managed runner.
Use a clean build output when switching trees.

## Native container ownership

The native-layout structs are not synchronized containers. Callers must serialize
their own reads, writes and disposal when sharing them across threads. An entity
enumeration snapshot protects the registry collection, not the lifetime of the
native entities it contains. Mid-hook dispatch takes one subscriber snapshot;
changes made by a callback apply to the following dispatch.

Constructed `CUtlMemoryFixedGrowable`, `CUtlVectorFixedGrowable` and `CCommand`
values now own stable native storage instead of retaining a pointer into a
movable inline managed field. Their layouts remain unchanged, but callers must
dispose the owning value when finished, for example:

```csharp
using var command = new CCommand("status");
```

Struct copies alias that storage; do not dispose copies independently or use any
copy after the owner has been disposed. Never dispose a borrowed native view of
an engine-owned command, vector, map or tree. Managed-created maps and trees must
also be disposed to release their rooted comparison callback. `Purge()` frees
their elements but retains the comparator so the container can be reused.

Read-only vector mutators reject changes before modifying size or payload.
The existing `ref T` indexer API is not an immutable reference: callers must not
write through it for a read-only native view.
