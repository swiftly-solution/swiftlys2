using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Hooks;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Core.Memory;

internal class MemoryService : IMemoryService, IDisposable
{

  private readonly ILogger<MemoryService> _Logger;
  private readonly HookManager _HookManager;
  private readonly ILoggerFactory _LoggerFactory;
  private readonly Dictionary<nint, UnmanagedFunction> _UnmanagedFunctions = new();
  private readonly Dictionary<nint, UnmanagedMemory> _UnmanagedMemories = new();

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
      if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Failed to get unmanaged function by address {0}.", address);
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
      if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Failed to get unmanaged function by vtable {0} and index {1}.", pVTable, index);
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
      if (GlobalExceptionHandler.Handle(e)) _Logger.LogError(e, "Failed to get unmanaged memory by address {0}.", address);
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
    var ptr = NativeMemoryHelpers.GetVirtualTableAddress(library, vtableName);
    if (ptr == 0)
    {
      return null;
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
}