using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Spectre.Console;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Memory;

namespace SwiftlyS2.Core.Hooks;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void MidHookInternalDelegate( void* contextPtr );

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
        public List<HookNode> Nodes { get; } = [];
    }

    private class MidHookChain
    {
        public bool Hooked { get; set; } = false;
        public required nint Address { get; set; }
        public nint HookHandle { get; set; }
        public MidHookNode[] Nodes = [];
        public MidHookInternalDelegate? InternalCallback { get; set; } // Keep delegate alive
    }

    private readonly ConcurrentDictionary<nint, HookChain> chains = new();
    private readonly ConcurrentDictionary<nint, MidHookChain> midChains = new();
    private readonly Lock mutationLock = new();

    public bool IsMidHooked( nint address )
    {
        lock (mutationLock)
        {
            return midChains.TryGetValue(address, out var chain) && chain.Hooked;
        }
    }

    public bool IsHooked( nint functionAddress )
    {
        lock (mutationLock)
        {
            return chains.TryGetValue(functionAddress, out var chain) && chain.Hooked;
        }
    }

    public nint GetOriginal( nint functionAddress )
    {
        lock (mutationLock)
        {
            return chains.TryGetValue(functionAddress, out var chain)
                ? !chain.Hooked ? functionAddress : chain.Nodes.Count == 0 ? chain.OriginalFunctionAddress : chain.Nodes[^1].OriginalFuncPtr
                : nint.Zero;
        }
    }

    public Guid AddMidHook( nint address, MidHookDelegate callback )
    {
        var node = new MidHookNode {
            Id = Guid.NewGuid(),
            BuiltDelegate = callback,
        };

        lock (mutationLock)
        {
            if (midChains.TryGetValue(address, out var existing))
            {
                Volatile.Write(ref existing.Nodes, [.. existing.Nodes, node]);
                return node.Id;
            }

            var chain = new MidHookChain {
                Address = address,
                HookHandle = NativeHooks.AllocateMHook(),
                Nodes = [node],
            };

            MidHookInternalDelegate internalCallback;

            unsafe
            {
                internalCallback = ( contextPtr ) =>
                {
                    try
                    {
                        ref var ctx = ref Unsafe.AsRef<MidHookContext>(contextPtr);
                        foreach (var subscriber in Volatile.Read(ref chain.Nodes))
                        {
                            subscriber.BuiltDelegate(ref ctx);
                        }
                    }
                    catch (Exception e)
                    {
                        if (!GlobalExceptionHandler.Handle(ref e)) return;
                    }
                };
            }

            // Keep delegate alive to prevent GC
            chain.InternalCallback = internalCallback;
            var callbackPtr = Marshal.GetFunctionPointerForDelegate(internalCallback);

            midChains[address] = chain;
            try
            {
                NativeHooks.SetMHook(chain.HookHandle, address, callbackPtr);
                NativeHooks.EnableMHook(chain.HookHandle);
                chain.Hooked = true;
            }
            catch
            {
                _ = midChains.TryRemove(address, out _);
                NativeHooks.DeallocateMHook(chain.HookHandle);
                throw;
            }

            return node.Id;
        }
    }

    public Guid AddHook( nint functionAddress, Func<Func<nint>, Delegate> callbackBuilder )
    {
        var node = new HookNode {
            Id = Guid.NewGuid(),
            CallbackBuilder = callbackBuilder,
        };

        lock (mutationLock)
        {
            if (!chains.TryGetValue(functionAddress, out var chain))
            {
                chain = new HookChain { FunctionAddress = functionAddress };
                chains[functionAddress] = chain;
            }
            var previousNodes = chain.Nodes.ToArray();
            chain.Nodes.Add(node);
            RebuildChain(chain, previousNodes);

            return node.Id;
        }
    }

    public void RemoveMidHook( List<Guid> nodeIds )
    {
        lock (mutationLock)
        {
            foreach (var chain in midChains.Values)
                Volatile.Write(ref chain.Nodes, chain.Nodes.Where(node => !nodeIds.Contains(node.Id)).ToArray());
        }
    }

    public void Remove( List<Guid> nodeIds )
    {
        lock (mutationLock)
        {
            foreach (var chain in chains.Values)
            {
                var previousNodes = chain.Nodes.ToArray();
                if (chain.Nodes.RemoveAll(node => nodeIds.Contains(node.Id)) > 0)
                    RebuildChain(chain, previousNodes);
            }
        }
    }

    private void RebuildChain( HookChain chain, HookNode[] previousNodes )
    {
        var previousDelegates = previousNodes.Select(node => node.BuiltDelegate).ToArray();
        try
        {
            // Rebuild delegates from first to last, wiring each to previous pointer (or original for first)
            if (chain.Hooked)
            {
                for (var index = previousNodes.Length - 1; index >= 0; index--)
                {
                    var previous = previousNodes[index];
                    if (previous.HookHandle != 0)
                    {
                        NativeHooks.DeallocateHook(previous.HookHandle);
                        previous.HookHandle = 0;
                    }
                    previous.BuiltDelegate = null;
                    previous.BuiltPointer = nint.Zero;
                }
                chain.OriginalFunctionAddress = 0;
                NativeHooks.DeallocateHook(chain.HookHandle);
                chain.HookHandle = 0;
                chain.Hooked = false;
            }
            if (chain.Nodes.Count == 0) return;
            chain.HookHandle = NativeHooks.AllocateHook();

            for (var i = 0; i < chain.Nodes.Count; i++)
            {
                var node = chain.Nodes[i];

                var built = node.CallbackBuilder(() => node.OriginalFuncPtr);
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
            if (!GlobalExceptionHandler.Handle(ref e)) return;
            AnsiConsole.WriteException(e);
        }
        finally
        {
            GC.KeepAlive(previousDelegates);
        }
    }
}
