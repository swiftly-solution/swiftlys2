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
        public List<MidHookNode> Nodes { get; } = [];
        public MidHookInternalDelegate? InternalCallback { get; set; } // Keep delegate alive
    }

    private readonly ConcurrentDictionary<nint, HookChain> chains = new();
    private readonly ConcurrentDictionary<nint, MidHookChain> midChains = new();

    public bool IsMidHooked( nint address )
    {
        return midChains.TryGetValue(address, out var chain) && chain.Hooked;
    }

    public bool IsHooked( nint functionAddress )
    {
        return chains.TryGetValue(functionAddress, out var chain) && chain.Hooked;
    }

    public nint GetOriginal( nint functionAddress )
    {
        return chains.TryGetValue(functionAddress, out var chain)
            ? !chain.Hooked ? functionAddress : chain.Nodes.Count == 0 ? chain.OriginalFunctionAddress : chain.Nodes[^1].OriginalFuncPtr
            : nint.Zero;
    }

    public Guid AddMidHook( nint address, MidHookDelegate callback )
    {
        var node = new MidHookNode {
            Id = Guid.NewGuid(),
            BuiltDelegate = callback,
        };

        if (!midChains.TryGetValue(address, out var chain))
        {
            chain = new MidHookChain {
                Address = address,
                HookHandle = NativeHooks.AllocateMHook()
            };

            MidHookInternalDelegate internalCallback;

            unsafe
            {
                internalCallback = ( contextPtr ) =>
                {
                    try
                    {
                        ref var ctx = ref Unsafe.AsRef<MidHookContext>(contextPtr);
                        foreach (var n in chain.Nodes)
                        {
                            n.BuiltDelegate(ref ctx);
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

            NativeHooks.SetMHook(chain.HookHandle, address, callbackPtr);
            NativeHooks.EnableMHook(chain.HookHandle);

            chain.Hooked = true;
            midChains[address] = chain;
        }

        chain.Nodes.Add(node);
        return node.Id;
    }

    public Guid AddHook( nint functionAddress, Func<Func<nint>, Delegate> callbackBuilder )
    {
        var node = new HookNode {
            Id = Guid.NewGuid(),
            CallbackBuilder = callbackBuilder,
        };

        if (!chains.TryGetValue(functionAddress, out var chain))
        {
            chain = new HookChain { FunctionAddress = functionAddress };
            chains[functionAddress] = chain;
        }
        chain.Nodes.Add(node);
        RebuildChain(chain);

        return node.Id;
    }

    public void RemoveMidHook( List<Guid> nodeIds )
    {
        midChains.Values.Where(c => c.Nodes.Any(n => nodeIds.Contains(n.Id))).ToList().ForEach(chain =>
        {
            _ = chain.Nodes.RemoveAll(n => nodeIds.Contains(n.Id));
        });
    }

    public void Remove( List<Guid> nodeIds )
    {
        chains.Values.Where(c => c.Nodes.Any(n => nodeIds.Contains(n.Id))).ToList().ForEach(chain =>
        {
            _ = chain.Nodes.RemoveAll(n => nodeIds.Contains(n.Id));
            RebuildChain(chain);
        });
    }

    private void RebuildChain( HookChain chain )
    {
        try
        {
            // Rebuild delegates from first to last, wiring each to previous pointer (or original for first)
            if (chain.Hooked)
            {
                for (var i = 0; i < chain.Nodes.Count; i++)
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
    }
}