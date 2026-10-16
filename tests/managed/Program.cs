using System.Collections.Concurrent;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Core.Hooks;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

internal static unsafe class Program
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int TestFunction(int value);

    private static int failed;
    private static int passed;

    private static int Main()
    {
        Run("native layouts remain compatible", Layouts);
        Run("borrowed memory survives repeated purge and rebind", BorrowedMemory);
        Run("read-only vectors reject insertions before changing size or payload", ReadOnlyVector);
        Run("vector insertion and removal preserve contents and bounds", VectorOperations);
        Run("lean vector numeric conversions: int", LeanVector<int>);
        Run("lean vector numeric conversions: ushort", LeanVector<ushort>);
        Run("lean vector numeric conversions: uint", LeanVector<uint>);
        Run("lean vector numeric conversions: ulong", LeanVector<ulong>);
        Run("lean vector uses the native signed external marker", SignedMarker);
        Run("ushort offsets do not wrap at element 16384", WideOffsets);
        Run("lean vector purge clears count and supports reuse", PurgeLeanVector);
        Run("schema lean vectors return wrappers for native addresses", SchemaLeanVector);
        Run("fixed growable storage survives copies and compacting GC", FixedStorage);
        Run("owned commands tokenize and release their buffers", CommandStorage);
        Run("map comparator survives GC, purge and replacement", ComparatorLifetime);
        Run("red-black tree handles both zigzag insertions", ZigzagInsertions);
        Run("red-black tree balances deletion of black leaves", BlackLeafRemoval);
        Run("map clear preserves a reusable free list", ClearMap);
        Run("randomized ushort maps preserve ordering and red-black invariants", RandomizedMap<ushort>);
        Run("randomized int maps preserve ordering and red-black invariants", RandomizedMap<int>);
        Run("mid-hook dispatch uses a snapshot during removal", MidHookRemoval);
        Run("mid-hook additions are visible on the following dispatch", MidHookAddition);
        Run("concurrent mid-hook registrations share one chain", ConcurrentMidHooks);
        Run("native mid-hook enable can reenter registration", MidHookEnableReentry);
        Run("ordinary hook mutation releases removed native hooks", OrdinaryHooks);
        Run("entity enumeration remains stable across deletion", EntitySnapshot);
        Run("concurrent creation registers each entity once", ConcurrentEntities);
        Console.WriteLine($"Passed: {passed}; failed: {failed}");
        return failed == 0 ? 0 : 1;
    }

    private static void Layouts()
    {
        Equal(16, Unsafe.SizeOf<CUtlMemory<int>>());
        Equal(24, Unsafe.SizeOf<CUtlVector<int>>());
        Equal(16, Unsafe.SizeOf<CUtlLeanVector<int, ushort>>());
        Equal(32, Unsafe.SizeOf<CUtlMap<int, int, ushort>>());
        Equal(40, Unsafe.SizeOf<CUtlMap<int, int, int>>());
        Equal(528, Unsafe.SizeOf<CUtlMemoryFixedGrowable<byte, FixedCharBuffer512>>());
        Equal(536, Unsafe.SizeOf<CUtlVectorFixedGrowable<byte, FixedCharBuffer512>>());
    }

    private static void BorrowedMemory()
    {
        var pointer = Marshal.AllocHGlobal(32);
        try
        {
            Marshal.WriteInt32(pointer, 1234);
            var memory = new CUtlMemory<int>(pointer, 8, false);
            memory.Purge();
            memory.Purge();
            Equal((nint)0, memory.Base);
            Equal(0, memory.Count);
            Equal(1234, Marshal.ReadInt32(pointer));
            memory.SetExternalBuffer(pointer, 8, false);
            memory.Init(0, 2);
            memory[0] = 42;
            Equal(42, memory[0]);
            memory.Purge();
            Equal(1234, Marshal.ReadInt32(pointer));
        }
        finally { Marshal.FreeHGlobal(pointer); }
    }

    private static void ReadOnlyVector()
    {
        var pointer = Marshal.AllocHGlobal(12);
        try
        {
            Marshal.WriteInt32(pointer, 0, 17);
            Marshal.WriteInt32(pointer, 4, 23);
            Marshal.WriteInt32(pointer, 8, 12345);
            var vector = default(CUtlVector<int>);
            vector.SetExternalBuffer(pointer, 2, 1, true);
            Throws<InvalidOperationException>(() => vector.AddToTail(99));
            vector.SetExternalBuffer(pointer, 1, 1, true);
            Throws<InvalidOperationException>(() => vector.AddToTail(99));
            Equal(1, vector.Count);
            Equal(1, vector.Capacity);
            Equal(17, vector[0]);
            Equal(23, Marshal.ReadInt32(pointer, 4));
            Equal(12345, Marshal.ReadInt32(pointer, 8));
            vector.Purge();
            Equal(17, Marshal.ReadInt32(pointer));
        }
        finally { Marshal.FreeHGlobal(pointer); }
    }

    private static void VectorOperations()
    {
        var vector = new CUtlVector<int>(0, 1);
        try
        {
            vector.AddToTail(2);
            vector.AddToHead(1);
            vector.AddToTail(4);
            vector.InsertBefore(2, 3);
            Check(vector.SequenceEqual([1, 2, 3, 4]));
            vector.RemoveMultiple(1, 2);
            Check(vector.SequenceEqual([1, 4]));
            Throws<IndexOutOfRangeException>(() => { _ = vector[2]; });
            Throws<IndexOutOfRangeException>(() => vector.InsertBefore(-1, 3));
            Throws<ArgumentOutOfRangeException>(() => vector.GrowVector(-1));
        }
        finally { vector.Purge(); }
    }

    private static void LeanVector<TIndex>() where TIndex : unmanaged, IBinaryInteger<TIndex>, IMinMaxValue<TIndex>
    {
        var vector = new CUtlLeanVector<int, TIndex>(TIndex.Zero, TIndex.Zero);
        try
        {
            for (var index = 0; index < 64; index++) vector.AddToTail(index);
            Equal(64, int.CreateChecked(vector.Count));
            Check(vector.NumAllocated >= 64);
            Check(!vector.ExternallyAllocated);
            for (var index = 0; index < 64; index++) Equal(index, vector[TIndex.CreateChecked(index)]);
            Throws<IndexOutOfRangeException>(() => { _ = vector[TIndex.CreateChecked(64)]; });
        }
        finally { vector.Purge(); }
    }

    private static void SignedMarker()
    {
        var pointer = Marshal.AllocHGlobal(16);
        try
        {
            var vector = new CUtlLeanVector<int, int>(pointer, 4, 0);
            Equal(0x40000004, vector.Allocated);
            Equal(4, vector.NumAllocated);
            Check(vector.ExternallyAllocated);
            vector.Purge();
        }
        finally { Marshal.FreeHGlobal(pointer); }
    }

    private static void WideOffsets()
    {
        var vector = new CUtlLeanVector<int, ushort>(0, 16385);
        try
        {
            vector.SetCount(16385);
            vector[0] = 11;
            vector[16384] = 99;
            Equal(11, vector[0]);
            Equal(99, Marshal.ReadInt32(vector.Base, 65536));
            vector.FastRemove(1);
            Equal(99, vector[1]);
        }
        finally { vector.Purge(); }
    }

    private static void PurgeLeanVector()
    {
        var vector = new CUtlLeanVector<int, int>(0, 1);
        vector.AddToTail(7);
        vector.Purge();
        Equal(0, vector.Count);
        Equal((nint)0, vector.Base);
        Check(!vector.IsValidIndex(0));
        Throws<IndexOutOfRangeException>(() => { _ = vector[0]; });
        try
        {
            Equal(0, vector.AddToTail(9));
            Equal(9, vector[0]);
        }
        finally { vector.Purge(); }
    }

    private static void SchemaLeanVector()
    {
        var pointer = Marshal.AllocHGlobal(32);
        try
        {
            new Span<byte>((void*)pointer, 32).Clear();
            Marshal.WriteInt32(pointer, 8, 11);
            Marshal.WriteInt32(pointer, 24, 22);
            var vector = new CUtlLeanVector<RegressionRecord, int>(pointer, 2, 2);
            Check(vector[0] is not null);
            Equal(pointer, vector[0].Address);
            Equal(pointer + 16, vector[1].Address);
            Check(vector.Select(record => record.Number).SequenceEqual([11, 22]));
            GC.Collect(2, GCCollectionMode.Forced, true, true);
            Equal(22, vector[1].Number);
            vector.Purge();
        }
        finally { Marshal.FreeHGlobal(pointer); }
    }

    private static void FixedStorage()
    {
        Check(typeof(IDisposable).IsAssignableFrom(typeof(CUtlVectorFixedGrowable<byte, FixedCharBuffer512>)));
        var values = new CUtlVectorFixedGrowable<byte, FixedCharBuffer512>[1];
        values[0] = new CUtlVectorFixedGrowable<byte, FixedCharBuffer512>(16);
        try
        {
            for (byte index = 0; index < 16; index++) values[0].AddToTail(index);
            var copy = values[0];
            GC.Collect(2, GCCollectionMode.Forced, true, true);
            for (var index = 0; index < 16; index++) Equal((byte)index, copy[index]);
            Equal(16, copy.MaxSize);
            Throws<InvalidOperationException>(() => copy.AddToTail(17));
            Throws<ArgumentOutOfRangeException>(() => copy.SetSize(17));
        }
        finally { ((IDisposable)(object)values[0]).Dispose(); }
    }

    private static void CommandStorage()
    {
        Check(typeof(IDisposable).IsAssignableFrom(typeof(CCommand)));
        var command = new CCommand("test \"two words\" -count 42");
        try
        {
            GC.Collect(2, GCCollectionMode.Forced, true, true);
            Equal(4, command.ArgC);
            Equal("two words", command[1]);
            Equal(42, command.FindArgInt("-count", -1));
        }
        finally { ((IDisposable)(object)command).Dispose(); }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static CUtlMap<int, int, ushort> MakeMap(out WeakReference target)
    {
        var state = new ComparisonState();
        target = new WeakReference(state);
        return new CUtlMap<int, int, ushort>((ref CUtlMapTreeNode<int, int> left, ref CUtlMapTreeNode<int, int> right) => state.Less(left.Key, right.Key));
    }

    private sealed class ComparisonState
    {
        internal bool Less(int left, int right) => left < right;
    }

    private static void ComparatorLifetime()
    {
        var map = MakeMap(out var target);
        try
        {
            GC.Collect(2, GCCollectionMode.Forced, true, true);
            Check(target.IsAlive);
            map.Insert(2, 20);
            map.Insert(1, 10);
            Equal(10, map[map.Find(1)]);
            map.Purge();
            map.Insert(3, 30);
            Equal(30, map[map.Find(3)]);
            map.RemoveAll();
            map.SetLessFunc(static (ref CUtlMapTreeNode<int, int> left, ref CUtlMapTreeNode<int, int> right) => left.Key < right.Key);
        }
        finally { map.Dispose(); }
        GC.Collect(2, GCCollectionMode.Forced, true, true);
        Check(!target.IsAlive);
        var disposed = MakeMap(out var disposedTarget);
        disposed.Dispose();
        GC.Collect(2, GCCollectionMode.Forced, true, true);
        Check(!disposedTarget.IsAlive);
    }

    private static CUtlMap<int, int, TIndex> NewMap<TIndex>() where TIndex : unmanaged, IBinaryInteger<TIndex>, IMinMaxValue<TIndex> =>
        new(static (ref CUtlMapTreeNode<int, int> left, ref CUtlMapTreeNode<int, int> right) => left.Key < right.Key);

    private static void ZigzagInsertions()
    {
        foreach (var keys in new[] { new[] { 3, 1, 2 }, new[] { 1, 3, 2 } })
        {
            var map = NewMap<ushort>();
            try
            {
                foreach (var key in keys) map.Insert(key, key);
                Validate(ref map, new SortedDictionary<int, int> { [1] = 1, [2] = 2, [3] = 3 });
            }
            finally { map.Dispose(); }
        }
    }

    private static void BlackLeafRemoval()
    {
        var map = NewMap<ushort>();
        var expected = new SortedDictionary<int, int>();
        try
        {
            foreach (var key in new[] { 10, 5, 15, 3 }) { map.Insert(key, key); expected[key] = key; }
            foreach (var key in new[] { 3, 5, 10, 15 })
            {
                Check(map.Remove(key));
                expected.Remove(key);
                Validate(ref map, expected);
            }
        }
        finally { map.Dispose(); }
    }

    private static void ClearMap()
    {
        var map = NewMap<ushort>();
        try
        {
            map.Insert(1, 1);
            map.Insert(2, 2);
            map.RemoveAll();
            var first = map.Insert(3, 3);
            var second = map.Insert(4, 4);
            var third = map.Insert(5, 5);
            Check(first != second && first != third && second != third);
            Validate(ref map, new SortedDictionary<int, int> { [3] = 3, [4] = 4, [5] = 5 });
        }
        finally { map.Dispose(); }
    }

    private static void RandomizedMap<TIndex>() where TIndex : unmanaged, IBinaryInteger<TIndex>, IMinMaxValue<TIndex>
    {
        var map = NewMap<TIndex>();
        var expected = new SortedDictionary<int, int>();
        var random = new Random(723982);
        try
        {
            for (var operation = 0; operation < 3000; operation++)
            {
                var key = random.Next(256);
                switch (random.Next(20))
                {
                    case 0:
                        map.RemoveAll();
                        expected.Clear();
                        break;
                    case < 9:
                        Equal(expected.Remove(key), map.Remove(key));
                        break;
                    default:
                        var value = random.Next();
                        map.InsertOrReplace(key, value);
                        expected[key] = value;
                        break;
                }
                Validate(ref map, expected);
            }
        }
        finally { map.Dispose(); }
    }

    private static void Validate<TIndex>(ref CUtlMap<int, int, TIndex> map, SortedDictionary<int, int> expected)
        where TIndex : unmanaged, IBinaryInteger<TIndex>, IMinMaxValue<TIndex>
    {
        Equal(expected.Count, map.Count);
        Check(map.IsValid());
        var seen = new HashSet<TIndex>();
        if (expected.Count > 0)
        {
            Check(map.Tree.IsBlack(map.Tree.Root));
            Equal(map.InvalidIndex(), map.Tree.Parent(map.Tree.Root));
        }
        _ = BlackHeight(ref map.Tree, map.Tree.Root, seen);
        Equal(expected.Count, seen.Count);
        var keys = new List<int>();
        for (var index = map.FirstInOrdered(); index != map.InvalidIndex(); index = map.NextInOrdered(index))
        {
            Check(keys.Count < expected.Count);
            keys.Add(map.Key(index));
        }
        Check(keys.SequenceEqual(expected.Keys));
        foreach (var pair in expected)
        {
            var index = map.Find(pair.Key);
            Check(index != map.InvalidIndex());
            Equal(pair.Value, map[index]);
        }
        var free = new HashSet<TIndex>();
        for (var index = map.Tree.FirstFree; index != map.InvalidIndex(); index = map.Tree.RightChild(index))
            Check(free.Add(index) && !seen.Contains(index) && !map.IsValidIndex(index));
    }

    private static int BlackHeight<TIndex>(ref CUtlRBTree<CUtlMapTreeNode<int, int>, TIndex> tree, TIndex index, HashSet<TIndex> seen)
        where TIndex : unmanaged, IBinaryInteger<TIndex>, IMinMaxValue<TIndex>
    {
        if (index == tree.InvalidIndex()) return 1;
        Check(seen.Add(index) && tree.IsValidIndex(index));
        var left = tree.LeftChild(index);
        var right = tree.RightChild(index);
        if (left != tree.InvalidIndex()) Equal(index, tree.Parent(left));
        if (right != tree.InvalidIndex()) Equal(index, tree.Parent(right));
        if (tree.IsRed(index)) Check(tree.IsBlack(left) && tree.IsBlack(right));
        var leftHeight = BlackHeight(ref tree, left, seen);
        var rightHeight = BlackHeight(ref tree, right, seen);
        Equal(leftHeight, rightHeight);
        return leftHeight + (tree.IsBlack(index) ? 1 : 0);
    }

    private static void DispatchMid(nint address)
    {
        var callback = Marshal.GetDelegateForFunctionPointer<MidHookInternalDelegate>(NativeHooks.MidCallback(address));
        var context = new MidHookContext();
        callback(&context);
    }

    private static void MidHookRemoval()
    {
        var manager = new HookManager();
        Guid removed = default;
        var calls = new List<int>();
        manager.AddMidHook(1001, (ref MidHookContext context) => { calls.Add(1); manager.RemoveMidHook([removed]); });
        removed = manager.AddMidHook(1001, (ref MidHookContext context) => calls.Add(2));
        DispatchMid(1001);
        DispatchMid(1001);
        Check(calls.SequenceEqual([1, 2, 1]));
        Check(SwiftlyS2.Core.GlobalExceptionHandler.Errors.IsEmpty);
    }

    private static void MidHookAddition()
    {
        var manager = new HookManager();
        var added = false;
        var calls = new List<int>();
        manager.AddMidHook(1002, (ref MidHookContext context) =>
        {
            calls.Add(1);
            if (added) return;
            added = true;
            manager.AddMidHook(1002, (ref MidHookContext nested) => calls.Add(2));
        });
        DispatchMid(1002);
        DispatchMid(1002);
        Check(calls.SequenceEqual([1, 1, 2]));
    }

    private static void ConcurrentMidHooks()
    {
        var manager = new HookManager();
        var calls = 0;
        var handles = new ConcurrentBag<Guid>();
        Parallel.For(0, 128, _ => handles.Add(manager.AddMidHook(1003, (ref MidHookContext context) => Interlocked.Increment(ref calls))));
        Equal(1, NativeHooks.MidCount(1003));
        DispatchMid(1003);
        Equal(128, calls);
        Parallel.ForEach(handles, handle => manager.RemoveMidHook([handle]));
        DispatchMid(1003);
        Equal(128, calls);
    }

    private static void OrdinaryHooks()
    {
        var manager = new HookManager();
        var handles = new ConcurrentBag<Guid>();
        Parallel.For(0, 24, _ => handles.Add(manager.AddHook(1004, next => (TestFunction)(value => value + 1))));
        Equal(24, NativeHooks.OrdinaryCount);
        Check(manager.IsHooked(1004));
        Parallel.ForEach(handles, handle => manager.Remove([handle]));
        Equal(0, NativeHooks.OrdinaryCount);
        Check(!manager.IsHooked(1004));
    }

    private static void MidHookEnableReentry()
    {
        var manager = new HookManager();
        var calls = 0;
        NativeHooks.OnEnableMidHook = () =>
        {
            NativeHooks.OnEnableMidHook = null;
            manager.AddMidHook(1005, (ref MidHookContext context) => calls++);
        };
        try
        {
            manager.AddMidHook(1005, (ref MidHookContext context) => calls++);
            Equal(1, NativeHooks.MidCount(1005));
            DispatchMid(1005);
            Equal(2, calls);
        }
        finally { NativeHooks.OnEnableMidHook = null; }
    }

    private static void EntitySnapshot()
    {
        EntityManager.OnEntityCreated(1);
        EntityManager.OnEntityCreated(2);
        try
        {
            using var entities = EntityManager.GetAllEntities().GetEnumerator();
            Check(entities.MoveNext());
            EntityManager.OnEntityDeleted(2);
            Check(entities.MoveNext());
            Equal((nint)2, entities.Current.Address);
            Check(!entities.MoveNext());
        }
        finally { EntityManager.OnEntityDeleted(1); EntityManager.OnEntityDeleted(2); }
    }

    private static void ConcurrentEntities()
    {
        Parallel.For(0, 256, index => EntityManager.OnEntityCreated(100 + index % 64));
        try
        {
            var entities = EntityManager.GetAllEntities().ToArray();
            Equal(64, entities.Length);
            Equal(64, entities.Select(entity => entity.Address).Distinct().Count());
        }
        finally
        {
            for (var index = 100; index < 164; index++) EntityManager.OnEntityDeleted(index);
        }
    }

    private static void Run(string name, Action test)
    {
        try
        {
            test();
            Equal(0, NativeAllocator.Outstanding);
            passed++;
            Console.WriteLine($"PASS {name}");
        }
        catch (Exception error)
        {
            failed++;
            Console.Error.WriteLine($"FAIL {name}: {error}");
        }
    }

    private static void Check(bool condition)
    {
        if (!condition) throw new InvalidOperationException("Assertion failed.");
    }

    private static void Equal<T>(T expected, T actual)
    {
        if (!EqualityComparer<T>.Default.Equals(expected, actual))
            throw new InvalidOperationException($"Expected {expected}, got {actual}.");
    }

    private static void Throws<TException>(Action action) where TException : Exception
    {
        try { action(); }
        catch (TException) { return; }
        throw new InvalidOperationException($"Expected {typeof(TException).Name}.");
    }
}
