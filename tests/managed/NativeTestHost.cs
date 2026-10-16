using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Core.Extensions
{
    internal static class RegressionExtensions { }
}

namespace SwiftlyS2.Core.Natives
{
    internal static unsafe class NativeAllocator
    {
        private static readonly ConcurrentDictionary<nint, ulong> allocations = new();

        internal static int Outstanding => allocations.Count;
        internal static bool Owns(nint memory) => allocations.ContainsKey(memory);

        internal static nint Alloc(ulong size)
        {
            var pointer = (nint)NativeMemory.AllocZeroed((nuint)Math.Max(1, size));
            allocations[pointer] = size;
            return pointer;
        }

        internal static void Free(nint memory)
        {
            if (memory == 0) return;
            if (!allocations.TryRemove(memory, out _))
                throw new InvalidOperationException("Attempted to free borrowed or already freed storage.");
            NativeMemory.Free((void*)memory);
        }

        internal static nint Resize(nint memory, ulong size)
        {
            if (memory == 0) return Alloc(size);
            if (!allocations.TryRemove(memory, out _))
                throw new InvalidOperationException("Attempted to resize borrowed storage.");
            var pointer = (nint)NativeMemory.Realloc((void*)memory, (nuint)Math.Max(1, size));
            allocations[pointer] = size;
            return pointer;
        }

        internal static void Move(nint destination, nint source, ulong size) =>
            new ReadOnlySpan<byte>((void*)source, checked((int)size)).CopyTo(new Span<byte>((void*)destination, checked((int)size)));

        internal static void Copy(nint destination, nint source, ulong size) => Move(destination, source, size);
    }

    internal static class ExternDLL
    {
        internal static void UtlVectorMemory_FailedAllocation(int count, int growth) => throw new OutOfMemoryException();

        internal static int UtlVectorMemory_CalcNewAllocationCount(int count, int growth, int requested, int elementSize) =>
            growth > 0 ? checked(((requested + growth - 1) / growth) * growth) : Math.Max(requested, Math.Max(1, checked(count * 2)));

        internal static nint UtlVectorMemory_Alloc(nint memory, bool owned, int bytes, int previousBytes)
        {
            if (owned) return NativeAllocator.Resize(memory, (ulong)bytes);
            var pointer = NativeAllocator.Alloc((ulong)bytes);
            NativeAllocator.Move(pointer, memory, (ulong)Math.Min(bytes, previousBytes));
            return pointer;
        }
    }

    internal static class NativeHooks
    {
        private sealed record Hook(bool Mid, nint Address, nint Callback);
        private static readonly ConcurrentDictionary<nint, Hook> hooks = new();
        private static long nextHandle;

        internal static Action? OnEnableMidHook;
        internal static int OrdinaryCount => hooks.Values.Count(hook => !hook.Mid);
        internal static int MidCount(nint address) => hooks.Values.Count(hook => hook.Mid && hook.Address == address);
        internal static nint MidCallback(nint address) => hooks.Values.Single(hook => hook.Mid && hook.Address == address).Callback;

        internal static nint AllocateHook() => Allocate(false);
        internal static nint AllocateMHook() => Allocate(true);

        private static nint Allocate(bool mid)
        {
            var handle = (nint)Interlocked.Increment(ref nextHandle);
            hooks[handle] = new Hook(mid, 0, 0);
            return handle;
        }

        internal static void SetHook(nint handle, nint address, nint callback) => hooks[handle] = new Hook(false, address, callback);
        internal static void SetMHook(nint handle, nint address, nint callback) => hooks[handle] = new Hook(true, address, callback);
        internal static nint GetHookOriginal(nint handle) => hooks[handle].Address;
        internal static void EnableHook(nint handle) { }
        internal static void EnableMHook(nint handle) => OnEnableMidHook?.Invoke();
        internal static void DeallocateHook(nint handle) => hooks.TryRemove(handle, out _);
        internal static void DeallocateMHook(nint handle) => hooks.TryRemove(handle, out _);
    }
}

namespace SwiftlyS2.Core
{
    internal static class GlobalExceptionHandler
    {
        internal static readonly ConcurrentQueue<Exception> Errors = new();
        internal static bool Handle(ref Exception error)
        {
            Errors.Enqueue(error);
            return true;
        }
    }
}

namespace Spectre.Console
{
    internal static class AnsiConsole
    {
        internal static void WriteException(Exception error) => System.Console.Error.WriteLine(error);
    }
}

namespace SwiftlyS2.Shared.Memory
{
    public struct MidHookContext { public int Value; }
    public delegate void MidHookDelegate(ref MidHookContext context);
}

namespace SwiftlyS2.Shared.SchemaDefinitions
{
    public abstract class RegressionRecord(nint address) : ISchemaClass<RegressionRecord>
    {
        public nint Address { get; private set; } = address;
        public bool IsValid => Address != 0;
        public void DangerouslySetAddress(nint address) => Address = address;
        static RegressionRecord ISchemaClass<RegressionRecord>.From(nint address) => new SwiftlyS2.Core.SchemaDefinitions.RegressionRecordImpl(address);
        static int ISchemaClass<RegressionRecord>.Size => 16;
        static string? ISchemaClass<RegressionRecord>.ClassName => nameof(RegressionRecord);
        public int Number => Marshal.ReadInt32(Address, 8);
    }

    public class CEntityInstance(nint address)
    {
        public nint Address { get; private set; } = address;
        public uint Index => (uint)Address;
        public string DesignerName => "regression_entity";
        public void DangerousSetHandle(nint address) => Address = address;
    }
}

namespace SwiftlyS2.Core.SchemaDefinitions
{
    public sealed class RegressionRecordImpl(nint address) : SwiftlyS2.Shared.SchemaDefinitions.RegressionRecord(address);
    public sealed class CEntityInstanceImpl(nint address) : SwiftlyS2.Shared.SchemaDefinitions.CEntityInstance(address);

    internal static class ClassConvertor
    {
        internal static SwiftlyS2.Shared.SchemaDefinitions.CEntityInstance ConvertEntityByDesignerName(nint address, string designerName) => new CEntityInstanceImpl(address);
    }
}
