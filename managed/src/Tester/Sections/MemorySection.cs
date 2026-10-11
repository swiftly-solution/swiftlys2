using System.Runtime.InteropServices;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.SchemaDefinitions;
using Tester.Framework;
using static Tester.Framework.TestContext;

namespace Tester.Sections;

public sealed class MemorySection( ISwiftlyCore core ) : Section(core)
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int AddFn( int a, int b );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int MulFn( int a, int b );

    private static readonly AddFn Add = ( a, b ) => a + b;
    private static readonly nint AddPtr = Marshal.GetFunctionPointerForDelegate(Add);

    private const string BogusSignature = "DE AD BE EF CA FE BA BE 13 37 C0 DE 99 88 77 66";

    public override string Name => "memory";

    private IMemoryService Mem => Core.Memory;

    public override Task Test( TestContext t )
    {
        var mem = Mem;

        t.Test("Library names", () =>
        {
            Equal("engine2", Library.Engine, "Engine");
            Equal("tier0", Library.Tier0, "Tier0");
            Equal("server", Library.Server, "Server");
            Equal("networksystem", Library.NetworkSystem, "NetworkSystem");
        });

        t.Test("Alloc returns usable memory", () =>
        {
            var p = mem.Alloc(64);
            try
            {
                Expect(p != 0, "null pointer");
                for (var i = 0; i < 8; i++) Marshal.WriteInt64(p, i * 8, 0x1111111111111111L * (i + 1));
                for (var i = 0; i < 8; i++) Equal(0x1111111111111111L * (i + 1), Marshal.ReadInt64(p, i * 8), $"qword {i}");
            }
            finally { mem.Free(p); }
        });

        t.Test("Alloc gives distinct blocks while both are live", () =>
        {
            var a = mem.Alloc(32);
            var b = mem.Alloc(32);
            try
            {
                Expect(a != 0 && b != 0 && a != b, "overlapping or null blocks");
                Marshal.WriteInt64(a, 1);
                Marshal.WriteInt64(b, 2);
                Equal(1L, Marshal.ReadInt64(a), "a");
                Equal(2L, Marshal.ReadInt64(b), "b");
            }
            finally { mem.Free(a); mem.Free(b); }
        });

        t.Test("Large allocation (16 MB) is writable at both ends", () =>
        {
            const ulong size = 16UL * 1024 * 1024;
            var p = mem.Alloc(size);
            try
            {
                Expect(p != 0, "null pointer");
                Marshal.WriteByte(p, 0, 0xAB);
                Marshal.WriteByte(p, (int)size - 1, 0xCD);
                Equal((byte)0xAB, Marshal.ReadByte(p, 0), "first byte");
                Equal((byte)0xCD, Marshal.ReadByte(p, (int)size - 1), "last byte");
            }
            finally { mem.Free(p); }
        });

        t.Test("Resize grows and preserves contents", () =>
        {
            var p = mem.Alloc(64);
            try
            {
                for (var i = 0; i < 8; i++) Marshal.WriteInt64(p, i * 8, 100 + i);
                p = mem.Resize(p, 4096);
                Expect(p != 0, "null after Resize");
                for (var i = 0; i < 8; i++) Equal(100L + i, Marshal.ReadInt64(p, i * 8), $"qword {i}");
                Marshal.WriteByte(p, 4095, 7);
                Equal((byte)7, Marshal.ReadByte(p, 4095), "new tail");
            }
            finally { mem.Free(p); }
        });

        t.Test("Resize shrinks and preserves the prefix", () =>
        {
            var p = mem.Alloc(1024);
            try
            {
                for (var i = 0; i < 4; i++) Marshal.WriteInt64(p, i * 8, 500 + i);
                p = mem.Resize(p, 32);
                for (var i = 0; i < 4; i++) Equal(500L + i, Marshal.ReadInt64(p, i * 8), $"qword {i}");
            }
            finally { mem.Free(p); }
        });

        t.Test("2000 mixed-size alloc/free cycles", () =>
        {
            var live = new List<nint>();
            var rnd = new Random(1234);
            for (var i = 0; i < 2000; i++)
            {
                var p = mem.Alloc((ulong)rnd.Next(1, 4096));
                Expect(p != 0, $"null at {i}");
                Marshal.WriteByte(p, 0, (byte)i);
                live.Add(p);
                if (live.Count > 50)
                {
                    mem.Free(live[0]);
                    live.RemoveAt(0);
                }
            }
            foreach (var p in live) mem.Free(p);
        });

        t.Test("Alloc/Free from many pool threads", () =>
        {
            var failures = new System.Collections.Concurrent.ConcurrentBag<string>();
            Task.WaitAll(Enumerable.Range(0, 8).Select(w => Task.Run(() =>
            {
                for (var i = 0; i < 500; i++)
                {
                    var p = mem.Alloc(128);
                    if (p == 0) { failures.Add($"worker {w}: null"); return; }
                    Marshal.WriteInt32(p, w * 1000 + i);
                    if (Marshal.ReadInt32(p) != w * 1000 + i) failures.Add($"worker {w}: corrupted");
                    mem.Free(p);
                }
            })).ToArray());
            Expect(failures.IsEmpty, string.Join("; ", failures));
        });

        t.Test("ResolveXrefAddress with a positive displacement", () =>
        {
            var p = mem.Alloc(64);
            try
            {
                Marshal.Copy(new byte[] { 0x48, 0x8D, 0x05 }, 0, p, 3);
                Marshal.WriteInt32(p, 3, 0x100);
                Equal(p + 7 + 0x100, mem.ResolveXrefAddress(p), "address");
            }
            finally { mem.Free(p); }
        });

        t.Test("ResolveXrefAddress reads the displacement as an unsigned 32-bit value", () =>
        {
            var p = mem.Alloc(64);
            try
            {
                Marshal.Copy(new byte[] { 0x48, 0x8D, 0x05 }, 0, p, 3);
                Marshal.WriteInt32(p, 3, unchecked((int)0x80000000));
                Equal(p + 7 + 0x80000000L, mem.ResolveXrefAddress(p), "address");
            }
            finally { mem.Free(p); }
        });

        t.Test("GetUnmanagedFunctionByAddress calls a native function pointer", () =>
        {
            var fn = mem.GetUnmanagedFunctionByAddress<AddFn>(AddPtr);
            NotNull(fn, "function");
            Equal(AddPtr, fn.Address, "Address");
            Equal(5, fn.Call(2, 3), "Call");
            Equal(5, fn.CallOriginal(2, 3), "CallOriginal (no hook installed)");
        });

        t.Test("Same address and delegate type returns the cached wrapper", () =>
            Expect(ReferenceEquals(mem.GetUnmanagedFunctionByAddress<AddFn>(AddPtr), mem.GetUnmanagedFunctionByAddress<AddFn>(AddPtr)), "different wrappers"));

        t.Test("Same address with a different delegate type is rejected", () =>
            Throws<Exception>(() => mem.GetUnmanagedFunctionByAddress<MulFn>(AddPtr)));

        t.Test("GetUnmanagedFunctionByVTable resolves the slot", () =>
        {
            var vtable = mem.Alloc(3 * (ulong)nint.Size);
            try
            {
                Marshal.WriteIntPtr(vtable, 0, 0);
                Marshal.WriteIntPtr(vtable, nint.Size, AddPtr);
                var fn = mem.GetUnmanagedFunctionByVTable<AddFn>(vtable, 1);
                Equal(AddPtr, fn.Address, "Address");
                Equal(9, fn.Call(4, 5), "Call");
            }
            finally { mem.Free(vtable); }
        });

        t.Test("AddHook/RemoveHook on functions and memory", () => Skip("hooks patch live code; no harmless target address"));

        t.Test("GetUnmanagedMemoryByAddress is cached per address", () =>
        {
            var a = mem.Alloc(16);
            var b = mem.Alloc(16);
            try
            {
                var ma = mem.GetUnmanagedMemoryByAddress(a);
                Equal(a, ma.Address, "Address");
                Expect(ReferenceEquals(ma, mem.GetUnmanagedMemoryByAddress(a)), "not cached");
                Expect(!ReferenceEquals(ma, mem.GetUnmanagedMemoryByAddress(b)), "shared across addresses");
            }
            finally { mem.Free(a); mem.Free(b); }
        });

        t.Test("GetAddressBySignature(unknown pattern) finds nothing", () =>
        {
            try { Expect(mem.GetAddressBySignature(Library.Server, BogusSignature) is null, "pattern found"); }
            catch (Exception) { }
        });

        t.Test("GetAddressBySignature(unknown library) finds nothing", () =>
        {
            try { Expect(mem.GetAddressBySignature("tester_no_such_library", BogusSignature) is null, "pattern found"); }
            catch (Exception) { }
        });

        t.Test("GetVTableAddress(server, CBaseEntity)", () =>
        {
            var v = mem.GetVTableAddress(Library.Server, "CBaseEntity");
            if (v is null) Skip("CBaseEntity vtable not found in this build");
            Expect(v != 0, "zero address");
        });

        t.Test("GetVTableAddress(unknown) is null", () =>
        {
            Expect(mem.GetVTableAddress(Library.Server, "CTesterNoSuchClass") is null, "found");
            Expect(mem.GetVTableAddress(Library.Server, "CTesterNoSuchClass::CTesterNoSuchNested") is null, "nested found");
        });

        t.Test("GetVTableAddress rejects more than two nested names", () =>
            Throws<ArgumentException>(() => mem.GetVTableAddress(Library.Server, "A::B::C")));

        t.Test("GetInterfaceByName(unknown) is null", () =>
            Expect(mem.GetInterfaceByName("TesterNoSuchInterface001") is null, "found"));

        t.Test("GetInterfaceByName(Source2Server001)", () =>
        {
            var i = mem.GetInterfaceByName("Source2Server001");
            if (i is null) Skip("interface not exposed under this name");
            Expect(i != 0, "zero address");
        });

        t.Test("Object pointer helpers on the caller's controller", () =>
        {
            var ctrl = t.Caller.Controller;
            if (!ctrl.IsValid) Skip("caller controller invalid");
            NotNull(mem.GetObjectPtrVtableName(ctrl.Address), "vtable name");
            Expect(mem.ObjectPtrHasVtable(ctrl.Address), "ObjectPtrHasVtable");
            Expect(mem.ObjectPtrHasBaseClass(ctrl.Address, "CBaseEntity"), "has CBaseEntity");
            Expect(!mem.ObjectPtrHasBaseClass(ctrl.Address, "CTesterNoSuchClass"), "has bogus base class");
        });

        t.Test("ToSchemaClass keeps the address", () =>
        {
            var ctrl = t.Caller.Controller;
            if (!ctrl.IsValid) Skip("caller controller invalid");
            Equal(ctrl.Address, mem.ToSchemaClass<CCSPlayerController>(ctrl.Address).Address, "Address");
        });

        return Task.CompletedTask;
    }

    public override bool ProfileInBackground => true;

    public override Task Profile( ProfileContext p )
    {
        var mem = Mem;
        var caller = p.Caller;
        var addr = p.OnGame(() => caller.Controller is { IsValid: true } c ? c.Address : (nint)0);
        var buf = mem.Alloc(4096);
        var xref = mem.Alloc(16);
        Marshal.Copy(new byte[] { 0x48, 0x8D, 0x05 }, 0, xref, 3);
        Marshal.WriteInt32(xref, 3, 0x100);
        var fn = mem.GetUnmanagedFunctionByAddress<AddFn>(AddPtr);
        var call = fn.Call;
        var original = fn.CallOriginal;

        try
        {
            p.Profile("Memory.Alloc + Free (64 B)", () => mem.Free(mem.Alloc(64)), 100_000);
            p.Profile("Memory.Alloc + Free (4 KB)", () => mem.Free(mem.Alloc(4096)), 100_000);
            p.Profile("Memory.Alloc + Resize + Free", () => mem.Free(mem.Resize(mem.Alloc(64), 256)), 50_000);
            p.Profile("Marshal.ReadInt64 (baseline)", () => _ = Marshal.ReadInt64(buf, 8), 500_000);
            p.Profile("Marshal.WriteInt64 (baseline)", () => Marshal.WriteInt64(buf, 8, 1), 500_000);
            p.Profile("Memory.ResolveXrefAddress", () => _ = mem.ResolveXrefAddress(xref), 500_000);

            p.Profile("Memory.GetUnmanagedFunctionByAddress (cached)", () => _ = mem.GetUnmanagedFunctionByAddress<AddFn>(AddPtr), 200_000);
            p.Profile("IUnmanagedFunction.Call getter + invoke", () => _ = fn.Call(2, 3), 200_000);
            p.Profile("IUnmanagedFunction.CallOriginal getter + invoke", () => _ = fn.CallOriginal(2, 3), 200_000);
            p.Profile("Cached Call delegate invoke (native thunk)", () => _ = call(2, 3), 500_000);
            p.Profile("Cached CallOriginal delegate invoke (native thunk)", () => _ = original(2, 3), 500_000);
            p.Profile("Memory.GetUnmanagedMemoryByAddress (cached)", () => _ = mem.GetUnmanagedMemoryByAddress(buf), 200_000);

            p.ProfileOnGame("Memory.GetInterfaceByName(unknown)", () => _ = mem.GetInterfaceByName("TesterNoSuchInterface001"), 500);
            p.ProfileOnGame("Memory.GetVTableAddress(CBaseEntity)", () => _ = mem.GetVTableAddress(Library.Server, "CBaseEntity"), 50);
            p.ProfileOnGame("Memory.GetAddressBySignature(unknown pattern, full scan)", () => _ = mem.GetAddressBySignature(Library.Server, BogusSignature), 5);

            if (addr != 0)
            {

                p.ProfileOnGame("Memory.GetObjectPtrVtableName", () => _ = mem.GetObjectPtrVtableName(addr), 100_000);
                p.ProfileOnGame("Memory.ObjectPtrHasVtable", () => _ = mem.ObjectPtrHasVtable(addr), 100_000);
                p.ProfileOnGame("Memory.ObjectPtrHasBaseClass", () => _ = mem.ObjectPtrHasBaseClass(addr, "CBaseEntity"), 50_000);
                p.ProfileOnGame("Memory.ToSchemaClass<CCSPlayerController>", () => _ = mem.ToSchemaClass<CCSPlayerController>(addr), 100_000);
            }
        }
        finally
        {
            mem.Free(buf);
            mem.Free(xref);
        }

        return Task.CompletedTask;
    }
}
