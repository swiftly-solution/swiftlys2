using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Hooks;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Schemas;
using SwiftlyS2.Shared.Engine;
using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Core.Engine;

namespace SwiftlyS2.Core.Memory;

internal class MemoryService : IMemoryService, IDisposable
{

    private readonly ILogger<MemoryService> _Logger;
    private readonly HookManager _HookManager;
    private readonly ILoggerFactory _LoggerFactory;
    private readonly Dictionary<nint, UnmanagedFunction> _UnmanagedFunctions = [];
    private readonly Dictionary<nint, UnmanagedMemory> _UnmanagedMemories = [];

    public MemoryService( ILogger<MemoryService> logger, HookManager hookManager, ILoggerFactory loggerFactory )
    {
        _Logger = logger;
        _HookManager = hookManager;
        _LoggerFactory = loggerFactory;
    }

    public IUnmanagedFunction<TDelegate> GetUnmanagedFunctionByAddress<TDelegate>( nint address ) where TDelegate : Delegate
    {
        try
        {
            if (_UnmanagedFunctions.TryGetValue(address, out var function))
            {
                if (function.DelegateType == typeof(TDelegate))
                {
                    return (UnmanagedFunction<TDelegate>)function;
                }
                else
                {
                    throw new Exception($"Cannot have two different delegate type on a same address. The previous one is {function.DelegateType}.");
                }
            }
            var newFunction = new UnmanagedFunction<TDelegate>(address, _HookManager, _LoggerFactory);
            _UnmanagedFunctions.Add(address, newFunction);
            return newFunction;
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e)) _Logger.LogError(e, "Failed to get unmanaged function by address {0}.", address);
            throw new Exception($"Failed to get unmanaged function by address {address}.");
        }
    }

    public IUnmanagedFunction<TDelegate> GetUnmanagedFunctionByVTable<TDelegate>( nint pVTable, int index ) where TDelegate : Delegate
    {
        try
        {
            var address = pVTable.Read<nint>(index * IntPtr.Size);
            return GetUnmanagedFunctionByAddress<TDelegate>(address);
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e)) _Logger.LogError(e, "Failed to get unmanaged function by vtable {0} and index {1}.", pVTable, index);
            throw new Exception($"Failed to get unmanaged function by vtable {pVTable} and index {index}.");
        }
    }

    public IUnmanagedMemory GetUnmanagedMemoryByAddress( nint address )
    {
        try
        {
            if (_UnmanagedMemories.TryGetValue(address, out var memory))
            {
                return memory;
            }
            var newMemory = new UnmanagedMemory(address, _HookManager, _LoggerFactory);
            _UnmanagedMemories.Add(address, newMemory);
            return newMemory;
        }
        catch (Exception e)
        {
            if (GlobalExceptionHandler.Handle(ref e)) _Logger.LogError(e, "Failed to get unmanaged memory by address {0}.", address);
            throw new Exception($"Failed to get unmanaged memory by address {address}.");
        }
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
        var ptr = NativeMemoryHelpers.GetAddressBySignature(library, signature, 0, false);
        if (ptr == 0)
        {
            return null;
        }
        return ptr;
    }

    public nint? GetVTableAddress( string library, string vtableName )
    {
        var classes = vtableName.Split("::");
        nint? ptr;
        if (classes.Length == 1)
        {
            ptr = NativeMemoryHelpers.GetVirtualTableAddress(library, vtableName);
        }
        else if (classes.Length == 2)
        {
            ptr = NativeMemoryHelpers.GetVirtualTableAddressNested2(library, classes[0], classes[1]);
        }
        else
        {
            throw new ArgumentException("Vtable has too many nested classes, which is not supported for now.");
        }
        if (ptr == 0)
        {
            ptr = null;
        }
        return ptr;
    }

    public nint ResolveXrefAddress( nint xrefAddress )
    {
        var offset = (xrefAddress + 3).Read<uint>();
        return xrefAddress + 7 + (nint)offset;
    }

    public string? GetObjectPtrVtableName( nint address )
    {
        var result = NativeMemoryHelpers.GetObjectPtrVtableName(address);
        return result == string.Empty ? null : result;
    }

    public bool ObjectPtrHasVtable( nint address )
    {
        return NativeMemoryHelpers.ObjectPtrHasVtable(address);
    }

    public bool ObjectPtrHasBaseClass( nint address, string baseClassName )
    {
        return NativeMemoryHelpers.ObjectPtrHasBaseClass(address, baseClassName);
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
            function.Value.Dispose();
        }
        foreach (var memory in _UnmanagedMemories)
        {
            memory.Value.Dispose();
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

    public IReadOnlyList<EntityFieldInfo> GetEntityFields( nint entity, string className )
    {
        var raw = NativeSchema.GetEntityFields( entity, className );
        if (string.IsNullOrEmpty(raw)) return [];

        var root = new List<EntityFieldInfo>();
        var stack = new List<List<EntityFieldInfo>>();

        foreach (var line in raw.Split('\n', StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = line.Split('\t');
            if (parts.Length < 5) continue;

            var depth = int.Parse(parts[0]);
            var field = new EntityFieldInfo
            {
                Name = parts[1],
                Type = parts[2],
                Offset = int.Parse(parts[3]),
                Value = string.Join("\t", parts.Skip(4))
            };

            while (stack.Count > depth)
                stack.RemoveAt(stack.Count - 1);

            var target = depth == 0 ? root : stack[depth - 1];
            target.Add(field);

            while (stack.Count <= depth)
                stack.Add(field.Children);
            stack[depth] = field.Children;
        }

        return root;
    }

    public string DebugProtobuf( nint protoMsgPtr )
    {
        return NativeNetMessages.DebugString( protoMsgPtr );
    }

    public string FormatMoveDetail( nint moveMsgPtr )
    {
        return NativeNetMessages.FormatMoveDebugString( moveMsgPtr );
    }
}