using System.Runtime.InteropServices;
using Spectre.Console;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Memory;

namespace SwiftlyS2.Core.Hooks;

internal class HookManager
{

  private class HookNode
  {
    public required Guid Id { get; init; }

    public nint HookHandle { get; set; }
    public nint OriginalFuncPtr { get; set; }
    public required Func<Func<nint>, Delegate> CallbackBuilder { get; init; }
    public Delegate? BuiltDelegate { get; set; }
    public nint BuiltPointer { get; set; }
  }

  private class MidHookNode
  {
    public required Guid Id { get; init; }
    public nint HookHandle { get; set; }
    public required MidHookDelegate BuiltDelegate { get; init; }
  }

  private class HookChain
  {
    public bool Hooked { get; set; } = false;
    public required nint FunctionAddress { get; set; }
    public nint HookHandle { get; set; }
    public nint OriginalFunctionAddress { get; set; }
    public List<HookNode> Nodes { get; } = new();
  }

  private class MidHookChain
  {
    public bool Hooked { get; set; } = false;
    public required nint Address { get; set; }
    public nint HookHandle { get; set; }
    public List<MidHookNode> Nodes { get; } = new();
  }

  private readonly Lock _sync = new();
  private readonly Dictionary<nint, HookChain> _chains = new();
  private readonly Dictionary<nint, MidHookChain> _midChains = new();

  public bool IsMidHooked( nint address )
  {
    lock (_sync)
    {
      return _midChains.TryGetValue(address, out var chain) && chain.Hooked;
    }
  }

  public bool IsHooked( nint functionAddress )
  {
    lock (_sync)
    {
      return _chains.TryGetValue(functionAddress, out var chain) && chain.Hooked;
    }
  }

  public nint GetOriginal( nint functionAddress )
  {
    lock (_sync)
    {
      if (_chains.TryGetValue(functionAddress, out var chain))
      {
        if (!chain.Hooked)
        {
          return functionAddress;
        }
        if (chain.Nodes.Count == 0)
        {
          return chain.OriginalFunctionAddress;
        }
        return chain.Nodes[^1].OriginalFuncPtr;
      }
      return nint.Zero;
    }
  }

  public Guid AddMidHook( nint address, MidHookDelegate callback )
  {
    MidHookChain chain;
    MidHookNode node = new MidHookNode {
      Id = Guid.NewGuid(),
      BuiltDelegate = callback,
    };

    lock (_sync)
    {
      if (!_midChains.TryGetValue(address, out chain))
      {
        chain = new MidHookChain { Address = address };
        chain.HookHandle = NativeHooks.AllocateMHook();
        MidHookDelegate _unmanagedCallback = ( ref MidHookContext ctx ) =>
        {
          try
          {
            foreach (var n in chain.Nodes)
            {
              n.BuiltDelegate(ref ctx);
            }
          }
          catch (Exception e)
          {
            if (!GlobalExceptionHandler.Handle(e)) return;
          }
        };
        NativeHooks.SetMHook(chain.HookHandle, address, Marshal.GetFunctionPointerForDelegate(_unmanagedCallback));
        NativeHooks.EnableMHook(chain.HookHandle);
        chain.Hooked = true;
        _midChains[address] = chain;
      }
      chain.Nodes.Add(node);
    }

    return node.Id;
  }

  public Guid AddHook( nint functionAddress, Func<Func<nint>, Delegate> callbackBuilder )
  {
    HookChain chain;
    HookNode node = new HookNode {
      Id = Guid.NewGuid(),
      CallbackBuilder = callbackBuilder,
    };

    lock (_sync)
    {
      if (!_chains.TryGetValue(functionAddress, out chain))
      {
        chain = new HookChain { FunctionAddress = functionAddress };
        _chains[functionAddress] = chain;
      }
      chain.Nodes.Add(node);
      RebuildChain(chain);
    }

    return node.Id;
  }

  public void RemoveMidHook( List<Guid> nodeIds )
  {
    lock (_sync)
    {
      var chains = _midChains.Values.Where(c => c.Nodes.Any(n => nodeIds.Contains(n.Id))).ToList();
      if (chains.Count == 0) return;
      foreach (var chain in chains)
      {
        chain.Nodes.RemoveAll(n => nodeIds.Contains(n.Id));
      }
    }
  }

  public void Remove( List<Guid> nodeIds )
  {
    lock (_sync)
    {
      var chains = _chains.Values.Where(c => c.Nodes.Any(n => nodeIds.Contains(n.Id))).ToList();
      if (chains.Count == 0) return;
      foreach (var chain in chains)
      {
        chain.Nodes.RemoveAll(n => nodeIds.Contains(n.Id));
        RebuildChain(chain);
      }
    }
  }

  private void RebuildChain( HookChain chain )
  {
    try
    {
      // Rebuild delegates from first to last, wiring each to previous pointer (or original for first)
      if (chain.Hooked)
      {
        for (int i = 0; i < chain.Nodes.Count; i++)
        {
          chain.Nodes[i].BuiltDelegate = null;
          chain.Nodes[i].BuiltPointer = nint.Zero;
          if (chain.Nodes[i].HookHandle != 0)
          {
            NativeHooks.DeallocateHook(chain.Nodes[i].HookHandle);
            chain.Nodes[i].HookHandle = 0;
          }
        }
        chain.OriginalFunctionAddress = 0;
        NativeHooks.DeallocateHook(chain.HookHandle);
        chain.HookHandle = 0;
        chain.Hooked = false;
      }
      chain.HookHandle = NativeHooks.AllocateHook();

      for (int i = 0; i < chain.Nodes.Count; i++)
      {
        var node = chain.Nodes[i];

        var built = node.CallbackBuilder.Invoke(() => node.OriginalFuncPtr);
        node.BuiltDelegate = built;
        node.BuiltPointer = Marshal.GetFunctionPointerForDelegate(node.BuiltDelegate);
        if (i == 0)
        {
          NativeHooks.SetHook(chain.HookHandle, chain.FunctionAddress, node.BuiltPointer);
          node.OriginalFuncPtr = NativeHooks.GetHookOriginal(chain.HookHandle);
          chain.OriginalFunctionAddress = node.OriginalFuncPtr;
          NativeHooks.EnableHook(chain.HookHandle);
          chain.Hooked = true;
        }
        else
        {
          node.HookHandle = NativeHooks.AllocateHook();
          NativeHooks.SetHook(node.HookHandle, chain.Nodes[i - 1].OriginalFuncPtr, node.BuiltPointer);
          NativeHooks.EnableHook(node.HookHandle);
          node.OriginalFuncPtr = NativeHooks.GetHookOriginal(node.HookHandle);
        }
      }
    }
    catch (Exception e)
    {
      if (!GlobalExceptionHandler.Handle(e)) return;
      AnsiConsole.WriteException(e);
    }
  }
}