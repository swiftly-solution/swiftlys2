using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Hooks;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Schemas;
using SwiftlyS2.Shared.Engine;
using SwiftlyS2.Core.Engine;

namespace SwiftlyS2.Core.Memory;

internal class MemoryService : IMemoryService, IDisposable
{

    private readonly HookManager _HookManager;
    private readonly ILogger<MemoryService> _logger;
    private readonly Dictionary<nint, UnmanagedFunction> _UnmanagedFunctions = [];
    private readonly Dictionary<nint, UnmanagedMemory> _UnmanagedMemories = [];

    public IS2BinLib S2BinLib { get; } = new S2BinLibService();

    public MemoryService( HookManager hookManager, ILogger<MemoryService> logger )
    {
        _HookManager = hookManager;
        _logger = logger;
    }

    public IUnmanagedFunction<TDelegate> GetUnmanagedFunctionByAddress<TDelegate>( nint address ) where TDelegate : Delegate
    {
        if (_UnmanagedFunctions.TryGetValue(address, out var function))
        {
            if (function.DelegateType == typeof(TDelegate))
            {
                return (UnmanagedFunction<TDelegate>)function;
            }

            throw new Exception($"Cannot have two different delegate type on a same address. The previous one is {function.DelegateType}.");
        }

        var newFunction = new UnmanagedFunction<TDelegate>(address, _HookManager);
        _UnmanagedFunctions.Add(address, newFunction);
        return newFunction;
    }

    public IUnmanagedFunction<TDelegate> GetUnmanagedFunctionByVTable<TDelegate>( nint pVTable, int index ) where TDelegate : Delegate
    {
        var address = pVTable.Read<nint>(index * IntPtr.Size);
        return GetUnmanagedFunctionByAddress<TDelegate>(address);
    }

    public IUnmanagedMemory GetUnmanagedMemoryByAddress( nint address )
    {
        if (_UnmanagedMemories.TryGetValue(address, out var memory))
        {
            return memory;
        }

        var newMemory = new UnmanagedMemory(address, _HookManager);
        _UnmanagedMemories.Add(address, newMemory);
        return newMemory;
    }

    public nint? GetInterfaceByName( string name )
    {
        var ptr = NativeMemoryHelpers.FetchInterfaceByName(name);
        if (ptr == 0)
        {
            return null;
        }
        return ptr;
    }

    public nint? GetAddressBySignature( string library, string signature )
    {
        return S2BinLib.PatternScan(library, signature);
    }

    public nint? GetVTableAddress( string library, string vtableName )
    {
        return S2BinLib.FindVTable(library, vtableName);
    }

    public nint ResolveXrefAddress( nint xrefAddress )
    {
        var offset = (xrefAddress + 3).Read<uint>();
        return xrefAddress + 7 + (nint)offset;
    }

    public string? GetObjectPtrVtableName( nint address )
    {
        return S2BinLib.GetObjectPtrVtableName(address);
    }

    public bool ObjectPtrHasVtable( nint address )
    {
        return S2BinLib.ObjectPtrHasVtable(address);
    }

    public bool ObjectPtrHasBaseClass( nint address, string baseClassName )
    {
        return S2BinLib.ObjectPtrHasBaseClass(address, baseClassName);
    }

    public T ToSchemaClass<T>( nint address ) where T : class, ISchemaClass<T>
    {
        return T.From(address);
    }

    public nint Alloc( ulong size )
    {
        return NativeAllocator.Alloc(size);
    }

    public void Free( nint pointer )
    {
        NativeAllocator.Free(pointer);
    }

    public nint Resize( nint pointer, ulong newSize )
    {
        return NativeAllocator.Resize(pointer, newSize);
    }

    public void Dispose()
    {
        foreach (var function in _UnmanagedFunctions)
        {
            try
            {
                function.Value.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to dispose unmanaged function at {Address}.", function.Key);
            }
        }
        foreach (var memory in _UnmanagedMemories)
        {
            try
            {
                memory.Value.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to dispose unmanaged memory at {Address}.", memory.Key);
            }
        }
        _UnmanagedFunctions.Clear();
        _UnmanagedMemories.Clear();
    }

    public IServerSideClient ToServerSideClient( nint address )
    {
        var serverSideClient = new ServerSideClient();
        serverSideClient.SetDangerousHandle(address);
        return serverSideClient;
    }
}
