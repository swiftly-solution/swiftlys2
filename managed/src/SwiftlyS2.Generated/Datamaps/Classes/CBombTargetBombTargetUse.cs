using Spectre.Console;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class DatamapHooksPublisher
{
    private delegate void CBombTargetBombTargetUseDelegate(nint a1);

    private static IUnmanagedFunction<CBombTargetBombTargetUseDelegate>? CBombTargetBombTargetUseUnmanagedFunction;
    private static Guid CBombTargetBombTargetUseHookGuid;

    private static IUnmanagedFunction<CBombTargetBombTargetUseDelegate> CBombTargetBombTargetUseGetUnmanagedFunction()
    {
        if (CBombTargetBombTargetUseUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBombTarget", "CBombTargetBombTargetUse");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBombTarget::CBombTargetBombTargetUse.");
            }
            CBombTargetBombTargetUseUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBombTargetBombTargetUseDelegate>(address);
        }
        return CBombTargetBombTargetUseUnmanagedFunction;
    }

    internal static Guid HookCBombTargetBombTargetUse()
    {
        CBombTargetBombTargetUseHookGuid = CBombTargetBombTargetUseGetUnmanagedFunction().AddHook(next => (a1) => CBombTargetBombTargetUsePipeline(a1, () => next()(a1)));
        return CBombTargetBombTargetUseHookGuid;
    }

    internal static Guid UnhookCBombTargetBombTargetUse()
    {
        CBombTargetBombTargetUseGetUnmanagedFunction().RemoveHook(CBombTargetBombTargetUseHookGuid);
        return Guid.Empty;
    }

    private static void CBombTargetBombTargetUsePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBombTarget>(a1);

            var preCtx = new CBombTargetBombTargetUsePreContext { SchemaObject = schemaObject };
            InvokeCBombTargetBombTargetUsePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBombTargetBombTargetUsePostContext { SchemaObject = schemaObject };
            InvokeCBombTargetBombTargetUsePost(ref postCtx);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    internal static void InvokeCBombTargetBombTargetUse(nint a1)
    {
        CBombTargetBombTargetUseGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBombTargetBombTargetUsePre(ref CBombTargetBombTargetUsePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBombTargetBombTargetUsePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBombTargetBombTargetUsePost(ref CBombTargetBombTargetUsePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBombTargetBombTargetUsePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBombTargetBombTargetUseHook : ICBombTargetBombTargetUseHook
{
    private event OnCBombTargetBombTargetUsePreDelegate? _Pre;
    private event OnCBombTargetBombTargetUsePostDelegate? _Post;

    public event OnCBombTargetBombTargetUsePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBombTargetBombTargetUse);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBombTargetBombTargetUse);
            }
        }
    }

    public event OnCBombTargetBombTargetUsePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBombTargetBombTargetUse);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBombTargetBombTargetUse);
            }
        }
    }

    public void InvokePre(ref CBombTargetBombTargetUsePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBombTargetBombTargetUsePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBombTargetBombTargetUse);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBombTargetBombTargetUse);
        }
    }

    public void Invoke(CBombTarget schemaObject) => DatamapHooksPublisher.InvokeCBombTargetBombTargetUse(schemaObject.Address);
}